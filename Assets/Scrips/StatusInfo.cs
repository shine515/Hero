using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusInfo : MonoBehaviour
{
    public float Damage;  // �ʿ� ��������.. ������ �ؾ߰ڴ�..
    public float HP;  //��HP����
    public float MaxHP;  //MAX HP��

    public bool isAtt = false;

    public GameObject nowWeap;
    public string nowWeapT;

    
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
    }

    //�ִϸ��̼� ���� �߰�
    private void Update()
    {
        if (HP < 0)
            HP = 0;
        else if (HP == 0)
                Destroy(this.gameObject);// �ִϸ��̼� ����
    }


}
