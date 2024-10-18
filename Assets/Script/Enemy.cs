using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject explosionPrefab; 
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (Random.value > 0.5f)
        {
            animator.SetTrigger("DeathPose");
        }
        else
        {
            animator.SetTrigger("DeathLay");
        }

        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosion.transform.localScale = transform.localScale * 0.1f;

        Destroy(explosion, 3f); 
    }
}

