using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour {
    public GameManagerGame2 gameManager;
    public ParticleSystem explosionParticle;
    public ParticleSystem getCoinParticle;
    public AudioClip explosionSound;
    public AudioClip getCoinSound;
    public TextMeshProUGUI resultText;

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

    private void OnTriggerEnter(Collider other) {
        other.gameObject.SetActive(false);
        if (other.gameObject.CompareTag("Phase1Select1Trigger")) {
            gameManager.nowPhase = 2;
            if (gameManager.phase1Answer == 1)
                StartCoroutine(Correct());
            if (gameManager.phase1Answer == 2)
                StartCoroutine(Wrong());
        }
        if (other.gameObject.CompareTag("Phase1Select2Trigger")) {
            gameManager.nowPhase = 2;
            if (gameManager.phase1Answer == 2)
                StartCoroutine(Correct());
            if (gameManager.phase1Answer == 1)
                StartCoroutine(Wrong());
        }
        if (other.gameObject.CompareTag("Phase2Select1Trigger")) {
            gameManager.nowPhase = 3;
            if (gameManager.phase1Answer == 1)
                StartCoroutine(Correct());
            if (gameManager.phase1Answer == 2)
                StartCoroutine(Wrong());
        }
        if (other.gameObject.CompareTag("Phase2Select2Trigger")) {
            gameManager.nowPhase = 3;
            if (gameManager.phase1Answer == 2)
                StartCoroutine(Correct());
            if (gameManager.phase1Answer == 1)
                StartCoroutine(Wrong());
        }
    }

    private IEnumerator DestroyAfterTime(GameObject obstacle, float time) {
        yield return new WaitForSeconds(time);
        Destroy(obstacle);
    }

    private IEnumerator Correct() {
        Debug.Log("이거는 정답이야");
        resultText.text = "정답!";
        for (int i = 0; i < 3; i++) {
            resultText.alpha = 0f;
            yield return new WaitForSeconds(0.1f);
            resultText.alpha = 1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator Wrong() {
        Debug.Log("이거는 오답이야");
        resultText.text = "오답";
        for (int i = 0; i < 3; i++) {
            resultText.alpha = 0f;
            yield return new WaitForSeconds(0.1f);
            resultText.alpha = 1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
