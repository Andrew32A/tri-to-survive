using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void NextLevel() {
        // load next level
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void QuitGame() {
        Application.Quit();
    }

    IEnumerator LoadLevel(int levelIndex) {
        // play transition animation
        transition.SetTrigger("Transition");

        // wait for transition to finish
        yield return new WaitForSeconds(transitionTime);

        // load level
        SceneManager.LoadScene(levelIndex);
    }
}