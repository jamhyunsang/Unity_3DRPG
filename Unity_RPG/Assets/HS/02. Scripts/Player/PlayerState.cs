using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public enum PlayerS
    {
        Idle,
        Move,
        Attack,
        Damage,
        Die
    }

public class PlayerState : MonoBehaviour
{

    #region 변수
    //이동 스피드
    float speed = 3.0f;
    //현재 체력
    public int curHp = 100;
    //최대체력
    public int maxHp = 100;
    //상태를 만든다.
    public PlayerS state;
    //입력받을 변수
    float h;
    float v;

    //스폰 위치
    Vector3 Spawn = new Vector3(60, 0, 1);

    public GameObject firePos;

    Rigidbody rb;

    #endregion

    #region 컴포넌트
    //캐릭터 컨트롤러 컴포넌트
    CharacterController charctrl;
    //애니메이터 컴포넌트
    Animator ani;
    //조이스틱 컴포넌트
    public VariableJoystick stick;
   

    #endregion

    void Start()
    {

        //캐릭터 컨트롤러 컴포넌트 가져오기
        charctrl = GetComponent<CharacterController>();
        //애니메이터 컴포넌트 가져오기
        ani = GetComponent<Animator>();
        state = PlayerS.Idle;        
    }

    void Update()
    {
        h = stick.Horizontal;
        v = stick.Vertical;
        switch (state)
        {
            case PlayerS.Idle:
                Idle();
                break;
            case PlayerS.Move:
                Move();
                break;
            case PlayerS.Attack:
                Attack();
                break;
            case PlayerS.Damage:
                break;
            case PlayerS.Die:
                break;
        }

        if((h * h) + (v * v) > 0.1f)
        {
            
        }
        else
        {
            
        }

        if (!charctrl.isGrounded)
        {
            charctrl.Move(new Vector3(0,-50.0f,0));
        }

        if(transform.position.y<-10)
        {
            transform.position = Spawn;
        }
    }

    private void Idle()
    {
        if ((h * h) + (v * v) > 0.1f)
        {
            ani.SetTrigger("Move");
            state = PlayerS.Move;
        }
    }

    private void Move()
    {
        if ((h * h) + (v * v) < 0.1f)
        {
            ani.SetTrigger("Idle");
            state = PlayerS.Idle;
        }
        else
        {
            Vector3 dir = new Vector3(h, 0, v);
            dir.Normalize();
            transform.rotation = Quaternion.LookRotation(dir);
            charctrl.Move(dir * speed * Time.deltaTime);
        }
     }

    private void Attack()
    {
        if(ani.GetCurrentAnimatorStateInfo(0).normalizedTime>0.9f)
        {
            state = PlayerS.Idle;
        }
    }

    public void Damage()
    {
        if (state != PlayerS.Die)
        {
            curHp -= 5;
            if (curHp <= 0)
            {
                state = PlayerS.Die;
                Die();
            }
        }
    }

    private void Die()
    {
        ani.SetTrigger("Die");
        Destroy(gameObject, 3.0f);

    }


    public void OnAttack1Click()
    {
        //트리거를 활성화해 애니메이터 상태를 바꾼다.
        ani.SetTrigger("Attack1");    
        state = PlayerS.Attack;
    }

    //공격2
    public void OnAttack2Click()
    {
        //트리거를 활성화해 애니메이터 상태를 바꾼다.
        ani.SetTrigger("Attack2t");
        state = PlayerS.Attack;

    }

    //공격3
    public void OnAttack3Click()
    {
        //트리거를 활성화해 애니메이터 상태를 바꾼다.
        ani.SetTrigger("Attack3");
        state = PlayerS.Attack;
    }
    public void heal()
    {
        curHp += 50;
        if(curHp>100)
        {
            curHp = 100;
        }
    }
    

    

}
