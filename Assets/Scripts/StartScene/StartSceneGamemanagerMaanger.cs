using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneGamemanagerMaanger : MonoBehaviour
{
    public GameObject StartButton;

    private void Awake()
    {
        //GameObject StartButton = GameObject.Find("StartButton");
    }
    

    public void GameStartButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
