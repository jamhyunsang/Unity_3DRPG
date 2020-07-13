using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//상태종류
enum State
{
    Idle,
    Move,
    Return,
    Attack,
    Damage,
    Die
}

public class EnemyState : MonoBehaviour
{
    //상태 enum
    State state;
    //체력
    public int hp = 100;
    public int maxHP = 100;
    //공격력
    int attackPoint = 5;
    //이동 속도
    public float speed = 5.0f;
    //공격 속도
    public float attackspeed = 2.0f;
    //타이머
    public float timer;
    //플레이어를 찾을 범위
    public float findRange = 15.0f;
    //공격 범위
    public float attackRange = 1.0f;
    //쫒아갈수 있는 최대 거리
    public float chaserRange = 30.0f;
    //처음 위치를 저장할 변수
    private Vector3 spawnPosition;

    public int AttackNumber;

    //플레이어를 타겟팅할 변수
    public GameObject Player;
    //캐릭터 컨트롤러를 저장할 변수
    private CharacterController charCtrl;
    //애니메이터 저장할 변수
    private Animator anim;

    //체력바 오브젝트
    public GameObject hpBarF;

    //네비게이션 가져오기 
    NavMeshAgent nav;

    //체력바 오브젝트2
    GameObject hpBar;
    //체력바 오프셋
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);

    private Canvas UICanvas;
    private Image hpBarImage;

    //리스폰
    private EnemyRespawn respawn;

    public GameObject potion;
    // Start is called before the first frame update
    void Start()
    {
        //처음 위치 저장
        spawnPosition = transform.position;
        //처음 상태 Idle
        state = State.Idle;
        //캐릭터 컨트롤러 가져오기
        charCtrl = GetComponent<CharacterController>();
        //플레이어 찾기
        Player = GameObject.Find("Player");
        //애니메이터 가져오기
        anim = GetComponent<Animator>();
        setHPBar();
        nav = GetComponent<NavMeshAgent>();
        respawn = GameObject.Find("Respawn").GetComponent<EnemyRespawn>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Move:
                Move();
                break;
            case State.Return:
                Return();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Damage:
                //Damage();
                break;
            case State.Die:
                //Die();
                break;
        }
    }


    private void Idle()
    {
        //인식 범위 안에 들어왔을때
        if (findRange >= Vector3.Distance(transform.position, Player.transform.position))
        {
            //Idle -> Move
            anim.SetTrigger("Move");
            state = State.Move;
        }
    }

    private void Move()
    {
        ////쫒을수 있는 최대 거리를 넘었을때
        //if (Vector3.Distance(transform.position, spawnPosition) > chaserRange)
        //{
        //    //Move -> Retrun
        //    state = State.Return;
        //}
        ////공격 범위 안에 들어왔을때
        //else if (Vector3.Distance(transform.position, Player.transform.position) < attackRange)
        //{
        //    //Move -> Attack
        //    state = State.Attack;
        //}
        ////그외에 
        //else
        //{
        //    //플레이어를 향해 다가간다.
        //    transform.LookAt(Player.transform.position);
        //    charCtrl.SimpleMove(Vector3.forward * speed);
        //}
        //이동중 이동할 수 있는 최대범위에 들어왔을때

        //쫒을수 있는 최대 거리를 넘었을때 
        if (Vector3.Distance(transform.position, spawnPosition) > chaserRange)
        {
            //Move -> Retrun
            anim.SetTrigger("Return");
            state = State.Return;
        }
        //공격 범위 보다 멀리 있을때
        else if (Vector3.Distance(transform.position, Player.transform.position) > attackRange)
        {
            //플레이어의 방향을 구한다.
            Vector3 dir = (Player.transform.position - transform.position).normalized;
            //플레이어 방향을 바라보도록 회전을 한다.
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(dir),
                10 * Time.deltaTime);
            //플레이어를 향해 이동한다.
            nav.SetDestination(Player.transform.position);
            nav.stoppingDistance = 2.0f;
        }
        else //공격범위 안에 들어옴
        {
            //Move -> Attack
            
            anim.SetTrigger("Attack");
            anim.SetInteger("AttackInt", Random.Range(0, AttackNumber));
            Player.GetComponent<PlayerState>().Damage();
            state = State.Attack;

        }
    }

    private void Return()
    {
        //스폰 위치를 바라보고 이동을 한다.
        if (Vector3.Distance(transform.position, spawnPosition) > 0.5f)
        {
            //처음 위치의 방향을 구한다.
            Vector3 dir = (spawnPosition - transform.position).normalized;
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(dir),
                10 * Time.deltaTime);
            nav.SetDestination(spawnPosition);
            nav.stoppingDistance = 0.1f;
        }
        else
        {
            //위치값을 초기 값으로 바꾼다.
            transform.position = spawnPosition;
            transform.rotation = Quaternion.identity;
            //Quaternion.identity 쿼터니온 회전 값을 0,0,0으로 초기화 시켜준다.
            //Return -> Idle
            anim.SetTrigger("Idle");
            state = State.Idle;
        }
    }

    private void Attack()
    {
        //공격범위안에 들어옴
        if (Vector3.Distance(transform.position, Player.transform.position) < attackRange)
        {
            //일정 시간마다 플레이어를 공격하기
            timer += Time.deltaTime;
            if (timer > attackspeed)
            {
                
                anim.SetTrigger("Attack");
                anim.SetInteger("AttackInt", Random.Range(0, AttackNumber));
                Player.GetComponent<PlayerState>().Damage();
                print("공격");
                //타이머 초기화
                timer = 0f;
            }
        }
        else//현재상태를 무브로 전환하기 (재추격)
        {
            //Attack -> Move
            anim.SetTrigger("Move");
            state = State.Move;
            //타이머 초기화
            timer = 0f;
        }
    }

    //플레이어쪽에서 충돌감지를 할 수 있으니 이함수는 퍼블릭으로 만들자
    public void hitDamage(int value)
    {
        //예외처리
        //피격상태이거나, 죽은 상태일때는 데미지 중첩으로 주지 않는다
        if (state == State.Damage || state == State.Die) return;

        //체력깍기
        hp -= value;
        hpBarImage.fillAmount = (float)hp / maxHP;
        transform.Translate(Vector3.back);

        //몬스터의 체력이 1이상이면 피격상태
        if (hp > 0)
        {
            //AnyState -> Damage
            anim.SetTrigger("Damage");
            Damage();
            state = State.Damage;
        }
        //0이하이면 죽음상태
        else
        {
            //AnyState -> Die
            anim.SetTrigger("Die");
            state = State.Die;
            Die();
        }
    }

    //피격상태 (Any State)
    private void Damage()
    {
        //피격 상태를 처리하기 위한 코루틴을 실행한다
        StartCoroutine(DamageProc());
        
    }

    //피격상태 처리용 코루틴
    IEnumerator DamageProc()
    {
        //피격모션 시간만큼 기다리기
        yield return new WaitForSeconds(0.3f);
        //현재상태를 이동으로 전환
        anim.SetTrigger("Move");
        state = State.Move;
    }

    //죽음상태 (Any State)
    private void Die()
    {
        //코루틴을 사용하자
        //1. 체력이 0이하
        //2. 몬스터 오브젝트 삭제
        //- 상태변경
        //- 상태전환 출력 (죽었다)

        //진행중인 모든 코루틴은 정지한다
        StopAllCoroutines();

        //죽음상태를 처리하기 위한 코루틴 실행
        StartCoroutine(DieProc());
    }

    IEnumerator DieProc()
    {
        //2초후에 자기자신을 제거한다
        yield return new WaitForSeconds(1.0f);
        Instantiate(potion, transform.position+new Vector3(0,1,0), transform.rotation);
        hpBar.SetActive(false);
        print("죽었다!!");
        Destroy(gameObject);
        respawn.Die();
    }

    void setHPBar()
    {
        UICanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        hpBar = Instantiate<GameObject>(hpBarF, UICanvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var _hpbar = hpBar.GetComponent<EnemyHPBar>();
        _hpbar.offset = hpBarOffset;
        _hpbar.targetTr = gameObject.transform;
    }
}
