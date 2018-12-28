using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour
{
    public static Rocks instance;

    [SerializeField]
    GameObject boundary;
    [SerializeField]
    GameObject empty;
    [SerializeField]
    GameObject rock;
    [SerializeField]
    float radius;
    [SerializeField]
    float desiredChunkSize;

    int numCells;
    float chunkSize;
    float cellSize;
    public List<GameObject> activeFields;
    bool[,,] chunkArray;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        cellSize = Mathf.Sqrt(3)*radius;
        numCells =Mathf.FloorToInt(desiredChunkSize/cellSize);
        chunkSize = cellSize*numCells;
        chunkArray  = new bool[1000,1000,1000];
        Debug.Log(chunkSize);
    }

    public struct IntVector3{
        public int x;
        public int y;
        public int z;
    }

    public  void CreateChunk(Vector3 desiredPosition)
    {
        Vector3 chunkPosition = ChunkCoordinates(desiredPosition)*chunkSize;
        IntVector3 arrayCoordinates = GetArrayCoordinate(desiredPosition);
        Debug.Log("Chunk position" + chunkPosition.ToString());

        Debug.Log("New chunk at: "+ arrayCoordinates.x.ToString() + ", "+ arrayCoordinates.y.ToString() + ", "+ arrayCoordinates.z.ToString() + ", ");
        chunkArray[arrayCoordinates.x,arrayCoordinates.y,arrayCoordinates.z] = true;

        Transform fieldParent = Instantiate(empty,chunkPosition,Quaternion.identity,transform).transform;
        fieldParent.name = "Field";
        activeFields.Add(fieldParent.gameObject);

        Transform asteroidParent = Instantiate(empty,fieldParent).transform;
        asteroidParent.name = "Asteroids";

        List<Vector3> coordinates = new List<Vector3>();
        GameObject collObj = Instantiate(boundary,fieldParent);
        collObj.name = "Collider";

        Transform collTrans = collObj.transform;
        collTrans.localScale = Vector3.one*chunkSize;
        float[,,] grid = new float[numCells,numCells,numCells];
        Random randPos = new Random();
        int numRocks = 0;
        for (int x = 0; x < numCells; x++)
        {
            for (int y = 0; y<numCells; y++)
            {
                for (int z = 0; z<numCells; z++)
                {
                    GameObject rockObject = Instantiate(rock, Vector3.zero,Random.rotation, fieldParent);
                    Vector3 position = new Vector3(Random.Range(0,cellSize),Random.Range(0,cellSize),Random.Range(0,cellSize));
                    rockObject.transform.localPosition = Vector3.right*x*cellSize+Vector3.up*y*cellSize+Vector3.forward*z*cellSize + position;
                    rockObject.name = "Rock"; 
                    rockObject.transform.localScale = Vector3.up*Random.Range(0.03f,0.3f) + Vector3.right*Random.Range(0.03f,0.3f) + Vector3.forward*Random.Range(0.03f,0.3f);
                    if (Random.Range(0f,10f) > 9.5f)
                        rockObject.transform.localScale*=20;
                    else if (Random.Range(0f,10f) > 9f)
                        rockObject.transform.localScale*=5;
                    grid[x,y,z] = numRocks;
                    numRocks++;
                }
            }
        }
        return;
        
    }
    
    public void EnterSection(Vector3 currentPos)
    {
        Vector3 TestLocation;
        for (int i = activeFields.Count - 1; i >= 0; i--)
        {
            Vector3 distance = activeFields[i].transform.position+Vector3.one*chunkSize/2 - currentPos;
            if (Mathf.Abs(distance.x)>chunkSize*5 || Mathf.Abs(distance.y)>chunkSize*5 || Mathf.Abs(distance.z)>chunkSize*5)
            {

                IntVector3 arrayPos = GetArrayCoordinate(activeFields[i].transform.position);
                chunkArray[arrayPos.x,arrayPos.y,arrayPos.z] = false;          
                Destroy(activeFields[i]);
                activeFields.Remove(activeFields[i]);
            }
                
            

        }

        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                for (int z = -2; z <= 2; z++)
                {
                    TestLocation = currentPos + new Vector3(x*chunkSize,y*chunkSize,z*chunkSize);
                    if (FieldInArea(TestLocation))
                        continue;
                    CreateChunk(TestLocation);
                }
            } 
        }

        

    }
    public bool FieldInArea(Vector3 position)
    {
        IntVector3 arrayCords = GetArrayCoordinate(position);
        return chunkArray[arrayCords.x,arrayCords.y,arrayCords.z];
    }
    public IntVector3 GetArrayCoordinate(Vector3 position)
    {
        IntVector3 coordinates = new IntVector3();
        coordinates.x = Mathf.FloorToInt(position.x/chunkSize)+500;
        coordinates.y = Mathf.FloorToInt(position.y/chunkSize)+500;
        coordinates.z = Mathf.FloorToInt(position.z/chunkSize)+500;
        return coordinates;
    }
    public Vector3 ChunkCoordinates(Vector3 position)
    {
        Vector3 coordinates = new Vector3();
        coordinates.x = Mathf.FloorToInt(position.x/chunkSize);
        coordinates.y = Mathf.FloorToInt(position.y/chunkSize);
        coordinates.z = Mathf.FloorToInt(position.z/chunkSize);
        return coordinates;
    }


}
