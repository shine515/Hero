using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.WSA;
using static UnityEditor.PlayerSettings;

public class MonsterControl : MonoBehaviour
{
    public float traceDist = 10f;
    public float attckDist = 1f;
    public float force = 2f;

    public bool isDie = false;
    bool isHit = false;
    bool IsAttack = false;

    public GameObject Player;
    public GameObject AttPos;


    [SerializeField]
    private Animator animator;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    [SerializeField]
    private AnimRepCheck animRepCheck;

    public Monster mos;

    [SerializeField]
    private GameObject HitEff;
    [SerializeField]
    private GameObject[] DropItem;



    public enum State  //몬스터의 상태
    {
        IDLE,
        SENSE,
        TRACE,
        ATTACK,
        DIE
    }
    public State state = State.IDLE;

    // Start is called before the first frame update
    void Start()
    {
        mos = AttPos.GetComponent<Monster>();
        ResetMonster();
        animator = GetComponent<Animator>();
        monsterTr = this.transform;
        playerTr = Player.transform;
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(CheckMonsState());
        StartCoroutine(MonsAction());
        AllFalse();
    }

    private void Update()
    {
        //GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * force);
    }

    private void ResetMonster()
    {
        mos.HP = mos.MaxHP;
    }

    IEnumerator CheckMonsState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.3f);
            if (mos.HP <= 0)
            {
                state = State.DIE;
            }
            else
            {
                float Distance = Vector3.Distance(playerTr.position, monsterTr.position);

                if (Distance <= attckDist && animator.GetBool("Fight"))
                {
                    state = State.ATTACK;
                }
                else if (Distance <= traceDist && !animator.GetBool("Fight"))
                {
                    state = State.SENSE;
                }
                else if (Distance <= traceDist && animator.GetBool("Fight"))
                {
                    state = State.TRACE;
                    agent.SetDestination(playerTr.position);
                }
                else
                    state = State.IDLE;
            }

        }
    }

    IEnumerator MonsAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.ATTACK:
                    if (!IsAttack)
                    {
                        StartCoroutine(MosAttack());
                        AllFalse();

                        agent.isStopped = true;
                    }
                    break;
                case State.TRACE:
                    AllFalse();
                    animator.SetBool("Move", true);
                    agent.isStopped = false;
                    agent.SetDestination(playerTr.position);
                    break;
                case State.SENSE:
                    AllFalse();
                    animator.SetBool("Sense", true);
                    this.transform.LookAt(playerTr.position);
                    agent.isStopped = true;
                    break;
                case State.DIE:
                    animator.SetTrigger("Die");
                    Destroy(this.gameObject, 2f);
                    isDie = true;
                    DropItems();
                    break;
                case State.IDLE:
                    FightFalse();
                    AllFalse();
                    //BackToSpon();
                    break;
            }

            yield return new WaitForSeconds(0.3f);
        }

    }

    private void AllFalse()
    {
        AttPos.SetActive(false);
        animator.SetBool("Sense", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Move", false);
    }
    private void FightFalse()
    {
        animator.SetBool("Fight", false);
    }

    float AttDelay; 
    IEnumerator MosAttack()
    {
        IsAttack = true;
        while (IsAttack)
        {
            this.transform.LookAt(playerTr);
            animator.SetBool("Attack", true);

            yield return new WaitForSeconds(0.5f);

            animator.SetBool("Attack", false);

            yield return new WaitForSeconds(0.5f);

            if (animator.GetBool("Move"))
                IsAttack = false;
        }

        animator.SetBool("Attack", false);
    }

    private void AttEnd() //애니메이션에서 조작
    {
        AttPos.SetActive(false);
    }
    private void AttStart() //애니메이션에서 조작
    {
        AttPos.SetActive(true);
    }
    Vector3 pos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WEAPON")&& !isDie)
        {
            if (!isHit)
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
    }

    private void IsHit(float HitRange,float Damage)
    {
        isHit = true;
        animRepCheck.AnimaRepCheck(animator, "Hit");
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * force);
        animator.SetBool("Fight", true);
        IsAttack = false;
        mos.HP -= Damage;
        Invoke("HitEnd", 0.1f);
        agent.isStopped = true;
    }
    private void HitEnd()
    {
        isHit = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private void Victory()  //SendMassage받을 함수
    {
        animator.Play("Victory");
        animator.SetBool("Victory", true);
        agent.isStopped = true;
    }

    private void DropItems()
    {
        DropItem = Resources.LoadAll<GameObject>("Prefab/ItemPrefab");
        foreach (GameObject item in DropItem)
        {
            Instantiate(item, pos, Quaternion.Euler(0, 0, 0));
        }
    }
}
