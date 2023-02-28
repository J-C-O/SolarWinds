using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunChange : MonoBehaviour
{
    public GameObject DirectionalLight;

    public GameObject SunArrowUp;
    public GameObject SunArrowDown;
    public GameObject SunArrowRight;
    public GameObject SunArrowLeft;
    public GameObject Emitter;

    private int sunState = 0;

    // Start is called before the first frame update
    void Start()
    {
        incrementSunState();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown("s")) {
            incrementSunState();
            Debug.Log("Sun moved");
            Debug.Log(sunState);
        } 
    }

    void incrementSunState(){
        if (sunState >= 4){
            sunState = 1;
        } else {
            sunState += 1;
        }
        GlobalEmitter globalEmitter = Emitter.GetComponent<Chunk>().GetEmitterFor(PowerType.Solar);
        switch (sunState) {
            case 1:
                //Right
                globalEmitter.direction = new Vector3Int(0, 0, 1);
                DirectionalLight.transform.rotation = Quaternion.Euler(50, 0, 0);
                SunArrowUp.SetActive(false);
                SunArrowDown.SetActive(false);
                SunArrowRight.SetActive(true);
                SunArrowLeft.SetActive(false);
                break;
            case 2:
                //Up
                globalEmitter.direction = new Vector3Int(-1, 0, 0);
                DirectionalLight.transform.rotation = Quaternion.Euler(50, -90, 0);
                SunArrowUp.SetActive(true);
                SunArrowDown.SetActive(false);
                SunArrowRight.SetActive(false);
                SunArrowLeft.SetActive(false);
                break;
            case 3:
                //Left
                globalEmitter.direction = new Vector3Int(0, 0, -1);
                DirectionalLight.transform.rotation = Quaternion.Euler(125, 0, 0);
                SunArrowUp.SetActive(false);
                SunArrowDown.SetActive(false);
                SunArrowRight.SetActive(false);
                SunArrowLeft.SetActive(true);
                break;
            case 4:
                //Down
                globalEmitter.direction = new Vector3Int(1, 0, 0);
                DirectionalLight.transform.rotation = Quaternion.Euler(125, -90, 0);
                SunArrowUp.SetActive(false);
                SunArrowDown.SetActive(true);
                SunArrowRight.SetActive(false);
                SunArrowLeft.SetActive(false);
                break;
        }
    }
}
