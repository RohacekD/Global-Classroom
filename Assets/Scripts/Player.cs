using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public string playerName;
    private WeaponManager weaponManager;
    public int P1Score { get; set; }
    public int bombs { get; set; }

    public Text scoreDisplay;
    public Text nameDisplay;
    public Text playerBombs;

    void Start () {
        weaponManager = GetComponent<WeaponManager>();
        P1Score = 0;
        scoreDisplay.text = P1Score.ToString();
        nameDisplay.text = playerName;
        bombs = weaponManager.bombAmount;
        updateBombsText();

    }

    private void updateBombsText()
    {
        playerBombs.text = bombs.ToString() + "x";
        if (bombs == 0)
        {
            playerBombs.color = Color.red;
        }
        else
        {
            playerBombs.color = new Color(255f,255f,255f,255f);
        }
    }
	
	void Update () {
        scoreDisplay.text = P1Score.ToString();
        bombs = weaponManager.bombAmount;
        updateBombsText();
    }

    public void P1AddScore(int value)
    {
        P1Score += value;
    }

    public void P1SubScore(int value)
    {
        P1Score -= value;
    }
}
