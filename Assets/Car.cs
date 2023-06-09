using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField, Range(0, 10)] float speed;
    Vector3 initialPosition;
    float distanceLimit = float.MaxValue;

    public void SetUpDistanceLimit(float distance)
    {
        this.distanceLimit = distance;
    }

    public void SetUpSpeed(float speed)
    {
        this.speed = speed;
    }

    private void Start()
    {   
        audioSource.Play();
        initialPosition = this.transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Vector3.Distance(initialPosition, this.transform.position) > this.distanceLimit)
        {
            Destroy(this.gameObject);
        }
    }


}
