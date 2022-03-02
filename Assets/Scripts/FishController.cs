using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FishController : MonoBehaviour
{
  Vector2 mousePosition;
  Vector2 direction;
  float angle;
  float impulse;
  public float turnSpeed;
  public float moveSpeed;
  Rigidbody2D rb;
  //public Transform playerLight;
  public Light2D lt;
  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {

    // Get world position for the mouse
    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    // Get the direction of the mouse relative to the player and rotate the player to said direction
    direction = mousePosition - (Vector2)transform.position;
    angle = Vector2.SignedAngle(transform.right, direction);
    impulse = angle * Mathf.Deg2Rad * turnSpeed * rb.inertia;
    rb.AddTorque(impulse, ForceMode2D.Force);

    if (Input.GetKey(KeyCode.Mouse1))
    {
      rb.AddForce((Vector2)transform.right * moveSpeed * Time.deltaTime);
    }
    //playerLight.transform.position = transform.position;
    lt.intensity = 0.3f + Mathf.PingPong(Time.time / 2, 0.7f);
  }

  //private void OnTriggerEnter2D(Collider2D collision){
  //  Destroy(gameObject);
  //}
}
