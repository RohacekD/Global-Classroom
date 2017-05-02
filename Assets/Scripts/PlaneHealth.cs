using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneHealth : MonoBehaviour
{

    public float m_StartingHealth = 100f;              
    public Slider m_Slider;                            
    public Image m_FillImage;                          
    public Color m_FullHealthColor = Color.green;      
    public Color m_ZeroHealthColor = Color.red;         
    private float m_CurrentHealth;                      
    private bool m_Dead;                         




    private void OnEnable()
    {
        // When the plane is enabled, reset the plane's health and whether or not it's dead.
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        // Update the health slider's value and color.
        SetHealthUI();
    }

    public void Update() //TakeDamage(float amount)
    {
        // Reduce current health by the amount of damage done.
        //m_CurrentHealth -= amount;

        m_CurrentHealth -= (Time.deltaTime * 5);  //test code remove when damage system is implemented
   
        
        SetHealthUI(); // set the health to new value when plane is shot

        // If the current health is at or below zero and it has not yet been registered, call OnDeath.
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        // Set the slider's value appropriately.
        m_Slider.value = m_CurrentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }


    private void OnDeath()
    {
        // Set the flag so that this function is only called once.
        m_Dead = true;

        // have this function play an explosion audio and animation then remove plane object

        gameObject.SetActive(false); //game object will currently refer to HUD UI elements if the HUD components is not a child of sopwith component
        //must fix
    }

}
