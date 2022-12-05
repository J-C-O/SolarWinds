using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget = 2;

    private Vector3 previousPosition;

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180;
            float rotationAroundXAxis = direction.y * 180;

            cam.transform.position = target.position;
            
            cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);
            
            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

            previousPosition = newPosition;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            /*
            GetComponent<Transform>().position = new Vector3(
                    transform.position.x,
                    transform.position.y - .3f,
                    transform.position.z - .3f
                );
            */
            cam.fieldOfView--;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            /*
            GetComponent<Transform>().position = new Vector3(
                    transform.position.x,
                    transform.position.y + .3f,
                    transform.position.z + .3f
                );
            */
            cam.fieldOfView++;
        }
        
    }
}