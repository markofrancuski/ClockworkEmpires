using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    void Start()
    {
        GenerateTerrain();
    }

    public int width = 10;
    public int length = 10;
    public int depth = 5;

    Mesh mesh = new Mesh();


    private void GenerateTerrain()
    {
        Vector3[] vertices;
        int[] triangles;

        for (int z = -width / 2; z < width / 2; z++)
        {
            for (int x = -length / 2; x < length / 2; x++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(x, 0, z);
            }
        }

    }
}
