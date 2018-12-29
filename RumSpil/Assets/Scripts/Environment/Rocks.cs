using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

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
    public List<GameObject> activeChunks;
    bool[,,] chunkArray;
    private List<GameObject> pooledChunks;


    // Start is called before the first frame update
    void Awake()
    {
        pooledChunks = new List<GameObject>();
        instance = this;
        cellSize = Mathf.Sqrt(3)*radius;
        numCells =Mathf.FloorToInt(desiredChunkSize/cellSize);
        chunkSize = cellSize*numCells;
        chunkArray  = new bool[1000,1000,1000];
        //Debug.Log(chunkSize);
    }

    public struct IntVector3{
        public int x;
        public int y;
        public int z;
    }

    public void CreateChunk(Vector3 desiredPosition)
    {

        Vector3 chunkPosition = ChunkCoordinates(desiredPosition)*chunkSize;
        IntVector3 arrayCoordinates = GetArrayCoordinate(desiredPosition);
        //("Chunk position" + chunkPosition.ToString());
        //Debug.Log("New chunk at: "+ arrayCoordinates.x.ToString() + ", "+ arrayCoordinates.y.ToString() + ", "+ arrayCoordinates.z.ToString() + ", ");
        chunkArray[arrayCoordinates.x,arrayCoordinates.y,arrayCoordinates.z] = true;

        Transform fieldParent = Instantiate(empty,chunkPosition,Quaternion.identity,transform).transform;
        fieldParent.name = "Field";
        activeChunks.Add(fieldParent.gameObject);

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
                    Transform rockObject = Instantiate(rock, fieldParent).transform;
                    rockObject.rotation = Random.rotation; 
                    Vector3 position = new Vector3(Random.Range(0,cellSize),Random.Range(0,cellSize),Random.Range(0,cellSize));
                    rockObject.localPosition = Vector3.right*x*cellSize+Vector3.up*y*cellSize+Vector3.forward*z*cellSize + position;
                    rockObject.name = "Rock"; 
                    rockObject.localScale = Vector3.up*Random.Range(0.03f,0.3f) + Vector3.right*Random.Range(0.03f,0.3f) + Vector3.forward*Random.Range(0.03f,0.3f);
                    if (Random.Range(0f,10f) > 9.5f)
                        rockObject.localScale*=20;
                    else if (Random.Range(0f,10f) > 9f)
                        rockObject.localScale*=5;
                    grid[x,y,z] = numRocks;
                    numRocks++;
                }
            }
        }
        return;
        
    }
    void MoveChunk(Vector3 chunkLocation,GameObject chunk)
    {
        chunk.transform.position = chunkLocation;
    
    }

    public void EnterSection(Vector3 currentPos)
    {
        Profiler.BeginSample("Delete chunks");
        Vector3 TestLocation;
        for (int i = activeChunks.Count - 1; i >= 0; i--)
        {
            Vector3 distance = activeChunks[i].transform.position+Vector3.one*chunkSize/2 - currentPos;
            if (Mathf.Abs(distance.x)>chunkSize*2 || Mathf.Abs(distance.y)>chunkSize*2 || Mathf.Abs(distance.z)>chunkSize*2)
            {

                IntVector3 arrayPos = GetArrayCoordinate(activeChunks[i].transform.position);
                chunkArray[arrayPos.x,arrayPos.y,arrayPos.z] = false;          
                pooledChunks.Add(activeChunks[i]);
                activeChunks.Remove(activeChunks[i]);
            }
                
            

        }
        Profiler.EndSample();
        Profiler.BeginSample("Create Chunks");
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    TestLocation = currentPos + new Vector3(x*chunkSize,y*chunkSize,z*chunkSize);
                    if (FieldInArea(TestLocation))
                        continue;
                    if (pooledChunks.Count != 0)
                    {
                        Debug.Log("Moving chunk");
                        MoveChunk(ChunkCoordinates(TestLocation)*chunkSize, pooledChunks[0]);
                        pooledChunks.RemoveAt(0);
                    }
                    else
                        CreateChunk(TestLocation);
                }
            } 
        }
        Profiler.EndSample();

        

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
