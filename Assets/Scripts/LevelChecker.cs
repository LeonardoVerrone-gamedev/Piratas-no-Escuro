using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChecker : MonoBehaviour
{
    public GameObject[] treasures;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkTreasures(){
        treasures = GameObject.FindGameObjectsWithTag("treasure");
        if (treasures.Length == 5){
            Debug.Log("certo");
        }else{
            Debug.Log("errado");
        }
    }

    void PathCheck(){
        
    }
}
