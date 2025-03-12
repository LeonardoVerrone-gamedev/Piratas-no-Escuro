using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float countdown;
    public TMP_Text Minutes;

    public TMP_Text Colected;
    public PlayerScript pl;
    //public TMP_Text Seconds;
    
    void Update()
    {

        if(pl == null){
            pl = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        }else{
            Colected.text = "Tesouros Encontrados:" + (pl.colectedTreasures.ToString()) + "/5";
        }

       

        if(countdown>0)
        {
            countdown-=Time.deltaTime;
        }
        double b=System.Math.Round(countdown,2);
        float min=Mathf.FloorToInt(countdown/60);
        float sec=Mathf.FloorToInt(countdown%60);
        Minutes.text="0"+min.ToString() + ":" + sec.ToString();

        if(countdown <= 0){
             SceneManager.LoadScene("Over", LoadSceneMode.Single);
        }
        
        
    }

}

