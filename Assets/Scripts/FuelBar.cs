using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{

    public float m_StartingFuel = 120f;               // The amount of fuel each plane starts with.
    public Slider m_Slider;                             // The slider to represent how much fuel the plane currently has.
    public Image m_FillImage;                           // The image component of the slider.  
    //public for testing. change to private later
    public float m_CurrentFuel;                      // How much fuel the plane currently has.
    public Color m_FuelBarColor = Color.blue;           // The color for the fuel bar
    private bool m_Dead;                                // Has the plane been reduced beyond zero health yet?

    


    void Start()
    {
        // When the plane is enabled, reset the plane's fuel and whether or not it's dead.
        m_CurrentFuel = m_StartingFuel;
        m_Dead = gameObject.GetComponent<PlaneHealth>().isDead;

        // Update the fuel slider's value and color.
        SetFuelUI();
    }

    public void Update()
    {
        // Reduce current fuel by the amount of damage done.
        m_CurrentFuel -= (Time.deltaTime);

        // Change the UI elements appropriately.
      //COMMENTED FOR TESTING  SetFuelUI();

        // If the current fuel is at or below zero and it has not yet been registered, call Death() in PlaneHealth.cs
        if (m_CurrentFuel <= 0 && !m_Dead)
        {
            var health = GetComponentInParent<PlaneHealth>();
            if (health != null)
            {
                health.Death();
            }
        }
        else
        {
            // Change the UI elements appropriately.
            SetFuelUI();
        }

    }


    private void SetFuelUI()
    {
        //////////////////////////COMMENTED FOR TESTING
        /*
        // Set the slider's value appropriately.
        m_Slider.value = m_CurrentFuel;

        // Fill the bar with assigned color
        m_FillImage.color = m_FuelBarColor;
        */
    }

    //Add fuel to the plane. Refill speed calculated in Base.cs
    public void AddFuel()
    {
        if (m_CurrentFuel < m_StartingFuel)
        {
            m_CurrentFuel++;
        }
    }

    /* This fucntion probably not needed. Using Death() function is PlaneHealth.cs instead
    private void OnDeath()
    {
        // Set the flag so that this function is only called once.
        m_Dead = true;

        gameObject.SetActive(false);
    }
    */

}
