//
//By Samuli Jylhä
//11.04.2017
//
//TODO:
// - Networking
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {

    public float enginePower = 0;
    public float maxEnginePower = 100.0f;
    public float addEnginePower = 9.0f; //How much plane should accelerate per second.
    public float movementSpeed = 0.0f; //Current speed of the plane. Will be applied to the plane when moving.

    private float angle; //Angle compared to the ground (world axis).

    //Variables for the direction the plane is facing
    private bool left = false;
    private bool right = true;

    //Variables that are used when plane flies too slowly, and it will drop towards the ground.
    public float dropSpeed = 5; //The speed that the plane will start dropping at
    private float dropGravityCalculation = 0; //This uses gravity
    private bool dropping = false;

    private bool playerIsInBase;    //Used to check if player is in the base
    private Vector3 aboveGround;    //Used to keep player slightly above the groung when inside base

    void Update () {
        //Get current position
        Vector3 currentPos = transform.position;

        KeyboardInput();

        if (enginePower > maxEnginePower)
        {
            enginePower = maxEnginePower;
        }
        if (enginePower < 0)
        {
            enginePower = 0;
        }

        //Returns gravity value ( between -9,8 and 9,8 )
        float customGravity = CalculateGravity();

        //Disable gravity inside base
        if (!playerIsInBase)
        {
            //Add gravity to current speed.
            movementSpeed += customGravity;
        }

        if (movementSpeed < dropSpeed && !playerIsInBase)
        {
            dropping = true;
        }

        //Plane is flying
        if (!dropping)
        {   
            //Calculate new position when flying.
            currentPos += transform.forward * movementSpeed * Time.deltaTime;
            //Set dropping gravity back to 0, so next time dropping won't start with unrealistic gravity.
            dropGravityCalculation = 0;
        }
        // Plane drops down
        else
        {
            if (playerIsInBase)
            {
                dropping = false;
                movementSpeed = 0;
                return;
            }
            //
            //TODO: Should add dropping speed to Z-axis, can't figure out how to get the right direction
            //
            //Add gravity on world axis
            dropGravityCalculation += 9.81f * Time.deltaTime;
            currentPos += new Vector3(0, -1 * dropGravityCalculation, 0) * Time.deltaTime;
            movementSpeed = dropGravityCalculation;
            
            //Disable dropping if enough speed and nose pointing towards ground
            if (movementSpeed > 20 && angle > 80)
            {
                enginePower = movementSpeed;
                dropping = false;
            }
        }

        //Keep player slighty above the ground when in base
        if (playerIsInBase && currentPos.y < aboveGround.y)
        {
            currentPos.y = aboveGround.y;
        }

        transform.position = currentPos;
    }

    //Add this to speed every second
    //Returns:
    //  0 when flying parallel to the groud
    //  9,8 when flying towards the ground
    //  -9,8 when flying towards sky (90deg compared to ground)
    private float CalculateGravity()
    {
        //Angle works.
        angle = 90 - Vector3.Angle(-Vector3.up, transform.forward);

        //Avoid dividing by zero later
        if (angle == 0)
            return 0;

        float gravity = 0.0f;
        gravity += angle / 9.1837f * Time.deltaTime;
        return gravity;
    }

    //Read Keyboard
    private void KeyboardInput()
    {
        if (GetComponent<PlaneHealth>().isDead)
        {
            return;
        }
        //Works, but an animation would make visually better
        //Spin the plane around
        if (Input.GetKeyDown("space"))
        {
            transform.Rotate(0, 0, 180);

            right = !right;
            left = !left;
        }

        //Up and down rotating.
        if (Input.GetKey("up"))
        {
            transform.Rotate(-100f * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("down"))
        {
            transform.Rotate(100f * Time.deltaTime, 0, 0);
        }

        //Can't add or decrease speed while dropping
        if (dropping)
        {
            return;
        }
        //Left and right. Accelerate and slow down.
        if (Input.GetKey("left"))
        {
            if (left)
            {
                if (enginePower < maxEnginePower)
                {
                    enginePower += addEnginePower * Time.deltaTime;
                    movementSpeed += addEnginePower * Time.deltaTime;
                }

            }
            else if (right)
            {
                if (movementSpeed > 0)
                {
                    enginePower -= addEnginePower * 1.5f * Time.deltaTime;
                    movementSpeed -= addEnginePower * 1.5f * Time.deltaTime;
                }
            }
        }

        if (Input.GetKey("right"))
        {
            if (right)
            {
                if (enginePower < maxEnginePower)
                {
                    enginePower += addEnginePower * Time.deltaTime;
                    movementSpeed += addEnginePower * Time.deltaTime;
                }
            }
            else if (left)
            {
                if (movementSpeed > 0)
                {
                    enginePower -= addEnginePower * 1.5f * Time.deltaTime;
                    movementSpeed -= addEnginePower * 1.5f * Time.deltaTime;
                }
            }
        }
    }

    //Called when player enters or exits base trigger
    public void SetBaseData(bool inBase, Transform baseTriggerSize)
    {
        playerIsInBase = inBase;
        if (baseTriggerSize != null)
        {
            //Get the size of base trigger, divide it by 2 and decrease it from the size -> you get the bottom lever of the collider
            //+2.5 so player will stay lighty above groung
            //-> player's center +2.5 looks like player is on ground
            Vector3 position = baseTriggerSize.position;
            Vector3 size = baseTriggerSize.localScale;
            position.y -= size.y / 2;
            position.y += 2.5f;
            aboveGround = position;
        }
    }
}
