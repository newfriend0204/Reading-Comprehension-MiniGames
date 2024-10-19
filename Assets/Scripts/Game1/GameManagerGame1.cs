using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class questionListGame1 {
    public string question { get; set; }
    public string answer { get; set; }
    public List<char> example { get; set; }
}

public class GameManagerGame1 : MonoBehaviour {
    public GameObject letterObject;
    private Camera mainCamera;
    public TextMeshProUGUI questionText;
    public new GameObject camera;
    public GameObject answerCube1;
    public GameObject answerCube2;
    public GameObject answerCube3;
    public GameObject answerCube4;
    public GameObject answerCube5;
    public GameObject answerCube6;
    public GameObject answerCubeCurrentPosition;
    public GameObject background;
    public TextMeshProUGUI answerLetter1;
    public TextMeshProUGUI answerLetter2;
    public TextMeshProUGUI answerLetter3;
    public TextMeshProUGUI answerLetter4;
    public TextMeshProUGUI answerLetter5;
    public TextMeshProUGUI answerLetter6;
    public TextMeshProUGUI hintItemText;
    public TextMeshProUGUI showScore;
    public Image hintItemImage;
    public GameObject stageClear;
    public TextMeshProUGUI stageClearExplain;
    public TextMeshProUGUI stageClearScore;
    private string answerCheck = "000000";
    private int score = 0;
    private float scoreTimer = 0f;
    private float answerTimer = 0f;
    public string question;
    public string answer;
    public int problemIndex;
    private float angle = 0f;

    void Awake() {
        Application.targetFrameRate = 60;
    }

