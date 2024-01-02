using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttManager : MonoBehaviour
{
    public GameObject Player;
    private Animator Panima;
    public WeaponInfo Winpo;
    public StatusInfo Pstatus;

    private bool Fight =false;
    // Start is called before the first frame update
    void Start()
    {
        Panima = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if (!Fight)
            {
                Fight = true;
                Panima.SetBool("Fight", true);
                Panima.SetTrigger(Pstatus.nowWeapT);
            }
            Panima.SetTrigger(Pstatus.nowWeapT+"Attack");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("인식 하는 내이름: "+ gameObject.name);
        Debug.Log("인식 이름: "+ collision.gameObject.name);
        if (collision.gameObject.tag == "weapon")
        {
            GameObject TargetEne = collision.gameObject;
            Debug.Log("인식 적 이름 :" + TargetEne.name);
            this.GetComponent<StatusInfo>().HP -= Winpo.Damage;
        }
    }
}
