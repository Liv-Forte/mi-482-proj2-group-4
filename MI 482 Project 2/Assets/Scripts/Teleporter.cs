using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportPoint;
    public Transform playerPosition;
    private void OnEnable()
    {
        teleport();
    }
    void teleport()
    {
        playerPosition.position = teleportPoint.position;
    }
 
}
