using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TetrisUI : MonoBehaviour
{
    public GameObject GameOverMenu;
    private TetrisLogic tetrisLogic;

    private bool IsPlay()
    {
        if(SceneManager.GetActiveScene().name == "PlayScreen") { return true; }
        else { return false;}
    }
    private void Start()
    {
        if (IsPlay()) { tetrisLogic = FindObjectOfType<TetrisLogic>(); }
    }
    private void Update()
    {
        if(IsPlay() && tetrisLogic.over) { GameOver(); }
    }
    public void GameOver() { GameOverMenu.SetActive(true);}
    public void Retry() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    public void Play() { SceneManager.LoadScene("PlayScreen"); }
    public void ExitGame() { Application.Quit(); }
    public void Menu() { SceneManager.LoadScene("StartScreen"); }
}
