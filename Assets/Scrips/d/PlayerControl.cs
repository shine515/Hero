using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    public float trunSpeed;

    [SerializeField]
    private Animator Panima;
    [SerializeField]
    private StatusInfo PStatus;
    [SerializeField]
    private AnimRepCheck AniRep;

    Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = this.transform;
        PStatus = GetComponent<StatusInfo>();
    }

    // Update is called once per frame
    float v;
    float h;
    float r;
    void Update()
    {
        ///캐릭터 움직임
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        r = Input.GetAxis("Mouse X");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        if (Input.GetKey("a") || Input.GetKey("w") || Input.GetKey("d") || Input.GetKey("s"))
        {
            tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
        }
        else { v = h = 0; }
        tr.Rotate(Vector3.up * trunSpeed * Time.deltaTime * r);
    }

    private bool HitTime =false;
    private bool AttackTime =false;
    private void OnTriggerEnter(Collider other)
    {
        GameObject Gob = other.gameObject;

        if (Gob.tag == "MONSATTACKPOS")
        {
            HitTime = true;
        }
        else if (Gob.tag == "Enemy")
        { AttackTime = true;
            Hitobj = Gob;
        }
    }

    private void Hit(float Damage) //맞으면 실행 됨
    {
        if (HitTime)
        {
            PStatus.HP -= Damage;
            Panima.SetTrigger("Hit");
            AniRep.AnimaRepCheck(Panima, "Hit");
            HitTime = false;
        }
    }
    public GameObject Hitobj;
    private void Attack()
    {
        if (AttackTime)
        {
            GameObject Attobj = gameObject;
            Hitobj.SendMessage("Hit", PStatus.Damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
