using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowAway : MonoBehaviour
{   
    [SerializeField] private LayerMask playerLayer;
    public float maxDistance = 5f; // Khoảng cách tối đa để tính toán lực đẩy
    public float maxLaunchForce = 5f;
    
    void Update()
    {
        CheckBlowing();
    }

    private void CheckBlowing() {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2 (2, 15), 0f, Vector2.up, .1f, playerLayer);
        if (hit.collider != null) {
            GameObject player = hit.collider.gameObject;
            float distance = Vector2.Distance(transform.position, player.transform.position);
            float launchPercentage = Mathf.Clamp01(distance / maxDistance);
            float launchForce = maxLaunchForce * launchPercentage;
            hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * launchForce, ForceMode2D.Impulse);
            hit.collider.gameObject.GetComponent<Animator>().SetInteger("state", 3);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawCube(transform.position, new Vector2(2, 15));
    }
}
