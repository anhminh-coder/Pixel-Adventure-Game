using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // public bool check = true;
    // private void OnTriggerEnter2D(Collider2D collision) {
    //     if (check == true) 
    //     {
    //         Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + 28, Camera.main.transform.position.y, Camera.main.transform.position.z);
    //         check = false;
    //     }   
    //     else {
    //         Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - 28, Camera.main.transform.position.y, Camera.main.transform.position.z);
    //         check = true;
    //     }
    // }

    [SerializeField] GameObject player;
    private bool check = false;
    private void Update() {
        if (player.transform.position.x > transform.position.x && check == false) {
            check = true;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + 28, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        else if (player.transform.position.x < transform.position.x && check == true) {
            check = false;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - 28, Camera.main.transform.position.y, Camera.main.transform.position.z);
        } 
    }
}
