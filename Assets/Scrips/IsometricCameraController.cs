using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class IsometricCameraController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 4f;
    Vector3 forward, right;
    public Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }
    void Update()
    {
        if (Input.anyKey)
        {
            Move();
        }
        else
        {
            animator.SetFloat("Walk", 0);
        }
    }

    void Move()
    {
        Vector3 RightMovement = right * moveSpeed * Time.smoothDeltaTime * Input.GetAxis("Horizontal");
        Vector3 ForwardMovement = forward * moveSpeed * Time.smoothDeltaTime * Input.GetAxis("Vertical");
        Vector3 FinalMovement = ForwardMovement + RightMovement;
        Vector3 direction = Vector3.Normalize(FinalMovement);

        //animator.SetFloat("Walk", 1);

        if (direction != Vector3.zero)
        {
            transform.forward = direction;
            transform.position += FinalMovement;
            animator.SetFloat("Walk", 1);
        }
        else
        {
            animator.SetFloat("Walk", 0);
        }
    }

}