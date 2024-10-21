using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class questionListGame2 {
    public string question { get; set; }
    public string example1 { get; set; }
    public string example2 { get; set; }
    public int answer { get; set; }
}

public class GameManagerGame2 : MonoBehaviour {
    public GameObject vehicle;
    public new GameObject camera;
    public GameObject[] obstaclePrefabs;
    public GameObject coinPrefab;
    public TextMeshProUGUI scoreText;
    public Joystick moveobject;
    public AudioClip fallSound;
    private Rigidbody vehicleRigidbody;
    public TextMeshPro phase1select1Text;
    public TextMeshPro phase1select2Text;
    public TextMeshPro phase2select1Text;
    public TextMeshPro phase2select2Text;
    public TextMeshProUGUI problemText;
    public int randomIndex1;
    public int randomIndex2;
    public int nowPhase = 1;
    public int phase1Answer = 0;
    public int phase2Answer = 0;
    private float currentSpeed = 0f;
    public int numberOfObstacles;
    public int numberOfCoins;
    public int score = 5000;
    private float scoreTimer = 0f;

    void Awake() {
        Application.targetFrameRate = 60;
        vehicleRigidbody = vehicle.GetComponent<Rigidbody>();
    }

    public List<questionListGame2> problemList = new List<questionListGame2> {
        new questionListGame2 {question = "������ ���ؿ� ���� ��ü�� �� ���� ���� ������ ��", example1 = "����", example2 = "����", answer = 1},
        new questionListGame2 {question = "�����̳� ������ ���� ���̰� ���ų� ������� ��", example1 = "����", example2 = "����", answer = 2},
        new questionListGame2 {question = "����̳� ���� ��ü�� ���η� �ǳ����� �Ÿ�", example1 = "�ʺ�", example2 = "����", answer = 1},
        new questionListGame2 {question = "������ ��鿡 ���� �ִ� �����̳� ������ ũ��", example1 = "�ʺ�", example2 = "����", answer = 2},
        new questionListGame2 {question = "���, ����, å, ��ǰ, ����, �η� �� �������� ���� ������Ŵ", example1 = "����", example2 = "���", answer = 1},
        new questionListGame2 {question = "���⳪ ���, ��� ������ �ϱ��� ��", example1 = "����", example2 = "���", answer = 2},
        new questionListGame2 {question = "�繰�� ��ü �״�� �׸��ų� ������ ������ ���� ��", example1 = "���", example2 = "����", answer = 1},
        new questionListGame2 {question = "� ����̳� �繰, ���� ������ ���� �����ϰų� �׸��� �׷��� ǥ���ϴ� ��", example1 = "���", example2 = "����", answer = 2},
        new questionListGame2 {question = "���̳� �׸� ������ �Ź��̳� ���� ������ �ƴ� ��", example1 = "����", example2 = "����", answer = 1},
        new questionListGame2 {question = "���� ������ ����Ͽ� �ø��� ��", example1 = "����", example2 = "����", answer = 2}
    };

    private void Start() {
        randomIndex1 = Random.Range(0, problemList.Count);
        randomIndex2 = Random.Range(0, problemList.Count);
        phase1Answer = problemList[randomIndex1].answer;
        phase2Answer = problemList[randomIndex2].answer;
        phase1select1Text.text = problemList[randomIndex1].example1;
        phase1select2Text.text = problemList[randomIndex1].example2;
        phase2select1Text.text = problemList[randomIndex2].example1;
        phase2select2Text.text = problemList[randomIndex2].example2;

        for (int i = 0; i < numberOfObstacles; i++) {
            float randomX = Random.Range(-8.5f, 8.5f);
            float randomZ = Random.Range(-593f, 114f);
            int randomIndex = Random.Range(0, obstaclePrefabs.Length);
            GameObject obstacle = obstaclePrefabs[randomIndex];
            Instantiate(obstacle, new Vector3(randomX, -150, randomZ), Quaternion.identity);
        }
        for (int i = 0; i < numberOfCoins; i++) {
            float randomX = Random.Range(-8.5f, 8.5f);
            float randomZ = Random.Range(-593f, 114f);
            GameObject coin = Instantiate(coinPrefab, new Vector3(randomX, -149.5f, randomZ), Quaternion.identity);
            coin.AddComponent<CoinRotation>();
        }
    }

    void Update() {
        camera.transform.position = vehicle.transform.position + new Vector3(0, 4.528f, -19);

        if (vehicle.transform.position.z < -619)
            vehicle.transform.position = new Vector3(vehicle.transform.position.x, vehicle.transform.position.y, -607);
        if (vehicle.transform.position.y > -152.11f && vehicle.transform.position.y < -148.11f) {
            if (currentSpeed < 5)
                currentSpeed = 5;
            float horizontal = moveobject.Horizontal;
            float vertical = moveobject.Vertical;
            if (vertical > 0)
                currentSpeed += Time.deltaTime * 30;
            else
                currentSpeed -= Time.deltaTime * 50;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, 35);
            vehicleRigidbody.MovePosition(vehicle.transform.position + vehicle.transform.forward * currentSpeed * Time.deltaTime);
            if (horizontal != 0 && currentSpeed != 0) {
                Quaternion turnRotation = Quaternion.Euler(0f, horizontal * 35 * Time.deltaTime, 0f);
                vehicleRigidbody.MoveRotation(vehicleRigidbody.rotation * turnRotation);
            }
        } else {
            Penalty();
            GetComponent<AudioSource>().PlayOneShot(fallSound, 1f);
            vehicle.transform.position = new Vector3(0, -150.3f, vehicle.transform.position.z - 40);
            vehicle.transform.rotation = Quaternion.Euler(0, 0, 0);
            currentSpeed = 5;
        }

        scoreTimer += Time.deltaTime;
        if (scoreTimer >= 0.003f) {
            score -= 1;
            scoreTimer = 0f;
        }
        if (score <= 0)
            score = 0;
        scoreText.text = "����:" + score.ToString();
        
        if (nowPhase == 1) {
            problemText.text = problemList[randomIndex1].question;
        }
        else if (nowPhase == 2) {
            problemText.text = problemList[randomIndex2].question;
        }
    }

    public void Penalty() {
        score -= 500;
        StartCoroutine(ChangeScoreColor());
    }

    private IEnumerator ChangeScoreColor() {
        Color originalColor = Color.black;
        for (int i = 0; i < 3; i++) {
            scoreText.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            scoreText.color = Color.black;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public class CoinRotation : MonoBehaviour {
        void Update() {
            transform.Rotate(Vector3.up, 100 * Time.deltaTime);
        }
    }
}