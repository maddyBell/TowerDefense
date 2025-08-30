using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  Description: 

This class is used to procedurally generate terrain. 
It makes use of Perlin Noise, Terrain Data and height maps to lay out the map terrain */
public class PerlinTerrain : MonoBehaviour
{
    //creating the initial variables and exposing them to the inspector

    public int width = 256, height = 20, length = 256;
    public float scale = 20f;

    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, height, length);
        terrainData.SetHeights(0, 0, GenerateHeights());

        return terrainData;
    } // end GenerateTerrain()

    float[,] GenerateHeights()
    {
        float[,] heightsArr = new float[width, length];
        float randomSeed = Random.Range(0f, 9999f); // making a seed value to hold a random float and used to randomise terrain possibilites

        //creating a nested for loop used to perform mathertically based randomisation to be applied to the Perlin Noise funtions 
        //to create a 2D height array corresponding with the associated width and length values in the map grid 
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                float xCoord = (x + randomSeed) / scale;
                float zCoord = (z + randomSeed) / scale;

                heightsArr[x, z] = Mathf.PerlinNoise(xCoord, zCoord);
            }
        }

        return heightsArr;
    } // End GenerateHeights()
}//Ends Perlin Terrain class


/* References: 

Brackeys: https://www.youtube.com/watch?v=bG0uEXV6aHQ&ab_channel=Brackeys
DVS Devs: https://www.youtube.com/watch?v=1qSjCu8av7Q&ab_channel=DVSDevs%28DanVioletSagmiller%29
Terrain Component Resources: https://discussions.unity.com/t/how-do-i-resize-terrain/177726
                             https://www.youtube.com/watch?v=DbJB9534PZQ&ab_channel=SoloGameDev
*/