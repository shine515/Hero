using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusInfo : MonoBehaviour
{
    public float Damage;  //개체 고유 고격력
    public float HP;  //현HP상태
    public float MaxHP;  //MAX HP값

    public bool isAtt = false;

    public GameObject nowWeap;
    public string nowWeapT;

    
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
    }

    //애니메이션 이후 추가
    private void Update()
    {
        if (HP < 0)
            HP = 0;
        else if (HP == 0)
            Destroy(this);
    }


}
