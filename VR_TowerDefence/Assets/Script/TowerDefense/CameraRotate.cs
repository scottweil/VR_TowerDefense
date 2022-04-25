using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//사용자가 마우스를 움직이면 x, y축의 방향으로 회전하고 싶다.
public class CameraRotate : MonoBehaviour
{
    float rx;
    float ry;
    float rotSpeed = 200;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        //x축의 회전은 -90 ~ 90으로 제한하고 싶다.
        rx += rotSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;
        ry += rotSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;

        rx = Mathf.Clamp(rx, -90, 90);

        gameObject.transform.eulerAngles = new Vector3(-rx, ry, 0);
    }
}
