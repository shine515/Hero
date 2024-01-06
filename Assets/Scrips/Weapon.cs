using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //이 함수의 정보들은 이후에 무기 레벨 디자인 파일로 불러와 써 질 예정

    public string Name;   //무기이름
    public float Damage;   //공격력
    public string type;    //무기 분류(공격타입)
    public GameObject Attarea;
    public Sprite WeapImg; //무기이미지
    public float AttSpeed; //공속
    public float AttCombo; //콤보갯수(공격애니메이션 수)
    //public float IncPar  //공증 계수
    //public float Weight  //무기 중량(추가 미정)

    // Start is called before the first frame update
    //local 데이터 저장으로 업글정보 저장
    void Start()
    {
        Name = "이름";
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
