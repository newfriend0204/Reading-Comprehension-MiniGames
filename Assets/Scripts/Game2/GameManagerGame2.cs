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
        new questionListGame2 {question = "일정한 기준에 따라 전체를 몇 개로 갈라 나누는 것", example1 = "구분", example2 = "구별", answer = 1},
        new questionListGame2 {question = "성질이나 종류에 따라 차이가 나거나 갈라놓는 것", example1 = "구분", example2 = "구별", answer = 2},
        new questionListGame2 {question = "평면이나 넓은 물체의 가로로 건너지른 거리", example1 = "너비", example2 = "넓이", answer = 1},
        new questionListGame2 {question = "일정한 평면에 걸쳐 있는 공간이나 범위의 크기", example1 = "너비", example2 = "넓이", answer = 2},
        new questionListGame2 {question = "기술, 경제, 책, 제품, 국토, 인력 등 물질적인 것을 발전시킴", example1 = "개발", example2 = "계발", answer = 1},
        new questionListGame2 {question = "슬기나 재능, 사상 따위를 일깨워 줌", example1 = "개발", example2 = "계발", answer = 2},
        new questionListGame2 {question = "사물을 형체 그대로 그리거나 원본을 베끼어 쓰는 것", example1 = "모사", example2 = "묘사", answer = 1},
        new questionListGame2 {question = "어떤 대상이나 사물, 현상 따위를 언어로 서술하거나 그림을 그려서 표현하는 것", example1 = "모사", example2 = "묘사", answer = 2},
        new questionListGame2 {question = "글이나 그림 따위를 신문이나 잡지 따위에 싣는 것", example1 = "게재", example2 = "기재", answer = 1},
        new questionListGame2 {question = "문서 따위에 기록하여 올리는 것", example1 = "게재", example2 = "기재", answer = 2}
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
        scoreText.text = "점수:" + score.ToString();
        
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