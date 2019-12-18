using System.Collections;
using UnityEngine;

/*
 * Class for Control the Stone Objects launched in the MainLoop of the game
 */
public class Stone : MonoBehaviour {

    public GameObject explosion;
    private AudioSource audio;

    private const float yDie = -50.0F;
    void Start() {
        audio = GetComponent<AudioSource>();
    }

    void Update() {
        if (transform.position.y < yDie) {
            Destroy(gameObject);
        }
    }

    void OnMouseDown() {
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 2.0f);
        GameManager.stonesDestroyed++;
        StartCoroutine(DestroyObject());

    }

    IEnumerator DestroyObject() {
        audio.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        yield return new WaitForSeconds(1.5F);
        
        Destroy(gameObject);
    }
}
