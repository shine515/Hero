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
            if (Panima.GetCurrentAnimatorStateInfo(0).IsName("Attack1") == true) //���߿� ��ũ��Ʈ �ϳ��� ������
            {
                // ���ϴ� �ִϸ��̼��̶�� �÷��� ������ üũ
                float animTime = Panima.GetCurrentAnimatorStateInfo(0).normalizedTime;
                /*if (animTime == 0)
                {
                    // �÷��� ���� �ƴ�
                }*/
                if (animTime > 0 && animTime < 1.0f)
                {
                    Panima.SetTrigger("Attack2");
                    // �ִϸ��̼� �÷��� ��
                }
                /*else if (animTime >= 1.0f)
                {
                    // �ִϸ��̼� ����
                }*/
            }
            else
                Panima.SetTrigger("Attack1");
        }
    }

    
}
