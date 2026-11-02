using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightToggle : MonoBehaviour
{
    public Material lens;

    private Light light;
    private AudioSource audio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light = GetComponentInChildren<light>();
        audio = GetComponent<AudioSource>();
    }

    public void LightOn()
    {
        audio.Play();
        lens.EnableKeyword("EMISSION");
        light.enabled = true;
    }

    public void LightOff()
    {
        audio.Play();
        lens.DisableKeyword("EMISSION");
        light.enabled = false;
    }
}
