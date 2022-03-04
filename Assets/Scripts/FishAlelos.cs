using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAlelos : MonoBehaviour
{
    public string[] colorAllele = {"A","a"};
    SpriteRenderer fishSprite;

    void Start()
    {
        fishSprite = GetComponent<SpriteRenderer>();        
        if(colorAllele[0] == "A" || colorAllele[1] == "A")
        {
            fishSprite.color = new Color(1.26f, 0.29f, 0.1f);
        }else fishSprite.color = Color.white;
    }
        
    // Start is called before the first frame update
    public void setfishColor(string[] bornColor){
        colorAllele = bornColor;
    }

    public string[] getfishColor(){
        return colorAllele;
    }

    void Update(){
        

    }
}
