using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int points = 0;
    [SerializeField] private Text pointsText;
    [SerializeField] private AudioSource collectingSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            collectingSoundEffect.Play();
            Destroy(collision.gameObject);
            points++;
            pointsText.text = "Points: " + points;
        }
    }
}
