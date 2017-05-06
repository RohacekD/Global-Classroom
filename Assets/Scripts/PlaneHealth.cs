/* 
 * 23.04.17
 BY MICHEAL AND CARELE 
  TO DO: 
  - Resparwing function (Platform missing, reference to the Refillpointstation Object )
  - Refill fuel while being on platform 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlaneHealth : MonoBehaviour {

    public float startingHealth = 100f;               // The amount of health each plane starts with.
    public Slider healthSlider;                       // The slider to represent how much health the plane currently has.
    public Image fillImage;                           // The image component of the slider.
    public Color fullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color zeroHealthColor = Color.red;         // The color the health bar will be when on no health.    
    private float currentHealth;                      // How much health the plane currently has.

    PlaneController planeController;                  // Reference to the player's movement.
    WeaponManager weaponManager;                      // Reference to the Plane Shooting script.


    bool isDead;                                      // Whether the player is dead.
    bool damaged;                                     // True when the player gets damaged.

    public AudioClip deathClip;                       // The audio clip to play when the player dies.
    AudioSource playerAudio;                          // Reference to the AudioSource component.



    private void OnEnable()
    { 
        // Update the health slider's value and color.

        SetHealthUI();
   
        // Setting up the references.
       // anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        planeController = GetComponent<PlaneController>();
        weaponManager = GetComponentInChildren<WeaponManager>();

        // Set the initial health of the player.

        currentHealth = startingHealth;
        isDead = false;
    }

    public void Update() //TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        //currentHealth -= amount;

        currentHealth -= (Time.deltaTime*5);//test code remove

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;

        SetHealthUI();//test code remove

        // Play the hurt sound effect.
        //playerAudio.Play ();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }

    }

    private void SetHealthUI()
    {
        // Set the slider's value appropriately.
        healthSlider.value = currentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
    }

    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        /*Turn off any remaining shooting effects.
        weaponManager.DisableEffects();
        -> see weaponmanager 
        */

        /* 
         * Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        playerAudio.clip = deathClip;
        playerAudio.Play ();   
        */

        // Turn off the movement and shooting scripts of the airplane 

        planeController.enabled = false;
        weaponManager.enabled = false;
    }

}