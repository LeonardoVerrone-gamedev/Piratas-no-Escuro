using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class levelGenerator : MonoBehaviour
{
[SerializeField] Tilemap ObstacleTimeMap;
[SerializeField] TileBase wall;
[SerializeField] GameObject _grid;

public Texture2D map;
public ColorToPrefab[] colorMappings;

public Transform parent;

public FileManagerUpdate fileManager;
//public WallScript wallScript;
public bool isGenerated;
public bool resetBool;
[SerializeField] GameObject LvChecker;

public GameObject MenuImport;

    void Awake(){
        MenuImport = GameObject.FindWithTag("UIOpenBrowser");
        MenuImport.SetActive(true);
    }
    public void GenerateLevel()
    {
        
        for(int x = 0; x < map.width; x++){
            for(int y = 0; y < map.height; y++){
                GenerateTile(x, y);
            }
        }
        isGenerated = true;
        MenuImport.SetActive(false);
        LvChecker.GetComponent<LevelChecker>().checkTreasures();
        _grid.SetActive(true);
    }

    void GenerateTile (int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        if (pixelColor.a == 0)
        {
            //se opacidade for 0 eh pq nn tem nada ali
            return;
        }
        if(pixelColor.Equals(Color.black)){
            Vector2 position = new Vector2(x,y);
            int xVal = (int)position.x;
            int yVal = (int)position.y;
            Vector3Int pos = new Vector3Int(xVal, yVal, 0);
            ObstacleTimeMap.SetTile(pos, wall);
        }

        foreach (ColorToPrefab colorMapping in colorMappings){
            if(colorMapping.color.Equals(pixelColor)){
                Vector2 position = new Vector2(x,y);
                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
                //colorMappings[0].prefab.GetComponent<WallScript>().CheckAll();
                //colorMappings[1].prefab.GetComponent<BauPequenoScript>().WallCheck();
            }
        }
    }

    public void Reset(){
        MenuImport.SetActive(true);
        isGenerated = false;
        resetBool = true;
        foreach(Transform child in parent){
                Destroy(child.gameObject);
    }
}
}
