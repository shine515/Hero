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
            if (Panima.GetCurrentAnimatorStateInfo(0).IsName("Attack1") == true) //���߿� ��ũ��Ʈ �ϳ��� ������
            {
                // �ִϸ��̼� �÷��� ������ üũ
                float animTime = Panima.GetCurrentAnimatorStateInfo(0).normalizedTime;
                if (animTime > 0 && animTime < 1.0f) //�÷��� ��
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
        List<Collider> WpRange = new List<Collider>(Physics.OverlapSphere(this.transform.position, Pstatus.WpRange+2f, 1 << 6)); //(�߽�, �ݰ�, ���̾�)(���� 6�� ���̾�)

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
            yield return new WaitForSeconds(0.2f);
            this.transform.LookAt(enemy.transform.position);  //�� �������� ȸ��
        }
    }
}
