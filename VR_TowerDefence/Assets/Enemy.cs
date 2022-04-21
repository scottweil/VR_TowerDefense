using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//nav mesh agent기능을 이용해서 타워를 향해 이동하고 싶다.
public class Enemy : MonoBehaviour
{
    public enum State
    {
        Search,
        Move,
        Attack
    }
    public State state;
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);
        agent.isStopped = true;
        state = State.Search;
    }


    void Update()
    {
        switch (state)
        {
            case State.Search:
                UpdateSearch();
                break;
            case State.Move:
                UpdateMove();
                break;
            case State.Attack:
                UpdateAttack();
                break;
        }
    }

    private void UpdateAttack()
    {

    }

    Vector3 targetPosition;
    private void UpdateMove()
    {
        //도착했다면
        float remainDistance = Vector3.Distance(transform.position, targetPosition);
        if (remainDistance <= agent.stoppingDistance)
        {
            //공격상태로 전이하고 싶다.
            agent.isStopped = true;
            state = State.Attack;
        }
    }

    // Transform target = null;

    private void UpdateSearch()
    {
        // GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower"); //해당 태그를 가진 오브젝트를 모두 찾아줘.
        // agent.destination = towers[UnityEngine.Random.Range(0, towers.Length)].transform.position;

        //반경 1000미터 안에 타워 충돌체들을 찾고 싶다.
        // Collider[] cols = Physics.OverlapSphere(transform.position, 1000, 1 << LayerMask.NameToLayer("Tower"));
        Collider[] cols = Physics.OverlapSphere(transform.position, 1000);

        // List<Vector3> list = new List<Vector3>();
        // for (int i = 0; i < cols.Length; i++)
        // {
        //     list.Add(cols[i].transform.position);
        // }

        // list.Sort((Vector3 a, Vector3 b) => {
        //     float d1 = Vector3.Distance(transform.position, a);
        //     float d2 = Vector3.Distance(transform.position, b);
        //     return (d1 - d2) == 0 ? 0 : d1 - d2 > 0 ? 1 : -1;
        // });

        // if(list.Count > 0){
        //     state = State.Move;
        //     agent.isStopped = false;
        //     agent.destination = list[0];
        // }

        //그 중에 가까운 타워를 agent의 목적지로 알려주고 싶다.
        int chooseIndex = -1;
        float distance = 1000f;

        for (int i = 0; i < cols.Length; i++)
        {
            if (false == cols[i].gameObject.CompareTag("Tower"))
            {
                continue;
            }

            float temp = Vector3.Distance(transform.position, cols[i].transform.position);
            if (distance > temp)
            {
                distance = temp;
                chooseIndex = i;
            }
        }

        if (chooseIndex != -1)
        {
            state = State.Move;
            agent.isStopped = false;
            targetPosition = cols[chooseIndex].transform.position;
            agent.destination = cols[chooseIndex].transform.position;
        }
    }
}
