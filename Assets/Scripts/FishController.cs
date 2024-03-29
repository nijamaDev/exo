using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FishController : MonoBehaviour
{
  Vector2 direction = new Vector2(5, 3);
  float angle;
  float impulse;
  public float turnSpeed;
  public float moveSpeed;
  public float flightSpeed;
  public float fishMemory;//Cada cuanto gira aleatoriamente
  public float tail;//que tan fuerte mueve la cola
  public float shoalRadius = 12;
  public Rigidbody2D cola;
  public CircleCollider2D shoal;
  public float hunger = 50;
  public float minHunger = 20;
  public bool selfishness;

  Rigidbody2D rb;
  private int shoalClose = 0;
  private int fishClose = 0;
  private int foodClose = 0;
  private int sharkClose = 0;
  private float maxHunger;
  private bool rotar = true;
  private bool isHungry = true;
  private bool flutter = true;
  private bool flutter_dir = true;
  private bool isFleeing = false;
  private float ogMoveSpeed;

    //public Transform playerLight;
    public Light2D lt;
  // Start is called before the first frame update

  void Start()
  {
    ogMoveSpeed = moveSpeed;
    flightSpeed = moveSpeed * 4;
    maxHunger = hunger;
    rb = GetComponent<Rigidbody2D>();
  }

  public int getShoalClose()
  {
    return shoalClose;
  }
  private void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.CompareTag("Fish"))
    {
      if (collider.IsTouching(shoal))
      {
        shoalClose++;
      }
      else
      {
        fishClose++;
      }
    }

    if (collider.CompareTag("Plancton"))
    {
      foodClose++;
    }

    if (collider.CompareTag("Shark"))
    {
        sharkClose++;
        //flightBehaviour(selfishness, collider);
    }
  }

  private void OnTriggerStay2D(Collider2D collider)
  {
    if (collider.attachedRigidbody != null)
    {
      if (hunger > minHunger)
      //Si no tienen hambre, forman un cardumen.
      {
        if (collider.CompareTag("Shark"))
        {
          sharkClose++;
          flightBehaviour(selfishness, collider);
        }
        if (collider.CompareTag("Fish"))
        {
          shoalBehaviour(collider);
        }
      }
      else
      //Si están hambrientos, su comportamiento depende de si hay comida cerca o no.
      {
        if (foodClose == 0)
        //Si no hay, se mantiene cerca del cardumen para sobrevivir.
        {
            if (collider.CompareTag("Shark"))
            {
              sharkClose++;
              flightBehaviour(selfishness, collider);
            }
            if (collider.CompareTag("Fish"))
            {
                desitionBehaviour(selfishness, collider);
            }
        }
        else
        //Si hay comida cerca, se vuelven unos egoístas.
        {
          if (collider.CompareTag("Plancton"))
          {
              /*f (foodClose > sharkClose)
              {*/
                 foodBehaviour(collider);
              /*}
             else 
             {
                 sharkClose++;
                 flightBehaviour(selfishness, collider);
             }*/
          }
        }
      }
    }
  }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Fish"))
        {
            if (collider.IsTouching(shoal))
            {
                shoalClose--;
            }
            else
            {
                fishClose--;
            }
        }

        if (collider.CompareTag("Plancton"))
        {
            foodClose--;
        }

        if (collider.CompareTag("Shark"))
        {
            sharkClose--;
            isFleeing = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
  {
    //Debug.Log(fishClose);

    if (rotar && fishClose < 2)
    {
      rotar = false;
      StartCoroutine("changeDirection");
    }

    if (flutter)
    {
      flutter = false;
      StartCoroutine("changeFlutter");
    }

    if (isHungry)
    {
      isHungry = false;
      StartCoroutine("feelHungry");
    }

    if (isFleeing)
    {
        moveSpeed = flightSpeed;
    } else moveSpeed = ogMoveSpeed;

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

  // RUTINAS: --------------------------------------------------------------------------
  IEnumerator changeDirection()
  {
    //Wait for fishMemory seconds
    yield return new WaitForSeconds(fishMemory);
    //Debug.Log("Cambi� la direcci�n: ");
    naturalMovement();
    isFleeing = false;
    rotar = true;
  }

  IEnumerator changeFlutter()
  {
    //Wait for fishMemory seconds
    yield return new WaitForSeconds(0.25f);
    angle = Vector2.SignedAngle(transform.up, direction);
    //Debug.Log("Vector: (" + direction.x + "," + direction.y + ")");
    if (flutter_dir)
    {
      //Debug.Log("Der: (" + Mathf.Sin(Mathf.Deg2Rad * 45f) + ")");
      angle = angle + 135;
    }
    else
    {
      //Debug.Log("Izq: (" + direction.x + "," + direction.y + ")");
      angle = angle - 135;
    }
    //angle = angle - 90 + Mathf.PingPong(Time.time,180);
    impulse = angle * Mathf.Deg2Rad * tail;
    cola.AddTorque(impulse, ForceMode2D.Impulse);

    cola.AddForce((Vector2)transform.right * moveSpeed * Time.deltaTime);
    //Debug.Log(angle);

    flutter_dir = !flutter_dir;
    flutter = true;

  }

  IEnumerator feelHungry()
  {
    //Wait for fishMemory seconds
    yield return new WaitForSeconds(1f);

    if (hunger > 0)
    {
      hunger--;
    }
    else
    {
      UnityEngine.Object.Destroy(gameObject);
    }

    isHungry = true;
  }

  //------------------------------------------------------------------------------------

  //Comportamiento en cardumen.
  private void shoalBehaviour(Collider2D collider)
  {
    Vector2 posn = collider.GetComponent<Transform>().position;//collider.attachedRigidbody.position;
    Vector2 pos0 = transform.position;//rb.position;//
                                      //Comportamiento de cardumen.
    if (fishClose > 1 && shoalClose <= collider.GetComponent<FishController>().getShoalClose())
    // Si el cardumen de otro pez tiene m�s peces que el propio...
    {
      // Se dirige hacia el "l�der".
      direction = posn - pos0;
      angle = angle + Mathf.PingPong(Time.time/2, 10)-5;
    }
    else
    {//SIno seguir al cardumen

      Vector2 dirF = collider.GetComponent<Rigidbody2D>().position;
      Vector2 back = collider.transform.GetChild(2).position;
      //Debug.Log("cardumen!");
      //direction = posn - back;
      direction = direction - (posn - pos0);
      //(dirF - back) (posn - pos0)
    }
  }

  //Comportamiento con Plancton.
  private void foodBehaviour(Collider2D collider)
  {
    Vector2 posn = collider.GetComponent<Transform>().position;//collider.attachedRigidbody.position;
    Vector2 pos0 = transform.position;//rb.position;//
    planktonController planktonObject = collider.GetComponent<planktonController>();
    //Ir por comida
    //Si el plancton está tocando la nuca, el pez come.
    if (collider.IsTouching(shoal))
    {
      if (hunger < maxHunger)
      {
        hunger = 50;
        planktonObject.removeHealth();
      }
    }
    //El HAMBRE se puede ajustar posteriormente.
    if (hunger < minHunger)
    {
      direction = posn - pos0;
    }
  }

  private void desitionBehaviour(bool desition, Collider2D collider)
  {
    if (desition)
    {
      StartCoroutine("changeDirection");
    }
    else
    {
      shoalBehaviour(collider);
    }
  }

  private void naturalMovement()
  {
    direction = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
  }

  //Comportamiento con tiburón
  private void flightBehaviour(bool desision, Collider2D collider)
    {
        isFleeing = true;
        if (desision)
        {
            StartCoroutine("changeDirection");
        }
        else 
        {
            Vector2 posn = collider.GetComponent<Transform>().position;//collider.attachedRigidbody.position;
            Vector2 pos0 = transform.position;//rb.position;//

            direction = posn + pos0;
        }
  }

  //private void OnTriggerEnter2D(Collider2D collision){
  //  Destroy(gameObject);
  //}
}
