using System.Collections;
using UnityEngine;

public class Game2Background : MonoBehaviour {
    public GameObject road1;
    public GameObject road2;
    public ParticleSystem explosionParticle;
    public GameObject[] obstacles;
    public float speed;
    private Vector3 road1StartPosition;
    private Vector3 road2StartPosition;
    private float angle = 54.206f;
    private Vector3 direction;

    void Start() {
        road1StartPosition = road1.transform.position;
        road2StartPosition = road2.transform.position;
        float radians = angle * Mathf.Deg2Rad;
        direction = new Vector3(Mathf.Cos(radians), 0, -Mathf.Sin(radians));
        StartCoroutine(ExplosionParticle());
    }

    private IEnumerator ExplosionParticle() {
        while (true) {
            yield return new WaitForSeconds(3.5f);
            explosionParticle.Play();
        }
    }

    void Update() {
        MoveRoad(road1);
        MoveRoad(road2);
    }

    private void MoveRoad(GameObject road) {
        foreach (GameObject obstacle in obstacles) {
            obstacle.transform.position += direction * speed * Time.deltaTime;
            if (obstacle.transform.position.z <= -14.1f && obstacle.transform.position.x >= 101.2f)
                obstacle.transform.position = new Vector3(27.8f, 0.8699951f, 87.7f);
        }
        road.transform.position += direction * speed * Time.deltaTime;
        if (road.transform.position.z <= -14.1f && road.transform.position.x >= 101.2f) {
            road.transform.position = new Vector3(27.8f, 0.8699951f, 87.7f);
        }
    }
}
