using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusInfo : MonoBehaviour
{
    public float HP;  //��HP����
    public float MaxHP;  //MAX HP��
    public float WpRange;  //���� �����Ÿ�

    public bool isAtt = false;

    public GameObject nowWeap;
    public string nowWeapT;


    public float Damage;  // �ʿ� ��������.. ������ �ؾ߰ڴ�..

    // Start is called before the first frame update
    void Start()
    {
        Damage = nowWeap.GetComponent<WeaponInfo>().Damage;
        nowWeapT= nowWeap.GetComponent<WeaponInfo>().type;
        HP = MaxHP;
    }

    //�ִϸ��̼� ���� �߰�
    private void Update()
    {
        if(HP < 0) { HP = 0;
            this.GetComponent<Animator>().SetTrigger("Die");
        }
    }


}
