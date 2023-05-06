using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] float rotateSpeed;
    [SerializeField] int value;
    [SerializeField] GameObject collectEffect;

    public int Value { get => value; }

    public void Collected()
    {   
        GetComponent<Collider>().enabled = false;
        // this.transform.DOJump(
        //     this.transform.position, 
        //     1, 1, 0.5f
        //     ).onComplete = SelfDestruct;
        Instantiate(collectEffect, transform.position, Quaternion.identity);
        SelfDestruct();
    }

    private void SelfDestruct()
    {
        Destroy(this.gameObject);
    }

    void Update()
    {
        transform.Rotate(0, 180 * rotateSpeed * Time.deltaTime, 0);
    }
}
