using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] int width = 10;
    [SerializeField] int height = 10;

    [Header("Target Tilemap")]
    [SerializeField] Tilemap targetTilemap;

    [Header("Generation Settings")]
    [SerializeField] float tileDelay = 0.5f; // Her tile arasındaki bekleme süresi

    [Header("Tile Prefabs")]
    [Tooltip("Top Left = 0, Top = 1, Top Right = 2, Left = 3, Center = 4, Right = 5, Bottom Left = 6, Bottom = 7, Bottom Right = 8")]
    [SerializeField] TilePrefabData[] tilePrefabs;



    void Start()
    {
        StartCoroutine(GenerateWorldCoroutine());
    }

    void Update()
    {
    }

    IEnumerator GenerateWorldCoroutine()
    {
        if (targetTilemap == null)
        {
            Debug.LogError("Target Tilemap not initialized!");
            yield break;
        }

        targetTilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int tileIndex = GetTileIndex(x, y);
                
                if (tilePrefabs[tileIndex].tile != null)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    targetTilemap.SetTile(tilePosition, tilePrefabs[tileIndex].tile);
                    
                    yield return new WaitForSeconds(tileDelay);
                }
            }
        }

        Debug.Log("World generation completed!");
    }

    void GenerateWorld()
    {
        if (targetTilemap == null)
        {
            Debug.LogError("Target Tilemap not initialized!");
            return;
        }

        targetTilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int tileIndex = GetTileIndex(x, y);
                
                if (tilePrefabs[tileIndex].tile != null)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    targetTilemap.SetTile(tilePosition, tilePrefabs[tileIndex].tile);
                }
            }
        }
    }

    int GetTileIndex(int x, int y)
    {
        if (x == 0 && y == height - 1) return 0;           
        if (x == width - 1 && y == height - 1) return 2;   
        if (x == 0 && y == 0) return 6;                    
        if (x == width - 1 && y == 0) return 8;            

        if (y == height - 1) return 1;                     
        if (y == 0) return 7;                              
        if (x == 0) return 3;                              
        if (x == width - 1) return 5;

        return 4;
    }
}