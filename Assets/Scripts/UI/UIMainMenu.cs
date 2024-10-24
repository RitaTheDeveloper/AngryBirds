using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    private UIManager _uIManager;
    public void Init(UIManager uIManager)
    {
        _uIManager = uIManager;
    }

    public void OnClickPlay()
    {       
        gameObject.SetActive(false);
        _uIManager.StartGame();
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
