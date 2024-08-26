﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows;
using UnityEngine.UI;

class questionList {
    public string question { get; set; }
    public string answer { get; set; }
    public List<char> example { get; set; }
}

public class GameManager : MonoBehaviour
{
    public GameObject letterObject;
    private Camera mainCamera;
    public TextMeshProUGUI questionText;
    public GameObject camera;
    public GameObject answerCube1;
    public GameObject answerCube2;
    public GameObject answerCube3;
    public GameObject answerCube4;
    public GameObject answerCube5;
    public GameObject answerCube6;
    public GameObject answerCubeCurrentPosition;
    public TextMeshProUGUI answerLetter1;
    public TextMeshProUGUI answerLetter2;
    public TextMeshProUGUI answerLetter3;
    public TextMeshProUGUI answerLetter4;
    public TextMeshProUGUI answerLetter5;
    public TextMeshProUGUI answerLetter6;
    public TextMeshProUGUI hintItemText;
    public TextMeshProUGUI showScore;
    public Image hintItemImage;
    private string answerCheck = "00000";
    private int score = 10000;
    private float scoreTimer = 0f;
    private float answerTimer = 0f;
    private string question;
    private string answer;

    List<questionList> problemList = new List<questionList> {
        new questionList {question = "갑자기 창문을 벌컥 .", answer = "열어젖히다", example = { '제', '제', '혔', '혔', '쳤', '쳤'}},
        new questionList {question = "참의 반댓말은 이다.", answer = "거짓", example = { '참', '구', '잣', '가', '젓', '구' }},
        new questionList {question = "내 이름은 이다.", answer = "새로운친구", example = { '오', '래', '된', '오', '래' }}
    };

    private void Start() {
        TextMeshProUGUI[] answerLetters = new TextMeshProUGUI[] { answerLetter1, answerLetter2, answerLetter3, answerLetter4, answerLetter5, answerLetter6 };
        int problemIndex = Random.Range(0, problemList.Count);
        question = problemList[problemIndex].question;
        answer = problemList[problemIndex].answer;
        for (int i = 0; i < answer.Length; i++) {
            answerLetters[i].text = answer[i].ToString();
        }
        questionText.text = question;

        mainCamera = Camera.main;
        StartCoroutine(SpawnObjectCoroutine());
    }

    private IEnumerator SpawnObjectCoroutine() {
        while (true) {
            float spawnTime = Random.Range(0.8f, 1.5f);
            yield return new WaitForSeconds(spawnTime);
            Vector3 cameraPosition = mainCamera.transform.position;
            float xMin = cameraPosition.x - (mainCamera.orthographicSize * mainCamera.aspect);
            float xMax = cameraPosition.x + (mainCamera.orthographicSize * mainCamera.aspect);
            float zMin = cameraPosition.z - mainCamera.orthographicSize;
            float zMax = cameraPosition.z + mainCamera.orthographicSize;
            float randomX = Random.Range(xMin, xMax);
            float randomZ = Random.Range(zMin, zMax);
            Vector3 spawnPosition = new Vector3(randomX, -10, randomZ);
            GameObject letter_hp = Instantiate(letterObject, spawnPosition, Quaternion.identity);
            letter_hp.GetComponent<Letter>().hp = Random.Range(1, 5);
        }
    }

    private void Update() {
        GameObject[] answerCubes = new GameObject[] { answerCube1, answerCube2, answerCube3, answerCube4, answerCube5, answerCube6 };
        TextMeshProUGUI[] answerLetters = new TextMeshProUGUI[] { answerLetter1, answerLetter2, answerLetter3, answerLetter4, answerLetter5, answerLetter6 };
        for (int i = 0; i < answerCheck.Length; i++) {
            bool isActiveCube = answerCheck[i] == '0';
            bool isActiveLetter = answerCheck[i] == '1';
            answerCubes[i].SetActive(isActiveCube);
            answerLetters[i].gameObject.SetActive(isActiveLetter);
        }

        int answerCurrentPosition = answerCheck.IndexOf('0');
        if (answerCurrentPosition == -1)
            answerCubeCurrentPosition.SetActive(false);
        answerCubeCurrentPosition.transform.position = answerCubes[answerCurrentPosition].transform.position;

        scoreTimer += Time.deltaTime;
        if (scoreTimer >= 0.008f) {
            score -= 1;
            scoreTimer = 0f;
        }
        if (score <= 0)
            score = 0;
        showScore.text = "점수:" + score;

        answerTimer += Time.deltaTime;
        if (answerTimer < 20f) {
            float fillAmount = Mathf.Clamp01(answerTimer / 20f);
            hintItemImage.fillAmount = fillAmount;
        } else {
            hintItemImage.color = Color.white;
            hintItemText.color = Color.white;
        }

    }

    public void InputHint() {
        if (answerTimer >= 20f) {
            answerTimer = 0;
            score -= 500;
            hintItemImage.color = new Color32(151, 151, 151, 255);
            hintItemText.color = new Color32(136, 126, 0, 255);
            ShowOneAnswer();
        }
    }

    private void ShowOneAnswer() {
        List<int> zeroIndices = new List<int>();
        for (int i = 0; i < answerCheck.Length; i++) {
            if (answerCheck[i] == '0') {
                zeroIndices.Add(i);
            }
        }

        if (zeroIndices.Count > 0) {
            int randomIndex = zeroIndices[Random.Range(0, zeroIndices.Count)];
            char[] charArray = answerCheck.ToCharArray();
            charArray[randomIndex] = '1';
            answerCheck = new string(charArray);
        }
    }

    public void InputLetter(string letter) {
        int index = answer.IndexOf(letter);
        if (index == -1) {
            Penalty();
            return;
        }
        if (answerCheck[index] == '1') {
            Penalty();
            return;
        }
        for (int i = 0; i < index; i++) {
            if (answerCheck[i] == '0') {
                Penalty();
                return;
            }
        }
        char[] chars = answerCheck.ToCharArray();
        chars[index] = '1';
        answerCheck = new string(chars);
    }

    public void Penalty() {
        score -= 500;
        StartCoroutine(ShakeScreen());
        StartCoroutine(ChangeScoreColor());
    }

    private IEnumerator ChangeScoreColor() {
        Color originalColor = showScore.color;
        for (int i = 0; i < 3; i++) {
            showScore.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            showScore.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator ShakeScreen() {
        Vector3 originalPosition = camera.transform.localPosition;
        float elapsed = 0f;
        while (elapsed < 0.5f) {
            float x = Random.Range(-0.3f, 0.3f);
            float y = Random.Range(-0.3f, 0.3f);
            float z = Random.Range(-0.3f, 0.3f);
            camera.transform.localPosition = originalPosition + new Vector3(x, y, z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        camera.transform.localPosition = originalPosition;
    }
}
