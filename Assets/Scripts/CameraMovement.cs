using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float minDistance = 2;
    [SerializeField] private float maxDistance = 15;
    [SerializeField] private float distanceToTarget = 3;

    private Vector3 previousPosition;

    private void Awake() {
        Camera.main.transform.Translate(new Vector3(0, 0, -distanceToTarget));
    }
    private void Start()
    {
        cam.transform.position = new Vector3(5, 9, -5);
        cam.transform.Rotate(new Vector3(45, 0, 0));
    }
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
            if (distanceToTarget > minDistance) {
                distanceToTarget -= 1;
                cam.transform.Translate(new Vector3(0, 0, 1));
            }

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (distanceToTarget < maxDistance) {
                distanceToTarget += 1;
                cam.transform.Translate(new Vector3(0, 0, -1));
            }
        }
        
    }
}