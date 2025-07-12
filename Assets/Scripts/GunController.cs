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
    public Transform laserMask;

    void Update()
    {
        // Load the gun
        load();

        // Fire the wave
        fire();
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
                // On hit
                print(hit.collider.gameObject.name);

                laserMask.localScale = new Vector3(hit.distance, laserMask.localScale.y, laserMask.localScale.z);
            } else {
                // Nothing was hit
                laserMask.localScale = new Vector3(2, laserMask.localScale.y, laserMask.localScale.z);
            }
        } else {
            // Hide all waves
            disableAll();
        }
    }

    void load() // Load the gun with selected EM Wave
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

        // Change sprites
        if (ammo[waveSelected]>0)
        {
            gun.GetComponent<SpriteRenderer>().sprite = sprites[waveSelected];
        } else {
            gun.GetComponent<SpriteRenderer>().sprite = sprites[7];
        }
    }

    void disableAll() // Disable all wave objects
    {
        foreach (GameObject wave in waves)
        {
            wave.SetActive(false);
        }
    }
}