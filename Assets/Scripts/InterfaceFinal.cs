using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Class for Control the Final Scene
 */
public class InterfaceFinal : MonoBehaviour {

    public Text textScore;
    public Text textTotal;
    public Text textMessage;
    void Start() {
        float value = (GameManager.stonesDestroyed * 100 / GameManager.stonesTotal);
        textScore.text = value + "%";
        textTotal.text = textTotal.text.Replace("X", ("" + GameManager.stonesTotal));
        textMessage.text = ChooseMessage(value);

        StartCoroutine(RandomColor());
    }

    public void ChangeScene(string scene) { SceneManager.LoadScene(scene); }

    private string ChooseMessage(float value) {
        return (value == 100F) ? "Perfect Game!" :
               (value > 80F) ? "Great job there!" :
               (value > 60F) ? "That was a good game" :
               (value > 40F) ? "Not bad at all" :
               (value > 20F) ? "Better luck next time" :
               (value == 0F) ? "Nothing to say..." : "Unexpected";
    }

    IEnumerator RandomColor() {
        Color[] colors = new Color[] { Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta };
        int i = 0;
        while (true) {
            textScore.color = colors[i++];
            if (i == colors.Length) { i = 0; }
            yield return new WaitForSeconds(0.5F);
        }
    }
}
