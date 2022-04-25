using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrabbable : MonoBehaviour
{
    [HideInInspector]
    public MyGrabberBase hand;
    virtual public void Catch(MyGrabberBase whereHand)
    {
        //잡은 손을 기억하겠다.
        hand = whereHand;
    }
    virtual public void Release()
    {
        if (hand)
        {
            hand.PutDown();
        }
    }
    virtual public void ToDo()
    {

    }
    virtual public void Restore()
    {

    }
}
