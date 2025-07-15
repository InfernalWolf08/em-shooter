using UnityEngine;

public class AssetController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D info)
    {
        if (info.transform.parent.gameObject.tag=="Player")
        {
            PlayerController player = info.transform.parent.gameObject.GetComponent<PlayerController>();
            player.scoreCont.score += 100;
            player.hasWon = true;
            gameObject.SetActive(false);
        }
    }
}