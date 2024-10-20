using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

public class FakeLetter : MonoBehaviour
{
    public TextMeshPro displayText;
    public GameObject hp1Image;
    public GameObject hp2Image;
    public GameObject hp3Image;
    public GameObject hp4Image;
    private bool isFading = false;
    private float angle = 0f;
    public int hp;
    private int movementType;
    private void Start() {
        movementType = Random.Range(1, 4);
        List<char> options = new List<char>();
        options.AddRange(new List<char> {'문', '제', '정', '답'});
        char selectedChar = options[Random.Range(0, options.Count)];
        displayText.text = "" + selectedChar;
    }
    private void Update() {
        if (movementType == 1) {
            transform.position += new Vector3(0, 6f * Time.deltaTime, 0);
        } else {
            angle += Time.deltaTime * 50f * (movementType == 2 ? 1 : -1);
            float xOffset = Mathf.Cos(angle * Mathf.Deg2Rad) * 0.008f;
            float zOffset = Mathf.Sin(angle * Mathf.Deg2Rad) * 0.008f;
            transform.position += new Vector3(xOffset, 2f * Time.deltaTime, zOffset);
        }

        if (transform.position.y >= 9 && !isFading) {
            StartFadeOut();
        }

        GameObject[] hpImages = { hp1Image, hp2Image, hp3Image, hp4Image };
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
        hp4Image.GetComponent<SpriteRenderer>()
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

    private IEnumerator ShrinkAndDestroyCoroutine() {
        Vector3 originalScale = transform.localScale;
        float elapsed = 0f;
        while (elapsed < 0.4f) {
            float scale = Mathf.Lerp(1f, 0f, elapsed / 0.4f);
            transform.localScale = originalScale * scale;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
