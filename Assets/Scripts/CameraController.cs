using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  float zoomSpeed = 2, minZoom = 1, maxZoom = 101;
  Camera cam;
  Transform move;
  Vector2 oldMousePos;
  Vector2 newMousePos;
  // Start is called before the first frame update
  void Start()
  {
    cam = gameObject.GetComponent<Camera>();
    move = gameObject.transform;
  }

  // Update is called once per frame
  void Update()
  {
    CameraZoom();
    CameraMovement();
  }
  void CameraZoom()
  {
    // Get mouse wheel scroll (value is either -1, 0 or 1)
    float zoom = -Input.mouseScrollDelta.y;
    // Add zoom value to the camera size
    cam.orthographicSize += zoom * zoomSpeed;
    // Set limits to camera size
    if (cam.orthographicSize < minZoom)
    {
      cam.orthographicSize = minZoom;
    }
    else if (cam.orthographicSize > maxZoom)
    {
      cam.orthographicSize = maxZoom;
    }
  }
  void CameraMovement()
  {
    if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
    {
      oldMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    else if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
    {
      newMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
      Vector2 camDelta = oldMousePos - newMousePos;
      move.Translate(camDelta);
    }
  }
}