    public List<questionListGame1> problemList = new List<questionListGame1> {
        new questionListGame1 {question = "갑자기 창문을 벌컥 .", answer = "열어젖히다", example = new List<char>{ '제', '제', '혔', '혔', '쳤', '쳤'}},
        new questionListGame1 {question = "양발을 어깨로 벌리다.", answer = "넓이", example = new List<char>{ '넓', '넓', '이', '이'}},
        new questionListGame1 {question = "책장의 책들을 색깔별로 하여 정리해 두었다.", answer = "구분", example = new List<char>{ '구', '별', '별'}},
        new questionListGame1 {question = "조화와 생화를 하기 힘들다.", answer = "구별", example = new List<char>{ '구', '분', '분'}},
        new questionListGame1 {question = "그는 유명한 작품을 하며 그림을 배웠다.", answer = "모사", example = new List<char>{ '묘', '묘', '사'}},
        new questionListGame1 {question = "그는 그 사건 현장을 생생하게 했다.", answer = "묘사", example = new List<char>{ '모', '모', '사'}},
        new questionListGame1 {question = "그의 논문은 세계적인 학술지에 될 예정이다.", answer = "게재", example = new List<char>{ '재', '기', '기', '개'}},
        new questionListGame1 {question = "지원서에 된 내용이 사실과 다를 경우 불합격 처리됩니다.", answer = "기재", example = new List<char>{ '재', '게', '게'}},
        new questionListGame1 {question = "삼각형의 를 구하시오.", answer = "너비", example = new List<char>{ '너', '너', '비', '비'}},
        new questionListGame1 {question = "그는 갑자기 했다.", answer = "주저", example = new List<char>{ '알', '려'}},
        new questionListGame1 {question = "그녀는 매일 에 나간다.", answer = "산책", example = new List<char>{ '문', '서'}},
        new questionListGame1 {question = "그들은 함께 을 나누었다.", answer = "이야기", example = new List<char>{ '사', '고'}},
        new questionListGame1 {question = "그의 집은 매우 하다.", answer = "아늑", example = new List<char>{ '차', '갑'}},
        new questionListGame1 {question = "오늘은 날씨가 하다.", answer = "따뜻", example = new List<char>{ '춥', '다'}},
        new questionListGame1 {question = "그는 항상 을 잊는다.", answer = "약속", example = new List<char>{ '시', '기'}},
        new questionListGame1 {question = "내일은 중요한 이 있다.", answer = "일정", example = new List<char>{ '인', '지'}},
        new questionListGame1 {question = "그녀는 매일 을 운동한다.", answer = "체조", example = new List<char>{ '사', '치'}},
        new questionListGame1 {question = "우리는 을 계획하고 있다.", answer = "여행", example = new List<char>{ '하', '기'}},
        new questionListGame1 {question = "그녀는 을 사랑한다.", answer = "자연", example = new List<char>{ '만', '들'}},
        new questionListGame1 {question = "내 친구는 매우 하다.", answer = "친절", example = new List<char>{ '강', '요'}},
        new questionListGame1 {question = "그는 항상 을 한다.", answer = "노력", example = new List<char>{ '피', '해'}},
        new questionListGame1 {question = "그들은 중요한 을 논의했다.", answer = "문제", example = new List<char>{ '해', '결'}},
        new questionListGame1 {question = "그녀는 늘 하게 웃는다.", answer = "상냥", example = new List<char>{ '매', '우'}},
        new questionListGame1 {question = "나는 자주 을 한다.", answer = "식사", example = new List<char>{ '줄', '기'}},
        new questionListGame1 {question = "그는 이 빠르다.", answer = "생각", example = new List<char>{ '심', '리'}},
        new questionListGame1 {question = "그녀는 에 대한 열정이 있다.", answer = "음악", example = new List<char>{ '학', '과'}},
        new questionListGame1 {question = "프로젝트에 대한 을 나누었다.", answer = "의견", example = new List<char>{ '정', '리'}},
        new questionListGame1 {question = "다음 주에 이 있을 것 같습니다.", answer = "회의", example = new List<char>{ '방', '해'}},
        new questionListGame1 {question = "기분이 좋지 않으면 해야 한다.", answer = "휴식", example = new List<char>{ '자', '람'}},
        new questionListGame1 {question = "아름다운 을 즐기기 위해 나갔다.", answer = "경치", example = new List<char>{ '식', '음'}},
        new questionListGame1 {question = "그는 자신의 꿈을 하기 위해 노력한다.", answer = "실현", example = new List<char>{ '기', '리'}},
        new questionListGame1 {question = "오늘은 정말 한 하루였다.", answer = "특별", example = new List<char>{ '다', '린'}},
        new questionListGame1 {question = "비 오는 날에는 집에서  읽는다.", answer = "책을", example = new List<char>{'볼'}},
        new questionListGame1 {question = "아침에 을 하면 하루가 상쾌해진다.", answer = "운동", example = new List<char>{'산'}},
        new questionListGame1 {question = "커피를 마시며 친구와 를 떨었다.", answer = "수다", example = new List<char>{'계', '산'}},
        new questionListGame1 {question = "매일 저녁 에서 산책하는 것을 좋아한다.", answer = "해변", example = new List<char>{'태', '양'}},
        new questionListGame1 {question = "새로운 로 저녁을 만들어 보았다.", answer = "레시피", example = new List<char>{'화', '장', '품'}},
        new questionListGame1 {question = "아빠와 함께 를 타고 공원에 갔다.", answer = "자전거", example = new List<char>{'우', '주', '선'}},
        new questionListGame1 {question = "해가 지고 에 별이 떠올랐다.", answer = "밤하늘", example = new List<char>{'낮'}},
        new questionListGame1 {question = "도서관에서  공부하는 것이 편하다.", answer = "조용히", example = new List<char>{'뜨', '겁', '게'}},
        new questionListGame1 {question = "가족과 함께  음식을 나누었다.", answer = "맛있는", example = new List<char>{'높', '은'}},
        new questionListGame1 {question = "책상 위에 을 놓았다.", answer = "펜", example = new List<char>{'식', '탁'}},
        new questionListGame1 {question = "아침에 커피를 마시며 을 생각했다.", answer = "계획", example = new List<char>{'이', '상'}},
        new questionListGame1 {question = "그는 를 매일 아침 마신다.", answer = "커피", example = new List<char>{'바', '람'}},
        new questionListGame1 {question = "주말마다 가족과 을 나누며 시간을 보낸다.", answer = "대화", example = new List<char>{'나', '무'}},
        new questionListGame1 {question = "책상 위에 이 놓여 있다.", answer = "책", example = new List<char>{'나', '무'}},
        new questionListGame1 {question = "요리할 때 을 추가해야 맛있다.", answer = "소금", example = new List<char>{'고', '양', '이'}},
        new questionListGame1 {question = "비 오는 날에는 이 필요하다.", answer = "우산", example = new List<char>{'책', '상'}},
        new questionListGame1 {question = "아침에 을 먹어야 건강하다.", answer = "과일", example = new List<char>{'바', '람'}},
        new questionListGame1 {question = "그는 아침마다 을 읽는다.", answer = "신문", example = new List<char>{'바', '람'}},
        new questionListGame1 {question = "주말마다 친구들과 을 한다.", answer = "산책", example = new List<char>{'구', '름'}},
        new questionListGame1 {question = "날씨가 추우면  옷을 입는다.", answer = "두꺼운", example = new List<char>{'빠', '른'}},
        new questionListGame1 {question = "아이들은 공원에서  뛰어놀았다.", answer = "즐겁게", example = new List<char>{'무', '겁', '게'}},
        new questionListGame1 {question = "바쁜 날에는  식사를 한다.", answer = "간단한", example = new List<char>{'깨', '끗', '한'}},
        new questionListGame1 {question = "그녀는 꽃을  가꾸는 것을 좋아한다.", answer = "정성껏", example = new List<char>{'아', '프', '게'}},
        new questionListGame1 {question = "요즘에는  옷을 많이 입는다.", answer = "편한", example = new List<char>{'달', '콤', '한'}},
        new questionListGame1 {question = "그는 매일  운동을 한다.", answer = "꾸준히", example = new List<char>{'차', '갑', '게'}},
        new questionListGame1 {question = "학교에서는  공부해야 한다.", answer = "열심히", example = new List<char>{'서', '늘', '하', '게'}},
        new questionListGame1 {question = "주말에는  쉬고 싶다.", answer = "푹", example = new List<char>{'따', '뜻', '하', '게'}},
        new questionListGame1 {question = "여행할 때는  준비가 필요하다.", answer = "철저한", example = new List<char>{'넓', '은'}},
        new questionListGame1 {question = "날씨가 좋으면  하늘을 본다.", answer = "맑은", example = new List<char>{'달', '린'}},
        new questionListGame1 {question = "겨울에는  차를 마신다.", answer = "따뜻한", example = new List<char>{'느', '린'}},
        new questionListGame1 {question = "그는 항상  공부하는 습관이 있다.", answer = "성실하게", example = new List<char>{'시', '원', '하', '게'}},
        new questionListGame1 {question = "아이들은  뛰어놀았다.", answer = "신나게", example = new List<char>{'어', '둡', '게'}},
        new questionListGame1 {question = "그녀는  화장을 한다.", answer = "정성스럽게", example = new List<char>{'높'}},
        new questionListGame1 {question = "비가 오면  창문을 닫아야 한다.", answer = "꼭", example = new List<char>{'높', '게'}},
        new questionListGame1 {question = "아침에는  운동을 한다.", answer = "가벼운", example = new List<char>{'깊', '은'}},
        new questionListGame1 {question = "그는 매일  책을 읽는다.", answer = "다양한", example = new List<char>{'넓', '은'}},
        new questionListGame1 {question = "여름에는  물을 많이 마신다.", answer = "시원한", example = new List<char>{'무', '거', '운'}},
        new questionListGame1 {question = "우리는 해변에서  시간을 보냈다.", answer = "행복한", example = new List<char>{'좁', '은'}},
        new questionListGame1 {question = "수업 시간에는  집중해야 한다.", answer = "깊이", example = new List<char>{'멀', '리'}},
        new questionListGame1 {question = "그는  걷는 것을 좋아한다.", answer = "천천히", example = new List<char>{'날', '카', '롭', '게'}},
        new questionListGame1 {question = "우리는  준비를 마쳤다.", answer = "철저한", example = new List<char>{'부', '드', '럽', '게'}},
        new questionListGame1 {question = "날씨가 흐리면 을 잊지 마세요.", answer = "우산", example = new List<char>{'선', '풍', '기'}},
        new questionListGame1 {question = "친구와 함께 에 갔다.", answer = "커피숍", example = new List<char>{'정', '원'}},
        new questionListGame1 {question = "그녀는 매일 저녁 을 준비한다.", answer = "요리", example = new List<char>{'영', '화'}},
        new questionListGame1 {question = "운동 후에는 을 꼭 마셔야 한다.", answer = "물", example = new List<char>{'차'}},
        new questionListGame1 {question = "나는 주말마다 을 즐기러 간다.", answer = "산책", example = new List<char>{'공', '부'}},
        new questionListGame1 {question = "공부하는 동안 을 자주 한다.", answer = "휴식", example = new List<char>{'장', '난'}},
        new questionListGame1 {question = "오늘은 가족과 을 보내기로 했다.", answer = "시간", example = new List<char>{'책'}},
        new questionListGame1 {question = "여름철에는 이 많이 필요하다.", answer = "물", example = new List<char>{'의', '자'}},
        new questionListGame1 {question = "친구와의 은 언제나 즐겁다.", answer = "대화", example = new List<char>{'경', '쟁'}},
        new questionListGame1 {question = "취미로 을 시작해 보았다.", answer = "사진찍기", example = new List<char>{'음', '악'}},
        new questionListGame1 {question = "그녀는 항상 을 챙겨서 다닌다.", answer = "필기도구", example = new List<char>{'신', '발'}},
        new questionListGame1 {question = "매일 아침에는 을 먹는 게 중요하다.", answer = "아침식사", example = new List<char>{'간', '식'}},
        new questionListGame1 {question = "새로운 소식을 들으면 을 적는다.", answer = "메모", example = new List<char>{'소', '설'}},
        new questionListGame1 {question = "도서관에서 을 찾는 것이 쉬웠다.", answer = "책", example = new List<char>{'정', '보'}},
        new questionListGame1 {question = "학교에서 을 잘 듣는 것이 중요하다.", answer = "수업", example = new List<char>{'시', '험'}},
        new questionListGame1 {question = "그녀는 다양한 을 좋아한다.", answer = "스포츠", example = new List<char>{ '미', '래'}}
    };


