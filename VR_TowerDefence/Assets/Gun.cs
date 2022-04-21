using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//마우스 왼쪽버튼을 누르면 hand에서 hand의 앞방향으로 Ray를 쏘고 부딪힌 곳에 총알흔적공장에서 총알흔적을 만들어서 배치하고 싶다.
public class Gun : MonoBehaviour
{
    public Transform hand;
    public GameObject bulletFactory;
    public LineRenderer lr;
    public Transform crosshair;
    Vector3 crosshairOriginScale;
    public float kAdjust = 1;

    private void Start()
    {
        crosshairOriginScale = crosshair.localScale;
    }
    void Update()
    {
        Ray ray = new Ray(hand.position, hand.forward);
        RaycastHit hitInfo;

        lr.SetPosition(0, ray.origin);

        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);

            crosshair.localScale = crosshairOriginScale * hitInfo.distance * kAdjust;
            crosshair.position = hitInfo.point;
            crosshair.forward = hitInfo.normal;
            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
            {
                GameObject bullet = Instantiate(bulletFactory);
                bullet.transform.position = hitInfo.point;
                bullet.transform.forward = hitInfo.normal;
            }
        }
        else
        {
            lr.SetPosition(1, ray.origin + ray.direction * 100);
            crosshair.position = ray.origin + ray.direction * 100;
            crosshair.forward = ray.direction;
            crosshair.localScale = crosshairOriginScale * 100 * kAdjust;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 from = hand.position;
        Vector3 to = hand.position + hand.forward * Mathf.Infinity;
        Gizmos.DrawLine(from, to);
    }
}
