using UnityEngine;

public class TileColliderDeleter : MonoBehaviour
{
    public bool delete;

    void Start()
    {
        if (delete)
        {
            BoxCollider2D[] tileColliders = Object.FindObjectsByType<BoxCollider2D>(FindObjectsSortMode.None);

            foreach (BoxCollider2D tileCollider in tileColliders)
            {
                if (LayerMask.LayerToName(tileCollider.gameObject.layer) == "Ground")
                {
                    tileCollider.enabled = false;
                }
            }
        }
    }
}