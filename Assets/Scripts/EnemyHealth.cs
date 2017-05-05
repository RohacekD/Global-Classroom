using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;            // The amount of health the enemy has at the beginning of the game 
    public int currentHealth;                   // The current health of the enemy 
    public int scoreValue = 15;                 // The amount added to the player's score when the enemy dies. -> depends on enemie
    public float fallSpeed = 3.5f;                     // if the enemy gots hurt it falls on the ground with a certain amount of speed

    public AudioClip deathClip;                 // The sound to play when the enemy dies.
    AudioSource enemyAudio;                     // Reference to the audio source.

    ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
    CapsuleCollider capsuleCollider;            // Reference to the capsule collider.

    private bool isDead;                                // Whether the enemy is dead.
    private bool isFalling;                             // Whether the enemy got hurt it falls on the ground 

    void Awake()
    {
        // Setting up the references.
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    void Update()
    {
        // If the enemy gots damaged so that it falls 
        if (isFalling)
        {
            // ... move the enemy down by the fallSpeed per second.
            transform.Translate(-Vector3.up * fallSpeed * Time.deltaTime);
        }
    }

    /* TO DO 
     -Sinking process of the enemie 
     - inform about 
         */

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

        // Play the hurt sound effect.
        enemyAudio.Play();

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;

        // Set the position of the particle system to where the hit was sustained.
        hitParticles.transform.position = hitPoint;

        // And play the particles.
        hitParticles.Play();

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is dead.
            Death();
        }
    }


    void Death()
    {
        // The enemy is dead.
        isDead = true;

        // Turn the collider into a trigger so shots can pass through it.
        //capsuleCollider.isTrigger = true;

        // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }


    public void StartFalling()
    {
       

        // Find the rigidbody component and make it kinematic (since we use Translate to fall the enemy).
        GetComponent<Rigidbody>().isKinematic = true;

        // The enemy should fall to the ground 
        isFalling = true;

        // Increase the score of the player by the enemy's score value. (Reference by playerscore )
        //ScoreManager.score += scoreValue;

        // After 2 seconds destory the enemy object -> not more visible on screen 
        Destroy(gameObject, 2f);
    }
}
