using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class Letter : MonoBehaviour {
    public TextMeshPro displayText;
    public GameManagerGame1 gameManager;
    public GameObject hp1Image;
    public GameObject hp2Image;
    public GameObject hp3Image;
    public GameObject hp4Image;
    public GameObject hp5Image;
    public GameObject hp6Image;
    public AudioSource letterPieceBreak;
    public AudioSource letterPieceClick;
    private bool isFading = false;
    public int hp;
    private float angle = 0f;
    private int movementType; // 1: y�����θ� ���, 2: �ð���� ȸ��, 3: �ݽð���� ȸ��
    private bool canClick = true;

    private void Start() {
        gameManager = FindObjectOfType<GameManagerGame1>();
        movementType = Random.Range(1, 4);
        List<char> options = new List<char>();
        options.AddRange(gameManager.problemList[gameManager.problemIndex].example);
        char[] answer = gameManager.answer.ToCharArray();
        options.AddRange(answer);
        char selectedChar = options[Random.Range(0, options.Count)];
        displayText.text = "" + selectedChar;
    }

    private void Update() {
        gameManager = FindObjectOfType<GameManagerGame1>();
        if (movementType == 1) {
            transform.position += new Vector3(0, 6f * Time.deltaTime, 0);
        } else {
            angle += Time.deltaTime * 50f * (movementType == 2 ? 1 : -1);
            float xOffset = Mathf.Cos(angle * Mathf.Deg2Rad) * 0.008f;
            float zOffset = Mathf.Sin(angle * Mathf.Deg2Rad) * 0.008f;
            transform.position += new Vector3(xOffset, 6f * Time.deltaTime, zOffset);
        }

        if (transform.position.y >= 13 && !isFading) {
            StartFadeOut();
        }

        GameObject[] hpImages = { hp1Image, hp2Image, hp3Image, hp4Image, hp5Image, hp6Image };
        for (int i = 0; i < hpImages.Length; i++) {
            hpImages[i].SetActive(i == hp - 1);
            hpImages[i].GetComponent<SpriteRenderer>().transform.Rotate(Vector3.forward, 500f * Time.deltaTime);
        }
    }

    public void StartFadeOut() {
        if (!isFading) {
            isFading = true;
            StartCoroutine(FadeOutCoroutine(0.2f));
        }
    }

    private IEnumerator FadeOutCoroutine(float duration) {
        float elapsedTime = 0f;
        SpriteRenderer[] spriteRenderers = new SpriteRenderer[] {
        hp1Image.GetComponent<SpriteRenderer>(),
        hp2Image.GetComponent<SpriteRenderer>(),
        hp3Image.GetComponent<SpriteRenderer>(),
        hp4Image.GetComponent<SpriteRenderer>(),
        hp5Image.GetComponent<SpriteRenderer>(),
        hp6Image.GetComponent<SpriteRenderer>()
    };
        Color textColor = displayText.color;

        while (elapsedTime < duration) {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / duration);
            foreach (var spriteRenderer in spriteRenderers) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            }
            textColor.a = alpha;
            displayText.color = textColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isFading = false;
        Destroy(gameObject);
    }

    void OnMouseDown() {
        if (gameManager.isGaming == 1 && canClick) {
            StartCoroutine(ShakeCoroutine());
            hp -= 1;
            canClick = false;
            if (hp == 0) {
                letterPieceBreak.Play();
                gameManager.InputLetter(displayText.text);
                StartCoroutine(ShrinkAndDestroyCoroutine());
                return;
            }
            letterPieceClick.Play();
            StartCoroutine(EnableClickAfterDelay(0.7f));
        }
    }

    private IEnumerator EnableClickAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        canClick = true;
    }

    private IEnumerator ShrinkAndDestroyCoroutine() {
        Vector3 originalScale = transform.localScale;
        float elapsed = 0f;
        while (elapsed < 0.4f) {
            float scale = Mathf.Lerp(1f, 0f, elapsed / 0.4f);
            transform.localScale = originalScale * scale;
            elapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject, letterPieceBreak.clip.length);
    }

    private IEnumerator ShakeCoroutine() {
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;
        while (elapsed < 0.7f) {
            float randomX = Random.Range(-0.3f, 0.3f);
            float randomZ = Random.Range(-0.3f, 0.3f);
            Vector3 newPosition = new Vector3(originalPosition.x + randomX, originalPosition.y, originalPosition.z + randomZ);
            transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(originalPosition.x + Random.Range(-0.3f, 0.3f), originalPosition.y, originalPosition.z + Random.Range(-0.3f, 0.3f));
    }
}
