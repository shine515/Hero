using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public float HP;  //��HP����
    public float MaxHP;  //MAX HP��
    public float WpRange;  //���� �����Ÿ�

    public bool isAtt = false;

    
    public GameObject[] Weapons;
    public GameObject nowWeap;
    public string nowWeapT;


    public float Damage;  // �ʿ� ��������.. ������ �ؾ߰ڴ�..

    // Start is called before the first frame update
    void Start()
    {
        Damage = nowWeap.GetComponent<Weapon>().Damage;
        nowWeapT= nowWeap.GetComponent<Weapon>().type;
        HP = MaxHP;
    }



}
