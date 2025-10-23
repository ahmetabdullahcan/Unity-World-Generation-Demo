using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    [Header("World Size")]
    [SerializeField] int width = 100;
    [SerializeField] int height = 50;

    [Header("Noise Settings")]
    [SerializeField] float scale = 10f;
    [SerializeField] float seed = 0f;

    [Header("Tilemap")]
    [SerializeField] Tilemap targetTilemap;

    [Header("Tiles")]
    [SerializeField] Tile waterTile;
    [SerializeField] Tile dirtTile;
    [SerializeField] Tile grassTile;

    void Start()
    {
        if (targetTilemap == null)
        {
            Debug.LogError("Tilemap not assigned!");
            return;
        }

        GenerateWorld();
    }

    void GenerateWorld()
    {
        targetTilemap.ClearAllTiles();

        if (seed == 0)
            seed = Random.Range(0f, 10000f);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float noiseValue = Mathf.PerlinNoise(
                    (x + seed) / scale,
                    (y + seed) / scale
                );

                Tile tileToPlace;

                if (noiseValue < 0.4f)
                    tileToPlace = waterTile; // düşük değerler → su
                else if (noiseValue < 0.6f)
                    tileToPlace = dirtTile; // orta değerler → toprak
                else
                    tileToPlace = grassTile; // yüksek değerler → çimen

                targetTilemap.SetTile(new Vector3Int(x, y, 0), tileToPlace);
            }
        }

        Debug.Log("World generation complete!");
    }
}
