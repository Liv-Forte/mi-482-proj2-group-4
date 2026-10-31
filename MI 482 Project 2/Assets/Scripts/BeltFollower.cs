using UnityEngine;

public class BeltFollower : MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] float heightBelowHead = 0.65f;
    [SerializeField] float forwardOffset = 0.3f;
    [SerializeField] float smoothing = 8f;

    void LateUpdate()
    {
        Quaternion yaw = Quaternion.Euler(0f, head.eulerAngles.y, 0f);

        Vector3 targetPos = head.position + Vector3.down * heightBelowHead + yaw * Vector3.forward * forwardOffset;

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation, yaw, Time.deltaTime * smoothing);
    }
}