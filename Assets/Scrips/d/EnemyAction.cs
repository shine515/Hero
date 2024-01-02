using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAction : MonoBehaviour
{
    Animator Myanimator;

    public float HP;  //현HP상태
    public float MaxHP;  //MAX HP값

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
                if (Myanimator.GetCurrentAnimatorStateInfo(0).IsName("Hit") == true)  //나중에 스크립트 하나에 모으기
                {
                    // 원하는 애니메이션이라면 플레이 중인지 체크
                    float animTime = Myanimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    /*if (animTime == 0)
                    {
                        // 플레이 중이 아님
                    }*/
                    if (animTime > 0 && animTime < 1.0f)
                    {
                        Debug.Log("실행 중");
                        Myanimator.Play("Hit",-1,0f);
                        // 애니메이션 플레이 중
                    }
                    /*else if (animTime >= 1.0f)
                    {
                        // 애니메이션 종료
                    }*/
                }
                else
                    Myanimator.SetTrigger("Hit");
            }
        }
    }

}
