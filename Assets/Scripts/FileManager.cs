using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AnotherFileBrowser.Windows;
using UnityEngine.Networking;

public class FileManagerUpdate : MonoBehaviour
{
    public RawImage image;
    public levelGenerator lvGen;
    public bool canGenerate = false;

    public bool AnotherImageLoaded;
    public bool DeuErro;
    public bool AbriuBrowser;

    public float realHeight;
    public float realWeight;

    public void OpenFileBrowser()
    {
        AbriuBrowser = true;

        if(AnotherImageLoaded == true){
            lvGen.Reset();
        }
        var bp = new BrowserProperties();
        bp.filter = "Image files (*.png) | *.png";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            StartCoroutine(LoadImage(path));
        });
    }
        

    IEnumerator LoadImage(string path)
    {
        using(UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
        {
            yield return uwr.SendWebRequest();
        
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log("ERROR");
            }
            else
            {
                var uwrTexture = DownloadHandlerTexture.GetContent(uwr);
                image.texture = uwrTexture;
                UpdateImage();
            }
        }
        
    }
    void UpdateImage()
    {
        AbriuBrowser = false;
        lvGen.map = image.texture as Texture2D;
        Debug.Log(lvGen.map.width);
        Debug.Log(lvGen.map.height);
        AnotherImageLoaded = true;
        if(lvGen.map.height == 32 && lvGen.map.width == 32)
        {
            canGenerate = true;
            lvGen.GenerateLevel();

        }
        else{
            Error();
        }
        
    }

    public void Error(){
        Debug.Log("Arquivo invalido, selecione outro");
        lvGen.Reset();
        AnotherImageLoaded = false;
        OpenFileBrowser();
    }

}