using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMove : MonoBehaviour
{
    // Start is called before the first frame update
    private AIPath aipath;

    void Start()
    {
        aipath = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aipath.desiredVelocity.x >= 0.01f) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aipath.desiredVelocity.x <= -0.01f) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
