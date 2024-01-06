using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalIn : MonoBehaviour
{
    public Transform Portal_destination;

    // Ãâ±¸Æ÷Å» transform
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(FreezeForDuration(col.gameObject, 1f));
        }
    }
    IEnumerator FreezeForDuration(GameObject player, float duration)
    {
        Vector3 exitDirection = Portal_destination.forward; 

        Time.timeScale = 0f;
        
        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;

        player.gameObject.transform.position = GameObject.Find("Portal_destination").transform.position;
        player.transform.rotation = Portal_destination.rotation; 

    }

}