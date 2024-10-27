using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static GameManagerGame2;

public class questionListGame2 {
    public string question { get; set; }
    public string example1 { get; set; }
    public string example2 { get; set; }
}

public class GameManagerGame2 : MonoBehaviour {
    public GameObject vehicle;
    public new GameObject camera;
    public Camera mainCamera;
    public GameObject[] obstaclePrefabs;
    public GameObject coinPrefab;
    public TextMeshProUGUI scoreText;
    public Joystick moveobject;
    public AudioClip fallSound;
    public AudioSource exhaustAudioSource;
    private Rigidbody vehicleRigidbody;
    public TextMeshPro phase1select1Text;
    public TextMeshPro phase1select2Text;
    public TextMeshPro phase2select1Text;
    public TextMeshPro phase2select2Text;
    public TextMeshProUGUI problemText;
    public GameObject stageClear;
    public GameObject portal;
    public Image fadeBackground;
    public TextMeshProUGUI stageClearPhase1Question;
    public TextMeshProUGUI stageClearPhase1Answer;
    public TextMeshProUGUI stageClearPhase2Question;
    public TextMeshProUGUI stageClearPhase2Answer;
    public TextMeshProUGUI stageClearScore;
    public RectTransform dashboardPin;
    public int randomIndex1;
    public int randomIndex2;
    public int nowPhase = 1;
    public int phase1Answer = 0;
    public int phase2Answer = 0;
    public float currentSpeed = 0f;
    public int numberOfObstacles;
    public int numberOfCoins;
    public int score = 5000;
    private float scoreTimer = 0f;
    private FileManager fileManager;

    void Awake() {
        Time.timeScale = 1;
        Application.targetFrameRate = 60;
        vehicleRigidbody = vehicle.GetComponent<Rigidbody>();
    }

