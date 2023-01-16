using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindChange : MonoBehaviour
{
    public Transform WindConeParent;

    public GameObject WindArrowUp;
    public GameObject WindArrowDown;
    public GameObject WindArrowRight;
    public GameObject WindArrowLeft;
    public GameObject Emitter;

    private int windState = 0;

    // Start is called before the first frame update
    void Start()
    {
        incrementWindState();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown("w")) {
            incrementWindState();
            Debug.Log("Wind moved");
            Debug.Log(windState);
        } 
    }

    void incrementWindState(){
        if (windState >= 4){
            windState = 1;
        } else {
            windState += 1;
        }
        GlobalEmitter globalEmitter = Emitter.GetComponent<Chunk>().GetEmitterFor(PowerType.Wind);
        switch (windState) {
            case 1:
                globalEmitter.direction = new Vector3Int(0, 0, 1);
                foreach (Transform cone in WindConeParent)
                {
                    var rotationVector = cone.rotation.eulerAngles;
                    rotationVector.y = 0;
                    cone.transform.rotation = Quaternion.Euler(rotationVector);
                }

                WindArrowUp.SetActive(false);
                WindArrowDown.SetActive(false);
                WindArrowRight.SetActive(true);
                WindArrowLeft.SetActive(false);

                break;
            case 2:
                
                globalEmitter.direction = new Vector3Int(1, 0, 0);
                foreach (Transform cone in WindConeParent)
                {
                    var rotationVector = cone.rotation.eulerAngles;
                    rotationVector.y = 90;
                    cone.transform.rotation = Quaternion.Euler(rotationVector);
                }
                WindArrowUp.SetActive(false);
                WindArrowDown.SetActive(true);
                WindArrowRight.SetActive(false);
                WindArrowLeft.SetActive(false);
                break;
            case 3:
                globalEmitter.direction = new Vector3Int(-1, 0, 0);
                
                foreach (Transform cone in WindConeParent)
                {
                    var rotationVector = cone.rotation.eulerAngles;
                    rotationVector.y = -90;
                    cone.transform.rotation = Quaternion.Euler(rotationVector);
                }
                WindArrowUp.SetActive(true);
                WindArrowDown.SetActive(false);
                WindArrowRight.SetActive(false);
                WindArrowLeft.SetActive(false);
                break;
            case 4:
                globalEmitter.direction = new Vector3Int(0, 0, -1);
                foreach (Transform cone in WindConeParent)
                {
                    var rotationVector = cone.rotation.eulerAngles;
                    rotationVector.y = -180;
                    cone.transform.rotation = Quaternion.Euler(rotationVector);
                }
                WindArrowUp.SetActive(false);
                WindArrowDown.SetActive(false);
                WindArrowRight.SetActive(false);
                WindArrowLeft.SetActive(true);
                break;
        }
    }
}
