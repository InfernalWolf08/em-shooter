using System;
using UnityEngine;

public class TileSpawnHandler : MonoBehaviour
{
    public float overlapRadius;
    public Collider2D[] childColliders = new Collider2D[4];

    void Start()
    {
        // Diable children
        foreach (Collider2D collider in childColliders)
        {
            collider.enabled = false;
        }

        // Kill if dupe
        if (Physics2D.OverlapCircleAll(transform.position, overlapRadius, LayerMask.GetMask("Ground")).Length >= 4)
        {
            Destroy(this.gameObject);
        }

        // Reenable children
        foreach (Collider2D collider in childColliders)
        {
            collider.enabled = true;
        }
    }
}