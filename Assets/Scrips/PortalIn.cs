using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalIn : MonoBehaviour
{
    public Transform TranslatePosition;

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("�浹üũ: "+col.gameObject.tag);
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.position = GameObject.Find("Portal_destination").transform.position;

           
        }
    }
}