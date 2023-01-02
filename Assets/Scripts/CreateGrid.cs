using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{

    public GameObject fieldBlock;
    public GameObject borderBlock;
    public float fieldLength;
    public float fieldWidth;

    private Vector3 myTransform;
    private GameObject parentField;
    private GameObject parentBorder;

    // Start is called before the first frame update
    void Start()
    {
        spawnBorder();
        spawnField();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnBorder ()
    {
        parentBorder = new GameObject();
        parentBorder.name = "Border";

        for (float x = 0f; x < fieldLength; x++) 
        {
            myTransform = new Vector3(x, 0f, 0f);
            Instantiate(borderBlock, myTransform, Quaternion.identity, parentBorder.transform);
            myTransform = new Vector3(x, 0f, fieldWidth-1);
            Instantiate(borderBlock, myTransform, Quaternion.identity, parentBorder.transform);
        }

        for (float z = 1f; z < fieldWidth-1; z++) 
        {   
            myTransform = new Vector3(0f, 0f, z);
            Instantiate(borderBlock, myTransform, Quaternion.identity, parentBorder.transform);
            myTransform = new Vector3(fieldLength-1, 0f, z);
            Instantiate(borderBlock, myTransform, Quaternion.identity, parentBorder.transform);
        }


    }

    void spawnField ()
    {
        parentField = new GameObject();
        parentField.name = "Field";

        for (float x = 1f; x < fieldLength-1; x++) 
        {
            for (float z = 1f; z < fieldWidth-1; z++) 
            {   
                myTransform = new Vector3(x, 0f, z);
                Instantiate(fieldBlock, myTransform, Quaternion.identity, parentField.transform);
            }
        }


    }
}
