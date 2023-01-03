using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunChange : MonoBehaviour
{
    
    public GameObject DirectionalLight;

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
                break;
            case 2:
                DirectionalLight.transform.rotation = Quaternion.Euler(50, -90, 0);
                break;
            case 3:
                DirectionalLight.transform.rotation = Quaternion.Euler(125, 0, 0);
                break;
            case 4:
                DirectionalLight.transform.rotation = Quaternion.Euler(125, -90, 0);
                break;
        }
    }
}
