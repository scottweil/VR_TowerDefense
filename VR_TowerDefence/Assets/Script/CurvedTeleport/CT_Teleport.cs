using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_Teleport : MonoBehaviour
{
    public Transform hand;
    public LineRenderer lr;
    public OVRInput.Controller controller;
    public float gravity = -9.81f;
    public float jumpPower = 5;
    float yVelocity;
    public float curveDeltaTime = 1 / 60f;
    int maxPoint = 1000;
    int pointCount = 0;
    Vector3 pos;
    void Start()
    {

    }

    private void Update()
    {

        pointCount = 0;
        velocity = hand.forward * jumpPower;
        pos = hand.transform.position;
        lr.positionCount = 2;

        for (int i = 0; i < maxPoint; i++)
        {
            if (false == MakeCurve())
            {

                if (OVRInput.GetDown(OVRInput.Button.One, controller))
                {
                    transform.position = hitInfo.point;
                }
            }
            break;
        }
        lr.positionCount = pointCount;
    }
    Vector3 velocity;
    RaycastHit hitInfo;
    bool MakeCurve()
    {



        //첫번째 점은 레이를 쏘지 않겠다.(예외처리)
        if (pointCount == 0)
        {
            lr.positionCount = pointCount + 1;
            lr.SetPosition(pointCount, pos);
            ++pointCount;
            return true;
        }

        velocity += gravity * Vector3.up * curveDeltaTime;

        pos += velocity * curveDeltaTime;

        //이전 경유지에서 새로운 경유지로 레이를 쏘고 싶다.
        Vector3 prevPos = lr.GetPosition(pointCount - 1);
        Vector3 rayDir = pos - prevPos;
        Ray ray = new Ray(prevPos, rayDir);

        if (Physics.Raycast(ray, out hitInfo, rayDir.magnitude))
        {
            lr.positionCount = pointCount + 1;
            lr.SetPosition(pointCount, hitInfo.point);
            ++pointCount;
            return false;
        }
        else
        {
            lr.positionCount = pointCount + 1;
            lr.SetPosition(pointCount, pos);
            ++pointCount;
            return true;
        }

    }
}
