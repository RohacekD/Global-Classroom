using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float actualHealth { get; set; }
    public float maxHealth { get; set; }
    public string playerName;
    public int score { get; set; }
    public int bombs {
        get { return bombs; }
        set {
            bombs = value;
            updateBombsText();
        }
    }

    public Slider healthBar;
    public Text scoreDisplay;
    public Text nameDisplay;
    public Text playerBombs;

    // Use this for initialization
    void Start () {
        maxHealth = 100f;
        score = 0;
        actualHealth = maxHealth;
        healthBar.value = calcHealth();
        scoreDisplay.text = score.ToString();
        nameDisplay.text = playerName;
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
            playerBombs.color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f, 1f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        scoreDisplay.text = score.ToString();
    }

    void addScore(int value)
    {
        score += value;
    }

    void subScore(int value)
    {
        score -= value;
    }

    void recieveDamage(float damage)
    {
        actualHealth -= damage;

        if (actualHealth <= 0)
            Die();
        healthBar.value = calcHealth();
    }

    void Die()
    {
        actualHealth = 0;
        Debug.Log("Player died");
    }

    float calcHealth()
    {
        return actualHealth / maxHealth;
    }
}
