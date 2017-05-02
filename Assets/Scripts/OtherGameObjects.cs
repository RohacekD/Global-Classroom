using UnityEngine;
using System.Collections;

public class OtherGameObjects : MonoBehaviour
{
	public GameObject player;
	Vector3 currentPosition;
	public bool isPlayerOnBase = false;
	public int bombAmount;
	public int bullets;
	public int maxBullets = 20;

	// Update is called once per frame

	void Update () {
		currentPosition = transform.position;

		//if player is in BaseArea
		if ((currentPosition.y > 0 || currentPosition.y < 300) & (currentPosition.z < 400)) { 
			isPlayerOnBase = true;
		}

		if (isPlayerOnBase) {
			Reload ();
		}
	}

	// Reload bullets and bombs
	void Reload () {
		bullets = maxBullets;
		bombAmount = 10;
	}

	// Make the health(hp) full
	void FullHealth () {
	}

	// Make the engine full
	void FullEngine ()
    {


	}

}

