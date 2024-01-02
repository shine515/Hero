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
    private AnimRepCheck AniRep;

    Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = this.transform;
    }

    // Update is called once per frame
    float v;
    float h;
    float r;
    void Update()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        GameObject Gob = other.gameObject;

        Debug.Log(Gob.gameObject.name);
        if (Gob.tag == "Enemy")
        {
            Debug.Log("ÄÝ Ãæ");
            Panima.SetTrigger("Hit");
            AniRep.AnimaRepCheck(Panima, "Hit");
        }
    }

}
