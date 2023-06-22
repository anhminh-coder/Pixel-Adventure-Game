using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    Vector2 checkpointPos;
    Vector2 enemyPos;
    private Animator anim;
    public GameObject enemy;
    public Animator enemyAnim;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource dealthSoundEffect;
    private void Start()
    {
        if (enemy != null) {
            enemyPos = enemy.transform.position;
        }
        checkpointPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Die();
        }
        else if (collision.gameObject.CompareTag("Enemy")) {
            enemyAnim.SetBool("idle", false);
            enemyAnim.SetBool("hit", true);
            Die();
            Invoke("ResetEnemy", 0.5f);
        }
    }

    private void ResetEnemy() {
        enemyAnim.SetBool("hit", false);
        enemyAnim.SetBool("idle", true);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            checkpointPos = collision.gameObject.transform.position;
        }
    }

    private void Die() {
        dealthSoundEffect.Play();
        anim.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Static;
        // Invoke("ReloadLevel", 2f);
    }
    
    private void ReloadLevel()
    {
        if (enemy != null) {
            enemy.transform.position = enemyPos ;
        }
        transform.position = checkpointPos;
        rb.bodyType = RigidbodyType2D.Dynamic;
        anim.SetTrigger("revival");
        Invoke("ResetAnimator", 2f);
       // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ResetAnimator()
    {
        anim.SetInteger("state", 0);
    }
}
