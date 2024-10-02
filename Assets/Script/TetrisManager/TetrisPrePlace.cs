using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TetrisPrePlace : MonoBehaviour
{
    public Vector3 rotationPoint;
    private TetrisBlock tetris;
    public bool isValid;
    public bool isDownValid;
    private void Start()
    {
        tetris = FindObjectOfType<TetrisBlock>();
        isValid = true;
        isDownValid = true;
        PrePlaceDown();
        ResetPreplace();
    }
    private void Update()
    {
        ResetPreplace();
    }
    private void PrePlaceDown()
    {
        if (gameObject.name == "L_Down(Clone)")
        {
            if (-8.5f + tetris.GetLevel() > 9.5f) { Destroy(gameObject); }
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, -8.5f + tetris.GetLevel(), 1);
        }
        else
        {
            if (-9.5f + tetris.GetLevel() > 9.5f) { Destroy(gameObject); }
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, -9.5f + tetris.GetLevel(), 1);
        }
        Debug.Log("Down");
    }
    public void ReCheck()
    {
        int max = -1;
        foreach (Transform children in transform)
        {
            int x_pos = Mathf.RoundToInt(children.transform.position.x + 4.5f);
            int y_pos = tetris.GetLevel();
            for(int i = y_pos; i >= 0; i--)
            {
                if(!tetris.IsValidGrid(x_pos, i))
                {
                    int temp = i + 1;
                    if(temp > max) { max = temp; }
                    break;
                }
            }
        }
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, -8.5f + max, 1);
    }
    #region Movement
    public void LeftMove()
    {
        gameObject.transform.position += Vector3.left;
    }
    public void RightMove()
    {
        gameObject.transform.position += Vector3.right;
    }
    public void Rotate()
    {
        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
    }
    public void ReverseRotate()
    {
        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
    }
    #endregion
    private void ResetPreplace()
    {
        foreach (Transform children in transform)
        {
            int x_pos = Mathf.RoundToInt(children.transform.position.x + 4.5f);
            int y_pos = Mathf.RoundToInt(children.transform.position.y + 9.5f);
            if (tetris.GetGrid(x_pos, 19) != null) { Destroy(gameObject); }
            if(gameObject.name == "L_Down(Clone)")
            {
                if (y_pos < -1 || !tetris.IsValidGrid(x_pos, y_pos)) { isValid = false; break; }
            }
            else
            {
                if (y_pos < 0 || !tetris.IsValidGrid(x_pos, y_pos)) { isValid = false; break; }
            }
        }
        if (isValid)
        {
            foreach (Transform children in transform)
            {
                int x_pos = Mathf.RoundToInt(children.transform.position.x + 4.5f);
                int y_pos = Mathf.RoundToInt(children.transform.position.y + 9.5f) - 1;
                if (!tetris.IsValidGrid(x_pos, y_pos)) { isDownValid = false; break; }
            }
        }
        if (!isValid)
        {
            gameObject.transform.position += Vector3.up;
            isValid = true;
        } 
        else if(isDownValid)
        {
            gameObject.transform.position += Vector3.down;
        }
        isDownValid = true;
    }
}
