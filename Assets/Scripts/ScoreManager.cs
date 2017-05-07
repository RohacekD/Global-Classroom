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
        RefreshUI();
    }

    public void AddScoreP2(int add)
    {
        p2Score += add;
        RefreshUI();
    }

    public void SubScoreP1(int sub)
    {
        p1Score -= sub;
        RefreshUI();
    }

    public void SubScoreP2(int sub)
    {
        p2Score -= sub;
        RefreshUI();
    }

    private void RefreshUI()
    {
        p1ScoreDisplay.text = p1Score.ToString();
        p2ScoreDisplay.text = p2Score.ToString();
    }
}
