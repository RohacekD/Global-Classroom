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

    public int startingHealth = 100;                  // The amount of health each plane starts with.
    private int currentHealth;                         // How much health the plane currently has.

    public Slider healthSlider;                       // The slider to represent how much health the plane currently has.
    public Image fillImage;                           // The image component of the slider.
    public Color fullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color zeroHealthColor = Color.red;         // The color the health bar will be when on no health.

    PlaneController planeController;                  // Reference to the player's movement.
    WeaponManager weaponManager;                      // Reference to the Plane Shooting script.

    public GameObject ExplosionPrefab;                // prefab that is instanstiated when the plane dies
    private AudioSource ExplosionAudio;               // Audio for plane explosion
    private ParticleSystem ExplosionParticles;        // The particle system tfor plane explosion
    public GameObject plane;

    public bool isDead;                               // Whether the player is dead.
    bool damaged;                                     // True when the player gets damaged.

    //public AudioClip deathClip;                       // The audio clip to play when the player dies.
    //AudioSource playerAudio;                          // Reference to the AudioSource component.

    public float respawnTimer;                        // Used to calculate remaining time to respawn
    public float respawnDelay = 5;                    // Set the time for respawn

    GameObject spawnPoint1;
    GameObject spawnPoint2;

    public int lives = 5;                             //How many lives player has

    private void Start()
    {
        //Spawnpoint for player 1 and 2
        spawnPoint1 = GameObject.Find("Spawnpoint1");
        spawnPoint2 = GameObject.Find("Spawnpoint2");
        plane = GameObject.Find("Player 1");

        // Setting up the references.
        // playerAudio = GetComponent<AudioSource>();
        planeController = GetComponent<PlaneController>();
        weaponManager = GetComponentInChildren<WeaponManager>();

        // Instantiate the explosion prefab and get a reference to the particle system on it.
        ExplosionParticles = Instantiate(ExplosionPrefab).GetComponent<ParticleSystem>();
        // Get a reference to the audio source on the instantiated prefab.
        ExplosionAudio = ExplosionParticles.GetComponent<AudioSource>();
        // Disable the prefab so it can be activated when it's required.
        ExplosionParticles.gameObject.SetActive(false);

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

            //Add score to other player. Atm has only score system for player 1. p2 score needed later
            GetComponent<Player>().P1AddScore(50);
        }

    }

    //Add health to the plane. Repairing speed calculated in Base.cs
    public void Repair()
    {
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

        //plane explosion
        // Move the instantiated explosion prefab to the plane's position and turn it on.
        ExplosionParticles.transform.position = plane.transform.position;
        ExplosionParticles.gameObject.SetActive(true);
        // Play the particle system ofor the plane explosion
        ExplosionParticles.Play();
        // Play the audio.
        ExplosionAudio.Play();

        
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
        transform.rotation = spawnPoint1.transform.rotation;
        transform.position = spawnPoint1.transform.position;

        //reset explosion to false again
        ExplosionParticles.gameObject.SetActive(false);

        //Reset movement
        planeController.movementSpeed = 0;
        planeController.enginePower = 0;
        
        //Enable physics and weapons
        GetComponent<Rigidbody>().isKinematic = false;
        weaponManager.enabled = true;

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
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground")
        {
            //Destroy enemy
            var enemyHealth = collision.gameObject.GetComponent<PlaneHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.Death();
            }

            //Suicide removes some score
            GetComponent<Player>().P1SubScore(50);

            //Destroy yourself
            Death();
        }
    }

}