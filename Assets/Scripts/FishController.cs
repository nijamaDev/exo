using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FishController : MonoBehaviour
{
  Vector2 mousePosition;
  Vector2 direction = new Vector2(5, 3);
  float angle;
  float impulse;
  public float turnSpeed;
  public float moveSpeed;
  public float fishMemory;
  Rigidbody2D rb;
  private bool rotar = true;   
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
        
        if (rotar)
        {
            rotar = false;
            StartCoroutine("changeDirection");
        }


        // Get world position for the mouse
        //position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Get the direction of the mouse relative to the player and rotate the player to said direction
        //direction = position - (Vector2)transform.position;

        //direction = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));    

        angle = Vector2.SignedAngle(transform.right, direction);
    impulse = angle * Mathf.Deg2Rad * turnSpeed * rb.inertia;
    rb.AddTorque(impulse, ForceMode2D.Force);

    rb.AddForce((Vector2)transform.right * moveSpeed * Time.deltaTime);
    
    //playerLight.transform.position = transform.position;
    //lt.intensity = 0.3f + Mathf.PingPong(Time.time / 2, 0.7f);
  }

  IEnumerator changeDirection()
  {        
        //Wait for fishMemory seconds
        yield return new WaitForSeconds(fishMemory);
        Debug.Log("Cambié la dirección: ");
        direction = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));

        rotar = true;

    }

    //private void OnTriggerEnter2D(Collider2D collision){
    //  Destroy(gameObject);
    //}
}
