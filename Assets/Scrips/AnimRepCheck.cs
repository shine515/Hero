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
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName(Aniname))
        {
            // 원하는 애니메이션이라면 플레이 중인지 체크
            float animTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime > 0 && animTime < 1.0f)
            {
                Anim.Play(Aniname, -1, 0f);
            }
        }
        else
        {
            Anim.SetTrigger(Aniname);
        }
    }
}
