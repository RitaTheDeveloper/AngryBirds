using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameResults : MonoBehaviour
{
    [SerializeField] private GameObject _winObj;
    [SerializeField] private GameObject _loseObj;

    [Header("Scripts: ")]
    [SerializeField] private LevelManager _levelManager;
    private void Start()
    {
        Off();
    }

    public void LevelCompleted()
    {
        AudioManager.instance.PlaySound("LevelCompleted");
        On();
        DisableAllObjs();
        _winObj.SetActive(true);
    }

    public void LevelFailed()
    {
        AudioManager.instance.PlaySound("LevelFailed");
        On();
        DisableAllObjs();
        _loseObj.SetActive(true);
    }


    private void DisableAllObjs()
    {
        _loseObj.SetActive(false);
        _winObj.SetActive(false);
    }

    private void Off()
    {
        gameObject.SetActive(false);
    }

    private void On()
    {
        gameObject.SetActive(true);
    }

    public void OnClickRestart()
    {
        _levelManager.RestartLevel();
        Off();

    }

    public void OnClickNextLevel()
    {
        _levelManager.LoadNextLevel();
        Off();
    }

}
