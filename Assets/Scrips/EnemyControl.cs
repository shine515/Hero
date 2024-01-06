using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyControl : MonoBehaviour
{
    public enum State  //몬스터의 상태
    {
        IDLE,
        TAUNT,
        SENSE,
        TRACE,
        ATTACK,
        DIE
    }
    public State state = State.IDLE;

    public float traceDist = 10f;
    public float attckDist = 1f;
    public bool isDie = false;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;

    Animator Myanimator;

    public float HP;  //현HP상태
    public float MaxHP;  //MAX HP값
    public float Damage;
    //public float AttRange;

    public bool isAtt = false;

    public GameObject Player;
    public GameObject nowWeap;
    public string nowWeapT;

    [SerializeField]
    private AnimRepCheck AniRep;

    // Start is called before the first frame update
    void Start()
    {
        ResetMonster();
        Myanimator = GetComponent<Animator>();
        //nowWeap = this.gameObject.transform.Find("AttPos").gameObject;
        Damage = nowWeap.GetComponent<Weapon>().Damage;
        monsterTr = this.transform;
        playerTr = Player.transform;
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(CheckMonsState());
        StartCoroutine(MonsAction());
    }

    private void ResetMonster()
    {
        HP = MaxHP;
    }

    IEnumerator CheckMonsState()
    {
        while(!isDie)
        {
            yield return new WaitForSeconds(0.3f);
            if (HP <= 0)
            {
                isDie = true;
            }
            else
            {
                float Distance = Vector3.Distance(playerTr.position, monsterTr.position);

                if (Distance <= attckDist)
                {
                    state = State.ATTACK;
                }
                else if (Distance <= traceDist && !Myanimator.GetBool("Fight"))
                {
                    state = State.SENSE;
                }
                else if (Distance <= traceDist && Myanimator.GetBool("Fight"))
                {
                    state = State.TRACE;
                    agent.SetDestination(playerTr.position);
                }
                else
                    state = State.IDLE;
            }
            
        }
        Debug.Log("Die");
        state = State.DIE;
        Destroy(this.gameObject, 2f);
    }

    IEnumerator MonsAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.ATTACK:
                    Myanimator.SetBool("Attack",true);
                    Myanimator.SetBool("Move", false);
                    this.transform.LookAt(playerTr.position);
                    agent.isStopped = true;
                    break;
                case State.TRACE:
                    Myanimator.SetBool("Move", true);
                    Myanimator.SetBool("Attack", false);
                    agent.isStopped = false;
                    agent.SetDestination(playerTr.position);
                    break;
                case State.TAUNT:
                    Myanimator.SetTrigger("Taunt");
                    break;
                case State.SENSE:
                    Myanimator.SetBool("Attack", false);
                    Myanimator.SetBool("Sense", true);
                    this.transform.LookAt(playerTr.position);
                    agent.isStopped = true;
                    break;
                case State.DIE:
                    Myanimator.SetTrigger("Die");
                    break;
                case State.IDLE:
                    Myanimator.SetBool("Fight", false);
                    Myanimator.SetBool("Sense", false);
                    Myanimator.SetTrigger("Die");
                    //BackToSpon();
                    break;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject WEAPON = other.gameObject;
        if (WEAPON.tag == "WEAPON")
        {
                HP -= Damage;
                if (!Myanimator.GetBool("Fight"))
                    Myanimator.SetBool("Fight", true);
                AniRep.AnimaRepCheck(Myanimator, "Hit");
        }
    }

}
