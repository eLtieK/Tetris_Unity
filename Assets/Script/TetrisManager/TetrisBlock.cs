using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    public float previousTime;
    public float fallingTime;
    public float right;
    public float left;
    public float top;
    public float bottom;
    public static int level;
    public static bool hold = false;
    private TetrisLogic tetrisLogic;
    private TetrisPrePlace prePlaceBlock;
    private TetrisSpawner spawner;
    private SoundManager sound;
    public float speed;
    public static Transform[,] grid = new Transform[10, 20];
    public Transform GetGrid(int x, int y)
    {
        return grid[x, y];
    }
    public bool IsPlayScreen()
    {
        if(SceneManager.GetActiveScene().name == "PlayScreen") { return true; }
        else { return false; }
    }
    public void SetLevel(int n) { level = n; }
    private void Start()
    {
        speed = Random.Range(0.1f, 0.3f);
        right = 4.5f;
        left = -4.5f;
        top = 9.5f;
        bottom = -9.5f;
        fallingTime = 1.2f;
        previousTime = -1.3f;
        tetrisLogic = FindObjectOfType<TetrisLogic>();
        prePlaceBlock = FindObjectOfType<TetrisPrePlace>();
        spawner = FindObjectOfType<TetrisSpawner>();
        sound = FindObjectOfType<SoundManager>();
    }
    private void Update()
    {
        if (IsPlayScreen() && !tetrisLogic.over)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left;
                prePlaceBlock.LeftMove();
                sound.Move(); 
                if (!ValidMove())
                {
                    transform.position -= Vector3.left;
                    prePlaceBlock.RightMove();
                }
                if (transform.position.y > level - 9.5f) { prePlaceBlock.ReCheck(); }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += Vector3.right;
                prePlaceBlock.RightMove();
                sound.Move();
                if (!ValidMove())
                {
                    transform.position -= Vector3.right;
                    prePlaceBlock.LeftMove();
                }
                if(transform.position.y > level - 9.5f) { prePlaceBlock.ReCheck(); }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                prePlaceBlock.Rotate();
                sound.Change();
                if (!ValidMove())
                {
                    transform.position += Vector3.right;
                    prePlaceBlock.RightMove();
                    if (!ValidMove())
                    {
                        transform.position -= Vector3.right;
                        transform.position += Vector3.left;
                        prePlaceBlock.LeftMove();
                        prePlaceBlock.LeftMove();
                        if (!ValidMove())
                        {
                            transform.position -= Vector3.left;
                            prePlaceBlock.RightMove();
                        }
                    }
                    if (!ValidMove())
                    {
                        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                        prePlaceBlock.ReverseRotate();
                    }
                }
                if (transform.position.y > level - 9.5f) { prePlaceBlock.ReCheck(); }
            }
            if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallingTime / 12 : fallingTime))
            {
                transform.position += Vector3.down;
                sound.Move();
                if (!ValidMove())
                {
                    transform.position -= Vector3.down;
                    AddToGrid();
                    CheckForLines();
                    tetrisLogic.AddNumTetris();
                    this.enabled = false;
                    if (!tetrisLogic.over)
                    {
                        tetrisLogic.NextTetris();
                        FindObjectOfType<TetrisSpawner>().NewTetromino();
                    }
                }
                previousTime = Time.time;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.position = prePlaceBlock.gameObject.transform.position;
                transform.position += Vector3.down;
                sound.Hit();
                if (!ValidMove())
                {
                    transform.position -= Vector3.down;
                    AddToGrid();
                    CheckForLines();
                    tetrisLogic.AddNumTetris();
                    this.enabled = false;
                    if (!tetrisLogic.over)
                    {
                        tetrisLogic.NextTetris();
                        FindObjectOfType<TetrisSpawner>().NewTetromino();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                if (!hold)
                {
                    spawner.FirstHoldTetromino();
                    tetrisLogic.NextTetris();
                    FindObjectOfType<TetrisSpawner>().NewTetromino();
                    hold = true;
                    Destroy(gameObject);
                } 
                else
                {
                    spawner.RestHoldTetromino();
                    Vector3 vector3 = new Vector3(0.5f, transform.position.y, 1);
                    Instantiate(spawner.Tetrominoes[tetrisLogic.current], vector3, Quaternion.identity);
                    Instantiate(spawner.TetrisPrePlace[tetrisLogic.current], vector3, Quaternion.identity).GetComponent<TetrisPrePlace>();
                    Destroy(gameObject);
                }
            }
        }
        else if(!IsPlayScreen())
        {
            transform.position += Vector3.down * speed;
            if(transform.position.y < -18) 
            {
                Destroy(gameObject);
                TetrisSpawner[] temp = FindObjectsOfType<TetrisSpawner>();
                for(int i = 0; i < temp.Length; i++)
                {
                    if (temp[i].transform.position.x == transform.position.x)
                    {
                        temp[i].BackGroundEffect();
                        break;
                    }
                }
            }
        }
    }
    private bool ValidMove()
    {
        foreach(Transform children in transform)
        {
            float x_pos = children.transform.position.x;
            float y_pos = children.transform.position.y;
            if(x_pos < left || x_pos > right || y_pos > top || y_pos < bottom)
            {
                Debug.Log("Faild");
                return false;
            }
            int new_x_pos = Mathf.RoundToInt(children.transform.position.x + 4.5f);
            int new_y_pos = Mathf.RoundToInt(children.transform.position.y + 9.5f);
            if (grid[new_x_pos, new_y_pos] != null) { return false; }
        }
        return true;
    }
    private void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int x_pos = Mathf.RoundToInt(children.transform.position.x + 4.5f);
            int y_pos = Mathf.RoundToInt(children.transform.position.y + 9.5f);
            if(y_pos > level) { level = y_pos; }
            grid[x_pos, y_pos] = children;
            if(y_pos >= 19) { level = 0; tetrisLogic.over = true; }
            Debug.Log(children.position.x);
        }
    }
    private void CheckForLines()
    {
        for(int i = 19; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDowns(i);
                tetrisLogic.AddScore(1);
                sound.Line();
            }
        }
    }
    private bool HasLine(int i)
    {
        for(int j = 0; j < 10; j++)
        {
            if (grid[j,i] == null) { return false; }
        }
        return true;
    }
    private void DeleteLine(int i)
    {
        for(int j = 0; j < 10; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }
    private void RowDowns(int i)
    {
        for(int z = i; z < 20; z++)
        {
            for(int j = 0; j < 10; j++)
            {
                if (grid[j, z] != null)
                {
                    grid[j, z - 1] = grid[j , z];
                    grid[j, z] = null;
                    grid[j, z - 1].transform.position += Vector3.down;
                }
            }
        }
    }
    public bool IsValidGrid(int x, int y)
    {
        if(y > 19 || y < 0) { return false; }
        if (grid[x,y] == null) { return true; }
        else { return false; }
    }
    public int GetLevel() { return level; }
}
