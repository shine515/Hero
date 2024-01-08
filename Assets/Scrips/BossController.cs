using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEditor.PlayerSettings;

public class BossController : MonoBehaviour
{
    public float MaxHp;
    public float Hp;
    public float Damage;
    public float attckDist = 1.5f;


    float Distance;

    public float moveSpeed =2;
    int movev;
    int moveH;
    int moveDirec;
    int TypeMaxNum;
    public enum State
    {
        Idle,
        Attack,
        Move
    }

    State state = State.Idle;


    [SerializeField]
    private GameObject HitEff;
    [SerializeField]
    private AnimRepCheck animRepCheck;
    [SerializeField]
    Animator animator;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    GameObject ResetPos;
    [SerializeField]
    GameObject Attarea;
    [SerializeField]
    GameObject SpinEff;
    [SerializeField]
    GameObject BossSkill1;
    [SerializeField]
    GameObject BossSkill2;
    [SerializeField]
    GameObject[] MoveCorner;

    [SerializeField]
    bool isDie;
    [SerializeField]
    bool isAttack;
    [SerializeField]
    bool isMove;
    bool isHit = false;
    [SerializeField]
    bool Moving;
    [SerializeField]
    bool Attaking;
    [SerializeField]
    bool NomalAtt;



    // Start is called before the first frame update
    void Start()
    {
        Hp = MaxHp;
        StartCoroutine(BaseMovement());
        StartCoroutine(StateControll());
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDie)
        {
            BossStateCheck();
        }
    }

    IEnumerator StateControll()
    {
        state = State.Idle;
        while(!isDie)
        {
            yield return new WaitForSeconds(1);
            isAttack = true;
            yield return new WaitForSeconds(6);
            RandomAction();
            while (Hp < MaxHp * 0.3)
            {
                yield return new WaitForSeconds(1);
                isAttack = true;
                yield return new WaitForSeconds(6);
                RandomAction();
            }
            StartCoroutine(Attack3());
            yield return new WaitForSeconds(6);
            RandomAction();
            while (!isDie)
            {
                yield return new WaitForSeconds(1);
                isAttack = true;
                yield return new WaitForSeconds(6);
                RandomAction();
            }
        }
    }

    void RandomAction()
    {
        int ran = Random.Range(0, 10);
        if (ran < 9) isMove = true;
        else isAttack = true;
    }
    void BossStateCheck()
    {
        Distance = Vector3.Distance(Player.transform.position, transform.position);

        if (isAttack || Distance <= attckDist)
            state = State.Attack;
        else if (isMove)
            state = State.Move;
        else
            state = State.Idle;
        Bossbehavior();
    }

    void Bossbehavior()
    {
        switch (state)
        {
            case State.Idle:
                Idlebehavior();
                break;
            case State.Attack:
                Attackbehavior();
                break;
            case State.Move:
                Movebehavior();
                break;
        }
    }

    void Idlebehavior()
    {
    }
    void Attackbehavior()
    {
        if(!Attaking && Distance >= attckDist)
        {
            AttackType();
            Attaking = true;
        }
        else if(!Attaking && Distance <= attckDist)
        {
            StartCoroutine(NomalAttack());
            Attaking = true;
        }
    }

    void AttackType()
    {
        if (Hp < MaxHp * 0.3)
            TypeMaxNum = 3;
        else
            TypeMaxNum = 2;
        int Type = Random.Range(1, TypeMaxNum);
        StartCoroutine("Attack" + Type.ToString());

    }

    IEnumerator NomalAttack()  //근접 공격 콤보
    {
        Debug.Log("노멀어택");
        animator.SetBool("Attack", true);
        animator.Play("Attack1");
        yield return new WaitForSeconds(3f);
        isAttack = false;
        yield return null;
        Attaking = false;

        animator.SetBool("Attack", false);
        Vector3 vec = new Vector3(ResetPos.transform.position.x, transform.position.y, ResetPos.transform.position.z);
        transform.position = vec;
    }

    IEnumerator Attack1()  //근접 공격 콤보
    {
        Debug.Log("1번어택");
        Vector3 movePos = new Vector3(Random.Range(2, 3),0, Random.Range(2, 3));
        Vector3 MoveBack = Player.transform.position + movePos;
        animator.SetBool("Attack", true);
        transform.position = MoveBack;
        animator.Play("Attack1");
        yield return new WaitForSeconds(3f);
        isAttack = false;
        yield return null;
        Attaking = false;

        animator.SetBool("Attack", false);
        Vector3 vec = new Vector3(ResetPos.transform.position.x, transform.position.y, ResetPos.transform.position.z);
        transform.position = vec;

        yield return new WaitForSeconds(0.3f);
        Attarea.SetActive(false);
    }

    IEnumerator Attack2()
    {
        transform.LookAt(Player.transform.position);
        int CornerNum = Random.Range(0, 4);
        animator.SetBool("Attack", true);
        transform.position = MoveCorner[CornerNum].transform.position;
        
        yield return new WaitForSeconds(0.3f);
        animator.Play("Attack2");
        Vector3 BS = transform.TransformDirection(transform.position + new Vector3(0,1,1) * 3);
        Instantiate(BossSkill1, transform.position, transform.rotation);
        Debug.Log("2번어택");
        yield return new WaitForSeconds(4f);

        animator.SetBool("Attack", false);
        isAttack = false;
        yield return null;
        Attaking = false;
    }
    IEnumerator Attack3()
    {
        Vector3 vec = new Vector3(ResetPos.transform.position.x, transform.position.y, ResetPos.transform.position.z);
        transform.position = vec;
        Debug.Log("3번어택"); 
        animator.SetBool("Attack", true);

        animator.Play("Attack3");
        float H = 5;
        Vector3 MoveBack = Player.transform.position + (Vector3.up* H);
        Instantiate(BossSkill2, MoveBack, Player.transform.rotation);
        yield return new WaitForSeconds(1f);
         MoveBack = Player.transform.position + (Vector3.up * H);
        Instantiate(BossSkill2, MoveBack, Player.transform.rotation);
        yield return new WaitForSeconds(1f);
         MoveBack = Player.transform.position + (Vector3.up * H);
        Instantiate(BossSkill2, MoveBack, Player.transform.rotation);
        yield return new WaitForSeconds(1f);
         MoveBack = Player.transform.position + (Vector3.up * H);
        Instantiate(BossSkill2, MoveBack, Player.transform.rotation);
        yield return new WaitForSeconds(1f);


        animator.SetBool("Attack", false);
        isAttack = false;
        yield return null;
        Attaking = false;

        transform.position = vec;

        yield return new WaitForSeconds(0.3f);
        Attarea.SetActive(false);
    }


    private void attack() //애니메이션에서 작동 
    {
        eff();
    }

    private void eff()
    {
        Attarea.SetActive(true);
    }
    
    private void Spineff()
    {
        Vector3 vec = new Vector3(transform.position.x,transform.position.y,transform.position.z) + new Vector3 (0, 0.3f, 0);
        Quaternion rot = Quaternion.LookRotation(new Vector3(90,0,0));
        Attarea.SetActive(true);
        GameObject Spineff = Instantiate(SpinEff, transform);
    }

    private void enabledEffect()//애니메이션에서 작동 
    {
        Attarea.SetActive(false);
    }

    void Movebehavior()
    {
        if (Moving)
        {
            Vector3 moveVec = new Vector3(moveH, 0, movev);
            rb.MovePosition(rb.position + transform.TransformDirection(moveVec) * (moveSpeed * Time.deltaTime));

            //transform.position += moveVec * moveSpeed*Time.deltaTime;
            //animator.SetBool("Move", moveVec != Vector3.zero);
        }
        else
        {
            Moving = true;
            Debug.Log("!moving");
            moveDir();
            StartCoroutine(MoveTime());
        }
    }

    void moveDir()
    {
        moveDirec = Random.Range(1, 5);
        switch (moveDirec)
        {
            case 1: //FWD
                movev = 1; moveH = 0; break;
            case 2: //LGT
                movev = 0; moveH = -1; break;
            case 3:  //BWD
                movev = 0; moveH = 1; break;
            case 4: //LGT
                movev = -1; moveH = 0; break;
        }
    }
    IEnumerator MoveTime() 
    {
        animator.SetBool("Move", true);
        animator.SetFloat("MinMove", moveDirec);
        animator.SetFloat("MaxMove", moveDirec);
        Debug.Log("MoveTime");
        int moveTime = Random.Range(1, 7);
        yield return new WaitForSeconds(moveTime);
        isMove = false;
        yield return new WaitForSeconds(0.1f);
        Moving = false;
        Debug.Log("MvingEnd");

        animator.SetBool("Move", false);
        animator.SetFloat("MinMove", 0);
        animator.SetFloat("MaxMove", 0);
    }


    IEnumerator BaseMovement()
    {
        while(!isDie)
        {
            this.transform.LookAt(Player.transform.position);
            yield return new WaitForSeconds(0.1f);
        }
    }

    Vector3 pos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WEAPON") && !isDie)
        {
                Weapon weap = other.GetComponentInParent<Weapon>();
                float HitRange = weap.AttSpeed;
                float Damage = weap.Damage;
                pos = other.ClosestPoint(transform.position);
                Quaternion rot = Quaternion.LookRotation(pos); //Camera.Main해보기
                GameObject blood = Instantiate(HitEff, pos, rot);
                Destroy(blood, 1f);
                IsHit(HitRange, Damage);
        }
    }

    private void IsHit(float HitRange, float Damage)
    {
        isHit = true;
        animRepCheck.AnimaRepCheck(animator, "Hit");
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * 2f);
        isAttack = false;
        Hp -= Damage;
    }

    public void Victory()
    {
        StopAllCoroutines();
        animator.Play("Idle");
    }
}
