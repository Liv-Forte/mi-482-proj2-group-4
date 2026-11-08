using UnityEngine;

public class BeltFollower : MonoBehaviour
{
    [SerializeField] Transform head;                 
    [SerializeField] float heightBelowHead = 0.65f;
    [SerializeField] float forwardOffset = 0f;

    [Header("Position give (spring)")]
    [SerializeField] float followStiffness = 40f;    
    [SerializeField] float dampingRatio = 0.8f;      
    [SerializeField] float verticalSmoothTime = 0.35f;
    [SerializeField] float snapDistance = 1f;        

    [Header("Turning")]
    [SerializeField] float yawDeadzone = 25f;         
    [SerializeField] float turnSmoothTime = 0.3f;    
    [SerializeField] float recenterSmoothTime = 1.5f; 

    Vector3 velocityXZ;
    float velocityY;
    float yawVelocity;
    float beltYaw;

    void OnEnable() => SnapToTarget();

    void SnapToTarget()
    {
        beltYaw = head.eulerAngles.y;
        transform.SetPositionAndRotation(GetTarget(), Quaternion.Euler(0f, beltYaw, 0f));
        velocityXZ = Vector3.zero;
        velocityY = 0f;
        yawVelocity = 0f;
    }

    Vector3 GetTarget()
    {
        Quaternion yaw = Quaternion.Euler(0f, beltYaw, 0f);
        return head.position + Vector3.down * heightBelowHead + yaw * Vector3.forward * forwardOffset;
    }

    void LateUpdate()
    {
        float dt = Mathf.Min(Time.deltaTime, 0.033f);   

        float headYaw = head.eulerAngles.y;
        float delta = Mathf.Abs(Mathf.DeltaAngle(beltYaw, headYaw));
        float t = Mathf.SmoothStep(0f, 1f, Mathf.InverseLerp(yawDeadzone * 0.5f, yawDeadzone * 1.5f, delta));
        float smoothTime = Mathf.Lerp(recenterSmoothTime, turnSmoothTime, t);
        beltYaw = Mathf.SmoothDampAngle(beltYaw, headYaw, ref yawVelocity, smoothTime, Mathf.Infinity, dt);

        Vector3 target = GetTarget();
        Vector3 pos = transform.position;

        if (Vector3.Distance(pos, target) > snapDistance)
        {
            SnapToTarget();
            return;
        }

        Vector3 posXZ = new Vector3(pos.x, 0f, pos.z);
        Vector3 targetXZ = new Vector3(target.x, 0f, target.z);
        float damping = 2f * dampingRatio * Mathf.Sqrt(followStiffness);
        Vector3 accel = (targetXZ - posXZ) * followStiffness - velocityXZ * damping;
        velocityXZ += accel * dt;
        posXZ += velocityXZ * dt;

        float y = Mathf.SmoothDamp(pos.y, target.y, ref velocityY, verticalSmoothTime, Mathf.Infinity, dt);

        transform.SetPositionAndRotation(new Vector3(posXZ.x, y, posXZ.z), Quaternion.Euler(0f, beltYaw, 0f));
    }
}