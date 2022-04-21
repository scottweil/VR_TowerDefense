using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//마우스 오른쪽 버튼을 누르고 있으면 조준하고 떼면 그 곳으로 이동하고 싶다.
public class Teleport : MonoBehaviour
{
    public Transform hand;
    public LineRenderer lr;

    bool canTeleport;
    void Update()
    {
        Ray ray = new Ray(hand.position, hand.forward);
        RaycastHit hitInfo = new RaycastHit(); //hiInfo는 Struct, 지역변수로 사용할 때 빈값이라도 할당해줘야함.
        lr.SetPosition(0, ray.origin);
        bool isSuccess = false;

        if (canTeleport)
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                lr.SetPosition(1, hitInfo.point);
                //만약 부딪힌 것이 타워라면 조준성공
                //collider : 콜라이더를 가지고 있는 오브젝트를 찾음.
                //transform : rigidbody를 가지고 있는 오브젝트를 찾음.
                if (hitInfo.collider.CompareTag("Tower"))
                {
                    isSuccess = true;
                }
            }
        }
        else
        {
            //조준 실패(허공)
            lr.SetPosition(1, ray.origin + ray.direction * 100);
        }
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            lr.enabled = true;
            canTeleport = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            lr.enabled = false;
            canTeleport = false;
            if (isSuccess)
            {
                transform.position = hitInfo.transform.position;
            }
        }
    }
}
