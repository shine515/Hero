using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class drop : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool isReturning = false;
    private bool isLand = false;

    void Start()
    {
        initialPosition = transform.position;
        
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Water"))
        {
            StartCoroutine(ReturnToSavedPositionCoroutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Land") && !isLand)
        {
            StartCoroutine(SavePositionCoroutine());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Land") && isLand)
        {
            isLand = false;
        }
    }

    IEnumerator SavePositionCoroutine()
    {
        isLand = true;
        while (isLand)
        {
            initialPosition = transform.position;
            yield return new WaitForSeconds(4f);
        }
    }

    IEnumerator ReturnToSavedPositionCoroutine()  //¼öÁ¤
    {
        /*float elapsedTime = 0f;
        float duration = 2f;*/
        Camera.main.GetComponent<CameraControl>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        transform.position = initialPosition ;
        Camera.main.GetComponent<CameraControl>().enabled = true;
        /*while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = initialPosition;
        */
    }
}
