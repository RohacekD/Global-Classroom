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
        if (hit.tag == "Bomb" ||
            hit.tag == "Base") return;

        
        var health = other.GetComponent<PlaneHealth>();

        if (health != null)
        {
            health.Death();
        }
        
        Debug.Log("BOMB HIT: " + hit);
        Destroy(gameObject);
    }
    
}
