using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyRespawn : MonoBehaviour
{


    //리스폰 되는 시간
    private float respawnTime = 5.0f;
    //시간
    private float spawnTime;
    //스폰되는 장소
    public Transform[] points;
    //스폰되는 몬스터들
    public GameObject slime;
    public GameObject turtle;
    //스폰되는 최대 몬스터 수
    private int maxSpawn = 10;
    //현재 소환된 몬스터 수
    public int curSpawn = 0;


    private void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        Respawn();    
    }

    private void Respawn()
    { 
        if(curSpawn < maxSpawn)
        {
            spawnTime += Time.deltaTime;
            if(spawnTime > respawnTime)
            {
                spawnTime = 0.0f;
                int spwanIndex = Random.Range(0, points.Length);
                int monsterIndex = Random.Range(0, 2);
                switch(monsterIndex)
                {
                    case 0:
                        Instantiate(slime, points[spwanIndex].position, points[spwanIndex].rotation);
                        curSpawn++;
                        break;
                    case 1:
                        Instantiate(turtle, points[spwanIndex].position, points[spwanIndex].rotation);
                        curSpawn++;
                        break;
                }
            }
        }
    }

    public void Die()
    {
        curSpawn--;
        Quest.instance.hunting();
    }
}
