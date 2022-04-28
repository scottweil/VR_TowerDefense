using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKManager : MonoBehaviour
{
    Animator anim;
    public Transform leftTarget;
    public Transform rightTarget;
    public Transform lookTarget;
    public float weight = 1; //0일 때 원래위치 1일 때 타겟위치로
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    //관절 움직임.
    //Inverse Kinematic : 손 끝 움직임을 기준으로 다른 관절이 따라 움직임.
    //Forward Kinematic : 중심 관절에서부터 손 끝으로 관절 움직임을 통제.
    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetLookAtWeight(weight);
        anim.SetLookAtPosition(lookTarget.position);

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftTarget.position);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, weight);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftTarget.rotation);

        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightTarget.position);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, weight);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightTarget.rotation);
    }
}
