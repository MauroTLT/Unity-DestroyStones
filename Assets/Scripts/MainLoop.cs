using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*
 * Class for Control the functionality of the game,
 * launching the differents Objects and control of the PauseMenu and the GameOver Screen
 */
public class MainLoop: MonoBehaviour {
	
	public GameObject[] stones = new GameObject[4];
    public GameObject gameOverWindow, pauseWindow;
    public float scale = 1;
	public float torque = 5.0f;
	public float minAntiGravity = 20.0f, maxAntiGravity = 50.0f;
	public float minLateralForce = -15.0f, maxLateralForce = 15.0f;
	public float minTimeBetweenStones = 1f, maxTimeBetweenStones = 5f;
	public float minX = -15.0f, maxX = 15.0f;
	public float minZ = -5.0f, maxZ = 40.0f;
	
	private bool enableStones = true;
	private Rigidbody rigidbody;
	
	void Start () {
		StartCoroutine(ThrowStones());
	}

    void Update() {
        enableStones = (GameManager.penguinDestroyed != 3) && (GameManager.stonesThrown != GameManager.stonesTotal);
    }

    IEnumerator ThrowStones()
	{
		// Initial delay
		yield return new WaitForSeconds(2.0f);
		
		while(enableStones) {

            int index = Random.Range(0, stones.Length);

            GameObject stone = Instantiate(stones[index]);
            stone.transform.localScale = new Vector3(
                Random.Range(0.5F, scale) * stone.transform.localScale.x,
                Random.Range(0.5F, scale) * stone.transform.localScale.y,
                Random.Range(0.5F, scale) * stone.transform.localScale.z
            );

            if (index != 3) {
                GameManager.stonesThrown++;
            }

			stone.transform.position = new Vector3(Random.Range(minX, maxX), -30.0f, Random.Range(minZ, maxZ));
			stone.transform.rotation = Random.rotation;

			rigidbody = stone.GetComponent<Rigidbody>();
			
			rigidbody.AddTorque(Vector3.up * torque, ForceMode.Impulse);
			rigidbody.AddTorque(Vector3.right * torque, ForceMode.Impulse);
			rigidbody.AddTorque(Vector3.forward * torque, ForceMode.Impulse);
            
			rigidbody.AddForce(Vector3.up * Random.Range(minAntiGravity, maxAntiGravity), ForceMode.Impulse);
			rigidbody.AddForce(Vector3.right * Random.Range(minLateralForce, maxLateralForce), ForceMode.Impulse);

			yield return new WaitForSeconds(Random.Range(minTimeBetweenStones, maxTimeBetweenStones));
		}
        yield return new WaitForSeconds(5F);
        
        if (GameManager.stonesThrown == GameManager.stonesTotal) {
            SceneManager.LoadScene("Final");
        } else {
            DestroyFinalObjects();
            ShowGameOverWindow();
        }
	}

    private void DestroyFinalObjects() {
        GameObject[] stones = GameObject.FindGameObjectsWithTag("StoneObject");
        foreach (GameObject obj in stones) {
            Destroy(obj);
        }
    }

    private void StonesVisible(bool visible) {
        GameObject[] stones = GameObject.FindGameObjectsWithTag("StoneObject");
        foreach (GameObject obj in stones) {
            if (obj.GetComponent<MeshRenderer>() != null) {
                obj.GetComponent<MeshRenderer>().enabled = visible;
                obj.GetComponent<SphereCollider>().enabled = visible;
            } else {
                obj.GetComponentInChildren<BoxCollider>().enabled = visible;
                MeshRenderer[] penguin = obj.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer mr in penguin) {
                    mr.enabled = visible;
                }
            }
        }
    }

    public void ResumeGame() {
        StonesVisible(true);
        Time.timeScale = (GameManager.difficulty == 1) ? 1.5F : (GameManager.difficulty == 2) ? 2.5F : 3.5F;
        pauseWindow.SetActive(false);
    }
    
    public void ShowPauseWindow() {
        StonesVisible(false);
        Time.timeScale = 0F;
        pauseWindow.SetActive(true);
    }

    private void ShowGameOverWindow() {
        gameOverWindow.SetActive(true);
    }
}

