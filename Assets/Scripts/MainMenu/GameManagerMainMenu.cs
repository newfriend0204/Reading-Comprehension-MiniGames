using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;

public class GameManagerMainMenu : MonoBehaviour
{
    private int nowMenu = 0; //0:���� ȭ��, 1:����1 ȭ��, 2:����2 ȭ�� 3:���� ȭ��
    private int nowShopMenu = 0; //0:����1 ���, 1:����1 ���� ����, 2:����2 ���, 3:����2 ��ۼ���
    public Camera mainCamera;
    public GameObject mainMenu;
    public GameObject game1Menu;
    public GameObject game2Menu;
    public GameObject game1Explain;
    public GameObject game2Explain;
    public GameObject shop;
    public GameObject shopGame1Background;
    public GameObject shopGame1LetterPiece;
    public GameObject shopGame2Background;
    public GameObject shopGame2Vehicle;
    public GameObject vehicle;
    public GameObject road1;
    public GameObject road2;
    public GameObject explosionParticle;
    public GameObject titleText1;
    public GameObject titleText2;
    public GameObject shopLeftButton;
    public GameObject shopRightButton;
    private GameObject[] menus;
    private GameObject[] shopMenus;
    public Button[] vehicles;
    public Button[] game1Backgrounds;
    public GameObject letterObject;
    public Image fadeBackground;
    public TextMeshProUGUI scoreText;
    public AudioClip purchaseSound;
    public AudioClip equipmentSound;
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
        vehicle.transform.position = new Vector3(73.2f, 0.8699951f, 24.62f);
        road1.transform.position = new Vector3(64.55737f, 0.8699951f, 36.693f);
        road2.transform.position = new Vector3(27.8f, 0.8699951f, 87.7f);
        explosionParticle.transform.position = new Vector3(68.55f, 0.8699961f, 31.05f);
        GameObject title1 = Instantiate(titleText1, new Vector3(-34.98f, 5.58f, -52.25f), Quaternion.Euler(5.433f, -63.495f, -0.001f));
        GameObject title2 = Instantiate(titleText2, new Vector3(72.291f, 9.667f, 17.421f), Quaternion.Euler(29.497f, -54.144f, 0));
        fileManager = new FileManager();

        vehicles[0].onClick.AddListener(() => VehiclePurchase(0, 0));
        vehicles[1].onClick.AddListener(() => VehiclePurchase(1, 7000));
        vehicles[2].onClick.AddListener(() => VehiclePurchase(2, 15000));
        vehicles[3].onClick.AddListener(() => VehiclePurchase(3, 5000));
        vehicles[4].onClick.AddListener(() => VehiclePurchase(4, 6000));

