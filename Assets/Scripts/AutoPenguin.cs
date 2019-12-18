using UnityEngine;

/*
 * Class for Control the Decorative Penguins
 * The Penguin is programmed to move forward and backwards forever
 */
public class AutoPenguin : MonoBehaviour
{
    public float speed = 10;

    private Vector3 direction;

    void Start() {
        direction = (name.Equals("Dekar"))? Vector3.back : Vector3.forward;
    }

    void Update() {
        transform.position += direction * Time.deltaTime * speed;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name.Equals("Forward") || collision.gameObject.name.Equals("Backward")) {
            transform.Rotate(0, 180, 0, Space.Self);
            direction = (direction == Vector3.forward) ? Vector3.back : Vector3.forward;
        }
    }
}
