using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 2.0f; 
    private bool canDealDamage = true;
    public int damageAmount = 10;

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canDealDamage)
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(damageAmount); 
                canDealDamage = false;
                StartCoroutine(ResetDamageCooldown()); 
            }
        }

        Destroy(gameObject);
    }

    private IEnumerator ResetDamageCooldown()
    {
        yield return new WaitForSeconds(1.0f); 
        canDealDamage = true;
    }

}