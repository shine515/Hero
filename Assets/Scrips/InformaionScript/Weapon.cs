using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //�� �Լ��� �������� ���Ŀ� ���� ���� ������ ���Ϸ� �ҷ��� �� �� ����

    public string Name;   //�����̸�
    public float Damage;   //���ݷ�
    public string type;    //���� �з�(����Ÿ��)
    public GameObject Attarea;
    public Sprite WeapImg; //�����̹���
    public float AttSpeed; //����
    public float AttCombo; //�޺�����(���ݾִϸ��̼� ��)
    //public float IncPar  //���� ���
    //public float Weight  //���� �߷�(�߰� ����)

    // Start is called before the first frame update
    //local ������ �������� �������� ����
    void Start()
    {
        Name = "�̸�";
        Damage = 10;
        type = "sword";
        AttCombo = 4;
        AttSpeed= 0.5f;
        WeapImg = Resources.Load<Sprite>("Sprite/WeaponImage/" + this.gameObject.name);
        Attarea = this.transform.GetChild(0).gameObject;
        Attarea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
