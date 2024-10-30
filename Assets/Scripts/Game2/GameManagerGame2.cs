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
        new questionListGame2 {question = "일정한 기준에 따라 전체를 몇 개로 갈라 나누는 것.", example1 = "구분", example2 = "구별"},
        new questionListGame2 {question = "성질이나 종류에 따라 차이가 나거나 갈라놓는 것.", example1 = "구별", example2 = "구분"},
        new questionListGame2 {question = "평면이나 넓은 물체의 가로로 건너지른 거리.", example1 = "너비", example2 = "넓이"},
        new questionListGame2 {question = "일정한 평면에 걸쳐 있는 공간이나 범위의 크기.", example1 = "넓이", example2 = "너비"},
        new questionListGame2 {question = "기술, 경제, 책, 제품, 국토, 인력 등 물질적인 것을 발전시킴.", example1 = "개발", example2 = "계발"},
        new questionListGame2 {question = "슬기나 재능, 사상 따위를 일깨워 줌.", example1 = "계발", example2 = "개발"},
        new questionListGame2 {question = "사물을 형체 그대로 그리거나 원본을 베끼어 쓰는 것.", example1 = "모사", example2 = "묘사"},
        new questionListGame2 {question = "어떤 대상이나 사물, 현상 따위를 언어로 서술하거나 그림을 그려서 표현하는 것.", example1 = "묘사", example2 = "모사"},
        new questionListGame2 {question = "글이나 그림 따위를 신문이나 잡지 따위에 싣는 것.", example1 = "게재", example2 = "기재"},
        new questionListGame2 {question = "문서 따위에 기록하여 올리는 것.", example1 = "기재", example2 = "게재"},
        new questionListGame2 {question = "이야기를 구성하는 사건의 흐름이나 구조를 의미하며, 문학 작품에서 중요한 요소.", example1 = "서사", example2 = "전개"},
        new questionListGame2 {question = "기존의 체제나 질서를 뒤엎거나 무너뜨리는 행위 또는 상태를 의미함.", example1 = "전복", example2 = "발전"},
        new questionListGame2 {question = "과거에 대한 그리움이나 애착을 느끼는 감정으로, 주로 잊혀진 시간이나 장소를 회상하게 함.", example1 = "향수", example2 = "과거"},
        new questionListGame2 {question = "서로 대립하거나 충돌하는 두 개념이나 사실이 동시에 존재하는 상태를 나타냄.", example1 = "모순", example2 = "무순"},
        new questionListGame2 {question = "여러 가지 다른 요소나 특성이 공존하는 상태를 의미하며, 사회적, 생태적 맥락에서 중요함.", example1 = "다양성", example2 = "상징성"},
        new questionListGame2 {question = "인간의 고통과 불행을 다룬 문학 장르로, 종종 비극적인 결말로 이어짐.", example1 = "비극", example2 = "희극"},
        new questionListGame2 {question = "사건이나 사실을 시간 순서대로 기록한 역사적인 문서나 저작물을 의미함.", example1 = "연대기", example2 = "실록"},
        new questionListGame2 {question = "존재의 본질과 우주의 기본 원리에 대해 탐구하는 철학의 한 분야.", example1 = "형이상학", example2 = "천채문학"},
        new questionListGame2 {question = "어떤 현상이나 사실을 설명하기 위해 세운 잠정적인 주장이나 이론.", example1 = "가설", example2 = "의견"},
        new questionListGame2 {question = "어떤 사물이나 현상을 이해하기 위한 기본적인 생각이나 아이디어.", example1 = "개념", example2 = "개시"},
        new questionListGame2 {question = "특정 현상이나 시스템을 설명하기 위해 만들어진 모형이나 구조.", example1 = "모델", example2 = "패턴"},
        new questionListGame2 {question = "논리적 사고의 방법으로, 대립되는 의견이나 개념의 상호작용을 통해 진리를 추구함.", example1 = "변증법", example2 = "변화성"},
        new questionListGame2 {question = "경험이나 학습을 통해 얻은 정보와 이해.", example1 = "지식", example2 = "지혜"},
        new questionListGame2 {question = "특정 주제나 사물에 대한 개인의 생각이나 믿음.", example1 = "관념", example2 = "주장"},
        new questionListGame2 {question = "어떤 개념이나 대상을 나타내기 위해 사용되는 기호나 표상.", example1 = "상징", example2 = "대표"},
        new questionListGame2 {question = "하나의 사물이나 개념을 다른 것에 빗대어 표현하는 방식.", example1 = "비유", example2 = "비교"},
        new questionListGame2 {question = "어떤 현상이나 사실에 대한 체계적인 설명이나 해석.", example1 = "이론", example2 = "형식"},
        new questionListGame2 {question = "겉보기에는 모순되는 진술이나 상황에서 심오한 진리를 발견하는 것.", example1 = "역설", example2 = "반어"},
        new questionListGame2 {question = "직접적으로 겪거나 체험한 일이나 사건.", example1 = "경험", example2 = "실화"},
        new questionListGame2 {question = "두 가지 이상의 사물이나 개념 간의 유사성을 통해 새로운 결론을 도출하는 방법.", example1 = "유추", example2 = "우추"},
        new questionListGame2 {question = "어떤 것의 유용성이나 가치.", example1 = "효용", example2 = "호응"},
        new questionListGame2 {question = "구체적인 것에서 본질이나 특성을 추출하여 일반화한 개념.", example1 = "추상", example2 = "구체"},
        new questionListGame2 {question = "두 개체나 시스템 간의 영향이나 작용.", example1 = "상호작용", example2 = "부작용"},
        new questionListGame2 {question = "어떤 개념이나 용어의 의미를 명확히 설명하는 것.", example1 = "정의", example2 = "설명"},
        new questionListGame2 {question = "권한이나 지위에 따른 체계적인 순서나 구조.", example1 = "위계", example2 = "관습"},
        new questionListGame2 {question = "어떤 사물이나 현상을 이해하고 설명하는 과정.", example1 = "해석", example2 = "번역"},
        new questionListGame2 {question = "한 상황이나 조건에서 다른 상황으로 변화하거나 적용되는 것.", example1 = "전이", example2 = "변이"},
        new questionListGame2 {question = "충분한 정보 없이 미리 형성된 의견이나 판단.", example1 = "선입견", example2 = "고정관념"},
        new questionListGame2 {question = "생각하거나 반성하는 과정.", example1 = "사유", example2 = "시우"},
        new questionListGame2 {question = "사건이나 생각의 연속적인 전개.", example1 = "흐름", example2 = "경과"},
        new questionListGame2 {question = "서로 맞서거나 반대되는 상태.", example1 = "대립", example2 = "맞짱"},
        new questionListGame2 {question = "실제로 존재하지 않는 것을 마음속으로 그리거나 생각하는 과정.", example1 = "상상", example2 = "허상"},
        new questionListGame2 {question = "더 나은 방향으로 변화시키거나 발전시키는 것.", example1 = "개선", example2 = "개발"},
        new questionListGame2 {question = "특정 목표를 달성하기 위해 미리 세운 방안이나 전략.", example1 = "계획", example2 = "기획"},
        new questionListGame2 {question = "여러 요소를 하나로 모으거나 결합하는 과정.", example1 = "통합", example2 = "합체"},
        new questionListGame2 {question = "일정한 상태나 성질이 계속 유지되는 것.", example1 = "지속성", example2 = "수속성"},
        new questionListGame2 {question = "자신이나 주변 환경에 대한 인식과 이해.", example1 = "의식", example2 = "판단"},
        new questionListGame2 {question = "심각한 상황이나 사건이 발생하여 어려움에 처한 상태.", example1 = "위기", example2 = "위험"},
        new questionListGame2 {question = "한 상태에서 다른 상태로의 변화.", example1 = "전환", example2 = "치환"},
        new questionListGame2 {question = "어떤 사물이나 상태가 다른 형태나 상태로 바뀌는 것.", example1 = "변화", example2 = "변환"},
        new questionListGame2 {question = "여러 요소가 균형 있게 어우러지는 상태.", example1 = "조화", example2 = "조각화"},
        new questionListGame2 {question = "어떤 주장이나 이론이 사실임을 증명하는 것.", example1 = "입증", example2 = "입장"},
        new questionListGame2 {question = "어떤 사물이나 현상을 바라보는 관점.", example1 = "시각", example2 = "시점"},
        new questionListGame2 {question = "서로 다른 사물이나 개념을 구별하는 능력.", example1 = "변별력", example2 = "판단력"},
        new questionListGame2 {question = "다른 사람의 감정이나 경험을 이해하고 느끼는 것.", example1 = "공감", example2 = "위로"},
        new questionListGame2 {question = "시간이 지나면서 점진적으로 변화하거나 발전하는 과정.", example1 = "진화", example2 = "변신"},
        new questionListGame2 {question = "어떤 사물이나 현상의 현재의 형세나 조건.", example1 = "상태", example2 = "형상"},
        new questionListGame2 {question = "특정 대상을 주의 깊게 살펴보는 것.", example1 = "관찰", example2 = "감시"},
        new questionListGame2 {question = "사물이나 주장을 연결하는 체계적인 사고 방식.", example1 = "논리", example2 = "윤리"},
        new questionListGame2 {question = "여러 요소를 모아 하나의 체계를 만드는 과정.", example1 = "구성", example2 = "지성"},
        new questionListGame2 {question = "창의적인 사고를 통해 새로운 아이디어나 이미지를 떠올리는 능력.", example1 = "상상력", example2 = "환상"},
        new questionListGame2 {question = "어떤 것이 이루어진 방식이나 형태.", example1 = "구조", example2 = "기재"},
        new questionListGame2 {question = "특정 목적이나 역할을 수행하는 능력.", example1 = "기능", example2 = "능력"},
        new questionListGame2 {question = "어떤 사물이나 현상이 지니고 있는 가치나 해석.", example1 = "의미", example2 = "감상"},
        new questionListGame2 {question = "두 개체나 개념 사이의 연결이나 연관성.", example1 = "관계", example2 = "상호작용"},
        new questionListGame2 {question = "어떤 것이 이루어지기 위한 토대나 근본적인 요소.", example1 = "기본", example2 = "발판"},
        new questionListGame2 {question = "여러 요소가 상호작용하며 이루어진 구조나 조직.", example1 = "체계", example2 = "방식"},
        new questionListGame2 {question = "특정 사실이나 조건을 기반으로 설정한 전제.", example1 = "가정", example2 = "추정"},
        new questionListGame2 {question = "어떤 사상이나 주제의 기본적인 흐름.", example1 = "기조", example2 = "방향"},
        new questionListGame2 {question = "어떤 주장이나 이론이 논리적으로 옳은지를 평가하는 것.", example1 = "타당성", example2 = "적절성"},
        new questionListGame2 {question = "어떤 아이디어를 머릿속에 그리는 과정.", example1 = "구상", example2 = "계획"},
        new questionListGame2 {question = "어떤 주장을 논리적으로 입증하는 과정.", example1 = "논증", example2 = "입증"},
        new questionListGame2 {question = "매우 자세하고 정교하게 다루는 것.", example1 = "세밀", example2 = "자세"},
        new questionListGame2 {question = "특정 정보나 자료를 참고하는 과정.", example1 = "참조", example2 = "참고"},
        new questionListGame2 {question = "목표나 이상을 달성하기 위해 노력하는 것.", example1 = "추구", example2 = "탐색"},
        new questionListGame2 {question = "어떤 현상이나 상황의 특정한 형태.", example1 = "양상", example2 = "모습"},
        new questionListGame2 {question = "회사가 주식 시장에 주식을 공개하는 상태.", example1 = "상장", example2 = "상장회사"},
        new questionListGame2 {question = "예술 작품이나 활동이 지닌 독창성과 창의성.", example1 = "예술성", example2 = "창의성"},
        new questionListGame2 {question = "어떤 대상을 다양한 시각에서 바라보는 것.", example1 = "조망", example2 = "시각"},
        new questionListGame2 {question = "특정 단어나 문장이 사용되는 상황이나 배경.", example1 = "문맥", example2 = "맥락"},
        new questionListGame2 {question = "사건이나 생각의 진행 방향이나 패턴.", example1 = "흐름", example2 = "경향"},
        new questionListGame2 {question = "특정한 형태나 체계를 가지는 것.", example1 = "구조적", example2 = "체계적"},
        new questionListGame2 {question = "어떤 시스템이나 상태가 변하지 않고 지속되는 성질.", example1 = "안정성", example2 = "안정성"},
        new questionListGame2 {question = "어떤 대상을 알아보거나 다가가는 방법.", example1 = "접근", example2 = "다가감"},
        new questionListGame2 {question = "특정 상황에서 이해되는 정보의 연결.", example1 = "맥락", example2 = "배경"},
        new questionListGame2 {question = "어떤 사물이나 현상이 차지하는 위치나 상태.", example1 = "위상", example2 = "상태"},
        new questionListGame2 {question = "미래에 일어날 일을 미리 추정하는 것.", example1 = "예측", example2 = "전망"},
        new questionListGame2 {question = "현실을 넘어서 새로운 아이디어를 떠올리는 능력.", example1 = "상상력", example2 = "창의력"},
        new questionListGame2 {question = "여러 가지 대안 중에서 하나를 고르는 과정.", example1 = "선택", example2 = "결정"},
        new questionListGame2 {question = "깊은 이해나 깨달음을 얻는 과정.", example1 = "통찰", example2 = "이해"},
        new questionListGame2 {question = "두 개체 간의 관계가 상황에 따라 달라지는 성질.", example1 = "상대성", example2 = "관계성"},
        new questionListGame2 {question = "어떤 말이나 행동이 내포하고 있는 의미.", example1 = "함의", example2 = "내포"},
        new questionListGame2 {question = "특정 분야에서의 높은 지식이나 기술.", example1 = "전문성", example2 = "특수성"},
        new questionListGame2 {question = "어떤 것의 중요성이나 유용성.", example1 = "가치", example2 = "중요성"},
        new questionListGame2 {question = "특정 사물이나 개념을 구별 짓는 고유한 속성.", example1 = "특징", example2 = "특성"},
        new questionListGame2 {question = "어떤 현상이나 사물이 나타나는 형태나 방식.", example1 = "양상", example2 = "경향"},
        new questionListGame2 {question = "어떤 분야나 상황에서의 변화나 발전 방향.", example1 = "동향", example2 = "변화"},
        new questionListGame2 {question = "문제나 상황을 분석하여 그 원인을 밝혀내는 것.", example1 = "진단", example2 = "평가"},
        new questionListGame2 {question = "특정 목표를 달성하기 위한 체계적인 계획.", example1 = "설계", example2 = "계획"},
        new questionListGame2 {question = "어떤 주장이 사실임을 입증하는 과정.", example1 = "확증", example2 = "증명"},
        new questionListGame2 {question = "자원이나 권한을 여러 대상에 나누어 주는 것.", example1 = "분배", example2 = "배분"},
        new questionListGame2 {question = "추상적인 개념이나 감정을 특정한 기호로 표현하는 과정.", example1 = "상징화", example2 = "표상"},
        new questionListGame2 {question = "어떤 주제를 더욱 깊이 있게 탐구하는 것.", example1 = "심화", example2 = "심층화"},
        new questionListGame2 {question = "여러 가지 중에서 가장 중요한 것을 정하는 기준.", example1 = "우선순위", example2 = "중요도"},
        new questionListGame2 {question = "기존의 구조나 형태를 새롭게 변화시키는 것.", example1 = "재구성", example2 = "재편성"},
        new questionListGame2 {question = "여러 개체가 함께 일하거나 노력하는 과정.", example1 = "협력", example2 = "협동"},
        new questionListGame2 {question = "어떤 일이 반드시 일어나게 되는 이유나 상황.", example1 = "필연", example2 = "불가피"},
        new questionListGame2 {question = "특정 목표나 목적을 달성하기 위해 보탬이 되는 것.", example1 = "기여", example2 = "공헌"},
        new questionListGame2 {question = "어떤 사실이나 개념을 정확히 이해하는 과정.", example1 = "파악", example2 = "이해"},
        new questionListGame2 {question = "특정 상황에 적절하게 반응하거나 행동하는 것.", example1 = "대응", example2 = "반응"},
        new questionListGame2 {question = "특정 활동이나 과정에서 이끌어가는 역할.", example1 = "주도", example2 = "이끌다"},
        new questionListGame2 {question = "정보나 아이디어가 널리 퍼지는 현상.", example1 = "확산", example2 = "퍼짐"},
        new questionListGame2 {question = "어떤 사물이나 상황에 불필요하게 영향을 미치는 것.", example1 = "간섭", example2 = "개입"},
        new questionListGame2 {question = "여러 요소나 상태를 일치시키는 과정.", example1 = "조정", example2 = "조율"},
        new questionListGame2 {question = "특정 대상이나 사람에 대해 미리 형성된 부정적인 생각.", example1 = "편견", example2 = "선입견"},
        new questionListGame2 {question = "어떤 일을 수행하기 위해 지출되는 자원이나 금전.", example1 = "비용", example2 = "지출"},
        new questionListGame2 {question = "심각한 상황이나 문제가 발생하여 어려움에 처한 상태.", example1 = "위기", example2 = "곤란"},
        new questionListGame2 {question = "전체가 여러 개의 작은 조각으로 나뉘어지는 과정.", example1 = "파편화", example2 = "분열"},
        new questionListGame2 {question = "한 상태에서 다른 상태로 변화하는 것.", example1 = "전환", example2 = "변화"},
        new questionListGame2 {question = "여러 사람 간의 의견이나 생각이 일치하는 상태.", example1 = "합의", example2 = "협의"},
        new questionListGame2 {question = "어떤 주장이나 의견에 대해 받아들이는 것.", example1 = "동의", example2 = "찬성"},
        new questionListGame2 {question = "어떤 것을 더 강하게 만들거나 보강하는 것.", example1 = "강화", example2 = "보강"},
        new questionListGame2 {question = "어떤 주제에 대해 깊이 생각하고 반성하는 과정.", example1 = "고찰", example2 = "숙고"},
        new questionListGame2 {question = "어떤 행동이나 말의 의도나 목적.", example1 = "취지", example2 = "목적"},
        new questionListGame2 {question = "특정 행동이나 상황에 제한을 두는 요소.", example1 = "제약", example2 = "제한"},
        new questionListGame2 {question = "어떤 사건이나 변화가 일어나는 중요한 순간.", example1 = "전환점", example2 = "기로"},
        new questionListGame2 {question = "어떤 것이 지닌 상징적 의미나 가치.", example1 = "상징성", example2 = "의미"},
        new questionListGame2 {question = "어떤 사실이나 주장에 대한 진위를 확인하는 과정.", example1 = "검증", example2 = "확인"},
        new questionListGame2 {question = "특정 행동이나 결정을 하려는 계획이나 이유.", example1 = "의도", example2 = "목적"},
        new questionListGame2 {question = "어떤 문제에 대해 깊이 생각하고 고민하는 과정.", example1 = "고민", example2 = "걱정"},
        new questionListGame2 {question = "서로 다른 두 개념이나 상황을 비교하여 차이를 이해하는 것.", example1 = "대비", example2 = "비교"},
        new questionListGame2 {question = "어떤 사건이나 상황이 변화하는 데 영향을 미치는 계기.", example1 = "계기", example2 = "촉발"},
        new questionListGame2 {question = "어떤 계획이나 목표를 실제로 이루는 것.", example1 = "실현", example2 = "실행"},
        new questionListGame2 {question = "특정 목표나 목적에 보탬이 되는 것.", example1 = "기여", example2 = "공헌"},
        new questionListGame2 {question = "어떤 상태나 수준을 계속해서 보존하는 것.", example1 = "유지", example2 = "보존"},
        new questionListGame2 {question = "어려운 상황이나 목표에 맞서 싸우는 것.", example1 = "도전", example2 = "시도"},
        new questionListGame2 {question = "특정 행동이나 아이디어를 제시하는 것.", example1 = "제안", example2 = "권고"},
        new questionListGame2 {question = "새로운 정보나 지식을 찾기 위해 조사하는 과정.", example1 = "탐색", example2 = "조사"},
        new questionListGame2 {question = "여러 요소나 의견을 잘 맞추어 정리하는 것.", example1 = "조율", example2 = "조정"},
        new questionListGame2 {question = "어떤 것보다 먼저 중요하게 여기는 것.", example1 = "우선", example2 = "먼저"},
        new questionListGame2 {question = "특정 목적이나 역할을 수행하는 능력.", example1 = "기능", example2 = "역할"},
        new questionListGame2 {question = "여러 선택 중에서 하나를 선택하는 과정.", example1 = "결정", example2 = "선택"},
        new questionListGame2 {question = "다른 사람이나 정보에 대한 믿음.", example1 = "신뢰", example2 = "믿음"},
        new questionListGame2 {question = "특정한 의견이나 사실을 강하게 내세우는 것.", example1 = "주장", example2 = "주장"},
        new questionListGame2 {question = "어떤 대상을 세부적으로 나누어 살펴보는 과정.", example1 = "분석", example2 = "해석"},
        new questionListGame2 {question = "어떤 일을 적극적으로 추진하려는 태도.", example1 = "적극성", example2 = "활발함"},
        new questionListGame2 {question = "어떤 사건이나 상황의 맥락이나 환경.", example1 = "배경", example2 = "상황"},
        new questionListGame2 {question = "추상적인 개념을 명확하고 구체적인 형태로 표현하는 것.", example1 = "구체화", example2 = "명확화"},
        new questionListGame2 {question = "특정 문제나 상황에 대한 인식과 고민.", example1 = "문제의식", example2 = "인식"},
        new questionListGame2 {question = "어떤 일이 더 빨리 이루어지도록 도와주는 것.", example1 = "촉진", example2 = "자극"},
        new questionListGame2 {question = "일반적으로 해안선과 접하는 큰 물의 집합체로, 대체로 연안 지역을 포함한다.", example1 = "바다", example2 = "대양"},
        new questionListGame2 {question = "지속적이고 깊은 감정으로, 삶에 대한 전반적인 만족감을 포함한다.", example1 = "행복", example2 = "즐거움"},
        new questionListGame2 {question = "여러 페이지로 구성된 인쇄물로, 특정 주제를 다루거나 이야기를 담고 있다.", example1 = "책", example2 = "문서"},
        new questionListGame2 {question = "특정한 멜로디와 가사를 가진 곡으로, 감정을 표현하거나 이야기를 전달하는 형태이다.", example1 = "노래", example2 = "음악"},
        new questionListGame2 {question = "상대적으로 많은 인구와 다양한 시설이 있는 지역으로, 경제적, 문화적 중심 역할을 한다.", example1 = "도시", example2 = "마을"},
        new questionListGame2 {question = "깊은 감정이나 유대감을 의미하며, 개인 간의 친밀한 관계를 포함한다.", example1 = "사랑", example2 = "애정"},
        new questionListGame2 {question = "식용 가능한 과일로, 단맛과 신맛이 혼합된 맛을 가진다.", example1 = "사과", example2 = "과일"},
        new questionListGame2 {question = "여러 가지 중에서 하나를 고르는 행위나 그 중의 하나.", example1 = "선택", example2 = "선택지"},
        new questionListGame2 {question = "단어의 의미나 발음을 정리한 책으로, 언어를 배우는 데 사용된다.", example1 = "사전", example2 = "어휘집"},
        new questionListGame2 {question = "이야기를 시각적으로 표현한 예술 작품으로, 연속된 이미지와 소리로 구성된다.", example1 = "영화", example2 = "필름"},
        new questionListGame2 {question = "새로운 장소를 방문하거나 발견하기 위한 이동 활동을 의미한다.", example1 = "여행", example2 = "탐험"},
        new questionListGame2 {question = "환자를 치료하거나 건강 상담을 하는 전문 직업인이다.", example1 = "의사", example2 = "의료인"},
        new questionListGame2 {question = "신체를 움직여 건강을 유지하거나 체력을 단련하는 활동이다.", example1 = "운동", example2 = "체육"},
        new questionListGame2 {question = "시각적인 매체를 통해 창의성을 표현하는 활동이나 작품을 의미한다.", example1 = "미술", example2 = "예술"},
        new questionListGame2 {question = "자연 현상이나 법칙을 연구하고 설명하는 학문 분야이다.", example1 = "과학", example2 = "자연학"},
        new questionListGame2 {question = "사람이 섭취하는 모든 종류의 식품을 총칭하는 용어이다.", example1 = "음식", example2 = "요리"},
        new questionListGame2 {question = "창의적 활동을 통해 만들어진 결과물로, 감정이나 메시지를 전달하는 데 사용된다.", example1 = "작품", example2 = "예술작품"},
        new questionListGame2 {question = "사실이나 지식으로, 분석하거나 해석할 수 있는 기초가 된다.", example1 = "정보", example2 = "데이터"},
        new questionListGame2 {question = "서로의 감정적 유대감과 친근함을 의미하는 관계의 특성이다.", example1 = "우정", example2 = "친밀감"},
        new questionListGame2 {question = "특정한 기능을 수행하도록 설계된 구조물이나 장비를 의미한다.", example1 = "기계", example2 = "장치"},
        new questionListGame2 {question = "인간의 개입이 없는 상태에서 존재하는 생태계와 그 구성 요소를 포함한다.", example1 = "자연", example2 = "환경"},
        new questionListGame2 {question = "신체적, 정신적, 사회적으로 완전한 상태를 나타내는 개념이다.", example1 = "건강", example2 = "웰빙"},
        new questionListGame2 {question = "개인의 행동이나 선택에 영향을 미치는 깊은 믿음이나 기준을 의미한다.", example1 = "신념", example2 = "가치관"},
        new questionListGame2 {question = "의사소통을 위한 기호 체계로, 특정한 규칙과 문법을 따른다.", example1 = "언어", example2 = "방언"},
        new questionListGame2 {question = "무한한 가능성을 탐색하거나 새로운 아이디어를 생성하는 능력을 의미한다.", example1 = "상상", example2 = "창의성"}
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
        scoreText.text = "점수:" + score.ToString();

        if (nowPhase == 4)
            currentSpeed = 0;

        portal.transform.Rotate(0, 0, 100 * Time.deltaTime);

        float imagenormalizedSpeed = (currentSpeed - 5f) / (45f - 5f);
        float targetRotationZ = Mathf.Lerp(132.482f, -42.458f, imagenormalizedSpeed);
        float shake = Mathf.Sin(Time.time * 0.5f) * 10f;
        float finalRotationZ = targetRotationZ + shake;
        dashboardPin.localEulerAngles = new Vector3(0, 0, finalRotationZ + Random.Range(-7f, 7f));

        if (Application.platform == RuntimePlatform.Android && nowPhase < 4) {
            if (Input.GetKey(KeyCode.Escape))
                ReturnMainMenu();
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

    public void StageClear() {
        nowPhase = 4;
        stageClear.SetActive(true);
        stageClearPhase1Question.text = problemList[randomIndex1].question;
        stageClearPhase2Question.text = problemList[randomIndex2].question;
        stageClearScore.text = "얻은점수: " + score.ToString();
        if (phase1Answer == 1)
            stageClearPhase1Answer.text = "→" + problemList[randomIndex1].example1;
        else
            stageClearPhase1Answer.text = "→" + problemList[randomIndex1].example2;
        if (phase2Answer == 1)
            stageClearPhase2Answer.text = "→" + problemList[randomIndex2].example1;
        else
            stageClearPhase2Answer.text = "→" + problemList[randomIndex2].example2;
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