using UnityEngine;

public class AmmoController : MonoBehaviour
{
    public GameObject player;
    public int ammoType;
    public int amount;

    void Start()
    {
        // Initialize
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D info)
    {
        if (info.gameObject==player.transform.GetChild(1).gameObject)
        {
            player.GetComponent<GunController>().ammo[ammoType]+=amount;
            this.gameObject.SetActive(false);
        }
    }
}