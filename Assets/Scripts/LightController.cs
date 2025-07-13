using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public Light2D light;

    void Start()
    {
        // Initialize
        light = GetComponent<Light2D>();
        light.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D info)
    {
        if (info.transform.parent.gameObject.tag=="Player")
        {
            light.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D info)
    {
        if (info.transform.parent.gameObject.tag=="Player")
        {
            light.enabled = false;
        }
    }
}