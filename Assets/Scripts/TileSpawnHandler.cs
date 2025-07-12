using System;
using UnityEngine;

public class TileSpawnHandler : MonoBehaviour
{
    public float overlapRadius;

    void Start()
    {
        if (Physics2D.OverlapCircleAll(transform.position, overlapRadius, LayerMask.GetMask("Ground")).Length > 4)
        {
            Destroy(this.gameObject);
        }
    }
}