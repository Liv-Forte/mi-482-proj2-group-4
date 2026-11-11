using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using System.Security.Cryptography;
using System.Threading;

public class FootstepController : MonoBehaviour
{
    public AudioSource[] footstepSound;
    public CharacterController playerController;
    private int rng = 0;
    public float footstepDelay;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerVelocity = new Vector3(playerController.velocity.x, 0 , playerController.velocity.z);
      
        if (timer > 0) 
        { 
            timer -= Time.deltaTime;
        }

        if (playerVelocity.magnitude > 2.3 && timer <= 0) 
        {
            rng = Random.Range(0, footstepSound.Length);
            footstepSound[rng].Play();
            timer = footstepDelay;
        }
    }
    private void Start()
    {
        timer = footstepDelay;
    }
}
