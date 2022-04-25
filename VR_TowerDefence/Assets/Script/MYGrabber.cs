using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//만약 핸드트리거가 눌렸다면
//내 손을 기준으로 반경 0.5m 안에 충돌체들을 가져오고 싶다.
//그 충돌체 중에 잡을 수 있는 물체가 있다면 잡고 싶다.
//그렇지 않고 핸드트리거를 떼면
//잡고 있는 물체가 있다면 놓고 싶다. 
public class MYGrabber : MyGrabberBase
{
    GameObject grabObject = null;
    public float radius = 0.5f;
    public OVRInput.Controller hand;
    public float kAdjustForce = 3;
    void Start()
    {

    }


    void Update()
    {
        if (grabObject == null)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, hand))
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, radius);
                for (int i = 0; i < cols.Length; i++)
                {
                    MyGrabbable mg = cols[i].GetComponent<MyGrabbable>();
                    if (mg)
                    {


                        //만약 이미 잡고 있는 물체라면 그 손에게 놓으라고 해야 함
                        if (mg.hand)
                        {

                            mg.hand.PutDown();

                        }

                        grabObject = cols[i].gameObject;
                        grabObject.transform.parent = transform;
                        mg.hand = this;

                        break;
                    }
                }
            }
        }
        else
        {
            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, hand))
            {
                grabObject.transform.parent = null;
                Rigidbody rb = grabObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch) * kAdjustForce;
                rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);
                grabObject = null;
            }
        }

    }

    override public void PutDown()
    {
        //기억하고 있던 grabObject를 잊어야 한다.
        grabObject = null;
    }
}
