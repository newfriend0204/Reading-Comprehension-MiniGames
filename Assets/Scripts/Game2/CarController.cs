using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {
    public GameManagerGame2 gameManager;
    public ParticleSystem explosionParticle;
    public ParticleSystem getCoinParticle;
    public AudioClip explosionSound;
    public AudioClip getCoinSound;

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Obstacle")) {
            collision.gameObject.tag = "Untagged";
            explosionParticle.transform.position = collision.gameObject.transform.position;
            explosionParticle.Play();
            Rigidbody obstacleRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (obstacleRigidbody != null) {
                float direction = Random.Range(0f, 1f) < 0.5f ? -1f : 1f;
                Vector3 force = new Vector3(direction * 5f, 5f, 0);
                obstacleRigidbody.AddForce(force, ForceMode.Impulse);
                StartCoroutine(DestroyAfterTime(collision.gameObject, 2f));
            }
            GetComponent<AudioSource>().PlayOneShot(explosionSound, 0.8f);
            gameManager.Penalty();
        } else if (collision.gameObject.CompareTag("Coin")) {
            getCoinParticle.transform.position = collision.gameObject.transform.position;
            getCoinParticle.Play();
            gameManager.score += 500;
            Destroy(collision.gameObject);
            GetComponent<AudioSource>().PlayOneShot(getCoinSound, 1f);
        }
    }

    private IEnumerator DestroyAfterTime(GameObject obstacle, float time) {
        yield return new WaitForSeconds(time);
        Destroy(obstacle);
    }
}
