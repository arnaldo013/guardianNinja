using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    private PlayerController player;
    // Start is called before the first frame update


    private void Start()
    {
         
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    public void TakeDamage (int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
        } 
    }
  
}
