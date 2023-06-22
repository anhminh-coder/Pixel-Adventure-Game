using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text finishText;
    [SerializeField] private Image border;
    private AudioSource finishSound;
    public GameObject finishLevelMenu;
    private bool levelCompleted = false;
    public int nextSceneLoad;

    private void Start() {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        finishLevelMenu.SetActive(false);
        finishSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player") && !levelCompleted) {
            finishSound.Play();
            finishText.text = collision.gameObject.name + " win!";
            if (border != null) {
                border.enabled = false;
                Destroy(border.gameObject);
            }
            levelCompleted = true;
            Invoke("CompleteLevel", 2f);
        }
    }

    private void CompleteLevel() {
        Time.timeScale = 0f;
        finishLevelMenu.SetActive(true);
        if (nextSceneLoad > PlayerPrefs.GetInt("levelAt")) {
            PlayerPrefs.SetInt("levelAt", nextSceneLoad);
        }

    }
}