        game1Backgrounds[0].onClick.AddListener(() => Game1BackgroundPurchase(0, 0));
        game1Backgrounds[1].onClick.AddListener(() => Game1BackgroundPurchase(1, 4000));
        game1Backgrounds[2].onClick.AddListener(() => Game1BackgroundPurchase(2, 6000));
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
            mainCamera.transform.position = Vector3.Lerp(startingPosition, new Vector3(77.05f, 9.81f, 10.49f), (elapsedTime / duration));
            mainCamera.transform.rotation = Quaternion.Slerp(startingRotation, Quaternion.Euler(13.267f, -385.249f, -0.311f), (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = new Vector3(77.05f, 9.81f, 10.49f);
        mainCamera.transform.rotation = Quaternion.Euler(13.267f, -385.249f, -0.311f);
    }

    public void MoveToMainMenu() {
        nowShopMenu = 0;
        nowMenu = 0;
        StartCoroutine(MoveCameraToMainMenu());
    }

    private IEnumerator MoveCameraToShop() {
        float elapsedTime = 0f;
        float duration = 0.4f;
        Vector3 startingPosition = mainCamera.transform.position;
        Quaternion startingRotation = mainCamera.transform.rotation;
        while (elapsedTime < duration) {
            mainCamera.transform.position = Vector3.Lerp(startingPosition, new Vector3(29.13097f, 6.752212f, -19.82013f), (elapsedTime / duration));
            mainCamera.transform.rotation = Quaternion.Slerp(startingRotation, Quaternion.Euler(13.161f, -378.782f, 1.412f), (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = new Vector3(29.13097f, 6.752212f, -19.82013f);
        mainCamera.transform.rotation = Quaternion.Euler(13.161f, -378.782f, 1.412f);
    }

    public void MoveToShop() {
        nowMenu = 3;
        StartCoroutine(MoveCameraToShop());
    }

    private IEnumerator MoveCameraToMainMenu() {
        game1Explain.SetActive(false);
        game2Explain.SetActive(false);
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
        menus = new GameObject[] { mainMenu, game1Menu, game2Menu, shop };
        for (int i = 0; i < menus.Length; i++) {
            menus[i].SetActive(i == nowMenu);
        }

        shopMenus = new GameObject[] { shopGame1Background, shopGame1LetterPiece, shopGame2Background, shopGame2Vehicle };
        for (int i = 0; i < shopMenus.Length; i++) {
            shopMenus[i].SetActive(i == nowShopMenu);
        }

        shopLeftButton.SetActive(nowShopMenu != 0);
        shopRightButton.SetActive(nowShopMenu != 3);

        scoreText.text = "���� ����: " + score.ToString();
        score = fileManager.LoadData(0);

        for (int i = 0; i < 15; i++) {
            if (fileManager.LoadData(15 + i) == 2) {
                TextMeshProUGUI buttonText = vehicles[i].GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = "���� ��";
                Image image = vehicles[i].GetComponent<Image>();
                image.color = new Color(39 / 255f, 111 / 255f, 22 / 255f, 150 / 255f);
            }
            if (fileManager.LoadData(15 + i) == 1) {
                TextMeshProUGUI buttonText = vehicles[i].GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = "�����ϱ�";
                Image image = vehicles[i].GetComponent<Image>();
                image.color = new Color(108 / 255f, 255 / 255f, 73 / 255f, 150 / 255f);
            }
        }

        for (int i = 0; i < 15; i++) {
            if (fileManager.LoadData(30 + i) == 2) {
                TextMeshProUGUI buttonText = game1Backgrounds[i].GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = "���� ��";
                Image image = game1Backgrounds[i].GetComponent<Image>();
                image.color = new Color(39 / 255f, 111 / 255f, 22 / 255f, 150 / 255f);
            }
            if (fileManager.LoadData(30 + i) == 1) {
                TextMeshProUGUI buttonText = game1Backgrounds[i].GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = "�����ϱ�";
                Image image = game1Backgrounds[i].GetComponent<Image>();
                image.color = new Color(108 / 255f, 255 / 255f, 73 / 255f, 150 / 255f);
            }
        }
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

    public void ShopMoveLeft() {
        nowShopMenu--;
    }

    public void ShopMoveRight() {
        nowShopMenu++;
    }

    public void Game1BackgroundPurchase(int index, int needScore) {
        if (needScore <= score && fileManager.LoadData(30 + index) == 0) {
            GetComponent<AudioSource>().PlayOneShot(purchaseSound, 1f);
            fileManager.SaveData(1, 30 + index);
            fileManager.SaveData(score - needScore, 0);
        } else if (fileManager.LoadData(30 + index) == 1) {
            for (int i = 0; i < 15; i++) {
                if (fileManager.LoadData(30 + i) == 2) {
                    fileManager.SaveData(1, 30 + i);
                    break;
                }
            }
            GetComponent<AudioSource>().PlayOneShot(equipmentSound, 1f);
            fileManager.SaveData(2, 30 + index);
        }
    }

    public void VehiclePurchase(int index, int needScore) {
        if (needScore <= score && fileManager.LoadData(15 + index) == 0) {
            GetComponent<AudioSource>().PlayOneShot(purchaseSound, 1f);
            fileManager.SaveData(1, 15 + index);
            fileManager.SaveData(score - needScore, 0);
        } else if (fileManager.LoadData(15 + index) == 1) {
            for (int i = 0; i < 15; i++) {
                if (fileManager.LoadData(15 + i) == 2) {
                    fileManager.SaveData(1, 15 + i);
                    break;
                }
            }
            GetComponent<AudioSource>().PlayOneShot(equipmentSound, 1f);
            fileManager.SaveData(2, 15 + index);
        }
    }
}
