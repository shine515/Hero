using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCtl : MonoBehaviour
{
    public GameObject Player;
    GameObject Target;
    GameObject Me;
    Animator Myanima;

    string[] AnimaPara = { "ModeTran", "Move", "run", "Attack", "Sense", "Taunt" };

    [SerializeField]
    StatusInfo StInfo = new StatusInfo();

    public bool Attack;
    bool Move;
    bool scan;
    bool FA; //선공격여부
    // Start is called before the first frame update
    void Start()
    {
        Me = this.gameObject;
        Myanima = this.gameObject.AddComponent<Animator>();
    }

    private void Update()
    {
        if (StInfo.HP == StInfo.MaxHP) { changeMode(false); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Attack)
        {
            Target = collision.gameObject;
            Target.GetComponent<StatusInfo>().HP -= StInfo.Damage;
        }

        if (StInfo.HP < StInfo.MaxHP) { changeMode(true); };
    }

    void changeMode(bool Mode)
    {
        Myanima.SetBool("Fight", Mode);
    }
}
