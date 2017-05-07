/* 
 * 23.04.17
 BY MICHEAL AND CARELE 
  TO DO: 
  - Resparwing function (Platform missing, reference to the Refillpointstation Object )
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlaneHealth : MonoBehaviour {

    bool player1;
    bool player2;

    public int startingHealth = 100;                  // The amount of health each plane starts with.
    private int currentHealth;                         // How much health the plane currently has.

    public Slider healthSlider;                       // The slider to represent how much health the plane currently has.
    public Image fillImage;                           // The image component of the slider.
    public Color fullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color zeroHealthColor = Color.red;         // The color the health bar will be when on no health.

    PlaneController planeController;                  // Reference to the player's movement.
    WeaponManager weaponManager;                      // Reference to the Plane Shooting script.

    ScoreManager scoreManager;

    public bool isDead;                               // Whether the player is dead.
    bool damaged;                                     // True when the player gets damaged.

    public AudioClip deathClip;                       // The audio clip to play when the player dies.
    AudioSource playerAudio;                          // Reference to the AudioSource component.

    public float respawnTimer;                        // Used to calculate remaining time to respawn
    public float respawnDelay = 5;                    // Set the time for respawn

    GameObject spawnPoint1;
    GameObject spawnPoint2;

    public int lives = 5;                             //How many lives player has

    private void Start()
    {
        player1 = GetComponent<Player>().player1;
        player2 = GetComponent<Player>().player2;

        //Spawnpoint for player 1 and 2
        spawnPoint1 = GameObject.Find("Spawnpoint1");
        spawnPoint2 = GameObject.Find("Spawnpoint2");      

        // Setting up the references.
        playerAudio = GetComponent<AudioSource>();
        planeController = GetComponent<PlaneController>();
        weaponManager = GetComponentInChildren<WeaponManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        // Set the initial health of the player.
        respawnTimer = respawnDelay;
        currentHealth = startingHealth;
        isDead = false;
    }

    private void Update()
    {
        // Update the health slider's value and color.
        SetHealthUI();

        if (isDead)
        {
            respawnTimer -= Time.deltaTime;
            if (respawnTimer <= 0)
            {
                isDead = false;
                RpcRespawn();
                CmdResetRespawnTime();
            }
        }
    }

    private void CmdResetRespawnTime()
    {
        respawnTimer = respawnDelay;
    }


    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        //healthSlider.value = currentHealth;

        // Play the hurt sound effect.
        //playerAudio.Play ();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();

            //Add score to other player.
            if (player1)
            { 
                scoreManager.AddScoreP2(50);
            }
            else if (player2)
            {
                scoreManager.AddScoreP1(50);
            }
        }

    }

    //Add health to the plane. Repairing speed calculated in Base.cs
    public void Repair()
    {
        if (isDead)
            return;

        if (currentHealth < startingHealth)
        {
            currentHealth++;
        }
    }
   
    private void SetHealthUI()
    {
        // Set the slider's value appropriately.
        healthSlider.value = currentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        fillImage.color = fullHealthColor;

        //Can't get the lerp working atm.
        //fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
    }

    public void Death()
    {
        if (isDead)
            return;
        // Set the death flag so this function won't be called again.
        isDead = true;

        //set this to 0, so slider shows it
        currentHealth = 0;

        /* 
         * Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        playerAudio.clip = deathClip;
        playerAudio.Play ();   
        */

        // Turn off shooting script of the airplane 
        weaponManager.enabled = false;

        if (lives > 0)
        {
            lives--;
        }
        else
        {
            Debug.Log("GAME OVER, YOU LOST!");
        }
    }

    private void RpcRespawn()
    {
        // if (isLocalPlayer)
        //{
        // Disable physic effects so plane will respawn correctly
        GetComponent<Rigidbody>().isKinematic = true;

        // Set the player’s position to the chosen spawn point and reset rotation
        if (player1)
        {
            transform.rotation = spawnPoint1.transform.rotation;
            transform.position = spawnPoint1.transform.position;

        }
        else
        {
            transform.rotation = spawnPoint2.transform.rotation;
            transform.position = spawnPoint2.transform.position;
        }


        //Reset movement
        planeController.movementSpeed = 0;
        planeController.enginePower = 0;
        planeController.SetDirections();

        //Enable physics and weapons
        GetComponent<Rigidbody>().isKinematic = false;
        weaponManager.enabled = true;
        weaponManager.bullets = weaponManager.maxBullets;
        weaponManager.reloading = false;

        //Reset health and fuel when respawning
        currentHealth = startingHealth;
        GetComponent<FuelBar>().Respawn();
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDead)
            return;

        //If colliding with other player or ground
        if (collision.gameObject.tag == "Player") 
        {

            var enemyHealth = collision.gameObject.GetComponent<PlaneHealth>();
            if (enemyHealth != null)
            {
                //Destroy enemy
                enemyHealth.Death();

                //Destroy yourself
                Death();
            }
        }

        if(collision.gameObject.tag == "Ground")
        {
            Suicide(40);
            Death();
        }
    }

    //Suicide removes some score
    void Suicide(int subScore)
    {
        if (player1)
            scoreManager.SubScoreP1(subScore);

        else if (player2)
            scoreManager.SubScoreP2(subScore);
    }

    //Parameters are the player that dropped the bomb. Player one or Player two
    public void BombKill(bool one, bool two)
    {
        //Bomb dropped by player 1
        if (one)
        {
            //Bomb hits player 2 -> add score to player 1
            if (player2)
                scoreManager.AddScoreP1(100);

            //Bomb hits player 1 -> subtract score from player 1
            else if (player1)
                Suicide(20);
        }
       
        //Bomb dropped by player 2
        else if (two)
        {
            if (player1)
                scoreManager.AddScoreP2(100);

            else if (player2)
                Suicide(20);
        }
    }

}