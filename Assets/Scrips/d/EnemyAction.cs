using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAction : MonoBehaviour
{
    Animator Myanimator;

    public float HP;  //현HP상태
    public float MaxHP;  //MAX HP값
    public float Damage;

    public bool isAtt = false;

    public GameObject Player;
    public GameObject nowWeap;
    public string nowWeapT;

    [SerializeField]
    private AnimRepCheck AniRep;

    // Start is called before the first frame update
    void Start()
    {
        Myanimator = this.GetComponent<Animator>();
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP < 0)
            HP = 0;
        else if (HP == 0)
        {
            Myanimator.SetTrigger("Die");
            Destroy(this.gameObject,2f); 
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WEAPON")
        {
            GameObject WEAPON = other.gameObject;
            HP -= Player.GetComponent<StatusInfo>().Damage;

            if (!Myanimator.GetBool("Fight"))
                Myanimator.SetBool("Fight", true);
            AniRep.AnimaRepCheck(Myanimator, "Hit");
            
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Myanimator.SetBool("Attack", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            Myanimator.SetBool("Attack", false);
    }

}
