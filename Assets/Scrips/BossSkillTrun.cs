using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossSkillTrun : MonoBehaviour
{
    private NavMeshAgent agent;
    Collider coll;
    GameObject Boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        { Destroy(this.gameObject);
            Debug.Log("삭제");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("생성");
        Boss = GameObject.FindWithTag("Boss");
        agent = GetComponent<NavMeshAgent>();
        coll = GetComponent<Collider>();
        StartCoroutine(LifeTime());
        this.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 600);
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(0.3f);
        coll.enabled = true;

        yield return new WaitForSeconds(1f);
        agent.SetDestination(Boss.transform.position);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
