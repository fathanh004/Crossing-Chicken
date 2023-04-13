using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Chicken : MonoBehaviour
{
    [SerializeField] float moveDuration;
    [SerializeField] float jumpPower;

    // Update is called once per frame
    void Update()
    {
        if (DOTween.IsTweening(transform)) return;

        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction += Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction += Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction += Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction += Vector3.right;
        }

        if (direction != Vector3.zero)
        {
            Move(direction);
            direction = Vector3.zero;
        }

    }
    public void Move(Vector3 direction)
    {
        // isMoving = true;
        //transform.DOJump(transform.position + direction, 0.5f, 1, 0.2f).OnComplete(() => isMoving = false);
        transform.DOJump(transform.position + direction, jumpPower, 1, moveDuration);
        transform.forward = direction;
    }
}
