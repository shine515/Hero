using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform playertransform;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = this.transform.position - playertransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = playertransform.position + offset;
    }
}
