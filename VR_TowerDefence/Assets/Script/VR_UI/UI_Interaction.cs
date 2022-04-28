using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Ray를 쏘고 
//오른쪽 컨트롤러의 One버튼을 누르고
//부딪힌 것이 버튼이라면
//버튼의 onclick을 호출하고 싶다.
public class UI_Interaction : MonoBehaviour
{
    void Start()
    {

    }

    public Transform hand;
    public LineRenderer lr;
    public OVRInput.Controller controller;
    Button btn;
    GameObject firstBtn;
    void Update()
    {
        Ray ray = new Ray(hand.position, hand.forward);
        RaycastHit hitInfo;
        lr.SetPosition(0, ray.origin);
        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);
            btn = hitInfo.transform.gameObject.GetComponent<Button>();
            if (btn)
            {
                if (OVRInput.GetDown(OVRInput.Button.One, controller))
                {

                    firstBtn = hitInfo.transform.gameObject;

                    PointerEventData eData = new PointerEventData(EventSystem.current); //씬에 있는 EventSystem
                    btn.OnPointerDown(eData);

                }

                else if (OVRInput.GetUp(OVRInput.Button.One, controller))
                {
                    PointerEventData eData = new PointerEventData(EventSystem.current); //씬에 있는 EventSystem
                    btn.OnPointerUp(eData);
                    if (hitInfo.transform.gameObject == firstBtn)
                    {
                        btn.OnPointerClick(eData);
                    }
                }
            }
        }
        else
        {
            lr.SetPosition(1, ray.origin + ray.direction * 10);
        }
    }
}
