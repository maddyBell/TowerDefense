using System.Collections; using System.Collections.Generic; using UnityEngine;

public class Path
{
    private int width, length;

    public Path(int terrainWidth, int terrainLength)
    {
        width = terrainWidth;
        length = terrainLength;
    }

    /// <summary>
    /// Carves a winding path from start to end using a biased random walk
    /// </summary>
    public void WindingPath(float[,] heights, Vector2 start, Vector2 end, int radius, float depth)
    {
        Vector2 position = start;
        int maxSteps = (width + length) * 2; // ✅ more generous but capped
        float randomStrength = 0.3f; // ✅ less wandering

        for (int i = 0; i < maxSteps; i++)
        {
            // ✅ Stop carving if close enough to target
            if (Vector2.Distance(position, end) < 3f)
                break;

            // ✅ Always carve inside terrain bounds
            if (position.x > 1 && position.x < width - 1 && position.y > 1 && position.y < length - 1)
            {
                LowerTerrainCircle(heights, Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), radius, depth);
            }

            // ✅ Calculate biased random direction toward the end
            Vector2 direction = (end - position).normalized;
            direction += new Vector2(Random.Range(-randomStrength, randomStrength), Random.Range(-randomStrength, randomStrength));
            direction.Normalize();

            // ✅ Move walker
            position += direction * 1.5f;
        }
    }

    /// <summary>
    /// Lowers terrain in a circular area to create the path
    /// </summary>
    private void LowerTerrainCircle(float[,] heights, int centerX, int centerZ, int radius, float depth)
    {
        for (int x = -radius; x <= radius; x++)
        {
            for (int z = -radius; z <= radius; z++)
            {
                int posX = centerX + x;
                int posZ = centerZ + z;

                if (posX >= 0 && posX < width && posZ >= 0 && posZ < length)
                {
                    float distance = Mathf.Sqrt(x * x + z * z);

                    if (distance <= radius)
                    {
                        heights[posX, posZ] -= depth * (1f - distance / radius);
                        heights[posX, posZ] = Mathf.Clamp01(heights[posX, posZ]);
                    }
                }
            }
        }
    }
}