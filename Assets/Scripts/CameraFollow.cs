// By Samuli Jylhä
// 6.4.2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;

    void Update() {
        if (player != null)
        {
            Vector3 camPos = new Vector3(transform.position.x, player.transform.position.y + 20, player.transform.position.z); //+something on Y, if we add rotation to camera
            transform.position = camPos;
        }
	}
}
