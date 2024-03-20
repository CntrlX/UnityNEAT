using UnityEngine;
using System.Collections;
using SharpNeat.Phenomes;

public class droneControl : UnitController
{
    Rigidbody Br,Bl,Fr,Fl;
    public GameObject Drone;
    public GameObject WingBr,WingBl,WingFr,WingFl;
    public float Abr,Abl,Afr,Afl;

    public GameObject CheckpointGenerator;

    Vector3[] positions;
    public int checkpoints = 0;
    public Vector3 rotation;
    public Vector3 position;
    private IBlackBox box;
    private bool IsRunning;
    public int points;

    // Start is called before the first frame update
    void Start()
    {
        Br = WingBr.GetComponent<Rigidbody>();
        Bl = WingBl.GetComponent<Rigidbody>();
        Fr = WingFr.GetComponent<Rigidbody>();
        Fl = WingFl.GetComponent<Rigidbody>();

        CheckpointGenerator =GameObject.Find("CheckpointGenerator");

        checkpointGEN CG = CheckpointGenerator.GetComponent<checkpointGEN>();
        positions = new Vector3[CG.depth];
        positions = CG.positions;

    }

    // Update is called once per frame

    void Update()
    {
        if (IsRunning)
        {

        //inputs
        rotation = gameObject.transform.rotation.eulerAngles;
        position = gameObject.transform.position; 
        checkpointGEN CG = CheckpointGenerator.GetComponent<checkpointGEN>();


        //Processing

        var check = CG.depth -  checkpoints;
        ISignalArray inputArr = box.InputSignalArray;
            inputArr[0] = rotation.x;
            inputArr[1] = rotation.y;
            inputArr[2] = rotation.z;
            inputArr[3] = position.x;
            inputArr[4] = position.y;
            inputArr[5] = position.z;
            inputArr[6] = positions[check-1].x;
            inputArr[7] = positions[check-1].y;
            inputArr[8] = positions[check-1].z;


            box.Activate();

            ISignalArray outputArr = box.OutputSignalArray;
        var qAbr = (float)outputArr[0];
        var qAbl = (float)outputArr[1];
        var qAfl = (float)outputArr[2];

        print(qAbl+ "      "+qAbr+"       "+qAfl);

        //outputs
        

        // WingBr.transform.Translate(Vector3.up * 100*qAbl*Time.deltaTime);
        // WingBl.transform.Translate(Vector3.up * 100*qAbl*Time.deltaTime);
        // WingFr.transform.Translate(Vector3.up * 100*qAfl*Time.deltaTime);
        // WingFr.transform.Translate(Vector3.up * 100*qAfr*Time.deltaTime);

        gameObject.transform.Translate(Vector3.up * 50*qAbl*Time.deltaTime);
        gameObject.transform.Rotate(new Vector3(((180*qAbr)-90)*Time.deltaTime, 0, ((180*qAfl)-90))*Time.deltaTime);

        // gameObject.transform.Translate(Vector3.forward*100*qAbr*Time.deltaTime);
        // gameObject.transform.Translate(Vector3.right*100*qAfl*Time.deltaTime);


        //fitness
        // if(rotation.x >= -1 && rotation.x <= 1 && rotation.y >= -1 && rotation.y <= 1 && rotation.z >= -1 && rotation.z <= 1){

        //     points = points +10;
        
        // }

        if(position.y > 60){
            points-=100;        
        }
        if(position.y >= 10&& position.y <=60){
            points+=100;   
        }
         if(position.y >= 0&& position.y <10){
            points-=100;   
        }

         Vector3 pos = position - positions[check - 1];
        float distance = pos.magnitude; // Calculate the distance

        if (distance <= 100)   
        {
           int pointsToAdd = (int)(100 - distance); // Adjust points based on the distance
           points += pointsToAdd;

           if (pos == Vector3.zero)
            {
                points += 100;
                checkpoints++;
            }
        }

        }
        
    }
   


   public override void Stop()
    {
        this.IsRunning = false;
    }

    public override void Activate(IBlackBox box)
    {
        this.box = box;
        this.IsRunning = true;
    }

    public override float GetFitness()
    {
        if(points>=0){
            return points;
        }else{
            return 0;
        }
    }
}



