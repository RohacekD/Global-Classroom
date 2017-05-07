// By Samuli Jylhä
// 6.4.2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    bool player1;
    bool player2;

    public GameObject bulletPrefab;
    public GameObject bombPrefab;
    public GameObject bombPosition;
    public GameObject gunPosition;

    public int bombAmount;
    public int maxBombs = 10;
    public int bullets;
    public int maxBullets = 40;
    public bool reloading;
    public float reloadTime = 4.0f;
    private float reloadCounter;

    //Delay between bullets when fire button is pressed down
    public float fireDelta = 0.06f;
    //Variable that is added every frame and compared to fireDelta
    private float shootingDelay = 0.0f;
    //Adding spray to the bullets
    public float maxDivergence = 5.0f;

    void Start()
    {
        player1 = GetComponent<Player>().player1;
        player2 = GetComponent<Player>().player2;

        bullets = maxBullets;
        reloadCounter = reloadTime;
        bombAmount = maxBombs;
    }

    void Update()
    {

        shootingDelay += Time.deltaTime;

        if (Input.GetButton("Fire1") && shootingDelay > fireDelta)
        {
            Shoot();
            shootingDelay = 0.0f;
        }


        if (Input.GetButtonDown("Fire2"))
        {
            DropBomb();
        }

        if (bullets <= 0)
        {
            reloading = true;
            reloadCounter -= Time.deltaTime;

            if (reloadCounter <= 0)
            {
                bullets = maxBullets;
                reloadCounter = reloadTime;
                reloading = false;
            }
        }
    }

    private void Shoot()
    {
        if (reloading)
        {
            return;
        }
        //
        //http://answers.unity3d.com/questions/36801/spraying-bullets.html
        // start with a perfect shot
        Vector3 divergence = Vector3.zero;
        // then we want to randomize the rotation around the X axis
        divergence.x = (1 - 2 * Random.value) * maxDivergence;
        /* Random.value only gives you a positive float between 0 and 1
         * (1 - 2 * Random.value) makes that between -1 and 1
         */
        // instantiate the bullet
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, gunPosition.transform.position, gunPosition.transform.rotation);
        bullet.transform.Rotate(divergence);

        bullets--;

        Destroy(bullet, 6.0f);

    }

    private void DropBomb()
    {
        if (bombAmount > 0)
        {
            bombAmount--;
            GameObject bomb = (GameObject)Instantiate(bombPrefab, bombPosition.transform.position, bombPosition.transform.rotation);
            bomb.GetComponent<Bomb>().movementSpeed = GetComponentInParent<PlaneController>().movementSpeed;
            bomb.GetComponent<Bomb>().Shooter(player1,player2);

            Destroy(bomb, 15.0f);
        }
    }

    public void AddBombs()
    {
        if (bombAmount < maxBombs)
        {
            bombAmount++;
        }
    }
}
