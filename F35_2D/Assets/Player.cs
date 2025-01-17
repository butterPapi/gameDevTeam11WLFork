using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health of the enemy.
    public int enemyCollisionDamage = 3; // Damage taken when colliding with another enemy.
    public int enemyBulletDamage = 1; // Damage taken from enemy bullets.

    private int currentHealth; // Current health of the enemy.
    private Animator animator; // Reference to the Animator component.
    private AudioSource audioSource; // Reference to the AudioSource component for death sound.
    public AudioClip deathSound; // Assign the death sound in the Inspector.

    private void Start()
    {
        currentHealth = maxHealth; // Initialize the current health to the maximum health.
        animator = GetComponent<Animator>(); // Get the Animator component.
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component.

        // Set the death sound for the AudioSource component.
        audioSource.clip = deathSound;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Decrement the current health by enemyCollisionDamage.
            currentHealth -= enemyCollisionDamage;

            // Check if the current health has reached zero, and if so, trigger the death animation.
            if (currentHealth <= 0)
            {
                Death();
            }
        }
        else if (collision.gameObject.CompareTag("Enemy Weapon"))
        {
            // Decrement the current health by enemyBulletDamage.
            currentHealth -= enemyBulletDamage;

            // Check if the current health has reached zero, and if so, trigger the death animation.
            if (currentHealth <= 0)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        // Perform any additional actions or effects before triggering the death animation.
        // For example, you can play an explosion animation or sound.

        // Trigger the death animation if an Animator is attached.
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }


        // Play the death sound.
        if (audioSource != null && deathSound != null)
        {
            audioSource.Play();
        }

        // Delay the actual destruction to allow the death animation to finish playing.
        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Wait for some time to allow the death animation to finish playing.
        yield return new WaitForSeconds(1.0f); // Adjust the time as needed.

        // Destroy the enemy GameObject.
        Destroy(gameObject);
    }
}
