using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    private Animator anim;
    private float bounce = 20f;
    private float collisionExitDelay = 0.2f;
    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.CompareTag("Player")) {
            anim.SetBool("isJumping", true);
            coll.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }   
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ExitDelayCoroutine());
        }
    }

    private System.Collections.IEnumerator ExitDelayCoroutine()
    {
        yield return new WaitForSeconds(collisionExitDelay);

        anim.SetBool("isJumping", false);
    }
}
