using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class EnemyHit : MonoBehaviour
{
    //적리스폰 컴포넌트
    private PlayerState ps;

    private void Start()
    {
        ps = GameObject.Find("Player").GetComponent<PlayerState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ps.state == PlayerS.Attack)
        {
            gameObject.GetComponent<EnemyState>().hitDamage(20);
            Debug.Log("충돌");
        }

    }
}
