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
    //���̽�ƽ �߰��ϰ� �ӵ��� �Ų����� ������, ī�޶� ���� �� fov�� ������ �� ������ �ؼ� ������ �����̴°� ó���ϰ�, ���� �������� �ؾ���
}
