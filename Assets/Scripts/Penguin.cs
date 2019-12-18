using System.Collections;
using UnityEngine;

/*
 * Class for Control the Penguin Objects launched in the MainLoop of the game
 */
public class Penguin : MonoBehaviour {

    public GameObject explosion;
    private AudioSource audio;

    private const float yDie = -30.0F;

    private void Start() {
        audio = GetComponent<AudioSource>();
    }

    void Update() {
        if (transform.position.y < yDie) {
            Destroy(gameObject);
        }
    }

    void OnMouseDown() {
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 1.5f);
        audio.Play();
        HidePenguin();
        StartCoroutine(Shake());
        // Actualizamos el contador de vidas
        InterfaceStone.PenguinDestroyed();
    }

    private void HidePenguin() {
        // Por cada Componente independiente del Objeto, lo ocultamos
        gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
        MeshRenderer[] penguin = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in penguin) {
            mr.enabled = false;
        }
    }

    IEnumerator Shake() {
        GameObject camera = GameObject.Find("Main Camera");
        Light light = GameObject.Find("Light").GetComponentInChildren<Light>();

        // Cambiamos el color de la camara a un tono rojizo
        light.color = Color.red;

        // Movemos de izquierda a derecha la camara X veces (3)
        for (int i = 0; i < 3; i++) {
            camera.transform.position = new Vector3(5, camera.transform.position.y, camera.transform.position.z);
            yield return new WaitForSeconds(0.15f);
            camera.transform.position = new Vector3(-5, camera.transform.position.y, camera.transform.position.z);
            yield return new WaitForSeconds(0.15f);
        }
        // Dejamos la camara en su posicion original
        camera.transform.position = new Vector3(0, camera.transform.position.y, camera.transform.position.z);

        // Cambiamos el color de las camaras al color original
        light.color = Color.white;

        // Esperamos un poco para que todo vuelva a la normalidad antes de destruir el objeto
        yield return new WaitForSeconds(1F);
        Destroy(gameObject);
    }
}
