using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject tile;
    public Vector3 snappedPos;
    public bool active;

    void Update()
    {
        if (Input.GetMouseButton(0) && active)
        {
            // Spawn new tile
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            snappedPos = new Vector3(roundToNearest(mousePos.x, .24f)-.24f, roundToNearest(mousePos.y, .24f), 0);
            GameObject newTile = Instantiate(tile, snappedPos, Quaternion.identity, transform);
            newTile.transform.localPosition = snappedPos;
            print("Spawned new tile");
        }
    }

    // Snap to nearest multiple of .24
    float roundToNearest(float n, float x)
    {
        return Mathf.Round(n/x)*x;
    }
}