    private void Start() {
        TextMeshProUGUI[] answerLetters = new TextMeshProUGUI[] { answerLetter1, answerLetter2, answerLetter3, answerLetter4, answerLetter5, answerLetter6 };
        problemIndex = Random.Range(0, problemList.Count);
        question = problemList[problemIndex].question;
        answer = problemList[problemIndex].answer;
        for (int i = 0; i < answer.Length; i++) {
            answerLetters[i].text = answer[i].ToString();
        }
        questionText.text = question;
        if(answer.Length < answerCheck.Length)
            answerCheck = answerCheck.Substring(0, answer.Length);
        score = answer.Length * 2000;

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

        if (answerCheck.All(c => c == '1')) {
            StageClear();
            Time.timeScale = 0;
        }

        int answerCurrentPosition = answerCheck.IndexOf('0');
        if (answerCurrentPosition == -1)
            answerCubeCurrentPosition.SetActive(false);
        if (!(answerCheck.All(c => c == '1'))) {
            answerCubeCurrentPosition.transform.position = answerCubes[answerCurrentPosition].transform.position;
        }

        scoreTimer += Time.deltaTime;
        if (scoreTimer >= 0.002f) {
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

        angle += Time.deltaTime * 30f;
        float xOffset = Mathf.Cos(angle * Mathf.Deg2Rad) * 0.005f;
        float zOffset = Mathf.Sin(angle * Mathf.Deg2Rad) * 0.005f;
        background.transform.position += new Vector3(xOffset, 0, zOffset);
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

    public void StageClear() {
        stageClear.SetActive(true);
        stageClearScore.text = "얻은 점수: " + score.ToString();
        int placeholderCount = question.Count(c => c == '');
        stageClearExplain.text = question.Replace(new string('', placeholderCount), answer);
    }

    public void ReturnMainMenu() {
        Debug.Log("메인메뉴로 돌아갑니다.");
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
