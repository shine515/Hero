using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public float HP;  //현HP상태
    public float MaxHP;  //MAX HP값
    public float WpRange;  //공격 사정거리

    public bool isAtt = false;

    
    public GameObject[] Weapons;
    public GameObject nowWeap;
    public string nowWeapT;


    public float Damage;  // 필요 없을지도.. 정리좀 해야겠다..

    // Start is called before the first frame update
    void Start()
    {
        Damage = nowWeap.GetComponent<Weapon>().Damage;
        nowWeapT= nowWeap.GetComponent<Weapon>().type;
        HP = MaxHP;
    }



}
