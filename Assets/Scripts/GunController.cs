using System;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Data")]
    public GameObject[] waves;
    public Sprite[] sprites;
    public int[] ammo;
    public Transform firePoint;
    public GameObject gun;
    public int waveSelected=0;
    private int lastWave=0;
    public LayerMask hitMask;

    void Update()
    {
        // Change selected wave
        lastWave = waveSelected;
        waveSelected += Convert.ToInt16(Input.mouseScrollDelta.y);
        
        if (waveSelected<0)
        {
            waveSelected = waves.Length-1;
        } else if (waveSelected>waves.Length-1) {
            waveSelected = 0;
        }

        // Clear lasers when players changes selected wave
        if (lastWave!=waveSelected)
        {
            disableAll();
        }

        // Fire the wave
        fire();

        // Change the gun's sprite
        load();
    }

    // Custom functions
    void fire() // Fire the gun
    {
        if (Input.GetMouseButton(0))
        {
            // Shoot the gun
            waves[waveSelected].SetActive(true);

            // Shoot the raycast
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, Mathf.Infinity, hitMask);
            if (hit)
            {
                print(hit.collider.gameObject.name);
            }
        } else {
            // Hide all waves
            disableAll();
        }
    }

    void load() // Load the gun with selected EM Wave
    {
        
    }

    void disableAll() // Disable all wave objects
    {
        foreach (GameObject wave in waves)
        {
            wave.SetActive(false);
        }
    }
}