using UnityEngine;

public class ProceduralTerrain : MonoBehaviour
{
    public int width = 64;
    public int length = 64;
    public int height = 20;
    public float scale = 20f;
    public GameObject towerPrefab;

    private Terrain terrain;
    private float[,] heights;
    private Path path;

    void Start()
    {
        terrain = GetComponent<Terrain>();

        TerrainData terrainData = terrain.terrainData;

        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, height, length);

        heights = GenerateHeights();

        path = new Path(width, length);
        Vector2 center = new Vector2(width / 2, length / 2);

        path.WindingPath(heights, new Vector2(0, length / 2), center, 3, 0.02f);
        path.WindingPath(heights, new Vector2(width - 1, length / 2), center, 3, 0.02f);
        path.WindingPath(heights, new Vector2(width / 2, 0), center, 3, 0.02f);

        terrain.terrainData.SetHeights(0, 0, heights);

        //PlaceTower(center);
    }

    float[,] GenerateHeights()
    {

        float[,] h = new float[width + 1, length + 1];
        float seed = Random.Range(0f, 9999f);

        for (int x = 0; x <= width; x++)
        {
            for (int z = 0; z <= length; z++)
            {
                float xCoord = (x + seed) / scale;
                float zCoord = (z + seed) / scale;
                h[x, z] = Mathf.PerlinNoise(xCoord, zCoord);
            }
        }
        return h;
    }

   /* void PlaceTower(Vector2 center)
    {
        Vector3 worldPos = new Vector3(center.x, 0, center.y);

        float y = terrain.SampleHeight(worldPos);
        worldPos.y = y + 1.5f;

        Instantiate(towerPrefab, worldPos, Quaternion.identity);
    }*/
}