    public List<questionListGame2> problemList = new List<questionListGame2> {
        new questionListGame2 {question = "������ ���ؿ� ���� ��ü�� �� ���� ���� ������ ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "�����̳� ������ ���� ���̰� ���ų� ������� ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "����̳� ���� ��ü�� ���η� �ǳ����� �Ÿ�.", example1 = "�ʺ�", example2 = "����"},
        new questionListGame2 {question = "������ ��鿡 ���� �ִ� �����̳� ������ ũ��.", example1 = "����", example2 = "�ʺ�"},
        new questionListGame2 {question = "���, ����, å, ��ǰ, ����, �η� �� �������� ���� ������Ŵ.", example1 = "����", example2 = "���"},
        new questionListGame2 {question = "���⳪ ���, ��� ������ �ϱ��� ��.", example1 = "���", example2 = "����"},
        new questionListGame2 {question = "�繰�� ��ü �״�� �׸��ų� ������ ������ ���� ��.", example1 = "���", example2 = "����"},
        new questionListGame2 {question = "� ����̳� �繰, ���� ������ ���� �����ϰų� �׸��� �׷��� ǥ���ϴ� ��.", example1 = "����", example2 = "���"},
        new questionListGame2 {question = "���̳� �׸� ������ �Ź��̳� ���� ������ �ƴ� ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "���� ������ ����Ͽ� �ø��� ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "�̾߱⸦ �����ϴ� ����� �帧�̳� ������ �ǹ��ϸ�, ���� ��ǰ���� �߿��� ���.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "������ ü���� ������ �ھ��ų� ���ʶ߸��� ���� �Ǵ� ���¸� �ǹ���.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "���ſ� ���� �׸����̳� ������ ������ ��������, �ַ� ������ �ð��̳� ��Ҹ� ȸ���ϰ� ��.", example1 = "���", example2 = "����"},
        new questionListGame2 {question = "���� �븳�ϰų� �浹�ϴ� �� �����̳� ����� ���ÿ� �����ϴ� ���¸� ��Ÿ��.", example1 = "���", example2 = "����"},
        new questionListGame2 {question = "���� ���� �ٸ� ��ҳ� Ư���� �����ϴ� ���¸� �ǹ��ϸ�, ��ȸ��, ������ �ƶ����� �߿���.", example1 = "�پ缺", example2 = "��¡��"},
        new questionListGame2 {question = "�ΰ��� ����� ������ �ٷ� ���� �帣��, ���� ������� �ḻ�� �̾���.", example1 = "���", example2 = "���"},
        new questionListGame2 {question = "����̳� ����� �ð� ������� ����� �������� ������ ���۹��� �ǹ���.", example1 = "�����", example2 = "�Ƿ�"},
        new questionListGame2 {question = "������ ������ ������ �⺻ ������ ���� Ž���ϴ� ö���� �� �о�.", example1 = "���̻���", example2 = "õä����"},
        new questionListGame2 {question = "� �����̳� ����� �����ϱ� ���� ���� �������� �����̳� �̷�.", example1 = "����", example2 = "�ǰ�"},
        new questionListGame2 {question = "� �繰�̳� ������ �����ϱ� ���� �⺻���� �����̳� ���̵��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "Ư�� �����̳� �ý����� �����ϱ� ���� ������� �����̳� ����.", example1 = "��", example2 = "����"},
        new questionListGame2 {question = "���� ����� �������, �븳�Ǵ� �ǰ��̳� ������ ��ȣ�ۿ��� ���� ������ �߱���.", example1 = "������", example2 = "��ȭ��"},
        new questionListGame2 {question = "�����̳� �н��� ���� ���� ������ ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "Ư�� ������ �繰�� ���� ������ �����̳� ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "� �����̳� ����� ��Ÿ���� ���� ���Ǵ� ��ȣ�� ǥ��.", example1 = "��¡", example2 = "��ǥ"},
        new questionListGame2 {question = "�ϳ��� �繰�̳� ������ �ٸ� �Ϳ� ����� ǥ���ϴ� ���.", example1 = "����", example2 = "��"},
        new questionListGame2 {question = "� �����̳� ��ǿ� ���� ü������ �����̳� �ؼ�.", example1 = "�̷�", example2 = "����"},
        new questionListGame2 {question = "�Ѻ��⿡�� ����Ǵ� �����̳� ��Ȳ���� �ɿ��� ������ �߰��ϴ� ��.", example1 = "����", example2 = "�ݾ�"},
        new questionListGame2 {question = "���������� �ްų� ü���� ���̳� ���.", example1 = "����", example2 = "��ȭ"},
        new questionListGame2 {question = "�� ���� �̻��� �繰�̳� ���� ���� ���缺�� ���� ���ο� ����� �����ϴ� ���.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "� ���� ���뼺�̳� ��ġ.", example1 = "ȿ��", example2 = "ȣ��"},
        new questionListGame2 {question = "��ü���� �Ϳ��� �����̳� Ư���� �����Ͽ� �Ϲ�ȭ�� ����.", example1 = "�߻�", example2 = "��ü"},
        new questionListGame2 {question = "�� ��ü�� �ý��� ���� �����̳� �ۿ�.", example1 = "��ȣ�ۿ�", example2 = "���ۿ�"},
        new questionListGame2 {question = "� �����̳� ����� �ǹ̸� ��Ȯ�� �����ϴ� ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "�����̳� ������ ���� ü������ ������ ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "� �繰�̳� ������ �����ϰ� �����ϴ� ����.", example1 = "�ؼ�", example2 = "����"},
        new questionListGame2 {question = "�� ��Ȳ�̳� ���ǿ��� �ٸ� ��Ȳ���� ��ȭ�ϰų� ����Ǵ� ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "����� ���� ���� �̸� ������ �ǰ��̳� �Ǵ�.", example1 = "���԰�", example2 = "��������"},
        new questionListGame2 {question = "�����ϰų� �ݼ��ϴ� ����.", example1 = "����", example2 = "�ÿ�"},
        new questionListGame2 {question = "����̳� ������ �������� ����.", example1 = "�帧", example2 = "���"},
        new questionListGame2 {question = "���� �¼��ų� �ݴ�Ǵ� ����.", example1 = "�븳", example2 = "��¯"},
        new questionListGame2 {question = "������ �������� �ʴ� ���� ���������� �׸��ų� �����ϴ� ����.", example1 = "���", example2 = "���"},
        new questionListGame2 {question = "�� ���� �������� ��ȭ��Ű�ų� ������Ű�� ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "Ư�� ��ǥ�� �޼��ϱ� ���� �̸� ���� ����̳� ����.", example1 = "��ȹ", example2 = "��ȹ"},
        new questionListGame2 {question = "���� ��Ҹ� �ϳ��� �����ų� �����ϴ� ����.", example1 = "����", example2 = "��ü"},
        new questionListGame2 {question = "������ ���³� ������ ��� �����Ǵ� ��.", example1 = "���Ӽ�", example2 = "���Ӽ�"},
        new questionListGame2 {question = "�ڽ��̳� �ֺ� ȯ�濡 ���� �νİ� ����.", example1 = "�ǽ�", example2 = "�Ǵ�"},
        new questionListGame2 {question = "�ɰ��� ��Ȳ�̳� ����� �߻��Ͽ� ����� ó�� ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "�� ���¿��� �ٸ� ���·��� ��ȭ.", example1 = "��ȯ", example2 = "ġȯ"},
        new questionListGame2 {question = "� �繰�̳� ���°� �ٸ� ���³� ���·� �ٲ�� ��.", example1 = "��ȭ", example2 = "��ȯ"},
        new questionListGame2 {question = "���� ��Ұ� ���� �ְ� ��췯���� ����.", example1 = "��ȭ", example2 = "����ȭ"},
        new questionListGame2 {question = "� �����̳� �̷��� ������� �����ϴ� ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "� �繰�̳� ������ �ٶ󺸴� ����.", example1 = "�ð�", example2 = "����"},
        new questionListGame2 {question = "���� �ٸ� �繰�̳� ������ �����ϴ� �ɷ�.", example1 = "������", example2 = "�Ǵܷ�"},
        new questionListGame2 {question = "�ٸ� ����� �����̳� ������ �����ϰ� ������ ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "�ð��� �����鼭 ���������� ��ȭ�ϰų� �����ϴ� ����.", example1 = "��ȭ", example2 = "����"},
        new questionListGame2 {question = "� �繰�̳� ������ ������ ������ ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "Ư�� ����� ���� ��� ���캸�� ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "�繰�̳� ������ �����ϴ� ü������ ��� ���.", example1 = "��", example2 = "����"},
        new questionListGame2 {question = "���� ��Ҹ� ��� �ϳ��� ü�踦 ����� ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "â������ ��� ���� ���ο� ���̵� �̹����� ���ø��� �ɷ�.", example1 = "����", example2 = "ȯ��"},
        new questionListGame2 {question = "� ���� �̷���� ����̳� ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "Ư�� �����̳� ������ �����ϴ� �ɷ�.", example1 = "���", example2 = "�ɷ�"},
        new questionListGame2 {question = "� �繰�̳� ������ ���ϰ� �ִ� ��ġ�� �ؼ�.", example1 = "�ǹ�", example2 = "����"},
        new questionListGame2 {question = "�� ��ü�� ���� ������ �����̳� ������.", example1 = "����", example2 = "��ȣ�ۿ�"},
        new questionListGame2 {question = "� ���� �̷������ ���� ��볪 �ٺ����� ���.", example1 = "�⺻", example2 = "����"},
        new questionListGame2 {question = "���� ��Ұ� ��ȣ�ۿ��ϸ� �̷���� ������ ����.", example1 = "ü��", example2 = "���"},
        new questionListGame2 {question = "Ư�� ����̳� ������ ������� ������ ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "� ����̳� ������ �⺻���� �帧.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "� �����̳� �̷��� �������� �������� ���ϴ� ��.", example1 = "Ÿ�缺", example2 = "������"},
        new questionListGame2 {question = "� ���̵� �Ӹ��ӿ� �׸��� ����.", example1 = "����", example2 = "��ȹ"},
        new questionListGame2 {question = "� ������ �������� �����ϴ� ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "�ſ� �ڼ��ϰ� �����ϰ� �ٷ�� ��.", example1 = "����", example2 = "�ڼ�"},
        new questionListGame2 {question = "Ư�� ������ �ڷḦ �����ϴ� ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "��ǥ�� �̻��� �޼��ϱ� ���� ����ϴ� ��.", example1 = "�߱�", example2 = "Ž��"},
        new questionListGame2 {question = "� �����̳� ��Ȳ�� Ư���� ����.", example1 = "���", example2 = "���"},
        new questionListGame2 {question = "ȸ�簡 �ֽ� ���忡 �ֽ��� �����ϴ� ����.", example1 = "����", example2 = "����ȸ��"},
        new questionListGame2 {question = "���� ��ǰ�̳� Ȱ���� ���� ��â���� â�Ǽ�.", example1 = "������", example2 = "â�Ǽ�"},
        new questionListGame2 {question = "� ����� �پ��� �ð����� �ٶ󺸴� ��.", example1 = "����", example2 = "�ð�"},
        new questionListGame2 {question = "Ư�� �ܾ ������ ���Ǵ� ��Ȳ�̳� ���.", example1 = "����", example2 = "�ƶ�"},
        new questionListGame2 {question = "����̳� ������ ���� �����̳� ����.", example1 = "�帧", example2 = "����"},
        new questionListGame2 {question = "Ư���� ���³� ü�踦 ������ ��.", example1 = "������", example2 = "ü����"},
        new questionListGame2 {question = "� �ý����̳� ���°� ������ �ʰ� ���ӵǴ� ����.", example1 = "������", example2 = "������"},
        new questionListGame2 {question = "� ����� �˾ƺ��ų� �ٰ����� ���.", example1 = "����", example2 = "�ٰ���"},
        new questionListGame2 {question = "Ư�� ��Ȳ���� ���صǴ� ������ ����.", example1 = "�ƶ�", example2 = "���"},
        new questionListGame2 {question = "� �繰�̳� ������ �����ϴ� ��ġ�� ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "�̷��� �Ͼ ���� �̸� �����ϴ� ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "������ �Ѿ ���ο� ���̵� ���ø��� �ɷ�.", example1 = "����", example2 = "â�Ƿ�"},
        new questionListGame2 {question = "���� ���� ��� �߿��� �ϳ��� ���� ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "���� ���س� �������� ��� ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "�� ��ü ���� ���谡 ��Ȳ�� ���� �޶����� ����.", example1 = "��뼺", example2 = "���輺"},
        new questionListGame2 {question = "� ���̳� �ൿ�� �����ϰ� �ִ� �ǹ�.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "Ư�� �о߿����� ���� �����̳� ���.", example1 = "������", example2 = "Ư����"},
        new questionListGame2 {question = "� ���� �߿伺�̳� ���뼺.", example1 = "��ġ", example2 = "�߿伺"},
        new questionListGame2 {question = "Ư�� �繰�̳� ������ ���� ���� ������ �Ӽ�.", example1 = "Ư¡", example2 = "Ư��"},
        new questionListGame2 {question = "� �����̳� �繰�� ��Ÿ���� ���³� ���.", example1 = "���", example2 = "����"},
        new questionListGame2 {question = "� �о߳� ��Ȳ������ ��ȭ�� ���� ����.", example1 = "����", example2 = "��ȭ"},
        new questionListGame2 {question = "������ ��Ȳ�� �м��Ͽ� �� ������ �������� ��.", example1 = "����", example2 = "��"},
        new questionListGame2 {question = "Ư�� ��ǥ�� �޼��ϱ� ���� ü������ ��ȹ.", example1 = "����", example2 = "��ȹ"},
        new questionListGame2 {question = "� ������ ������� �����ϴ� ����.", example1 = "Ȯ��", example2 = "����"},
        new questionListGame2 {question = "�ڿ��̳� ������ ���� ��� ������ �ִ� ��.", example1 = "�й�", example2 = "���"},
        new questionListGame2 {question = "�߻����� �����̳� ������ Ư���� ��ȣ�� ǥ���ϴ� ����.", example1 = "��¡ȭ", example2 = "ǥ��"},
        new questionListGame2 {question = "� ������ ���� ���� �ְ� Ž���ϴ� ��.", example1 = "��ȭ", example2 = "����ȭ"},
        new questionListGame2 {question = "���� ���� �߿��� ���� �߿��� ���� ���ϴ� ����.", example1 = "�켱����", example2 = "�߿䵵"},
        new questionListGame2 {question = "������ ������ ���¸� ���Ӱ� ��ȭ��Ű�� ��.", example1 = "�籸��", example2 = "����"},
        new questionListGame2 {question = "���� ��ü�� �Բ� ���ϰų� ����ϴ� ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "� ���� �ݵ�� �Ͼ�� �Ǵ� ������ ��Ȳ.", example1 = "�ʿ�", example2 = "�Ұ���"},
        new questionListGame2 {question = "Ư�� ��ǥ�� ������ �޼��ϱ� ���� ������ �Ǵ� ��.", example1 = "�⿩", example2 = "����"},
        new questionListGame2 {question = "� ����̳� ������ ��Ȯ�� �����ϴ� ����.", example1 = "�ľ�", example2 = "����"},
        new questionListGame2 {question = "Ư�� ��Ȳ�� �����ϰ� �����ϰų� �ൿ�ϴ� ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "Ư�� Ȱ���̳� �������� �̲���� ����.", example1 = "�ֵ�", example2 = "�̲���"},
        new questionListGame2 {question = "������ ���̵� �θ� ������ ����.", example1 = "Ȯ��", example2 = "����"},
        new questionListGame2 {question = "� �繰�̳� ��Ȳ�� ���ʿ��ϰ� ������ ��ġ�� ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "���� ��ҳ� ���¸� ��ġ��Ű�� ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "Ư�� ����̳� ����� ���� �̸� ������ �������� ����.", example1 = "���", example2 = "���԰�"},
        new questionListGame2 {question = "� ���� �����ϱ� ���� ����Ǵ� �ڿ��̳� ����.", example1 = "���", example2 = "����"},
        new questionListGame2 {question = "�ɰ��� ��Ȳ�̳� ������ �߻��Ͽ� ����� ó�� ����.", example1 = "����", example2 = "���"},
        new questionListGame2 {question = "��ü�� ���� ���� ���� �������� ���������� ����.", example1 = "����ȭ", example2 = "�п�"},
        new questionListGame2 {question = "�� ���¿��� �ٸ� ���·� ��ȭ�ϴ� ��.", example1 = "��ȯ", example2 = "��ȭ"},
        new questionListGame2 {question = "���� ��� ���� �ǰ��̳� ������ ��ġ�ϴ� ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "� �����̳� �ǰ߿� ���� �޾Ƶ��̴� ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "� ���� �� ���ϰ� ����ų� �����ϴ� ��.", example1 = "��ȭ", example2 = "����"},
        new questionListGame2 {question = "� ������ ���� ���� �����ϰ� �ݼ��ϴ� ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "� �ൿ�̳� ���� �ǵ��� ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "Ư�� �ൿ�̳� ��Ȳ�� ������ �δ� ���.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "� ����̳� ��ȭ�� �Ͼ�� �߿��� ����.", example1 = "��ȯ��", example2 = "���"},
        new questionListGame2 {question = "� ���� ���� ��¡�� �ǹ̳� ��ġ.", example1 = "��¡��", example2 = "�ǹ�"},
        new questionListGame2 {question = "� ����̳� ���忡 ���� ������ Ȯ���ϴ� ����.", example1 = "����", example2 = "Ȯ��"},
        new questionListGame2 {question = "Ư�� �ൿ�̳� ������ �Ϸ��� ��ȹ�̳� ����.", example1 = "�ǵ�", example2 = "����"},
        new questionListGame2 {question = "� ������ ���� ���� �����ϰ� ����ϴ� ����.", example1 = "���", example2 = "����"},
        new questionListGame2 {question = "���� �ٸ� �� �����̳� ��Ȳ�� ���Ͽ� ���̸� �����ϴ� ��.", example1 = "���", example2 = "��"},
        new questionListGame2 {question = "� ����̳� ��Ȳ�� ��ȭ�ϴ� �� ������ ��ġ�� ���.", example1 = "���", example2 = "�˹�"},
        new questionListGame2 {question = "� ��ȹ�̳� ��ǥ�� ������ �̷�� ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "Ư�� ��ǥ�� ������ ������ �Ǵ� ��.", example1 = "�⿩", example2 = "����"},
        new questionListGame2 {question = "� ���³� ������ ����ؼ� �����ϴ� ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "����� ��Ȳ�̳� ��ǥ�� �¼� �ο�� ��.", example1 = "����", example2 = "�õ�"},
        new questionListGame2 {question = "Ư�� �ൿ�̳� ���̵� �����ϴ� ��.", example1 = "����", example2 = "�ǰ�"},
        new questionListGame2 {question = "���ο� ������ ������ ã�� ���� �����ϴ� ����.", example1 = "Ž��", example2 = "����"},
        new questionListGame2 {question = "���� ��ҳ� �ǰ��� �� ���߾� �����ϴ� ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "� �ͺ��� ���� �߿��ϰ� ����� ��.", example1 = "�켱", example2 = "����"},
        new questionListGame2 {question = "Ư�� �����̳� ������ �����ϴ� �ɷ�.", example1 = "���", example2 = "����"},
        new questionListGame2 {question = "���� ���� �߿��� �ϳ��� �����ϴ� ����.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "�ٸ� ����̳� ������ ���� ����.", example1 = "�ŷ�", example2 = "����"},
        new questionListGame2 {question = "Ư���� �ǰ��̳� ����� ���ϰ� ������� ��.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "� ����� ���������� ������ ���캸�� ����.", example1 = "�м�", example2 = "�ؼ�"},
        new questionListGame2 {question = "� ���� ���������� �����Ϸ��� �µ�.", example1 = "���ؼ�", example2 = "Ȱ����"},
        new questionListGame2 {question = "� ����̳� ��Ȳ�� �ƶ��̳� ȯ��.", example1 = "���", example2 = "��Ȳ"},
        new questionListGame2 {question = "�߻����� ������ ��Ȯ�ϰ� ��ü���� ���·� ǥ���ϴ� ��.", example1 = "��üȭ", example2 = "��Ȯȭ"},
        new questionListGame2 {question = "Ư�� ������ ��Ȳ�� ���� �νİ� ���.", example1 = "�����ǽ�", example2 = "�ν�"},
        new questionListGame2 {question = "� ���� �� ���� �̷�������� �����ִ� ��.", example1 = "����", example2 = "�ڱ�"},
        new questionListGame2 {question = "�Ϲ������� �ؾȼ��� ���ϴ� ū ���� ����ü��, ��ü�� ���� ������ �����Ѵ�.", example1 = "�ٴ�", example2 = "���"},
        new questionListGame2 {question = "�������̰� ���� ��������, � ���� �������� �������� �����Ѵ�.", example1 = "�ູ", example2 = "��ſ�"},
        new questionListGame2 {question = "���� �������� ������ �μ⹰��, Ư�� ������ �ٷ�ų� �̾߱⸦ ��� �ִ�.", example1 = "å", example2 = "����"},
        new questionListGame2 {question = "Ư���� ��ε�� ���縦 ���� ������, ������ ǥ���ϰų� �̾߱⸦ �����ϴ� �����̴�.", example1 = "�뷡", example2 = "����"},
        new questionListGame2 {question = "��������� ���� �α��� �پ��� �ü��� �ִ� ��������, ������, ��ȭ�� �߽� ������ �Ѵ�.", example1 = "����", example2 = "����"},
        new questionListGame2 {question = "���� �����̳� ���밨�� �ǹ��ϸ�, ���� ���� ģ���� ���踦 �����Ѵ�.", example1 = "���", example2 = "����"},
        new questionListGame2 {question = "�Ŀ� ������ ���Ϸ�, �ܸ��� �Ÿ��� ȥ�յ� ���� ������.", example1 = "���", example2 = "����"},
        new questionListGame2 {question = "���� ���� �߿��� �ϳ��� ���� ������ �� ���� �ϳ�.", example1 = "����", example2 = "������"},
        new questionListGame2 {question = "�ܾ��� �ǹ̳� ������ ������ å����, �� ���� �� ���ȴ�.", example1 = "����", example2 = "������"},
        new questionListGame2 {question = "�̾߱⸦ �ð������� ǥ���� ���� ��ǰ����, ���ӵ� �̹����� �Ҹ��� �����ȴ�.", example1 = "��ȭ", example2 = "�ʸ�"},
        new questionListGame2 {question = "���ο� ��Ҹ� �湮�ϰų� �߰��ϱ� ���� �̵� Ȱ���� �ǹ��Ѵ�.", example1 = "����", example2 = "Ž��"},
        new questionListGame2 {question = "ȯ�ڸ� ġ���ϰų� �ǰ� ����� �ϴ� ���� �������̴�.", example1 = "�ǻ�", example2 = "�Ƿ���"},
        new questionListGame2 {question = "��ü�� ������ �ǰ��� �����ϰų� ü���� �ܷ��ϴ� Ȱ���̴�.", example1 = "�", example2 = "ü��"},
        new questionListGame2 {question = "�ð����� ��ü�� ���� â�Ǽ��� ǥ���ϴ� Ȱ���̳� ��ǰ�� �ǹ��Ѵ�.", example1 = "�̼�", example2 = "����"},
        new questionListGame2 {question = "�ڿ� �����̳� ��Ģ�� �����ϰ� �����ϴ� �й� �о��̴�.", example1 = "����", example2 = "�ڿ���"},
        new questionListGame2 {question = "����� �����ϴ� ��� ������ ��ǰ�� ��Ī�ϴ� ����̴�.", example1 = "����", example2 = "�丮"},
        new questionListGame2 {question = "â���� Ȱ���� ���� ������� �������, �����̳� �޽����� �����ϴ� �� ���ȴ�.", example1 = "��ǰ", example2 = "������ǰ"},
        new questionListGame2 {question = "����̳� ��������, �м��ϰų� �ؼ��� �� �ִ� ���ʰ� �ȴ�.", example1 = "����", example2 = "������"},
        new questionListGame2 {question = "������ ������ ���밨�� ģ������ �ǹ��ϴ� ������ Ư���̴�.", example1 = "����", example2 = "ģ�а�"},
        new questionListGame2 {question = "Ư���� ����� �����ϵ��� ����� �������̳� ��� �ǹ��Ѵ�.", example1 = "���", example2 = "��ġ"},
        new questionListGame2 {question = "�ΰ��� ������ ���� ���¿��� �����ϴ� ���°�� �� ���� ��Ҹ� �����Ѵ�.", example1 = "�ڿ�", example2 = "ȯ��"},
        new questionListGame2 {question = "��ü��, ������, ��ȸ������ ������ ���¸� ��Ÿ���� �����̴�.", example1 = "�ǰ�", example2 = "����"},
        new questionListGame2 {question = "������ �ൿ�̳� ���ÿ� ������ ��ġ�� ���� �����̳� ������ �ǹ��Ѵ�.", example1 = "�ų�", example2 = "��ġ��"},
        new questionListGame2 {question = "�ǻ������ ���� ��ȣ ü���, Ư���� ��Ģ�� ������ ������.", example1 = "���", example2 = "���"},
        new questionListGame2 {question = "������ ���ɼ��� Ž���ϰų� ���ο� ���̵� �����ϴ� �ɷ��� �ǹ��Ѵ�.", example1 = "���", example2 = "â�Ǽ�"}
    };

    private void Start() {
        fileManager = new FileManager();
        StartCoroutine(FadeOut());
        randomIndex1 = Random.Range(0, problemList.Count);
        randomIndex2 = Random.Range(0, problemList.Count);
        phase1Answer = Random.Range(1, 3);
        if (phase1Answer == 2) {
            string temp = problemList[randomIndex1].example1;
            problemList[randomIndex1].example1 = problemList[randomIndex1].example2;
            problemList[randomIndex1].example2 = temp;
        }
        phase2Answer = Random.Range(1, 3);
        while (randomIndex2 == randomIndex1) {
            randomIndex2 = Random.Range(0, problemList.Count);
        }
        if (phase2Answer == 2) {
            string temp = problemList[randomIndex2].example1;
            problemList[randomIndex2].example1 = problemList[randomIndex2].example2;
            problemList[randomIndex2].example2 = temp;
        }
        phase1select1Text.text = problemList[randomIndex1].example1;
        phase1select2Text.text = problemList[randomIndex1].example2;
        phase2select1Text.text = problemList[randomIndex2].example1;
        phase2select2Text.text = problemList[randomIndex2].example2;
        problemText.text = problemList[randomIndex1].question;

        for (int i = 0; i < numberOfObstacles; i++) {
            float randomX = Random.Range(-8.5f, 8.5f);
            float randomZ = Random.Range(-593f, 114f);
            int randomIndex = Random.Range(0, obstaclePrefabs.Length);
            GameObject obstacle = obstaclePrefabs[randomIndex];
            Instantiate(obstacle, new Vector3(randomX, -150, randomZ), Quaternion.identity);
        }
        for (int i = 0; i < numberOfCoins; i++) {
            float randomX = Random.Range(-8.5f, 8.5f);
            float randomZ = Random.Range(-630f, 175f);
            GameObject coin = Instantiate(coinPrefab, new Vector3(randomX, -149.5f, randomZ), Quaternion.identity);
            coin.AddComponent<CoinRotation>();
        }
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        exhaustAudioSource.loop = true;
        exhaustAudioSource.Play();
    }

    void Update() {
        float t = Mathf.InverseLerp(5, 45, currentSpeed);
        mainCamera.fieldOfView = Mathf.Lerp(50, 70, t);

        float normalizedSpeed = Mathf.Clamp01((currentSpeed - 5f) / (45f - 5f));
        exhaustAudioSource.pitch = Mathf.Lerp(0.5f, 1f, normalizedSpeed);

        camera.transform.position = vehicle.transform.position + new Vector3(0, 4.528f, -19);

        if (vehicle.transform.position.z < -670)
            vehicle.transform.position = new Vector3(vehicle.transform.position.x, vehicle.transform.position.y, -662);
        if (vehicle.transform.position.y > -152.11f && vehicle.transform.position.y < -148.11f) {
            if (currentSpeed < 5 && nowPhase != 4)
                currentSpeed = 5;
            float horizontal = moveobject.Horizontal;
            float vertical = moveobject.Vertical;
            if (vertical > 0)
                currentSpeed += Time.deltaTime * 40;
            else
                currentSpeed -= Time.deltaTime * 80;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, 45);
            vehicleRigidbody.MovePosition(vehicle.transform.position + vehicle.transform.forward * currentSpeed * Time.deltaTime);
            if (horizontal != 0 && currentSpeed != 0) {
                Quaternion turnRotation = Quaternion.Euler(0f, horizontal * 45 * Time.deltaTime, 0f);
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
        if (scoreTimer >= 0.003f && nowPhase != 4) {
            score -= 1;
            scoreTimer = 0f;
        }
        if (score <= 0)
            score = 0;
        scoreText.text = "����:" + score.ToString();

        if (nowPhase == 4)
            currentSpeed = 0;

        portal.transform.Rotate(0, 0, 100 * Time.deltaTime);

        float imagenormalizedSpeed = (currentSpeed - 5f) / (45f - 5f);
        float targetRotationZ = Mathf.Lerp(132.482f, -42.458f, imagenormalizedSpeed);
        float shake = Mathf.Sin(Time.time * 0.5f) * 10f;
        float finalRotationZ = targetRotationZ + shake;
        dashboardPin.localEulerAngles = new Vector3(0, 0, finalRotationZ + Random.Range(-7f, 7f));
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

    public void StageClear() {
        nowPhase = 4;
        stageClear.SetActive(true);
        stageClearPhase1Question.text = problemList[randomIndex1].question;
        stageClearPhase2Question.text = problemList[randomIndex2].question;
        stageClearScore.text = "��������: " + score.ToString();
        if (phase1Answer == 1)
            stageClearPhase1Answer.text = "��" + problemList[randomIndex1].example1;
        else
            stageClearPhase1Answer.text = "��" + problemList[randomIndex1].example2;
        if (phase2Answer == 1)
            stageClearPhase2Answer.text = "��" + problemList[randomIndex2].example1;
        else
            stageClearPhase2Answer.text = "��" + problemList[randomIndex2].example2;
        StartCoroutine(MoveAnswer());
    }

    private IEnumerator MoveAnswer() {
        yield return new WaitForSeconds(0.5f);
        yield return MoveAnswerText(stageClearPhase1Answer);
        yield return MoveAnswerText(stageClearPhase2Answer);
    }

    private IEnumerator MoveAnswerText(TextMeshProUGUI text) {
        float elapsedTime = 0;
        Vector3 startPosition = text.transform.localPosition;
        while (elapsedTime < 0.5f) {
            text.transform.localPosition = Vector3.Lerp(startPosition, new Vector3(-6, text.transform.localPosition.y, text.transform.localPosition.z), elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        text.transform.localPosition = new Vector3(-6, text.transform.localPosition.y, text.transform.localPosition.z);
    }

    public void ReturnMainMenu() {
        StartCoroutine(FadeIn(1));
    }

    public void RestartGame() {
        StartCoroutine(FadeIn(2));
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
        fileManager.AddData(score, 0);
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
            SceneManager.LoadScene("MainMenu");
        else if (check == 2)
            SceneManager.LoadScene("Game2");
    }
}