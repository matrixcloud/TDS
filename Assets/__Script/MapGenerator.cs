using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
    public Transform tilePrefab;
    public Vector2 mapSize;
    [Range(0, 1)]
    public float outlinePercent;

	// Use this for initialization
	void Start () {
	    GenerateMap();
	}

    public void GenerateMap() {
        const string holderName = "Generated Map";
        Transform mapHolder = transform.FindChild(holderName);
        if (mapHolder) {
            DestroyImmediate(mapHolder.gameObject);
        }

        mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = this.transform;

        for (int x = 0; x < mapSize.x; x++) {
            for (int y = 0; y < mapSize.y; y++) {
                Vector3 tilePos = new Vector3(-mapSize.x/2+.5f+x, 0, -mapSize.y/2+.5f+y);
                Transform newTile = Instantiate(tilePrefab, tilePos, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.parent = mapHolder;
            }
        }
    }
}
