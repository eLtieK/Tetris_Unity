using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TetrisLogic : MonoBehaviour
{
    public int current;
    public int next;
    public int hold;
    public int score;
    public int longScore;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI longScoreText;
    public TextMeshProUGUI longTopText;
    public TextMeshProUGUI score_L_Down;
    public TextMeshProUGUI score_L_Up;
    public TextMeshProUGUI score_Line;
    public TextMeshProUGUI score_N_Left;
    public TextMeshProUGUI score_N_Right;
    public TextMeshProUGUI score_Square;
    public TextMeshProUGUI score_T;
    private TetrisSpawner spawner;

    public bool over;

    private void Start()
    {
        score = 0;
        over = false;
        longScore = 0;
        AddLongNumText(PlayerPrefs.GetInt("Top"), longTopText);
        current = -1;
        spawner = FindObjectOfType<TetrisSpawner>();
        for (int i = 0; i < 7; i++)
        {
            PlayerPrefs.SetInt(i.ToString(), 0);
        }
        NextTetris();
    }
    public void NextTetris()
    {
        if(current == -1)
        {
            current = Random.Range(0, spawner.Tetrominoes.Length);
        }
        else
        {
            current = next;
        }
       next = Random.Range(0, spawner.Tetrominoes.Length);
    }

    public void FirstHold()
    {
        hold = current;
    }
    
    public void RestHold()
    {
        int temp = hold;
        hold = current;
        current = temp;
    }
    public void AddLongScore(int n)
    {
        longScore += n;
        AddLongNumText(longScore, longScoreText);
        if (!PlayerPrefs.HasKey("Top"))
        {
            PlayerPrefs.SetInt("Top", longScore);
            longTopText.text = longScoreText.text;
        }
        else if (longScore > PlayerPrefs.GetInt("Top"))
        {
            PlayerPrefs.SetInt("Top", longScore);
            longTopText.text = longScoreText.text;
        }
        else { AddLongNumText(PlayerPrefs.GetInt("Top"), longTopText); }
    }
    public void AddScore(int n)
    {
        score += n;
        if(score < 10) { scoreText.text = "00" + score.ToString(); }
        else if(score < 100) { scoreText.text = "0" + score.ToString(); }
        else { scoreText.text = score.ToString(); }
    }
    public void AddNumText(int scoreToShow, TextMeshProUGUI textToShow) 
    {
        if(scoreToShow < 10) { textToShow.text = "00" + scoreToShow.ToString(); }
        else if(scoreToShow < 100) { textToShow.text = "0" + scoreToShow.ToString(); }
        else { textToShow.text = scoreToShow.ToString(); }
    }
    public void AddLongNumText(int scoreToShow, TextMeshProUGUI textToShow)
    {
        if (scoreToShow < 100) { textToShow.text = "000000" + scoreToShow.ToString(); }
        else if (scoreToShow < 1000) { textToShow.text = "00000" + scoreToShow.ToString(); }
        else if (scoreToShow < 10000) { textToShow.text = "0000" + scoreToShow.ToString(); }
        else if (scoreToShow < 100000) { textToShow.text = "000" + scoreToShow.ToString(); }
        else if (scoreToShow < 1000000) { textToShow.text = "00" + scoreToShow.ToString(); }
        else if (scoreToShow < 10000000) { textToShow.text = "0" + scoreToShow.ToString(); }
        else { textToShow.text = scoreToShow.ToString(); }
    }
    public void AddNumTetris()
    {
        int n = current;
        int temp = PlayerPrefs.GetInt(n.ToString());
        PlayerPrefs.SetInt(n.ToString(), temp + 1);
        int scoreTemp = PlayerPrefs.GetInt(n.ToString());
        switch (n)
        {
            case 0:
                AddNumText(scoreTemp, score_L_Down);
                break;
            case 1:
                AddNumText(scoreTemp, score_L_Up);
                break;
            case 2:
                AddNumText(scoreTemp, score_Line);
                break;
            case 3:
                AddNumText(scoreTemp, score_N_Left);
                break;
            case 4:
                AddNumText(scoreTemp, score_N_Right);
                break;
            case 5:
                AddNumText(scoreTemp, score_Square);
                break;
            case 6:
                AddNumText(scoreTemp, score_T);
                break;
        }
        AddLongScore(Random.Range(50,100));
    }
}
