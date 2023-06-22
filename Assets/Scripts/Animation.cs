using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
   [SerializeField] private float speed = 10f;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);

        if (Vector2.Distance(waypoints[0].transform.position, transform.position) < 0.1f) {
            anim.SetInteger("direction", 0);
            speed = 1f;
            Invoke("ResetSpeed", 0.5f);
            Invoke("ResetAnimation", 0.2f);
        }
        else if (Vector2.Distance(waypoints[1].transform.position, transform.position) < 0.1f) {
            anim.SetInteger("direction", 1);
            speed = 1f;
            Invoke("ResetSpeed", 0.5f);
            Invoke("ResetAnimation", 0.2f);
        }
    }

    private void ResetAnimation() {
        anim.SetInteger("direction", -1);
    }

    private void ResetSpeed() {
        speed = 10f;
    }
}
