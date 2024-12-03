using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [SerializeField] private float zoomStep, minCamSize, maxCamSize;

    [SerializeField] private SpriteRenderer mapRenderer;

    private float mapMinX, mapMaxX, mapMinY, mapMaxY;
    
    private Vector3 dragOrigin;

    private void Awake()
    {
        //Receiving image max X and Y values
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;
        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    private void Update()
    {
        //Zooming with mouse scroll
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
        
        //Zooming with two fingers
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchZeroOnePos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchZeroOnePos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;
            
            Zoom(difference * 0.01f);
        }
        else
        {
            PanCamera();
        }
    }

    private void PanCamera()
    {
        //Save position of mouse in world space when drags
        if (Input.GetMouseButtonDown(0))
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        //Caalculate distance between drag origin and new position
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            
            print("origin" + dragOrigin + "newPosition" + cam.ScreenToWorldPoint(Input.mousePosition) + "=difference" + difference);
            
            //Move cam by that distance
            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }
    }

    public void Zoom(float increment)
    {
        float newSize = cam.orthographicSize - increment;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
    }

    //Controlling limits of image panning
    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camWidth;
        float maxY = mapMaxY - camWidth;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, -10);
    }
}
