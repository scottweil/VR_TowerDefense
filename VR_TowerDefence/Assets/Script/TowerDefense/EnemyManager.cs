using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//일정시간마다 적공장에서 적을 만들어서 내 위치에 배치하고 싶다.
public class EnemyManager : MonoBehaviour
{
    public GameObject enemyFactory;
    float curTime;
    float createTime;

    void Start()
    {
        createTime = Random.Range(0, 3);
    }


    void Update()
    {
        //시간이 흐르다가
        //현재시간이 생성시간이 되면
        //적공장에서 적을 만들어서
        //현재 위치에 배치를 하고
        //현재시간을 0으로 초기화한다.
        curTime += Time.deltaTime;
        if (curTime > createTime)
        {
            GameObject enemy = Instantiate(enemyFactory);
            enemy.transform.position = transform.position;
            createTime = Random.Range(0, 3);
            curTime = 0;
        }
    }
}
