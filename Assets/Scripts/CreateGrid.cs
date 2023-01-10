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

    //private Dictionary<Vector2, Tile> _tiles;
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
        parentBorder.transform.parent = this.transform;
        parentBorder.transform.localPosition = Vector3.zero;

        for (float x = 0f; x < fieldLength; x++) 
        {
            myTransform = new Vector3(x, 0f, 0f);
            var instantiatedBorder = Instantiate(borderBlock, Vector3.zero, Quaternion.identity, parentBorder.transform);
            instantiatedBorder.transform.localPosition = myTransform;
            myTransform = new Vector3(x, 0f, fieldWidth-1);
            instantiatedBorder = Instantiate(borderBlock, Vector3.zero, Quaternion.identity, parentBorder.transform);
            instantiatedBorder.transform.localPosition = myTransform;
        }

        for (float z = 1f; z < fieldWidth-1; z++) 
        {   
            myTransform = new Vector3(0f, 0f, z);
            var instantiatedBorder = Instantiate(borderBlock, Vector3.zero, Quaternion.identity, parentBorder.transform);
            instantiatedBorder.transform.localPosition = myTransform;
            myTransform = new Vector3(fieldLength-1, 0f, z);
            instantiatedBorder = Instantiate(borderBlock, Vector3.zero, Quaternion.identity, parentBorder.transform);
            instantiatedBorder.transform.localPosition = myTransform;
        }


    }

    void spawnField ()
    {
        parentField = new GameObject();
        parentField.name = "Field";
        parentField.transform.parent = this.transform;
        parentField.transform.localPosition = Vector3.zero;

        float middleX = GetMiddleValue(fieldLength);
        float middleZ = GetMiddleValue(fieldWidth);

        for (float x = 1f; x < fieldLength-1; x++) 
        {
            for (float z = 1f; z < fieldWidth-1; z++) 
            {   
                myTransform = new Vector3(x, 0f, z);
                var instantiatedField = Instantiate(fieldBlock, Vector3.zero, Quaternion.identity, parentField.transform);
                instantiatedField.transform.localPosition = myTransform;

                if(x == middleX && z == middleZ)
                {
                    if(GameManager.GMInstance != null)
                    {
                        GameManager.GMInstance.FieldCenter = instantiatedField;
                    }    
                }
            }
        }
    }

    private float GetMiddleValue(float value)
    {
        if(value % 2 == 0)
        {
            return value / 2;
        }
        else
        {
            return (value - 1) / 2;
        }
    }
}
