using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    //�� �Լ��� �������� ���Ŀ� ���� ���� ������ ���Ϸ� �ҷ��� �� �� ����

    public string Name;   //�����̸�
    public float Damage;   //���ݷ�
    public string type;    //���� �з�(����Ÿ��)
    public Sprite WeapImg; //�����̹���
    //public float AttSpeed; //����
    //public float IncPar  //���� ���
    //public float Weight  //���� �߷�(�߰� ����)

    // Start is called before the first frame update
    //local ������ �������� �������� ����
    void Start()
    {
        Name = "�̸�";
        Damage = 10;
        type = "sword";
        WeapImg = Resources.Load<Sprite>("Sprite/WeaponImage/" + this.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
