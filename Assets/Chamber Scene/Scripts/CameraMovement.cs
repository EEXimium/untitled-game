using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //------------------- Move -------------------
    [SerializeField] private Camera cam;
    private Vector3 dragorigin;

    //------------------- ZOOM -------------------
    [SerializeField] private float maximumCamsize;
    [SerializeField] private float minimumCamsize;

    //------------------- Limit -------------------
    [SerializeField] private SpriteRenderer camlimitrender;
    private float limitminX, limitmaxX, limitminY, limitmaxY;


    private void Start()
    {
        limitminX = camlimitrender.transform.position.x - camlimitrender.bounds.size.x;
        limitmaxX = camlimitrender.transform.position.x + camlimitrender.bounds.size.x;

        limitminY = camlimitrender.transform.position.y - camlimitrender.bounds.size.y;
        limitmaxY = camlimitrender.transform.position.y + camlimitrender.bounds.size.y;
    }

    private void Update()
    {
        PanCamera();

        //------------------- ZOOM -------------------
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) 
        {
            cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel");
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minimumCamsize, maximumCamsize);
            cam.transform.position = CameraLimit(cam.transform.position);
        }
    }
   
    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(2))
        {
            dragorigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 diff = dragorigin - cam.ScreenToWorldPoint(Input.mousePosition);

            cam.transform.position = CameraLimit(cam.transform.position + diff);
        }
    }

    private Vector3 CameraLimit(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float newX = Mathf.Clamp(targetPosition.x, limitminX + camWidth, limitmaxX - camWidth);
        float newY = Mathf.Clamp(targetPosition.y, limitminY + camHeight, limitmaxY - camHeight);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
