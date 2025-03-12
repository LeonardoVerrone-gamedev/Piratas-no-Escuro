using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureScript : MonoBehaviour
{

    public  Transform[] Transformpoints;
    public Transform PlayerPointCheck;
    public LayerMask wallLayer;
    public levelGenerator lvGen;
    public bool canSpawn;

    public bool isColected;
    public LayerMask playerLayer;
    public void Awake(){
        
        lvGen = GameObject.FindWithTag("generator").GetComponent<levelGenerator>();
        CheckPoints();
    }
    void Update(){
        if(!canSpawn){
            lvGen.Reset();
        }


    }

    

    public void CheckPoints(){
        int cont = 0;
        int max = Transformpoints.Length;
        for (int i = 0; i < max; i++){
            Collider2D colliders = Physics2D.OverlapPoint(Transformpoints[cont].position, wallLayer);
            if(colliders != null){
                Debug.Log("Opa");
                canSpawn = false;
            }
            else{
                canSpawn = true;
            }
        }
        }
    
    

}
