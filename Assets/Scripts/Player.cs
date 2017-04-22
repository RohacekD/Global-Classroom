using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float actualHealth { get; set; }
    public float maxHealth { get; set; }
    public string playerName;
    public int score { get; set; }

    public Slider healthBar;
    public Text scoreDisplay;
    public Text nameDisplay;

    // Use this for initialization
    void Start () {
        maxHealth = 100f;
        score = 0;
        actualHealth = maxHealth;
        healthBar.value = calcHealth();
        scoreDisplay.text = score.ToString();
        nameDisplay.text = playerName;
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
