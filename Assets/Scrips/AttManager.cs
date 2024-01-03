using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AttManager : MonoBehaviour
{
    public GameObject Player;

    private Animator anima;
    public StatusInfo Pstatus;
    public Collider AttPos;

    [SerializeField]
    private AnimRepCheck AniRep;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(PlayerCtl());
            anima = Player.GetComponent<Animator>();
    }

    // Update is called once per frame

    /*IEnumerator PlayerCtl()
    {while (true)
        {
            

            yield return null;
        }
    }*/

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LookatEnemy(10f, 6);
            anima.SetTrigger(Pstatus.nowWeapT);

            if (anima.GetCurrentAnimatorStateInfo(0).IsName("Attack1") == true) //나중에 스크립트 하나에 모으기
            {
                // 애니메이션 플레이 중인지 체크
                float animTime = anima.GetCurrentAnimatorStateInfo(0).normalizedTime;
                if (animTime > 0 && animTime < 1.0f) //플레이 중
                {
                    anima.SetTrigger("Attack2");
                }
            }
            else
                anima.SetTrigger("Attack1");
        }
    }

    private void LookatEnemy(float AttRange, int layer)
    {
        Collider enemy;
        List<Collider> WpRange = new List<Collider>(Physics.OverlapSphere(this.transform.position, AttRange, 1 << layer)); //(중심, 반경, 레이어)(적은 6번 레이어)

        if (WpRange.Count<1) Debug.Log("공격!");
        else
        {
            enemy = WpRange[0]; // 첫번째를 먼저 
            float shortDis = Vector3.Distance(gameObject.transform.position, WpRange[0].transform.position); // 첫번째를 기준으로 잡아주기 
            foreach (Collider shortist in WpRange)  //제일 가까운 적 찾기
            {
                float Distance = Vector3.Distance(gameObject.transform.position, shortist.transform.position);

                if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
                {
                    enemy = shortist;
                    shortDis = Distance;
                }
            }
            Player.transform.LookAt(enemy.transform.position);  //그 적쪽으로 회전
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject Monster = other.gameObject;
        if (Monster.tag == "MONSATTACK")
        {
            Pstatus.HP -= Monster.GetComponent<StatusInfo>().Damage;

            AniRep.AnimaRepCheck(anima, "Hit");
        }
    }
}
