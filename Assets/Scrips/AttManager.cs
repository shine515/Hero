using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttManager : MonoBehaviour
{
    public GameObject Player;
    private Animator Panima;
    public WeaponInfo Weapo;
    public StatusInfo Pstatus;

    private bool Fight =false;
    // Start is called before the first frame update
    void Start()
    {
        Panima = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!Fight)
            {
                Fight = true;
                Panima.SetBool("Fight", true);
                Panima.SetTrigger(Pstatus.nowWeapT);
            }
            if (Panima.GetCurrentAnimatorStateInfo(0).IsName("Attack1") == true) //나중에 스크립트 하나에 모으기
            {
                // 원하는 애니메이션이라면 플레이 중인지 체크
                float animTime = Panima.GetCurrentAnimatorStateInfo(0).normalizedTime;
                /*if (animTime == 0)
                {
                    // 플레이 중이 아님
                }*/
                if (animTime > 0 && animTime < 1.0f)
                {
                    Panima.SetTrigger("Attack2");
                    // 애니메이션 플레이 중
                }
                /*else if (animTime >= 1.0f)
                {
                    // 애니메이션 종료
                }*/
            }
            else
                Panima.SetTrigger("Attack1");
        }
    }

    
}
