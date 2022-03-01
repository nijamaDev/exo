using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class FishController : MonoBehaviour
{
  public Texture2D cursorTexture;
  Vector2 mousePosition;
  Vector2 direction;
  float angle;
  float impulse;
  public float turnSpeed;
  public float moveSpeed;
  Rigidbody2D rb;
  public Rigidbody2D projectile;
  public float bulletSpeed;
  public Transform playerLight;
  public Light2D lt;
  // Start is called before the first frame update
  void Start()
  {
    // Set custom cursor
    //Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    rb = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {

    // Get world position for the mouse
    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    // Get the direction of the mouse relative to the player and rotate the player to said direction
    direction = mousePosition - (Vector2)transform.position;
    angle = Vector2.SignedAngle(-transform.up, direction);
    impulse = angle * Mathf.Deg2Rad * turnSpeed * rb.inertia;
    rb.AddTorque(impulse, ForceMode2D.Force);

    if (Input.GetKeyDown(KeyCode.Mouse0))
    {
      Rigidbody2D bullet = Instantiate(projectile, transform.position, transform.rotation);
      bullet.velocity = rb.velocity + ((Vector2)transform.up * -bulletSpeed);
    }
    if (Input.GetKey(KeyCode.Mouse1))
    {
      rb.AddForce(-(Vector2)transform.up * moveSpeed * Time.deltaTime);
    }
    playerLight.transform.position = transform.position;
    lt.intensity = 0.7f + Mathf.PingPong(Time.time / 8, 0.3f);
  }

  //private void OnTriggerEnter2D(Collider2D collision){
  //  Destroy(gameObject);
  //}
}
