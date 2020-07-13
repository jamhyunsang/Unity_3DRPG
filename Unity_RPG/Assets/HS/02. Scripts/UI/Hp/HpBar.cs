using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{

    //hp이미지 컴포넌트
    Image hp;
    //player 컴포넌트
    private PlayerState ps;
    //player 가져오기
    public GameObject player;

    // Start is called before the first frame update
    private void Start()
    {
        hp = GetComponent<Image>();
        ps = player.GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        hp.fillAmount = (float)ps.curHp / ps.maxHp;
    }
}
