using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAlelos : MonoBehaviour
{
    public string[] colorAllele = {"A","a"};
    SpriteRenderer fishSprite;
    public string[] speedAllele = { "S", "s" };
    FishController fishController;

    void Start()
    {
        // Color.
        fishSprite = GetComponent<SpriteRenderer>();        
        if(colorAllele[0] == "A" || colorAllele[1] == "A")
        {
            fishSprite.color = new Color(1.26f, 0.29f, 0.1f);
        }else fishSprite.color = Color.white;

        // Speed. 
        fishController = GetComponent<FishController>();
        if (speedAllele[0] == "S" || speedAllele[1] == "S")
        {
            fishController.moveSpeed = 8000;
        }
        else fishController.moveSpeed = 4000;

    }
        
    // Start is called before the first frame update
    public void setfishColor(string[] bornColor){
        colorAllele = bornColor;
    }

    public string[] getfishColor(){
        return colorAllele;
    }

    public void setfishSpeed(string[] bornColor)
    {
        speedAllele = bornColor;
    }

    public string[] getfishSpeed()
    {
        return speedAllele;
    }

    void Update(){
        

    }
}
