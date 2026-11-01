using UnityEngine;

[ExecuteAlways]
public class BeltArranger : MonoBehaviour
{
    [SerializeField] float radius = 0.35f;      
    [SerializeField] float totalArc = 180f;
    [SerializeField] float centerAngle = 0f;

    void Update()
    {
        int n = transform.childCount;
        if (n == 0) return;

        float step = 0f;
        float start = centerAngle;

        if (n > 1)
        {
            bool fullCircle = totalArc >= 360f;
            step = fullCircle ? 360f / n : totalArc / (n - 1);
            start = centerAngle - (fullCircle ? 0f : totalArc / 2f);
        }

        for (int i = 0; i < n; i++)
        {
            float angle = start + step * i;
            Quaternion rot = Quaternion.Euler(0f, angle, 0f); 

            Transform slot = transform.GetChild(i);
            slot.localPosition = rot * Vector3.forward * radius;
            slot.localRotation = rot;
        }
        
    }
}