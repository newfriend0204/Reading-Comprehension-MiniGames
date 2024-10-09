using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerGame2 : MonoBehaviour {
    public GameObject vehicle;
    public new GameObject camera;

    void Update()
    {
        camera.transform.position = vehicle.transform.position + new Vector3(0, 4.528f, -10);
    }
    //조이스틱 추가하고 속도는 매끄럽게 나가고, 카메라도 뭔가 뭐 fov값 조절할 수 있으면 해서 실제로 움직이는것 처럼하고, 바퀴 굴러가게 해야함
}
