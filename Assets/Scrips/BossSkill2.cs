using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill2 : MonoBehaviour
{
    GameObject Effect;
    private void Start()
    {
        Effect = transform.GetChild(0).gameObject;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Effect.SetActive(true);
        Destroy(this.gameObject,1.3f);
    }
}
