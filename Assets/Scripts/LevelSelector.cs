using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void OpenScence() {
        string level = transform.gameObject.GetComponentInChildren<Text>().text.Trim();   
        SceneManager.LoadScene("Level " + level);
    }
}
