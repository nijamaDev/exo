using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public float bornRate = 0.4f;
    public float breedingTime = 10f;
    private bool isBreedingTime = true;
    public GameObject fish;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        if (isBreedingTime) {
            isBreedingTime = false;
            StartCoroutine("spawnFish");
        }
        //GameObject go = Instantiate(fish, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        //go.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    IEnumerator spawnFish()
    {
        //Wait for fishMemory seconds
        yield return new WaitForSeconds(breedingTime);        
        GameObject[] shoal = GameObject.FindGameObjectsWithTag("Fish");//Crear arreglo de todos los peces
        //ordenar aleatoriamente
        if(shoal.Length>1){
            
            for(int i = 0; i < shoal.Length*bornRate; i++){//Spawnea segun un ratio[x,x,x,x,x]
                        //Debug.Log(i);
                        int hembra = Random.Range(0,(shoal.Length)/2);//0<=x<length es un entero
                        int macho = Random.Range(shoal.Length/2,shoal.Length);
                        //Debug.Log("hembra: "+hembra+"   macho: "+macho);

                        //Instantiate(fish, shoal[i].transform.position, Quaternion.identity);//spawn en la posision
                        GameObject nemo = Instantiate(fish, shoal[hembra].transform.position, Quaternion.identity) as GameObject;
                        FishAlelos fishAlelos = nemo.GetComponent<FishAlelos>();
                        
                        //COLOR:
                        string aleloH = shoal[hembra].GetComponent<FishAlelos>().getfishColor()[Random.Range(0,2)];
                        string aleloM = shoal[macho].GetComponent<FishAlelos>().getfishColor()[Random.Range(0,2)];
                        string[] alelosNemoC = { aleloH , aleloM };
                        fishAlelos.setfishColor(alelosNemoC);
                        //nemo.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

                        //SPEED:
                        aleloH = shoal[hembra].GetComponent<FishAlelos>().getfishSpeed()[Random.Range(0,2)];
                        aleloM = shoal[macho].GetComponent<FishAlelos>().getfishSpeed()[Random.Range(0,2)];
                        string[] alelosNemoSp = { aleloH , aleloM };
                        nemo.GetComponent<FishAlelos>().setfishSpeed(alelosNemoSp);

                        //SELFISHNESS:
                        aleloH = shoal[hembra].GetComponent<FishAlelos>().getfishSelfishness()[Random.Range(0,2)];
                        aleloM = shoal[macho].GetComponent<FishAlelos>().getfishSelfishness()[Random.Range(0,2)];
                        string[] alelosNemoSf = { aleloH , aleloM };
                        nemo.GetComponent<FishAlelos>().setfishSelfishness(alelosNemoSf);
                    }
        }
        

        //Debug.Log(shoal[0].transform.position);

        //Instantiate(fish, shoal[0].transform.position, Quaternion.identity);

        isBreedingTime = true;
    }
}
