using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//만약 인덱스트리거가 눌렸다면
//내 손을 기준으로 반경 0.5m 안에 충돌체들을 가져오고 싶다.
//그 충돌체 중에 잡을 수 있는 물체가 있다면 잡고 싶다.
//그렇지 않고 인덱스트리거를 떼면
//잡고 있는 물체가 있다면 놓고 싶다. 
public class MYGrabberIndexTrigger : MyGrabberBase
{
    LineRenderer lr;
    GameObject grabObject = null;
    public OVRInput.Controller hand;
    public float kAdjustForce = 3;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }


    void Update()
    {

        if (grabObject == null)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hitInfo;
            bool isHit = Physics.Raycast(ray, out hitInfo);
            lr.SetPosition(0, ray.origin);
            if (isHit)
            {
                lr.SetPosition(1, hitInfo.point);
            }
            else
            {
                lr.SetPosition(1, ray.origin + ray.direction * 10);
            }

            if (isHit && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, hand))
            {

                MyGrabbable mg = hitInfo.transform.GetComponent<MyGrabbable>();
                if (mg)
                {
                    //만약 이미 잡고 있는 물체라면 그 손에게 놓으라고 해야 함

                    mg.Release();
                    grabObject = hitInfo.transform.gameObject;
                    grabObject.transform.parent = transform;
                    mg.hand = this;
                    lr.enabled = false;
                    mg.Catch(this);

                }
            }
        }
        else
        {
            grabObject.transform.position = Vector3.Lerp(grabObject.transform.position, transform.position, Time.deltaTime * 5);
            grabObject.transform.rotation = Quaternion.Lerp(grabObject.transform.rotation, transform.rotation, Time.deltaTime * 10);
            // grabObject.transform.forward = transform.forward;

            if (OVRInput.GetDown(OVRInput.Button.One, hand))
            {
                MyGrabbable mg = grabObject.GetComponent<MyGrabbable>();
                if (mg)
                {
                    mg.ToDo();
                }
            }
            else if (OVRInput.GetDown(OVRInput.Button.Two, hand))
            {
                MyGrabbable mg = grabObject.GetComponent<MyGrabbable>();
                if (mg)
                {
                    mg.Restore();
                }
            }
            else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, hand))
            {
                grabObject.transform.parent = null;
                Rigidbody rb = grabObject.GetComponent<Rigidbody>();
                rb.velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch) * kAdjustForce;
                rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);

                MyGrabbable mg = grabObject.GetComponent<MyGrabbable>();
                if (mg)
                {
                    mg.Release();
                    mg.Catch(null);
                }
            }
        }

    }

    override public void PutDown()
    {
        //기억하고 있던 grabObject를 잊어야 한다.
        grabObject = null;
        lr.enabled = true;
    }
}
