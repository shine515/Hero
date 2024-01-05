using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRepCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

   public void AnimaRepCheck(Animator Anim, string Aniname)
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName(Aniname) == true)  //나중에 스크립트 하나에 모으기
        {
            // 원하는 애니메이션이라면 플레이 중인지 체크
            float animTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            /*if (animTime == 0)
            {
                // 플레이 중이 아님
            }*/
            if (animTime > 0 && animTime < 1.0f)
            {
                Debug.Log("실행 중");
                Anim.Play(Aniname, -1, 0f);
                // 애니메이션 플레이 중
            }
            /*else if (animTime >= 1.0f)
            {
                // 애니메이션 종료
            }*/
        }
        else
            Anim.SetTrigger(Aniname);
    }
}
