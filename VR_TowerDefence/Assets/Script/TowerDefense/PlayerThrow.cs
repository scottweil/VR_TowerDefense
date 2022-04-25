
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//오른쪽 컨트롤러의 Hand버튼을 눌렀을 때
//핸드의 반경 0.5M에 폭탄이 있다면
//폭탄을 생성해서 손에 잡고 싶다.
//오른쪽 컨트롤러의 Hand버튼을 떼면
//잡고있던 물체를 놓고싶다.
//단, 컨트롤러의 속도/회전속도를 물체에 반영해주고 싶다.
//폭탄이 어딘가 부딪히면 범위폭발을 하고싶다. 반경 5m 이내의 적들을 파괴하고 싶다.
public class PlayerThrow : MonoBehaviour
{
    public float radius = 1f;
    public Transform hand;
    GameObject grenade;
    public float kAdjustForce = 1;

    // Update is called once per frame
    void Update()
    {
        if (grenade != null)
        {
            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            {
                grenade.transform.parent = null;
                Rigidbody rb = grenade.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch) * kAdjustForce;
                rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);
                grenade = null;
            }
        }
        else
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            {
                Collider[] cols = Physics.OverlapSphere(hand.position, radius);
                for (int i = 0; i < cols.Length; i++)
                {
                    if (cols[i].gameObject.name.Contains("Grenade"))
                    {
                        grenade = Instantiate(cols[i].gameObject);
                        grenade.transform.parent = hand;
                        break;
                    }
                }
            }
        }
    }
}
