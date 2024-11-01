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
    public AudioClip fallSound;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI problemText;
    public Mesh[] visualMeshes;
    public Mesh[] colliderMeshes;
    private FileManager fileManager;

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
            collision.gameObject.tag = "Untagged";
            getCoinParticle.transform.position = collision.gameObject.transform.position;
            getCoinParticle.Play();
            gameManager.score += 750;
            Destroy(collision.gameObject);
            GetComponent<AudioSource>().PlayOneShot(getCoinSound, 1f);
        } else if (collision.gameObject.CompareTag("Arch")) {
            gameManager.Penalty();
            transform.position = new Vector3(0, -150.3f, transform.position.z - 40);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GetComponent<AudioSource>().PlayOneShot(fallSound, 1f);
        } else if (collision.gameObject.CompareTag("End")) {
            gameManager.StageClear();
        }
    }

        private void OnTriggerEnter(Collider other) {
        other.gameObject.SetActive(false);
        if (other.gameObject.CompareTag("Phase1Select1Trigger") && gameManager.nowPhase == 1) {
            gameManager.nowPhase = 2;
            if (gameManager.phase1Answer == 1)
                StartCoroutine(Correct());
            if (gameManager.phase1Answer == 2)
                StartCoroutine(Wrong());
        }
        if (other.gameObject.CompareTag("Phase1Select2Trigger") && gameManager.nowPhase == 1) {
            gameManager.nowPhase = 2;
            if (gameManager.phase1Answer == 2)
                StartCoroutine(Correct());
            if (gameManager.phase1Answer == 1)
                StartCoroutine(Wrong());
        }
        if (other.gameObject.CompareTag("Phase2Select1Trigger") && gameManager.nowPhase == 2) {
            gameManager.nowPhase = 3;
            if (gameManager.phase2Answer == 1)
                StartCoroutine(Correct());
            if (gameManager.phase2Answer == 2)
                StartCoroutine(Wrong());
        }
        if (other.gameObject.CompareTag("Phase2Select2Trigger") && gameManager.nowPhase == 2) {
            gameManager.nowPhase = 3;
            if (gameManager.phase2Answer == 2)
                StartCoroutine(Correct());
            if (gameManager.phase2Answer == 1)
                StartCoroutine(Wrong());
        }
    }

    private IEnumerator DestroyAfterTime(GameObject obstacle, float time) {
        yield return new WaitForSeconds(time);
        Destroy(obstacle);
    }

    private IEnumerator Correct() {
        resultText.text = "정답!";
        problemText.text = "다음 문제 출제 중...";
        if (gameManager.nowPhase == 3)
            problemText.text = "완주하십시오.";
        gameManager.score += 1000;
        GetComponent<AudioSource>().PlayOneShot(getCoinSound, 1f);
        for (int i = 0; i < 3; i++) {
            resultText.alpha = 0f;
            yield return new WaitForSeconds(0.25f);
            resultText.alpha = 1f;
            yield return new WaitForSeconds(0.25f);
        }
        resultText.alpha = 0f;
        StartCoroutine(NextProblem());
    }

    private IEnumerator Wrong() {
        resultText.text = "오답";
        problemText.text = "다음 문제 출제 중...";
        if (gameManager.nowPhase == 3)
            problemText.text = "완주하십시오.";
        gameManager.score -= 500;
        GetComponent<AudioSource>().PlayOneShot(fallSound, 1f);
        for (int i = 0; i < 3; i++) {
            resultText.alpha = 0f;
            yield return new WaitForSeconds(0.25f);
            resultText.alpha = 1f;
            yield return new WaitForSeconds(0.25f);
        }
        resultText.alpha = 0f;
        StartCoroutine(NextProblem());
    }

    private IEnumerator NextProblem() {
        resultText.text = "다음 문제";
        if (gameManager.nowPhase == 3) {
            resultText.text = "완주하십시오.";
            problemText.text = "완주하십시오.";
        }
            GetComponent<AudioSource>().PlayOneShot(getCoinSound, 1f);
        for (int i = 0; i < 3; i++) {
            resultText.alpha = 0f;
            yield return new WaitForSeconds(0.25f);
            resultText.alpha = 1f;
            yield return new WaitForSeconds(0.25f);
        }
        resultText.alpha = 0f;
        if (gameManager.nowPhase == 2)
            problemText.text = gameManager.problemList[gameManager.randomIndex2].question;
    }

    private void Start() {
        fileManager = new FileManager();
        int index = 0;
        for (int i = 15; i < 30; i++) {
            if (fileManager.LoadData(i) == 2)
                index = i - 15;
        }
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshFilter.mesh = visualMeshes[index];
        meshCollider.sharedMesh = colliderMeshes[index];
    }
}
