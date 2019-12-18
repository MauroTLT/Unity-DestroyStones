using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Class for Control the Awake Scene
 * Controls all the UI Menus and the setting sub-menu
 */
public class InterfaceAwake : MonoBehaviour {

    public GameObject mainMenu, settingMenu, statisticsMenu;
    public Slider sliderStones;
    public Text sliderStonesText;
    public Toggle[] toggleDifficulty;
    public Toggle toggleObstacles, toggleQuality;

    public AudioSource audio;

    private void Start() {
        // Initialize the Settings menu at the values of the GameManager
        sliderStones.value = GameManager.stonesTotal;
        foreach (Toggle t in toggleDifficulty) { t.isOn = false; }
        toggleDifficulty[GameManager.difficulty - 1].isOn = true;
        if (!GameManager.obstacles) {
            toggleObstacles.isOn = false;
        }
        if (GameManager.quality) {
            toggleQuality.isOn = true;
        }
    }

    public void ChangeScene(string scene) {
        audio.Play();
        SceneManager.LoadScene(scene);
    }
    public void ExitGame() {
        audio.Play();
        Application.Quit();
    }

    public void ChangeSubMenu(string direction) {
        audio.Play();
        if (mainMenu.activeSelf && direction.Equals("Right")) {
            // Menu to Right (Settings)
            mainMenu.SetActive(false);
            StartCoroutine(RotateCamera(direction, settingMenu));
        } else if (mainMenu.activeSelf && direction.Equals("Left")) {
            // Menu to Left (Statistics)
            mainMenu.SetActive(false);
            StartCoroutine(RotateCamera(direction, statisticsMenu));
        } else if (!mainMenu.activeSelf && direction.Equals("Left")) {
            // Back to Menu (Settings)
            settingMenu.SetActive(false);
            StartCoroutine(RotateCamera(direction, mainMenu));
        } else if (!mainMenu.activeSelf && direction.Equals("Right")) {
            // Back to Menu (Statistics)
            statisticsMenu.SetActive(false);
            StartCoroutine(RotateCamera(direction, mainMenu));
        }
    }

    IEnumerator RotateCamera(string direction, GameObject activate) {
        Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        for (int i = 0; i < 30; i++) {
            camera.transform.Rotate(
                new Vector3(0F, (direction.Equals("Right") ? 3 : -3), 0F)
            );
            yield return new WaitForSeconds(0.05F);
        }
        activate.SetActive(true);
    }

    public void ChangeStonesValue() {
        GameManager.stonesTotal = int.Parse("" + sliderStones.value);
        sliderStonesText.text = "" + GameManager.stonesTotal;
    }

    /* SETTINGS METHODS */

    public void ChangeDifficulty(int difficulty) {
        if (toggleDifficulty[difficulty-1].isOn) {
            audio.Play();
            GameManager.difficulty = difficulty;
        }
    }

    public void EnableObstacles() {
        audio.Play();
        GameManager.obstacles = toggleObstacles.isOn;
        toggleObstacles.GetComponentInChildren<Text>().text = (toggleObstacles.isOn) ? "Enabled" : "Disabled";
    }

    public void SwitchPerformance() {
        audio.Play();
        GameManager.quality = toggleQuality.isOn;
        toggleQuality.GetComponentInChildren<Text>().text = (toggleQuality.isOn) ? "Enabled" : "Disabled";
    }

    public void ResetSettings() {
        audio.Play();
        // Reset Total Stones
        GameManager.stonesTotal = 20;
        sliderStones.value = 20;
        sliderStonesText.text = "" + GameManager.stonesTotal;
        // Reset Difficulty
        toggleDifficulty[1].isOn = true;
        // Reset Obstacles
        if (!toggleObstacles.isOn) {
            toggleObstacles.isOn = true;
        }
    }
}
