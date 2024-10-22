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
        new questionListGame1 {question = "대기 중 이산화탄소 는 지속적으로 증가하고 있다.", answer = "농도", example = new List<char>{'심', '시'}},
        new questionListGame1 {question = "기후 변화는 인류의 생존에 한 위협이 된다.", answer = "심각", example = new List<char>{'기', '출'}},
        new questionListGame1 {question = "북극의 빙하가 하게 녹고 있다.", answer = "속도", example = new List<char>{'빠', '름'}},
        new questionListGame1 {question = "대체 에너지 사용은 탄소 배출을 는 데 도움이 된다.", answer = "도움", example = new List<char>{'경', '지'}},
        new questionListGame1 {question = "교육과 인식 개선은 지구온난화 대응의 이다.", answer = "첫걸음", example = new List<char>{'기', '초', '질'}},
        new questionListGame1 {question = "생태계의 회복력은 온난화에 대한 을 좌우한다.", answer = "저항력", example = new List<char>{'강', '도', '력'}},
        new questionListGame1 {question = "온난화로 인한 는 피해를 증가시킨다.", answer = "자연재해", example = new List<char>{'노', '력', '부', '족'}},
        new questionListGame1 {question = "개인의 탄소 발자국 줄이기는 중요한  방법이다.", answer = "실천", example = new List<char>{'지', '력'}},
        new questionListGame1 {question = "지속 가능한 개발은 지구온난화 해결의 이다.", answer = "열쇠", example = new List<char>{'해', '결'}},
        new questionListGame1 {question = "지구온난화는 기후 변화의 을 좌우한다.", answer = "원인", example = new List<char>{'결', '과'}},
        new questionListGame1 {question = "온실가스의 은 지구온난화를 가속화한다.", answer = "배출", example = new List<char>{'상', '추'}},
        new questionListGame1 {question = "기온 상승은 생태계에 한 영향을 미친다.", answer = "영향", example = new List<char>{'양', '화'}},
        new questionListGame1 {question = "생물 다양성 감소는 생태계의 을 초래한다.", answer = "불안정", example = new List<char>{'해', '수', '면'}},
        new questionListGame1 {question = "지구온난화는 농업 생산성에 한 영향을 미친다.", answer = "부정적", example = new List<char>{'해', '지', '운'}},
        new questionListGame1 {question = "지구온난화는 인 기후 현상을 증가시킨다.", answer = "극단적", example = new List<char>{'심', '노', '한'}},
        new questionListGame1 {question = "국제 협력은 지구온난화 해결에 이다.", answer = "필수", example = new List<char>{'중', '요', '학'}},
        new questionListGame1 {question = "개인의 노력도 지구온난화 완화에 한 역할을 한다.", answer = "역할", example = new List<char>{'지', '여'}},
        new questionListGame1 {question = "신선한 재료는 요리의 을 좌우한다.", answer = "맛", example = new List<char>{'신'}},
        new questionListGame1 {question = "요리의 은 창의성을 요구한다.", answer = "과정", example = new List<char>{'기', '주'}},
        new questionListGame1 {question = "조리 방법에 따라 음식의 이 달라진다.", answer = "질감", example = new List<char>{'지', '우'}},
        new questionListGame1 {question = "향신료는 요리에 를 더하는 역할을 한다.", answer = "풍미", example = new List<char>{'향', '자'}},
        new questionListGame1 {question = "요리의 은 맛의 균형을 결정짓는다.", answer = "비율", example = new List<char>{'주', '교'}},
        new questionListGame1 {question = "제철 재료는 요리의 을 높인다.", answer = "신선함", example = new List<char>{'품', '질', '우'}},
        new questionListGame1 {question = "각국의 은 문화의 다양성을 반영한다.", answer = "요리법", example = new List<char>{'전', '통', '미'}},
        new questionListGame1 {question = "고온에서 조리된 음식은 한 식감을 제공한다.", answer = "크리스피", example = new List<char>{'바', '삭', '함', '이'}},
        new questionListGame1 {question = "식재료의 는 요리의 완성도를 높인다.", answer = "조화", example = new List<char>{'균', '허'}},
        new questionListGame1 {question = "소스는 요리의 을 살리는 중요한 요소이다.", answer = "특성", example = new List<char>{'개', '지'}},
        new questionListGame1 {question = "수프의 는 맛을 느끼는 데 영향을 준다.", answer = "온도", example = new List<char>{'뜨', '겁'}},
        new questionListGame1 {question = "디저트는 식사의 를 장식하는 역할을 한다.", answer = "마무리", example = new List<char>{'종', '료', '어'}},
        new questionListGame1 {question = "요리의 은 재료의 성질에 따라 달라진다.", answer = "시간", example = new List<char>{'조', '요'}},
        new questionListGame1 {question = "채소는 요리의 을 보충하는 역할을 한다.", answer = "비타민", example = new List<char>{'영', '영', '소'}},
        new questionListGame1 {question = "다양한 을 활용하면 요리의 변화를 줄 수 있다.", answer = "기법", example = new List<char>{'고', '저'}},
        new questionListGame1 {question = "프리미엄 재료는 요리의 를 높여준다.", answer = "퀄리티", example = new List<char>{'수', '준', '조'}},
        new questionListGame1 {question = "요리의 은 시각적인 즐거움을 더한다.", answer = "장식", example = new List<char>{'비', '주'}},
        new questionListGame1 {question = "불 조절은 요리의 에 중요한 요소이다.", answer = "성공", example = new List<char>{'경', '과'}},
        new questionListGame1 {question = "저온 조리는 재료의 을 살리는 데 효과적이다.", answer = "맛", example = new List<char>{'미'}},
        new questionListGame1 {question = "요리에서의 는 완벽한 결과를 가져온다.", answer = "인내", example = new List<char>{'언', '나'}},
        new questionListGame1 {question = "빛의 는 우주에서 가장 빠르다.", answer = "속도", example = new List<char>{'구', '두'}},
        new questionListGame1 {question = "음악의 이 사람의 감정을 움직인다.", answer = "리듬", example = new List<char>{'지', '름'}},
        new questionListGame1 {question = "꿈의 은 열정과 노력이 필요하다.", answer = "실현", example = new List<char>{'설', '치'}},
        new questionListGame1 {question = "자연의 는 계절에 따라 다르게 나타난다.", answer = "변화", example = new List<char>{'오', '잉'}},
        new questionListGame1 {question = "과학의 은 인류의 진보에 기여한다.", answer = "발견", example = new List<char>{'소', '보'}},
        new questionListGame1 {question = "우주의 는 인류의 끊임없는 탐구 대상이다.", answer = "신비", example = new List<char>{'슬', '러'}},
        new questionListGame1 {question = "글쓰기의 은 자신을 이해하는 방법이기도 하다.", answer = "과정", example = new List<char>{'리', '오'}},
        new questionListGame1 {question = "여행의 은 새로운 경험을 통해 발견된다.", answer = "의미", example = new List<char>{'우', '마'}},
        new questionListGame1 {question = "과거의 은 현재의 선택에 영향을 미친다.", answer = "교훈", example = new List<char>{'지', '험'}},
        new questionListGame1 {question = "인공지능의 은 미래 사회를 변화시킬 것이다.", answer = "발전", example = new List<char>{'지', '정'}},
        new questionListGame1 {question = "사회의 은 다양한 요소들이 얽혀 있다.", answer = "복잡성", example = new List<char>{'혼', '합', '물'}},
        new questionListGame1 {question = "신뢰의 은 오랜 시간이 걸린다.", answer = "형성", example = new List<char>{'행', '정'}},
        new questionListGame1 {question = "사랑의 은 이해와 배려에 있다.", answer = "본질", example = new List<char>{'뿐', '지'}},
        new questionListGame1 {question = "교육의 는 개인의 성장에 크게 기여한다.", answer = "가치", example = new List<char>{'같', '이'}},
        new questionListGame1 {question = "상상의 는 무한한 가능성을 제공한다.", answer = "세계", example = new List<char>{'네', '개'}},
        new questionListGame1 {question = "기술의 은 우리의 삶을 변화시키고 있다.", answer = "응용", example = new List<char>{'아', '니'}},
        new questionListGame1 {question = "인간의 은 문제 해결의 열쇠가 된다.", answer = "창의성", example = new List<char>{'신', '비', '적'}},
        new questionListGame1 {question = "스포츠의 은 도전과 협력에 있다.", answer = "정신", example = new List<char>{'장', '실'}},
        new questionListGame1 {question = "인생의 은 끊임없는 배움의 연속이다.", answer = "여정", example = new List<char>{'배', '울'}},
        new questionListGame1 {question = "아침 이 창문을 통해 방 안을 가득 채운다.", answer = "햇살", example = new List<char>{'해', '쌀'}},
        new questionListGame1 {question = "바람에 흔들리는 나무의 가 마음을 편안하게 만든다.", answer = "소리", example = new List<char>{'눈', '알'}},
        new questionListGame1 {question = "책을 읽는 것은 새로운 세계로의 과 같다.", answer = "여행", example = new List<char>{'어', '항'}},
        new questionListGame1 {question = "친구와의 는 언제나 즐거운 순간을 선사한다.", answer = "대화", example = new List<char>{'책', '상'}},
        new questionListGame1 {question = "비 오는 날, 창밖의 풍경을 바라보며 에 잠긴다.", answer = "사색", example = new List<char>{'샤', '쇅'}},
        new questionListGame1 {question = "커피 한 잔의 가 하루를 시작하는 힘이 된다.", answer = "여유", example = new List<char>{'분', '주'}},
        new questionListGame1 {question = "생각하는 과정은 창의력을 발휘할 수 있는 좋은 다.", answer = "기회", example = new List<char>{'가', '휘'}},
        new questionListGame1 {question = "은 감정을 표현하는 가장 아름다운 방법 중 하나다.", answer = "음악", example = new List<char>{'으', '막'}},
        new questionListGame1 {question = "길을 걷다 보면 예상치 못한 을 발견하기도 한다.", answer = "즐거움", example = new List<char>{'즈', '걸', '몽'}},
        new questionListGame1 {question = "작은 일에도 하는 마음을 잊지 않으려 한다.", answer = "감사", example = new List<char>{'걈', '샹'}},
        new questionListGame1 {question = "가족과의 소중한 은 무엇보다 가치가 있다.", answer = "시간", example = new List<char>{'인', '물'}},
        new questionListGame1 {question = "새로운 를 시작하는 것은 항상 설레는 일이다.", answer = "취미", example = new List<char>{'특', '기'}},
        new questionListGame1 {question = "일상 속의 소소한 을 찾아보는 것도 좋다.", answer = "행복", example = new List<char>{'음', '식'}},
        new questionListGame1 {question = "꿈을 이루기 위해서는 꾸준한 이 필요하다.", answer = "노력", example = new List<char>{'죽', '음'}},
        new questionListGame1 {question = "해가 지는 은 언제나 감동적이다.", answer = "풍경", example = new List<char>{'푸', '겅'}},
        new questionListGame1 {question = "을 떠나면 새로운 인연을 만나게 된다.", answer = "여행", example = new List<char>{'어', '향'}},
        new questionListGame1 {question = "그리운 사람의 를 떠올리면 마음이 따뜻해진다.", answer = "미소", example = new List<char>{'무', '수'}},
        new questionListGame1 {question = "은 삶을 더욱 풍요롭게 만들어주는 힘이다.", answer = "사랑", example = new List<char>{'가', '마'}},
        new questionListGame1 {question = "오늘 아침에는 이 맑아서 기분이 좋았다.", answer = "하늘", example = new List<char>{'허', '눌'}},
        new questionListGame1 {question = "와 함께 커피를 마시며 오랜 이야기를 나눴다.", answer = "친구", example = new List<char>{'찬', '고'}},
        new questionListGame1 {question = "저녁 식사로 를 만들기로 했다.", answer = "피자", example = new List<char>{'패', '자'}},
        new questionListGame1 {question = "새로운 을 읽기 시작했는데, 매우 흥미롭다.", answer = "책", example = new List<char>{'췤'}},
        new questionListGame1 {question = "주말에는 가족과 함께 을 계획하고 있다.", answer = "여행", example = new List<char>{'어', '항'}},
        new questionListGame1 {question = "을 꾸준히 하니 몸이 점점 건강해진다.", answer = "운동", example = new List<char>{'완', '두'}},
        new questionListGame1 {question = "이 되니 나무의 잎이 아름다운 색으로 변했다.", answer = "가을", example = new List<char>{'거', '울'}},
        new questionListGame1 {question = "오늘은 집에서 를 보기로 했다.", answer = "영화", example = new List<char>{'주', '여'}},
        new questionListGame1 {question = "의 파도 소리가 마음을 편안하게 해준다.", answer = "바다", example = new List<char>{'받', '아'}},
        new questionListGame1 {question = "요즘 일찍 일어나는 을 들이고 있다.", answer = "습관", example = new List<char>{'해', '빗'}},
        new questionListGame1 {question = "친구가 생일이라서 특별한 를 준비했다.", answer = "선물", example = new List<char>{'성', '줄'}},
        new questionListGame1 {question = "새로운 로 그림 그리기를 시작했다.", answer = "취미", example = new List<char>{'추', '모'}},
        new questionListGame1 {question = "매일 조금씩 를 공부하고 있다.", answer = "영어", example = new List<char>{'공', '부'}},
        new questionListGame1 {question = "주말마다 를 타며 자연을 만끽한다.", answer = "자전거", example = new List<char>{'장', '작', '불'}},
        new questionListGame1 {question = "하는 것이 스트레스를 푸는 데 도움이 된다.", answer = "요리", example = new List<char>{'욜', '이'}},
        new questionListGame1 {question = "카페에서 좋아하는 을 들으며 시간을 보냈다.", answer = "음악", example = new List<char>{'으', '막'}},
        new questionListGame1 {question = "매일 저녁 을 하며 생각을 정리한다.", answer = "산책", example = new List<char>{'상', '췤'}},
        new questionListGame1 {question = "이번 달에는 새로운 을 배우기로 결심했다.", answer = "기술", example = new List<char>{'가', '정'}},
        new questionListGame1 {question = "좋은 를 보면 항상 감동받는다.", answer = "영화", example = new List<char>{'여', '희'}},
        new questionListGame1 {question = "을 이루기 위해 꾸준히 노력하는 것이 중요하다.", answer = "꿈", example = new List<char>{'굼'}},
        new questionListGame1 {question = "인간의 은 세상을 이해한다.", answer = "지각", example = new List<char>{'꼬', '리'}},
        new questionListGame1 {question = "과학의 은 검증과 수정이 필요하다.", answer = "이론", example = new List<char>{'오', '리'}},
        new questionListGame1 {question = "문화의 는 사회적 요인에 따라 달라진다.", answer = "변화", example = new List<char>{'해', '서'}},
        new questionListGame1 {question = "정보의 은 협업에 중요하다.", answer = "소통", example = new List<char>{'바', '다'}},
        new questionListGame1 {question = "지속 가능한 은 환경과 경제를 고려한다.", answer = "개발", example = new List<char>{'여', '해'}},
        new questionListGame1 {question = "인간의 은 복잡한 반응이다.", answer = "감정", example = new List<char>{'간', '장'}},
        new questionListGame1 {question = "기술의 은 윤리적 문제를 제기한다.", answer = "발전", example = new List<char>{'감', '정'}},
        new questionListGame1 {question = "생태계의 은 생물의 상호작용으로 유지된다.", answer = "균형", example = new List<char>{'구', '호'}},
        new questionListGame1 {question = "글로벌화는 의 동질화를 초래한다.", answer = "문화", example = new List<char>{'뭐', '해'}},
        new questionListGame1 {question = "아침에 일어나서 시원한 를 마셨다.", answer = "공기", example = new List<char>{'바', '람'}},
        new questionListGame1 {question = "와 함께 영화를 보러 갔다.", answer = "친구", example = new List<char>{'치', '누'}},
        new questionListGame1 {question = "주말에는 항상 를 하며 시간을 보낸다.", answer = "독서", example = new List<char>{'도', '거'}},
        new questionListGame1 {question = "오늘 저녁에는 를 요리할 예정이다.", answer = "스파게티", example = new List<char>{'습', '하', '겠', '디'}},
        new questionListGame1 {question = "바람이 불어 기분이 했다.", answer = "상쾌", example = new List<char>{'사', '괘'}},
        new questionListGame1 {question = " 준비를 하느라 바쁜 하루였다.", answer = "여행", example = new List<char>{'어', '항'}},
        new questionListGame1 {question = "자연 속에서 하는 것이 정말 좋다.", answer = "산책", example = new List<char>{'사', '췤'}},
        new questionListGame1 {question = "새로 생긴 에서 디저트를 맛봤다.", answer = "카페", example = new List<char>{'가', '패'}},
        new questionListGame1 {question = "일주일 동안 꾸준히 을 했다.", answer = "운동", example = new List<char>{'우', '도'}},
        new questionListGame1 {question = "저녁 이 아름다워 사진을 찍었다.", answer = "노을", example = new List<char>{'농', '부'}},
        new questionListGame1 {question = "친구에게 를 써서 감정을 전했다.", answer = "편지", example = new List<char>{'평', '쥐'}},
        new questionListGame1 {question = "새로운 를 배우는 것이 재미있다.", answer = "언어", example = new List<char>{'연', '고'}},
        new questionListGame1 {question = "요즘들어 더 많은 시간을 과 보내고 있다.", answer = "가족", example = new List<char>{'작', '자'}},
        new questionListGame1 {question = " 한 잔과 함께 하루를 시작하는 것이 좋다.", answer = "커피", example = new List<char>{'거', '지'}},
        new questionListGame1 {question = "수영장에서 시간을 보내며 를 피했다.", answer = "더위", example = new List<char>{'추', '움'}},
        new questionListGame1 {question = "다양한 을 시도해보는 것이 즐겁다.", answer = "음식", example = new List<char>{'죽', '음'}},
        new questionListGame1 {question = "저녁에 을 보며 소원을 빌었다.", answer = "별", example = new List<char>{'런'}},
        new questionListGame1 {question = "새로운 을 찾아 듣는 것이 취미다.", answer = "음악", example = new List<char>{'으', '막'}},
        new questionListGame1 {question = "주말마다 를 하기로 결정했다.", answer = "자원봉사", example = new List<char>{'지', '위', '보', '상'}},
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
        for (int i = 0; i < 3; i++) {
            showScore.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            showScore.color = Color.white;
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
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame() {
        SceneManager.LoadScene("Game1");
    }
}
