using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointGEN : MonoBehaviour
{
    public GameObject checkpoint;
    public Vector3[] positions;
    
    public int depth = 25;
    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector3[depth];
        int i;
        for(i = 0; i <= depth; i++)
        {
            generateCheckPoint(i);
        }



    }

    public void generateCheckPoint(int x)
    {
        int y = Random.Range(10, 60);
        int z = Random.Range(-25, 25);
        var pos = new Vector3(x*50-(50*depth), y, z);
        positions[x] = pos;
        var rotation = Quaternion.Euler(0,90,0);
        Instantiate(checkpoint,pos,rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
