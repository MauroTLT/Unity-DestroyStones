using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

/*
 * Class for Control the Stone Scene
 * Controls the music and the UI, also set the variables of the game
 */
public class InterfaceStone : MonoBehaviour {

    public AudioClip[] music = new AudioClip[3];
    public AudioSource audio;
    public Slider progressBar;
    public Text score;
    
    private static GameObject[] lives;

    void Start() {
        audio.clip = music[GameManager.difficulty-1];
        audio.volume = 0.5F;
        audio.Play();

        lives = GameObject.FindGameObjectsWithTag("Live");
        GameManager.stonesThrown = 0;
        GameManager.stonesDestroyed = 0;
        GameManager.penguinDestroyed = 0;
        SetSettings();
    }

    private void SetSettings() {
        progressBar.maxValue = GameManager.stonesTotal;
        Time.timeScale = (GameManager.difficulty == 1) ? 1.5F : (GameManager.difficulty == 2) ? 2.5F : 3.5F;

        if (!GameManager.obstacles) {
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (GameObject obs in obstacles) {
                Destroy(obs);
            }
        }

        if (GameManager.quality) {
            GameObject[] badPerformances = GameObject.FindGameObjectsWithTag("BadPerformance");
            foreach (GameObject obj in badPerformances) {
                Destroy(obj);
            }
        }
    }

    public void ChangeScene(string scene) {
        Time.timeScale = 2.5F;
        SceneManager.LoadScene(scene);
    }

    void FixedUpdate() {
        progressBar.value = GameManager.stonesDestroyed;
        score.text = GameManager.stonesDestroyed +  " of " + GameManager.stonesTotal;
    }

    public static void PenguinDestroyed() {
        lives[GameManager.penguinDestroyed++].GetComponentsInChildren<Image>()[1].GetComponent<Image>().enabled = true;
    }
}
