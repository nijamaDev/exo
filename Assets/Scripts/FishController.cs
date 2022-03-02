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
    public float tail;
    Rigidbody2D rb;
    public Rigidbody2D cola;
    private bool rotar = true;
    private bool flutter = true;
    private bool flutter_dir = true;
    //public Transform playerLight;
    public Light2D lt;
  // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (rotar)
        {
            rotar = false;
            StartCoroutine("changeDirection");
        }

        if (flutter) {
            flutter = false;
            StartCoroutine("changeFlutter");
        }


        // Get world position for the mouse
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Get the direction of the mouse relative to the player and rotate the player to said direction
        //direction = mousePosition - (Vector2)transform.position;
        
        //direction = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));    
        angle = Vector2.SignedAngle(transform.up, direction);

        impulse = angle * Mathf.Deg2Rad * turnSpeed * rb.inertia;
        rb.AddTorque(impulse, ForceMode2D.Force);

        rb.AddForce((Vector2)transform.up * moveSpeed * Time.deltaTime);
        //playerLight.transform.position = transform.position;
        //lt.intensity = 0.3f + Mathf.PingPong(Time.time / 2, 0.7f);

    }

    IEnumerator changeDirection()
    {
        //Wait for fishMemory seconds
        yield return new WaitForSeconds(fishMemory);
        //Debug.Log("Cambié la dirección: ");
        direction = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));

        rotar = true;

    }

    IEnumerator changeFlutter()
    {
        //Wait for fishMemory seconds
        yield return new WaitForSeconds(0.2f);
        angle = Vector2.SignedAngle(transform.up, direction);
        //Debug.Log("Vector: (" + direction.x + "," + direction.y + ")");
        if (flutter_dir) {
            //Debug.Log("Der: (" + Mathf.Sin(Mathf.Deg2Rad * 45f) + ")");
            angle = angle + 90;
        }
        else {
            //Debug.Log("Izq: (" + direction.x + "," + direction.y + ")");
            angle = angle - 90;
        }
        //angle = angle - 90 + Mathf.PingPong(Time.time,180);
        impulse = angle * Mathf.Deg2Rad * tail;
        cola.AddTorque(impulse, ForceMode2D.Impulse);

        cola.AddForce((Vector2)transform.right * moveSpeed * Time.deltaTime);
        Debug.Log(angle);

        flutter_dir = !flutter_dir;
        flutter = true;

    }


    //private void OnTriggerEnter2D(Collider2D collision){
    //  Destroy(gameObject);
    //}
}
