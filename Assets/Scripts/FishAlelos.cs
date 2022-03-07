using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAlelos : MonoBehaviour
{
    public string[] colorAllele = {"A","a"};
    SpriteRenderer fishSprite;
    public string[] speedAllele = { "F", "f" };
    public string[] selfishnessAllele = { "S", "s" };
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

    void Update(){
        parseToColor("ADN");
    }

    string parseToString(Color alele)//"N0.6f,0.5f,0.7f" A > D > N
    {
        //Debug.Log(alele[0]);
        Debug.Log(new Color(1,1,1).ToString()[4]);
        return alele.ToString();
    }

    Color parseToColor(string alele)//"N0.6f,0.5f,0.7f" A > D > N
    {
        //Debug.Log(alele[0]);
        Debug.Log(new Color(1,1,1).ToString()[4]);
        return Color.white;
    }
    //-------------------------------------------------

}
