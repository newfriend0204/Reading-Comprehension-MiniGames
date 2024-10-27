using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerMainMenu : MonoBehaviour
{
    private int nowMenu = 0; //0:메인 화면, 1:게임1 화면, 2:게임2 화면
    public Camera mainCamera;
    public GameObject mainMenu;
    public GameObject game1Menu;
    public GameObject game2Menu;
    public GameObject game1Explain;
    public GameObject game2Explain;
    public GameObject vehicle;
    public GameObject road1;
    public GameObject road2;
    public GameObject explosionParticle;
    public GameObject titleText1;
    public GameObject titleText2;
    private GameObject[] menus;
    public GameObject letterObject;
    public Image fadeBackground;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private FileManager fileManager;

    public void Awake() {
        Time.timeScale = 1;
        Application.targetFrameRate = 60;
        mainCamera.transform.position = new Vector3(-70.70401f, 5.37792f, -7.097689f);
        mainCamera.transform.rotation = Quaternion.Euler(6.395f, -285.846f, 0.013f);
        StartCoroutine(SpawnObjectCoroutine());
    }

    private void Start() {
        StartCoroutine(FadeOut());
        vehicle.transform.position = new Vector3(70.42f, 0.8699951f, 28.56f);
        road1.transform.position = new Vector3(64.55737f, 0.8699951f, 36.693f);
        road2.transform.position = new Vector3(27.8f, 0.8699951f, 87.7f);
        explosionParticle.transform.position = new Vector3(68.55f, 0.8699961f, 31.05f);
        GameObject title1 = Instantiate(titleText1, new Vector3(-34.94f, 5.1f, -52.27f), Quaternion.Euler(5.433f, -63.495f, -0.001f));
        GameObject title2 = Instantiate(titleText2, new Vector3(71.962f, 9.729f, 12.894f), Quaternion.Euler(29.497f, -54.144f, 0));
        fileManager = new FileManager();
        score = fileManager.LoadData(0);
    }

    private IEnumerator SpawnObjectCoroutine() {
        while (true) {
            float spawnTime = Random.Range(0.2f, 0.7f);
            yield return new WaitForSeconds(spawnTime);
            float xMin = -34.35f;
            float xMax = -27.06f;
            float yMin = 1.36f;
            float yMax = 5.188773f;
            float randomX = Random.Range(xMin, xMax);
            float randomY = Random.Range(yMin, yMax);
            Vector3 spawnPosition = new Vector3(randomX, randomY, -46.94826f);
            GameObject letter_hp = Instantiate(letterObject, spawnPosition, Quaternion.Euler(-90, 0, 0));
            letter_hp.GetComponent<FakeLetter>().hp = Random.Range(1, 5);
        }
    }

    public void MoveToGame1() {
        nowMenu = 1;
        StartCoroutine(MoveCameraToGame1());
    }

    private IEnumerator MoveCameraToGame1() {
        float elapsedTime = 0f;
        float duration = 0.4f;
        Vector3 startingPosition = mainCamera.transform.position;
        Quaternion startingRotation = mainCamera.transform.rotation;
        while (elapsedTime < duration) {
            mainCamera.transform.position = Vector3.Lerp(startingPosition, new Vector3(-22.99117f, 3.115955f, -66.24719f), (elapsedTime / duration));
            mainCamera.transform.rotation = Quaternion.Slerp(startingRotation, Quaternion.Euler(-2.68f, -389.117f, -0.005f), (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = new Vector3(-22.99117f, 3.115955f, -66.24719f);
        mainCamera.transform.rotation = Quaternion.Euler(-2.68f, -389.117f, -0.005f);
    }

    public void MoveToGame2() {
        nowMenu = 2;
        StartCoroutine(MoveCameraToGame2());
    }

    private IEnumerator MoveCameraToGame2() {
        float elapsedTime = 0f;
        float duration = 0.4f;
        Vector3 startingPosition = mainCamera.transform.position;
        Quaternion startingRotation = mainCamera.transform.rotation;
        while (elapsedTime < duration) {
            mainCamera.transform.position = Vector3.Lerp(startingPosition, new Vector3(74.18389f, 9.732926f, 9.602714f), (elapsedTime / duration));
            mainCamera.transform.rotation = Quaternion.Slerp(startingRotation, Quaternion.Euler(13.271f, -383.892f, 0.001f), (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = new Vector3(74.18389f, 9.732926f, 9.602714f);
        mainCamera.transform.rotation = Quaternion.Euler(13.271f, -383.892f, 0.001f);
    }

    public void MoveToMainMenu() {
        nowMenu = 0;
        StartCoroutine(MoveCameraToMainMenu());
    }

    private IEnumerator MoveCameraToMainMenu() {
        float elapsedTime = 0f;
        float duration = 0.4f;
        Vector3 startingPosition = mainCamera.transform.position;
        Quaternion startingRotation = mainCamera.transform.rotation;
        while (elapsedTime < duration) {
            mainCamera.transform.position = Vector3.Lerp(startingPosition, new Vector3(-70.70401f, 5.37792f, -7.097689f), (elapsedTime / duration));
            mainCamera.transform.rotation = Quaternion.Slerp(startingRotation, Quaternion.Euler(6.395f, -285.846f, 0.013f), (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = new Vector3(-70.70401f, 5.37792f, -7.097689f);
        mainCamera.transform.rotation = Quaternion.Euler(6.395f, -285.846f, 0.013f);
    }

    private void Update() {
        menus = new GameObject[] {mainMenu, game1Menu, game2Menu};
        for (int i = 0; i < menus.Length; i++) {
            menus[i].SetActive(i == nowMenu);
        }

        scoreText.text = "점수: " + score.ToString();
    }

    public void StartGame1() {
        StartCoroutine(FadeIn(1));
    }

    public void StartGame2() {
        StartCoroutine(FadeIn(2));
    }

    public void ShowExpalnGame1() {
        game1Explain.gameObject.SetActive(true);
    }

    public void HideExpalnGame1() {
        game1Explain.gameObject.SetActive(false);
    }

    public void ShowExpalnGame2() {
        game2Explain.gameObject.SetActive(true);
    }

    public void HideExpalnGame2() {
        game2Explain.gameObject.SetActive(false);
    }

    private IEnumerator FadeOut() {
        Color color = fadeBackground.color;
        color.a = 1;
        fadeBackground.color = color;
        float elapsedTime = 0f;

        while (elapsedTime < 0.7f) {
            color.a = Mathf.Lerp(1, 0, elapsedTime / 0.7f);
            fadeBackground.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 0;
        fadeBackground.color = color;
        fadeBackground.gameObject.SetActive(false);
    }

    private IEnumerator FadeIn(int check) {
        fadeBackground.gameObject.SetActive(true);
        Color color = fadeBackground.color;
        color.a = 0;
        fadeBackground.color = color;
        float elapsedTime = 0f;

        while (elapsedTime < 0.7f) {
            color.a = Mathf.Lerp(0, 1, elapsedTime / 0.7f);
            fadeBackground.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 1;
        fadeBackground.color = color;
        if (check == 1)
            SceneManager.LoadScene("Game1");
        else if (check == 2)
            SceneManager.LoadScene("Game2");
    }
}
