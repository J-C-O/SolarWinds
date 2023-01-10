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
        switch (sunState) {
            case 1:
                DirectionalLight.transform.rotation = Quaternion.Euler(50, 0, 0);
                SunArrowUp.SetActive(false);
                SunArrowDown.SetActive(false);
                SunArrowRight.SetActive(true);
                SunArrowLeft.SetActive(false);
                break;
            case 2:
                DirectionalLight.transform.rotation = Quaternion.Euler(50, -90, 0);
                SunArrowUp.SetActive(true);
                SunArrowDown.SetActive(false);
                SunArrowRight.SetActive(false);
                SunArrowLeft.SetActive(false);
                break;
            case 3:
                DirectionalLight.transform.rotation = Quaternion.Euler(125, 0, 0);
                SunArrowUp.SetActive(false);
                SunArrowDown.SetActive(false);
                SunArrowRight.SetActive(false);
                SunArrowLeft.SetActive(true);
                break;
            case 4:
                DirectionalLight.transform.rotation = Quaternion.Euler(125, -90, 0);
                SunArrowUp.SetActive(false);
                SunArrowDown.SetActive(true);
                SunArrowRight.SetActive(false);
                SunArrowLeft.SetActive(false);
                break;
        }
    }
}
