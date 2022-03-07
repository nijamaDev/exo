using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planktonController : MonoBehaviour
{
    public float health;

    public void setSize(float radius){
        transform.localScale = new Vector3(radius,radius,1);
    }

    public void removeHealth(){
        health--;
    }

    void Update()
    {           
        if(health<=4){
            UnityEngine.Object.Destroy(gameObject);
        }
        setSize(health);
    }
}
