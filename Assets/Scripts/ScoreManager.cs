using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    private int p1Score = 0;
    private int p2Score = 0;

    public Text p1ScoreDisplay;
    public Text p2ScoreDisplay;

    public void AddScoreP1(int add)
    {
        p1Score += add;
        GameData.Score1 = p1Score;
        RefreshUI();
    }

    public void AddScoreP2(int add)
    {
        p2Score += add;
        GameData.Score2 = p2Score;
        RefreshUI();
    }

    public void SubScoreP1(int sub)
    {
        p1Score -= sub;
        GameData.Score1 = p1Score;
        RefreshUI();
    }

    public void SubScoreP2(int sub)
    {
        p2Score -= sub;
        GameData.Score2 = p2Score;
        RefreshUI();
    }

    private void RefreshUI()
    {
        p1ScoreDisplay.text = p1Score.ToString();
        p2ScoreDisplay.text = p2Score.ToString();
    }
}
