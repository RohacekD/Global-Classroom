// By Samuli Jylhä
// 6.4.2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float movementSpeed;
	
	void Update () {

        Vector3 currentPos = transform.position;
        currentPos += transform.forward * movementSpeed * Time.deltaTime;
        transform.position = currentPos;

    }

    
    private void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject;

        //two bombs can't collide
        if (hit.tag == "bomb") return;

        /*
        var health = other.GetComponent<Health>();

        if (health != null)
        {
            health.Kill();
        }
        */
        Debug.Log("HIT: " + hit);
        Destroy(gameObject);
    }
    
}
