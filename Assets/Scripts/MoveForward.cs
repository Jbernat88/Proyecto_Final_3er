using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 1000f;

    void Update()
    {
        //El objeto se mueve hacia delante constantemente
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
