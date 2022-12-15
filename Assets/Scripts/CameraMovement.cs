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
            previousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180;
            float rotationAroundXAxis = direction.y * 180;

            Camera.main.transform.position = target.position;
            
            Camera.main.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            Camera.main.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);
            
            Camera.main.transform.Translate(new Vector3(0, 0, -distanceToTarget));

            previousPosition = newPosition;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (distanceToTarget > 2) {
                distanceToTarget -= 1;
                Camera.main.transform.Translate(new Vector3(0, 0, 1));
            }

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (distanceToTarget < 15) {
                distanceToTarget += 1;
                Camera.main.transform.Translate(new Vector3(0, 0, -1));
            }
        }
        
    }
}