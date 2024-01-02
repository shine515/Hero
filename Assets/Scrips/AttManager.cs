using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttManager : MonoBehaviour
{
    public GameObject Player;
    private Animator Panima;
    public WeaponInfo Weapo;
    public StatusInfo Pstatus;
    public Collider AttPos;

    private bool Fight = false;
    // Start is called before the first frame update
    void Start()
    {
        Panima = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine( LookatEnemy());
            if (!Fight)
            {
                Fight = true;
                Panima.SetBool("Fight", true);
                Panima.SetTrigger(Pstatus.nowWeapT);
            }
            if (Panima.GetCurrentAnimatorStateInfo(0).IsName("Attack1") == true) //나중에 스크립트 하나에 모으기
            {
                // 애니메이션 플레이 중인지 체크
                float animTime = Panima.GetCurrentAnimatorStateInfo(0).normalizedTime;
                if (animTime > 0 && animTime < 1.0f) //플레이 중
                {
                    Panima.SetTrigger("Attack2");
                }
            }
            else
                Panima.SetTrigger("Attack1");
        }
    }

    private IEnumerator LookatEnemy()
    {
        Collider enemy;
        List<Collider> WpRange = new List<Collider>(Physics.OverlapSphere(this.transform.position, Pstatus.WpRange+2f, 1 << 6)); //(중심, 반경, 레이어)(적은 6번 레이어)

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
            yield return new WaitForSeconds(0.2f);
            this.transform.LookAt(enemy.transform.position);  //그 적쪽으로 회전
        }
    }
}
