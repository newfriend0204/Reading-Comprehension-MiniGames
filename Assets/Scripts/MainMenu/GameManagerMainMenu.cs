using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private GameObject[] menus;
    public GameObject letterObject;

    public void Awake() { 
        Application.targetFrameRate = 60;
        mainCamera.transform.position = new Vector3(-70.70401f, 5.37792f, -7.097689f);
        mainCamera.transform.rotation = Quaternion.Euler(6.395f, -285.846f, 0.013f);
        StartCoroutine(SpawnObjectCoroutine());
    }

    private IEnumerator SpawnObjectCoroutine() {
        while (true) {
            float spawnTime = Random.Range(0.2f, 0.7f);
            yield return new WaitForSeconds(spawnTime);
            Vector3 cameraPosition = mainCamera.transform.position;
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
    }

    public void StartGame1() {
        SceneManager.LoadScene("Game1");
    }

    public void StartGame2() {
        SceneManager.LoadScene("Game2");
    }

    public void ShowExpalnGame1() {
        game1Explain.gameObject.SetActive(true);
    }

    public void HideExpalnGame1() {
        game1Explain.gameObject.SetActive(false);
    }
}
