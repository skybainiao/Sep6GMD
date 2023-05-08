using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float knockbackForce = 5f;
    public float maxHealth = 100f;
    public float health;
    public int lives = 3;
    public Transform respawnPoint;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Respawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 10f;

            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    private void Respawn()
    {
        lives -= 1;
        if (lives <= 0)
        {
            SceneManager.LoadScene("Start");
        }
        else
        {
            transform.position = respawnPoint.position;
            health = maxHealth;
        }
    }
}
