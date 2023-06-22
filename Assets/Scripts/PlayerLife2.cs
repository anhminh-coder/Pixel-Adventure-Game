using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife2 : MonoBehaviour
{
    Vector2 checkpointPos;
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource dealthSoundEffect;
    private void Start()
    {
        checkpointPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Die();
        }
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
        Invoke("ReloadLevel", 2f);
    }
    
    private void ReloadLevel()
    {
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
