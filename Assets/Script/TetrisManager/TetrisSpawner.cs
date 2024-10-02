using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class TetrisSpawner : MonoBehaviour
{
    public GameObject[] Tetrominoes;
    public GameObject[] TetrisUI;
    public GameObject[] TetrisHold;
    public GameObject[] TetrisPrePlace;
    private Transform temp;
    private TetrisBlock tetris;
    private TetrisLogic tetrisLogic;
    void Start()
    {
        tetrisLogic = FindObjectOfType<TetrisLogic>();
        tetris = FindObjectOfType<TetrisBlock>();
        if (SceneManager.GetActiveScene().name == "PlayScreen") { NewTetromino(); }
        else { BackGroundEffect(); }
    }
    public void NextTetrisUI()
    {
        GameObject[] tempUI = GameObject.FindGameObjectsWithTag("TetrisUI");
        GameObject[] tempPreplace = GameObject.FindGameObjectsWithTag("TetrisPreplace");
        if (tempUI.Length > 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("TetrisUI").gameObject);
        }
        if(tempPreplace.Length > 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("TetrisPreplace").gameObject);
        }
        if (tetrisLogic.next == 1 || tetrisLogic.next == 3 || tetrisLogic.next == 4 || tetrisLogic.next == 6)
        {
            Instantiate(TetrisUI[tetrisLogic.next], new Vector3(9, 9.6f, 1), Quaternion.identity);
        }
        else if (tetrisLogic.next == 5)
        {
            Instantiate(TetrisUI[tetrisLogic.next], new Vector3(8.5f, 9.6f, 1), Quaternion.identity);
        }
        else if (tetrisLogic.next == 0)
        {
            Instantiate(TetrisUI[tetrisLogic.next], new Vector3(9, 10.6f, 1), Quaternion.identity);
        }
        else
        {
            Instantiate(TetrisUI[tetrisLogic.next], new Vector3(9.4f, 10.1f, 1), Quaternion.identity);
        }
    }
    public void PrePlaceTetromino()
    {
        Instantiate(TetrisPrePlace[tetrisLogic.current], transform.position, Quaternion.identity).transform.position = temp.position;
    }
    public void NewTetromino()
    {
        temp = Instantiate(Tetrominoes[tetrisLogic.current], transform.position, Quaternion.identity).transform;
        NextTetrisUI();
        PrePlaceTetromino();
    }
    public void CreatHoldTetromono() {
        if(tetrisLogic.hold == 1 || tetrisLogic.hold == 3 || tetrisLogic.hold == 4 || tetrisLogic.hold == 6)
        {
            Instantiate(TetrisHold[tetrisLogic.hold], new Vector3(-9, 9.6f, 1), Quaternion.identity);
        }
        else if (tetrisLogic.hold == 5)
        {
            Instantiate(TetrisHold[tetrisLogic.hold], new Vector3(-9.3f, 9.6f, 1), Quaternion.identity);
        }
        else if (tetrisLogic.hold == 0)
        {
            Instantiate(TetrisHold[tetrisLogic.hold], new Vector3(-9, 10.6f, 1), Quaternion.identity);
        }
        else
        {
            Instantiate(TetrisHold[tetrisLogic.hold], new Vector3(-8.4f, 10.1f, 1), Quaternion.identity);
        }
    }
    public void FirstHoldTetromino()
    {
        tetrisLogic.FirstHold();
        CreatHoldTetromono();
    }

    public void RestHoldTetromino()
    {
        GameObject[] tempHold = GameObject.FindGameObjectsWithTag("TetrisHold");
        GameObject[] tempPreplace = GameObject.FindGameObjectsWithTag("TetrisPreplace");
        if (tempHold.Length > 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("TetrisHold").gameObject);
        }
        if (tempPreplace.Length > 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("TetrisPreplace").gameObject);
        }
        tetrisLogic.RestHold();
        CreatHoldTetromono();
    }
    public void BackGroundEffect()
    {
        Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);
    }
}