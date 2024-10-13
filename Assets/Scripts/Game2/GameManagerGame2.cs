using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManagerGame2 : MonoBehaviour {
    public GameObject vehicle;
    public new GameObject camera;
    public GameObject[] obstaclePrefabs;
    public GameObject coinPrefab;
    public TextMeshProUGUI scoreText;
    public Joystick moveobject;
    private Rigidbody vehicleRigidbody;
    private float currentSpeed = 0f;
    public int numberOfObstacles;
    public int numberOfCoins;
    public int score = 5000;
    private float scoreTimer = 0f;

    void Awake() {
        Application.targetFrameRate = 60;
        vehicleRigidbody = vehicle.GetComponent<Rigidbody>();
    }

    private void Start() {
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
            float horizontal = moveobject.Horizontal;
            float vertical = moveobject.Vertical;
            if (vertical > 0)
                currentSpeed += Time.deltaTime * 30;
            else
                currentSpeed -= Time.deltaTime * 50;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, 40);
            vehicleRigidbody.MovePosition(vehicle.transform.position + vehicle.transform.forward * currentSpeed * Time.deltaTime);
            if (horizontal != 0 && currentSpeed != 0) {
                Quaternion turnRotation = Quaternion.Euler(0f, horizontal * 40 * Time.deltaTime, 0f);
                vehicleRigidbody.MoveRotation(vehicleRigidbody.rotation * turnRotation);
            }
        } else {
            Penalty();
            vehicle.transform.position = new Vector3(0, -150.3f, vehicle.transform.position.z - 40);
            vehicle.transform.rotation = Quaternion.Euler(0, 0, 0);
            currentSpeed = 0;
        }

        scoreTimer += Time.deltaTime;
        if (scoreTimer >= 0.003f) {
            score -= 1;
            scoreTimer = 0f;
        }
        if (score <= 0)
            score = 0;
        scoreText.text = "Á¡¼ö:" + score.ToString();
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