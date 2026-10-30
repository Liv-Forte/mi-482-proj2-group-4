using UnityEngine;

public class BeltFollower : MonoBehaviour
{
    [SerializeField] Transform head;                
    [SerializeField] float heightBelowHead = 0.65f;
    [SerializeField] float smoothing = 8f;

    void LateUpdate()
    {
        Vector3 targetPos = head.position + Vector3.down * heightBelowHead;
        Quaternion targetRot = Quaternion.Euler(0f, head.eulerAngles.y, 0f);

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * smoothing);
    }
}