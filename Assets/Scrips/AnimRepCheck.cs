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
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName(Aniname) == true)  //���߿� ��ũ��Ʈ �ϳ��� ������
        {
            // ���ϴ� �ִϸ��̼��̶�� �÷��� ������ üũ
            float animTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            /*if (animTime == 0)
            {
                // �÷��� ���� �ƴ�
            }*/
            if (animTime > 0 && animTime < 1.0f)
            {
                Debug.Log("���� ��");
                Anim.Play(Aniname, -1, 0f);
                // �ִϸ��̼� �÷��� ��
            }
            /*else if (animTime >= 1.0f)
            {
                // �ִϸ��̼� ����
            }*/
        }
        else
            Anim.SetTrigger(Aniname);
    }
}
