using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAlelos : MonoBehaviour
{
    public string[] colorAllele = {"A","a"};//{"D4f,2f,6f","N1.0f,1.0f,1.0f"} A > M > a
    SpriteRenderer fishSprite;
    public string[] speedAllele = { "F", "f" };
    public string[] selfishnessAllele = { "S", "s" };
    FishController fishController;

    void Start()
    {
        // Color.
        fishSprite = GetComponent<SpriteRenderer>();        
        if(colorAllele[0][0].ToString() == "A" || colorAllele[1][0].ToString() == "A")//Cuando sea dominante naranja pone naranja
        {
            fishSprite.color = new Color(1.00f, 0.29f, 0.1f);//naranjita
        }else if(colorAllele[0][0].ToString() == "a" && colorAllele[1][0].ToString() == "a"){
            fishSprite.color = Color.white;
        }else if(colorAllele[0][0].ToString() == "M"){//Si la mama es M pone a la mama, sino al papá
            fishSprite.color = parseToColor(colorAllele[0]);
        }else fishSprite.color = parseToColor(colorAllele[1]);

        // Speed. 
        fishController = GetComponent<FishController>();
        if (speedAllele[0] == "F" || speedAllele[1] == "F")
        {
            fishController.moveSpeed = 8000;
        }
        else fishController.moveSpeed = 4000;

        // Selfishness. 
        if (selfishnessAllele[0] == "S" || selfishnessAllele[1] == "S")
        {
            fishController.selfishness = false;
        }
        else fishController.selfishness = true;

    }
        
    // Start is called before the first frame update

    // Set&Get del color. -----------------------------
    public void setfishColor(string[] bornColor){
        colorAllele = bornColor;
    }
    public string[] getfishColor(){
        return colorAllele;
    }
    //-------------------------------------------------

    // Set&Get de la velocidad. -----------------------
    public void setfishSpeed(string[] bornSpeed)
    {
        speedAllele = bornSpeed;
    }
    public string[] getfishSpeed()
    {
        return speedAllele;
    }
    //-------------------------------------------------

    // Set&Get del ego�smo. ---------------------------
    public void setfishSelfishness(string[] bornSelfishness)
    {
        selfishnessAllele = bornSelfishness;
    }
    public string[] getfishSelfishness()
    {
        return selfishnessAllele;
    }
    //-------------------------------------------------

    Color parseToColor(string alele)//"N0.6f,0.5f,0.7f" A > M > a
    {
        //Debug.Log(alele.Substring(11,13));
        float R = float.Parse(alele.Substring(1,3))/10;
        float G = float.Parse(alele.Substring(6,3))/10;
        float B = float.Parse(alele.Substring(11,3))/10;

        return new Color(R,G,B);
    }
    //-------------------------------------------------

}
