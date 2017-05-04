//By Samuli Jylhä and Hyowon Woo
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
    public bool playerIsInBase;

    //Calculate short delay between adding bombs
    public float addBombDelay = 0.5f;
    private float addBomdTimer;

    //Calculate how fast fuel is added
    public float addFuelDelay = 0.02f;
    private float addFuelTimer;

    //Calculate how fast fuel is added
    public float addHealthDelay = 0.05f;
    private float addHealthTimer;

    //Player that enters in base
    private GameObject player;

	void Start () {
        addBomdTimer = addBombDelay;
        addFuelTimer = addFuelDelay;
        addHealthTimer = addHealthDelay;
	}
	
    //Calculate speed for adding bombs, fuel, and health
    //Call adding fucntions in other scripts
	void Update () {
        if (playerIsInBase)
        {
            addBomdTimer -= Time.deltaTime;
            if(addBomdTimer <= 0)
            {
                player.GetComponent<WeaponManager>().AddBombs();
                addBomdTimer = addBombDelay;
            }

            addFuelTimer -= Time.deltaTime;
            if(addFuelTimer <= 0)
            {
                player.GetComponent<FuelBar>().AddFuel();
                addFuelTimer = addFuelDelay;
            }

            addHealthTimer -= Time.deltaTime;
            if(addHealthTimer <= 0)
            {
                player.GetComponent<PlaneHealth>().Repair();
                addHealthTimer = addHealthDelay;
            }
        }
	}

    //Base is empty gameobject with collider that is set to "Is Trigger".
    //Entering and exiting this collider set player inside or outside the base
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject + " ENTER");
        if (other.tag == "Player")
        {
            player = other.gameObject;
            playerIsInBase = true;

            //Used for landing the plane in PlaneController.cs
            player.GetComponent<PlaneController>().SetBaseData(playerIsInBase, gameObject.GetComponent<Collider>().transform);
        }  
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject + " EXIT");
        if (other.tag == "Player")
        {
            playerIsInBase = false;
            player.GetComponent<PlaneController>().SetBaseData(playerIsInBase, null);
        }
    }


}
