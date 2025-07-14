using System;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    private Vector3 curPos;
    private Vector3 targetPos;
    public float speed = 1;
    private float t;
    public AudioSource slideSound;

    void Start()
    {
        // Initialize
        startPos = new Vector3(0, 0, 0);
        targetPos = startPos;
        curPos = startPos;
        slideSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Lock t between 0 and 1
        if (t+Time.deltaTime*speed<=1 && t+Time.deltaTime*speed>=0)
        {
            t += Time.deltaTime*speed;
        } else if (t>1) {
            t = 1;
        } else if (t<0) {
            t = 0;
        }

        // Animate
        transform.GetChild(0).localPosition = Vector3.Lerp(curPos, targetPos, t);
    }

    void OnTriggerEnter2D(Collider2D info)
    {
        try
        {
            if (info.transform.parent.gameObject.tag=="Player")
            {
                // Open
                t = 1-t;
                targetPos = endPos;
                curPos = startPos;
                
                if (!slideSound.isPlaying)
                {
                    slideSound.Play();
                }
            }
        } catch (Exception e) {}
    }

    void OnTriggerExit2D(Collider2D info)
    {
        try
        {
            if (info.transform.parent.gameObject.tag=="Player")
            {
                // Close
                t = 1-t;
                targetPos = startPos;
                curPos = endPos;
                
                if (!slideSound.isPlaying)
                {
                    slideSound.Play();
                }
            }
        } catch (Exception e) {}
    }
}