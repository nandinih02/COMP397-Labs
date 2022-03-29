using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class MapGenerator : MonoBehaviour
{
    [Header("Tile Resources")]
    public List<GameObject> tilePrefabs;
    public GameObject startTile;
    public GameObject goalTile;


    [Header("Map Properties")]
    [Range(2,30)]
    public int width=2;
    [Range(2, 30)]
    public int height=2;
    public Transform parent;

    [Header("Generated Tiles")]
    public List<GameObject> tiles;

    private int startWidth;
    private int startHeight;

    // Start is called before the first frame update
    void Start()
    {
        startWidth = width;
        startHeight = height;
        buildMap();
        BakeNavMeshes();
    }

    // Update is called once per frame
    void Update()
    {
        if(width != startWidth || height != startHeight)
        {
            resetMap();
            buildMap();
            Invoke(nameof(BakeNavMeshes), 0.2f);
        }
    }

    public void resetMap()
    {
        startWidth = width;
        startHeight = height;
        var tempTile = tiles[0];
        var size = tiles.Count;
        for (int i = 0; i < size; i++)
        {
            Destroy(tiles[i]);
        }
        tiles.Clear();
    }

    public void buildMap()
    {
        var offset = new Vector3(20.0f, 0.0f, 20.0f);
        //place the start tile
        tiles.Add(Instantiate(startTile, Vector3.zero, Quaternion.identity, parent));

        //choose random goal position
        var randomGoalRow = Random.Range(1, height + 1);
        var randomGoalCol = Random.Range(1, width + 1);
        int count = 0;

        //Generate more tiles if both width and height are greater than 2
        for (int row = 1; row <= height; row++)
        {

            for (int col = 1; col <= width; col++)
            {
                if (row == 1 && col == 1) {continue;}

                var tilePosition = new Vector3(col * 20.0f, 0.0f, row * 20.0f) - offset;

                if (row == randomGoalRow && col == randomGoalCol) 
                { 
                    tiles.Add(Instantiate(goalTile, tilePosition, Quaternion.identity, parent));
                }
                else
                {
                    var randomPrefabIndex = Random.Range(0, 4);
                    var randomRotation = Quaternion.Euler(0.0f, Random.Range(0, 4) * 90.0f, 0.0f);
                    tiles.Add(Instantiate(tilePrefabs[randomPrefabIndex], tilePosition, randomRotation, parent));
                }
             
               
            }
        }
        
       

    }
    public void BakeNavMeshes()
    {
        foreach (var tile in tiles)
        {
            tile.GetComponent<NavMeshSurface>().BuildNavMesh();
        }
    }

}
