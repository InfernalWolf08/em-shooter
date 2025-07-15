using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunController : MonoBehaviour
{
    [Header("Data")]
    public GameObject[] waves;
    public Sprite[] sprites;
    public float[] ammo;
    public int[] damage;
    public Color32[] colors;
    public Transform firePoint;
    public GameObject gun;
    public int waveSelected=0;
    private int lastWave=0;
    public LayerMask hitMask;
    public Transform laserMask;
    public AudioSource fireSound;

    [Header("UI")]
    public TextMeshProUGUI ammoAmount;

    void Update()
    {
        // Load the gun
        load();

        // Fire the wave
        fire();

        // Update the UI
        UI();
    }

    // Custom functions
    void fire() // Fire the gun
    {
        if (Input.GetMouseButton(0) && ammo[waveSelected]>0)
        {
            // Shoot the gun
            waves[waveSelected].SetActive(true);

            // Shoot the raycast
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, Mathf.Infinity, hitMask);
            if (hit)
            {
                // On hit
                print(hit.collider.gameObject.tag);
                if (hit.collider.gameObject.tag=="Monster")
                {
                    hit.collider.transform.parent.gameObject.GetComponent<MonsterController>().health -= damage[waveSelected];
                    hit.collider.transform.parent.gameObject.GetComponent<MonsterController>().animator.SetTrigger("Dmg");
                }

                laserMask.localScale = new Vector3(hit.distance, laserMask.localScale.y, laserMask.localScale.z);
            } else {
                // Nothing was hit
                laserMask.localScale = new Vector3(2, laserMask.localScale.y, laserMask.localScale.z);
            }

            // Lose ammo
            ammo[waveSelected] -= Time.deltaTime;

            // Play sound
            if (!fireSound.isPlaying)
            {
                fireSound.Play();
            }
        } else {
            // Hide all waves
            disableAll();

            // Stop playing sound
            if (fireSound.isPlaying)
            {
                fireSound.Stop();
            }
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

    void UI()
    {
        ammoAmount.text = waveSelected==0 ? "Radio Waves:\nInfinite" : waves[waveSelected].name + "s:\n" + Convert.ToString(Convert.ToInt16(ammo[waveSelected]));
        ammoAmount.color = colors[waveSelected];
    }

    void disableAll() // Disable all wave objects
    {
        foreach (GameObject wave in waves)
        {
            wave.SetActive(false);
        }
    }
}