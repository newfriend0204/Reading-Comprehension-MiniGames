using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerGame2 : MonoBehaviour {
    public GameObject vehicle;
    public new GameObject camera;
    public Joystick moveobject;
    private Rigidbody vehicleRigidbody;
    private float currentSpeed = 0f;

    void Awake() {
        Application.targetFrameRate = 60;
        vehicleRigidbody = vehicle.GetComponent<Rigidbody>();
    }

    void Update() {
        camera.transform.position = vehicle.transform.position + new Vector3(0, 4.528f, -19);
        float horizontal = moveobject.Horizontal;
        float vertical = moveobject.Vertical;
        if (vertical > 0)
            currentSpeed += Time.deltaTime * 30;
        else
            currentSpeed -= Time.deltaTime * 10;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, 40);
        vehicleRigidbody.MovePosition(vehicle.transform.position + vehicle.transform.forward * currentSpeed * Time.deltaTime);
        if (horizontal != 0) {
            Quaternion turnRotation = Quaternion.Euler(0f, horizontal * 40 * Time.deltaTime, 0f);
            vehicleRigidbody.MoveRotation(vehicleRigidbody.rotation * turnRotation);
        }
    }
}
