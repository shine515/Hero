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

            if (anima.GetCurrentAnimatorStateInfo(0).IsName("Attack1") == true) //���߿� ��ũ��Ʈ �ϳ��� ������
            {
                // �ִϸ��̼� �÷��� ������ üũ
                float animTime = anima.GetCurrentAnimatorStateInfo(0).normalizedTime;
                if (animTime > 0 && animTime < 1.0f) //�÷��� ��
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
        List<Collider> WpRange = new List<Collider>(Physics.OverlapSphere(this.transform.position, AttRange, 1 << layer)); //(�߽�, �ݰ�, ���̾�)(���� 6�� ���̾�)

        if (WpRange.Count<1) Debug.Log("����!");
        else
        {
            enemy = WpRange[0]; // ù��°�� ���� 
            float shortDis = Vector3.Distance(gameObject.transform.position, WpRange[0].transform.position); // ù��°�� �������� ����ֱ� 
            foreach (Collider shortist in WpRange)  //���� ����� �� ã��
            {
                float Distance = Vector3.Distance(gameObject.transform.position, shortist.transform.position);

                if (Distance < shortDis) // ������ ���� �������� �Ÿ� ���
                {
                    enemy = shortist;
                    shortDis = Distance;
                }
            }
            Player.transform.LookAt(enemy.transform.position);  //�� �������� ȸ��
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
