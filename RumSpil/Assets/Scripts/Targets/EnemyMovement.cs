using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private int upDown = 1;
    private int leftRight = 1;
    private Laser laser;
    [SerializeField] bool randomEvents = true;
    [SerializeField] float baseSpeed;
    float speed
    {
        get
        {
            return baseSpeed+factor*100f;
        }
    }
    [SerializeField] float baseTurnRate = 40f;
    float turnRate
    {
        get
        {
            return baseTurnRate+factor*100f;
        }
    }
    [SerializeField] float baseScanDistance = 140f;
    float scanDistance
    {
        get
        {
            return baseScanDistance+factor*100f;
        }
    }
    // Start is called before the first frame update
    Transform targetPlayer;
    float nextRandomEvent;
    Vector3 noise;
    float factor=0;
    int side = 0;
    bool event1 = false;
    Vector3 target
    {
        get
        {
           return (targetPlayer.position+targetPlayer.forward*1500*Time.deltaTime)+(factor)*(targetPlayer.forward+targetPlayer.right*side)*20000f;
        }
    }
    
    Vector3 difference;
    void Awake()
    {
        laser = GetComponentInChildren<Laser>();
        targetPlayer = FindObjectOfType<PlayerInput>().transform;
        noise = new Vector3(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f));
        upDown = Random.Range(0,2)*2-1;
        leftRight = Random.Range(0,2)*2-1;

    }
    void Start()
    {
        nextRandomEvent = Time.time+ Random.Range(5,40);
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Time.time > nextRandomEvent && randomEvents) 
        {
            if (!event1)
            {
                StartRandEvent();
                nextRandomEvent = Time.time + 5;
                event1 = true;
            }
            else
            {
                EndRandEvent();
                event1 = false;
                nextRandomEvent = Time.time + Random.Range(20,50);
            }


        }
        Move();
    }

    void Move(){
        Vector3 up = transform.position+upDown*transform.up*40;
        Vector3 down = transform.position-upDown*transform.up*40;
        Vector3 left = transform.position-leftRight*transform.right*40;
        Vector3 right = transform.position+leftRight*transform.right*40;
        Debug.DrawRay(up,transform.forward*scanDistance);
        Debug.DrawRay(down,transform.forward*scanDistance,Color.cyan);
        Debug.DrawRay(left,transform.forward*scanDistance, Color.red);
        Debug.DrawRay(right,transform.forward*scanDistance,Color.blue);
        Vector3 adjustment = Vector3.zero;

        if (Physics.Raycast(down,transform.forward,scanDistance))
            adjustment += -upDown*Vector3.right;
            
        else if (Physics.Raycast(up,transform.forward,scanDistance))
            adjustment += upDown*Vector3.right;

        if (Physics.Raycast(right,transform.forward,scanDistance))
            adjustment += -leftRight*Vector3.up;
        
        else if (Physics.Raycast(left,transform.forward,scanDistance))
            adjustment += leftRight*Vector3.up;

        difference = target-transform.position;
        if (adjustment == Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(difference+difference.magnitude*0.2f*noise ,Vector3.up),Time.deltaTime*turnRate/8);
        }
        else
        {
            transform.Rotate(adjustment*Time.deltaTime*150f,Space.Self);
        }
        transform.position += transform.forward*speed*Time.deltaTime;
    }
    void StartRandEvent()
    {
        if (Random.Range(0,2) == 1)
        {
            side = Random.Range(0,2)*2-1;
            factor = Random.Range(2f,3f);
        }
        else
            transform.position = targetPlayer.position + targetPlayer.forward*20000;


    }
    void EndRandEvent()
    {
        side = 0;
        factor = 0;
    }
}
