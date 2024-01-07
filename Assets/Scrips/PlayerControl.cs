using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerControl : MonoBehaviour
{
    float hAxis;
    float vAxis;
    Vector3 moveVec;
    Vector3 dodgeVec;
    [SerializeField]
    float moveSpeed = 4f;

    public float force = 2f;

    bool isDie = false;

    /*[SerializeField]
    float moveSpeed = 4f;
    Vector3 forward, trun;*/
    public Animator animator;

    [SerializeField]
    private Status Status;
    [SerializeField]
    private AnimRepCheck animRepCheck;
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private float AttDelay;
    [SerializeField]
    private GameObject HitEff;

    //public Collider MyWeapCollider;

    bool isAttReady = false;
    bool canMove = true;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();  //�굵
        Status = GetComponent<Status>();  //public ����

        weapon = Status.nowWeap.GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDie)
        {
            if (Status.HP <= 0)
            {
                Die();
            }
            if (canMove)
                Move();
            else
                animator.SetBool("Move", false);
            Attack();
        }
    }

    void Die()
    {
        this.GetComponent<Animator>().Play("Die");
        isDie = true;

        List<Collider> nearMonList = new List<Collider>(Physics.OverlapSphere(this.transform.position, 20, 1 << 6)); //(�߽�, �ݰ�, ���̾�)(���� 6�� ���̾�)

        if (nearMonList.Count < 1) Debug.Log("����!");
        else
        {foreach (Collider Mon in nearMonList)  //���� ����� �� ã��
            {
                Mon.SendMessage("Victory",SendMessageOptions.RequireReceiver);
            }
        }
    }

            void Move()
    {
        /// �����¿� �Է°� �޾ƿ���///
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        ///ĳ���� ������///
        /*if (isDodge)
        {
            moveVec = dodgeVec;
        }
        else*/ //ȸ�Ǳ⵿ �߶� ��Ұ� ������ �������� ���� �� ����
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        transform.position += moveVec * moveSpeed * Time.deltaTime;
        animator.SetBool("Move", moveVec != Vector3.zero);

        ///ĳ���� ȸ��///
        transform.LookAt(transform.position + moveVec);
    }

    /// <summary>
    /// �ǰ� �� �ڵ�
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MONSATTACKPOS") && !isDie)
        {
            Monster mos = other.GetComponent<Monster>();
            float Damage = mos.Damage;
            Vector3 pos = other.ClosestPoint(transform.position);
            Quaternion rot = Quaternion.LookRotation(pos);
            GameObject blood = Instantiate(HitEff, pos, rot);
            Destroy(blood, 1f);
            transform.LookAt(other.transform);
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * force);
            IsHit(Damage);
        }
        else if(other.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();
            switch (item.type.ToString())
            {
                case "Coin":
                    Status.Coin += item.value;
                    break;
                case "Essense":
                    Status.Essense += item.value;
                    break;
            }
            
        }
    }

    private void IsHit(float Damage)
    {
        Status.HP -= Damage;
        CantMove();
        animRepCheck.AnimaRepCheck(animator, "Hit");
        //GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * force);
        Invoke("HitEnd", 0.1f);
    }

    private void HitEnd()
    {
        CanMove();
    }
    /*IEnumerator Dodge() //�ϴ� ����..
    {
        if (!isDodge)
        {
            Debug.Log("ȸ��");
            isDodge = true;
            animator.SetTrigger("Dodge");
            dodgeVec = moveVec;
            moveSpeed *= 1.5f;
            yield return new WaitForSeconds(0.8f);
            moveSpeed /= 1.5f;

            yield return new WaitForSeconds(0.5f);
            isDodge = false;
        }
    }*/

    private void CantMove()
    { canMove = false; }
    private void CanMove()
    { canMove = true; }

    /// <summary>
    /// �����ڵ�
    /// </summary>
    private void Attack()
    {
        AttDelay += Time.deltaTime;
        isAttReady = weapon.AttSpeed < AttDelay;

        if (Input.GetMouseButtonDown(0))
        {
            if (isAttReady)
            {
                CantMove();
                LookatEnemy(15f, 6);
                animator.SetBool("Attack", true);
                AttDelay = 0;
            }
        }
        

        
    }

    private void attack() //�ִϸ��̼ǿ��� �۵� 
    {
        animator.SetBool("Attack", false);
        eff();
    }

    private void eff()
    {
        weapon.Attarea.SetActive(true);
    }

    private void enabledEffect()//�ִϸ��̼ǿ��� �۵� 
    {
        weapon.Attarea.SetActive(false);
        CanMove();
    }


    /*Vector3 RightMovement = trun * moveSpeed * Time.smoothDeltaTime * Input.GetAxisRaw("Horizontal");
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
    }*/


    private void LookatEnemy(float AttRange, int layer)
    {
        ///��ó Enemy���̾��� �ݸ��� ���� ���///
        Collider Target;
        List<Collider> WpRange = new List<Collider>(Physics.OverlapSphere(this.transform.position, AttRange, 1 << layer)); //(�߽�, �ݰ�, ���̾�)(���� 6�� ���̾�)

        if (WpRange.Count < 1) Debug.Log("����!");
        else
        {
            Target = WpRange[0]; //����Ʈ ù��° Ÿ�� ����
            float shortDis = Vector3.Distance(this.gameObject.transform.position, WpRange[0].transform.position); // ù��°�� �������� ����ֱ� 
            foreach (Collider shortist in WpRange)  //���� ����� �� ã��
            {
                float Distance = Vector3.Distance(gameObject.transform.position, shortist.transform.position);

                if (Distance < shortDis) // ������ ���� �������� �Ÿ� ���
                {
                    Target = shortist;
                    shortDis = Distance;
                }
            }
            this.transform.LookAt(Target.transform.position);  //�� �������� ȸ��
        }
    }
}
    


