using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    Vector2 mousePosition;
    Vector2 direction = new Vector2(5, 3);
    float angle;
    float impulse;
    public float turnSpeed;
    public float impulseSpeed;
    public float moveSpeed;
    public float huntSpeed;
    public float fishMemory;//Cada cuanto gira aleatoriamente
    public Rigidbody2D neck;
    public float hunger = 50;
    public float minHunger = 20;

    Rigidbody2D rb;
    private float ogMoveSpeed;
    private float maxHunger;
    private bool isHungry = true;
    private bool rotar = true;
    private bool flutter = true;
    private bool flutter_dir = true;
    private bool isHunting = false;
    // Start is called before the first frame update
    void Start()
    {
        ogMoveSpeed = moveSpeed;
        maxHunger = hunger;
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Fish") && isHunting){
                hunger = hunger + 30;
                Destroy(collision.gameObject);
            }
        }

    private void OnTriggerStay2D(Collider2D collider)
        {
            if (hunger > minHunger)
            //Si no tienen hambre, forman un cardumen.
            {
                isHunting = false;
            }else{
                if (collider.CompareTag("Fish")){
                    if(collider.GetComponent<FishController>().getShoalClose() < 2){
                        foodBehaviour(collider);
                        isHunting = true;
                    }                
                }
            }
        }

    private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.CompareTag("Fish")){
                isHunting = false;
                }
        }

    void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Fish")){
                isHunting = false;
            }
        }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotar && !isHunting) {
            rotar = false;
            StartCoroutine("changeDirection");
        }

        if (flutter) {
            flutter = false;
            StartCoroutine("changeFlutter");
        }

        if (isHungry) {
            isHungry = false;
            StartCoroutine("feelHungry");
        }

        if(isHunting){
            moveSpeed = huntSpeed;
        } else moveSpeed = ogMoveSpeed;
        // Get world position for the mouse
        // mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Get the direction of the mouse relative to the player and rotate the player to said direction
        // direction = mousePosition - (Vector2)transform.position;

        //direction = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));   
        angle = Vector2.SignedAngle(transform.up, direction);
        impulse = angle * Mathf.Deg2Rad * turnSpeed * rb.inertia;
        rb.AddTorque(impulse, ForceMode2D.Force);
        rb.AddForce((Vector2)transform.up * moveSpeed * Time.deltaTime);

    }

    IEnumerator changeFlutter()
    {
        //Wait for fishMemory seconds
        direction = rb.position - transform.GetChild(2).GetComponent<Rigidbody2D>().position;
        angle = Vector2.SignedAngle(transform.up, direction);
        yield return new WaitForSeconds(0.25f);
        //Debug.Log("Vector: (" + direction.x + "," + direction.y + ")");
        if (flutter_dir) {
            //Debug.Log("Der: (" + Mathf.Sin(Mathf.Deg2Rad * 45f) + ")");
            angle = angle + 45;
        }
        else {
            //Debug.Log("Izq: (" + direction.x + "," + direction.y + ")");
            angle = angle - 45;
        }
        impulse = angle * Mathf.Deg2Rad * impulseSpeed * rb.inertia;
        rb.AddTorque(impulse, ForceMode2D.Impulse);
        //rb.AddForce((Vector2)transform.up * Time.deltaTime);

        flutter_dir = !flutter_dir;
        flutter = true;

    }

    IEnumerator changeDirection()
    {
        //Wait for fishMemory seconds
        yield return new WaitForSeconds(fishMemory);
        //Debug.Log("Cambia la direccion: ");
        naturalMovement();

        rotar = true;
    }

    IEnumerator feelHungry()
    {
        //Wait for fishMemory seconds
        yield return new WaitForSeconds(1f);
        
        if (hunger != 0)
        {
            hunger--;
            
        }
        else
        {
            UnityEngine.Object.Destroy(gameObject);
        }

        isHungry = true;
    }

    //-----------------------------------

    private void foodBehaviour(Collider2D collider){
        Vector2 posn = collider.GetComponent<Transform>().position;//collider.attachedRigidbody.position;
        Vector2 pos0 = transform.position;//rb.position;//
        
        direction = posn - pos0;
    }

    private void naturalMovement()
    {
        direction = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
    }
}
