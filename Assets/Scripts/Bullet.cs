// By Samuli Jylhä
// 6.4.2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float movementSpeed = 200.0f;

    private int damage;
    public int minDamage = 20;
    public int maxDamage = 30;

    // Use this for initialization
    void Start()
    {
        damage = Random.Range(minDamage, maxDamage);
    }

    // Update is called once per frame
    void Update()
    {
        //Get current position
        Vector3 currentPos = transform.position;
        currentPos += transform.forward * movementSpeed * Time.deltaTime;
        transform.position = currentPos;
    }


    private void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject;
        var health = other.GetComponent<PlaneHealth>();
        Debug.Log("bullet hits: " + hit);

        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
    
