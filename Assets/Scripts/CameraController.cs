using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public void Update()
    {
        Vector2 mouse = Mouse.current.delta.ReadValue();

        if (!Mathf.Approximately(mouse.x, 0.0f) || !Mathf.Approximately(mouse.y, 0.0f))
        {
            float sensitivity = 2.5f;
            float rotationX = sensitivity * mouse.x * Time.deltaTime;
            float rotationY = sensitivity * mouse.y * Time.deltaTime;

            Vector3 cameraRotation = Camera.main.transform.rotation.eulerAngles;

            cameraRotation.y += rotationX;
            cameraRotation.x -= rotationY;
            Camera.main.transform.rotation = Quaternion.Euler(cameraRotation);
        }
    }


    //Causes very jittery camera movement:
    //public void OnRotationX(InputValue value)
    //{
    //    float mouseX = value.Get<float>();
    //    float horizontalSensitivity = 2.0f;
    //    float rotationX = horizontalSensitivity * mouseX * Time.deltaTime;

    //    Vector3 cameraRotation = Camera.main.transform.rotation.eulerAngles;

    //    cameraRotation.y += rotationX;
    //    Camera.main.transform.rotation = Quaternion.Euler(cameraRotation);
    //}

    //public void OnRotationY(InputValue value)
    //{
    //    float mouseY = value.Get<float>();
    //    float verticalSensitivity = 2.0f;
    //    float rotationY = verticalSensitivity * mouseY * Time.deltaTime;

    //    Vector3 cameraRotation = Camera.main.transform.rotation.eulerAngles;
    //    cameraRotation.x -= rotationY;
    //    Camera.main.transform.rotation = Quaternion.Euler(cameraRotation);

    //}
}
