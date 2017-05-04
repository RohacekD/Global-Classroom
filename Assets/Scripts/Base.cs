//By Samuli Jylhä
//23.4.2017
//
//TODO:
//
//Figure out how to land a plane without getting transform fucked by rigidbody physics
//-> maybe keep the transform slightly above ground (transoform.position.y = asdasd);
//-> and/or remove collider from base and do something else
//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

    //Used to check if player is in the base
    private bool playerIsInBase;

    //Calculate short delay between adding bombs
    public float addBombDelay = 0.5f;
    private float addBomdTimer;

    //Player that enters in base
    private GameObject player;

	void Start () {
        addBomdTimer = addBombDelay;
	}
	
	void Update () {
        if (playerIsInBase)
        {
            addBomdTimer -= Time.deltaTime;
            if(addBomdTimer <= 0)
            {
                player.GetComponentInParent<WeaponManager>().AddBombs();
                addBomdTimer = addBombDelay;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject + " ENTER");
        if (other.tag == "Player")
        {
            player = other.gameObject;
            playerIsInBase = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject + " EXIT");
        if (other.tag == "Player")
        {
            playerIsInBase = false;
        }
    }

}
