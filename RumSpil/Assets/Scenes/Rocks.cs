using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static List<Vector3> CreateAsteroidBelt(float radius, Vector3 gridDimensions, GameObject rock, Transform parent)
    {

        List<Vector3> coordinates = new List<Vector3>();
        float cellSize = Mathf.Sqrt(3)*radius;
        int gridSizeX = Mathf.CeilToInt(gridDimensions.x/cellSize);
        int gridSizeY = Mathf.CeilToInt(gridDimensions.y/cellSize);
        int gridSizeZ = Mathf.CeilToInt(gridDimensions.z/cellSize);
        float[,,] grid = new float[gridSizeX,gridSizeY,gridSizeZ];
        Random randPos = new Random();
        int numRocks = 0;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y<gridSizeY; y++)
            {
                for (int z = 0; z<gridSizeZ; z++)
                {
                    Vector3 position = new Vector3(Random.Range(0,cellSize),Random.Range(0,cellSize),Random.Range(0,cellSize));
                    GameObject rockObject = Instantiate(rock, Vector3.right*x*cellSize+Vector3.up*y*cellSize+Vector3.forward*z*cellSize + position,Quaternion.identity,parent);
                    rockObject.transform.localScale*=Random.Range(1,100);
                    grid[x,y,z] = numRocks;
                    numRocks++;
                }
            }
        }
        return coordinates;

    }
}
