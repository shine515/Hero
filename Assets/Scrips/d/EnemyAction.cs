using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAction : MonoBehaviour
{
    Animator Myanimator;

    public float HP;  //��HP����
    public float MaxHP;  //MAX HP��

    public bool isAtt = false;

    public GameObject Player;
    public GameObject nowWeap;
    public string nowWeapT;
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP < 0)
            HP = 0;
        else if (HP == 0)
        {
            Myanimator.SetTrigger("Die");
            Destroy(this.gameObject,2f); 
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WEAPON")
        {
            GameObject WEAPON = other.gameObject;
            HP -= Player.GetComponent<StatusInfo>().Damage;

            Myanimator = this.GetComponent<Animator>();
            if (!Myanimator.GetBool("Fight"))
                Myanimator.SetBool("Fight", true);
            else
            {
                if (Myanimator.GetCurrentAnimatorStateInfo(0).IsName("Hit") == true)  //���߿� ��ũ��Ʈ �ϳ��� ������
                {
                    // ���ϴ� �ִϸ��̼��̶�� �÷��� ������ üũ
                    float animTime = Myanimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    /*if (animTime == 0)
                    {
                        // �÷��� ���� �ƴ�
                    }*/
                    if (animTime > 0 && animTime < 1.0f)
                    {
                        Debug.Log("���� ��");
                        Myanimator.Play("Hit",-1,0f);
                        // �ִϸ��̼� �÷��� ��
                    }
                    /*else if (animTime >= 1.0f)
                    {
                        // �ִϸ��̼� ����
                    }*/
                }
                else
                    Myanimator.SetTrigger("Hit");
            }
        }
    }

}
