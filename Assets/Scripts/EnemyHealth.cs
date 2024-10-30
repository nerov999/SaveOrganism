using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; 
    public int currentHealth;  

    private void Start()
    {
        currentHealth = maxHealth; 
    }

    // ����� ��� ��������� �����
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; 

        if (currentHealth <= 0)
        {
            Die(); 
        }
    }

    // ����� ��� ������ �����
    private void Die()
    {

        Destroy(gameObject); 
    }
}
