using System;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
    public int ammoType;
    public int amount;

    void OnTriggerEnter2D(Collider2D info)
    {
        try
        {
            if (info.transform.parent.gameObject.tag=="Player")
            {
                info.transform.parent.gameObject.GetComponent<GunController>().ammo[ammoType]+=amount;
                this.gameObject.SetActive(false);
            }
        } catch (Exception error) {}
    }
}