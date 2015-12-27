using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {
    public Transform tilePrefab;
    public Transform obstaclePrefab;
    public Vector2 mapSize;
    [Range(0, 1)]
    public float outlinePercent;
    private List<Coord> allTileCoords;
    private Queue<Coord> shuffleTileCoords;
    public int seed = 10;

	// Use this for initialization
	void Start () {
	    GenerateMap();
	}

    public void GenerateMap() {
        allTileCoords = new List<Coord>();
        for (int x = 0; x < mapSize.x; x++) {
            for (int y = 0; y < mapSize.y; y++) {
                allTileCoords.Add(new Coord(x, y));
            }
        }
        shuffleTileCoords = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(), seed));

        const string holderName = "Generated Map";
        Transform mapHolder = transform.FindChild(holderName);
        if (mapHolder) {
            DestroyImmediate(mapHolder.gameObject);
        }

        mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = this.transform;

        for (int x = 0; x < mapSize.x; x++) {
            for (int y = 0; y < mapSize.y; y++) {
                Vector3 tilePos = CoordToPosition(x, y);
                Transform newTile = Instantiate(tilePrefab, tilePos, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.parent = mapHolder;
            }
        }

        int obstacleCount = 10;
        for (int i = 0; i < obstacleCount; i++) {
            Coord randomCoord = GetRandomCoord();
            Vector3 obstaclePos = CoordToPosition(randomCoord.x, randomCoord.y);
            Transform newObstacle = Instantiate(obstaclePrefab, obstaclePos + Vector3.up * .5f, Quaternion.identity) as Transform;
            newObstacle.parent = mapHolder;
        }
    }

    private Vector3 CoordToPosition(int x, int y) {
        return new Vector3(-mapSize.x / 2 + .5f + x, 0, -mapSize.y / 2 + .5f + y);
    }

    public Coord GetRandomCoord() {
        Coord randomCoord = shuffleTileCoords.Dequeue();
        shuffleTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    public struct Coord {
        public int x;
        public int y;

        public Coord(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }
}
