using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGun : MyGrabbable
{
    LineRenderer lr;
    public Transform firePosition;
    Rigidbody rb;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Ray ray = new Ray(firePosition.position, firePosition.forward);
        RaycastHit hitInfo;
        lr.SetPosition(0, ray.origin);
        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);
        }
        else
        {
            lr.SetPosition(1, ray.origin + ray.direction * 10);
        }

    }

    override public void Catch(MyGrabberBase whereHand)
    {
        base.Catch(whereHand);
        if (whereHand)
        {
            lr.enabled = true;
            rb.isKinematic = true;
        }
    }
    override public void Release()
    {
        base.Release();
        lr.enabled = false;
        rb.isKinematic = false;
    }
    public GameObject bulletFactory;
    public int maxBulletCount = 20;
    int bulletCount = 0;
    override public void ToDo()
    {
        if (bulletCount > maxBulletCount) return;

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;
        bool isHit = Physics.Raycast(ray, out hitInfo);
        if (isHit)
        {
            bulletCount++;
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = hitInfo.point;
            bullet.transform.forward = hitInfo.normal;
        }
    }
    override public void Restore()
    {
        bulletCount = 0;
    }
}
