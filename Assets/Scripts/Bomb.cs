// By Samuli Jylhä
// 6.4.2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float movementSpeed;

    //the player that drops the bomb
    bool player1;
    bool player2;

    void Update () {

        Vector3 currentPos = transform.position;
        currentPos += transform.forward * movementSpeed * Time.deltaTime;
        transform.position = currentPos;
    }

    //Set the player that drops the bomb
    public void Shooter(bool one, bool two)
    {
        player1 = one;
        player2 = two;
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
            health.BombKill(player1, player2);
            health.Death();
        }

        if (hit.tag != "Base")
        {
            Destroy(gameObject);
        }
    }
}
