using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour
{
    [SerializeField] float moveDuration;
    [SerializeField] int moves;

    // Update is called once per frame
    void Update()
    {
        if (DOTween.IsTweening(transform)) return;

        Vector3 direction = Vector3.zero;

        // after two moves, change direction to make the car move backwards
        if (moves == 2)
        {
            direction = Vector3.left * 2;
            moves = -1;
        }
        else
        {
            direction += Vector3.right;
        }
        
        if (direction != Vector3.zero)
        {
            Move(direction);
            moves++;
        }
    }

    public void Move(Vector3 direction)
    {
        transform.DOMoveX(transform.position.x + direction.x, moveDuration).SetEase(Ease.Linear);
        transform.forward = direction;
    }
}
