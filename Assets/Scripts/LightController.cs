using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public Light2D light;
    private AudioSource click;

    void Start()
    {
        // Initialize
        light = GetComponent<Light2D>();
        light.enabled = false;
        click = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D info)
    {
        try
        {
            if (info.transform.parent.gameObject.tag=="Player")
            {
                light.enabled = true;

                if (!click.isPlaying)
                {
                    click.Play();
                }
            }
        } catch (Exception error) {}
    }

    void OnTriggerExit2D(Collider2D info)
    {
        try
        {
            if (info.transform.parent.gameObject.tag=="Player")
            {
                light.enabled = false;

                if (!click.isPlaying)
                {
                    click.Play();
                }
            }
        } catch (Exception error) {}
    }
}