using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type
    {
        Coin,
        Essense
    }
    public Type type;
    public int value;
    [SerializeField]
    private GameObject GetEff;

    public Rigidbody body;

    Collider[] colls;

    private void Start()
    {
        colls = this.GetComponents<Collider>();
        StartCoroutine(sponItem());
        StartCoroutine(DestroyItem());
    }


    private void Update()
    {
        
        transform.Rotate(Vector3.up*50*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 pos = other.ClosestPoint(transform.position);
            Quaternion rot = Quaternion.LookRotation(pos);
            GameObject blood = Instantiate(GetEff, pos, rot);
            Destroy(gameObject.transform.parent.gameObject, 0.1f);
        }
    }

    IEnumerator sponItem()
    {
        transform.parent.rotation = Quaternion.Euler(new Vector3(0, Random.Range(1, 300), 0));

        yield return new WaitForSeconds(0.1f);
        this.GetComponentInParent<Rigidbody>().AddRelativeForce((Vector3.forward + Vector3.up) * 800);
        yield return new WaitForSeconds(1.5f);
        foreach (Collider coll in colls)
        {
            coll.enabled = true;
        }
        this.GetComponent<Collider>().enabled = true;
    }

    IEnumerator DestroyItem() {
        yield return new WaitForSeconds(300f);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
