using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public bool player1;
    public bool player2;

    public string playerName;
    private WeaponManager weaponManager;

    public int bombs { get; set; }

    public Text nameDisplay;
    public Text playerBombs;

    void Start ()
    {
        if (player1)
            playerName = GameData.Name1;
        if (player2)
            playerName = GameData.Name2;
        weaponManager = GetComponent<WeaponManager>();
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
        bombs = weaponManager.bombAmount;
        updateBombsText();
    }
}
