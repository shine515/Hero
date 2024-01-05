using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 4f;
    Vector3 forward, right;
    public Animator animator;

    [SerializeField]
    private Animator Panima;
    [SerializeField]
    private Status PStatus;
    [SerializeField]
    private AnimRepCheck AniRep;

    List<string> UseKeyList = new List<string>() {"w","a","s","d" };

    // Start is called before the first frame update
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        animator = GetComponent<Animator>();
        PStatus = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)//UseKeyList.Contains(Input.inputString))
        {
            Move();
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }

    private bool HitTime =false;
    private bool AttackTime =false;
    private void OnTriggerEnter(Collider other)
    {
        GameObject Gob = other.gameObject;
        Debug.Log(Gob.gameObject.name);

        if (Gob.tag == "MONSATTACKPOS")
        {
            HitTime = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject Gob = collision.gameObject;
        Debug.Log(Gob.gameObject.name);
        if (Gob.tag == "Enemy")
        {
            AttackTime = true;
            Hitobj = Gob;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject Gob = collision.gameObject;
        Debug.Log(Gob.gameObject.name);
        if (Gob.tag == "Enemy")
        {
            AttackTime = false;
            Hitobj = Gob;
        }

    }

    void Move()
    {
        Vector3 RightMovement = right * moveSpeed * Time.smoothDeltaTime * Input.GetAxisRaw("Horizontal");
        Vector3 ForwardMovement = forward * moveSpeed * Time.smoothDeltaTime * Input.GetAxisRaw("Vertical");
        Vector3 FinalMovement = ForwardMovement + RightMovement;
        Vector3 direction = FinalMovement.normalized;

        //animator.SetFloat("Walk", 1);

        if (direction != Vector3.zero)
        {
            transform.forward = direction;
            transform.position += FinalMovement;
            animator.SetBool("Move", true); ;
        }
        else
        {
            animator.SetBool("Move", false);
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
