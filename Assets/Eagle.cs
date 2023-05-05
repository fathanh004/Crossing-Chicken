using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField, Range(0, 50)] float speed = 25;

    private void Start() {
        audioSource.Play();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

}
