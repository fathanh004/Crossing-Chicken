using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Chicken : MonoBehaviour
{
    [SerializeField] float moveDuration;
    [SerializeField] float jumpPower;
    [SerializeField] int leftMoveLimit;
    [SerializeField] int rightMoveLimit;
    [SerializeField] int backMoveLimit;
    [SerializeField] Animator animator;
    private bool isDead = false;

    public UnityEvent<Vector3> OnJumpEnd;
    public UnityEvent<int> OnGetCoin;
    public UnityEvent OnDie;

    float timer;
    // Update is called once per frame  
    void Update()
    {
        if (isDead)
        {
            return;
        }
        //if no input in 5 seconds, do idle animation
        if (Input.anyKeyDown)
        {
            animator.SetBool("Turn Head", false);
            timer = 0;
        }

        if (timer >= 5)
        {
            animator.SetBool("Turn Head", true);
        }

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

        timer += Time.deltaTime;
    }
    public void Move(Vector3 direction)
    {
        var targetPosition = transform.position + direction;

        if (targetPosition.x < leftMoveLimit ||
            targetPosition.x > rightMoveLimit ||
            targetPosition.z < backMoveLimit ||
            Tree.AllPositions.Contains(targetPosition))
        {
            targetPosition = transform.position;
        }

        transform.
        DOJump(targetPosition,
            jumpPower,
            1,
            moveDuration).onComplete = BroadCastPositionOnJumpEnd;

        transform.forward = direction;
    }

    public void UpdateMoveLimit(int horizontalSize, int backLimit)
    {
        leftMoveLimit = -horizontalSize / 2;
        rightMoveLimit = horizontalSize / 2;
        backMoveLimit = backLimit;
    }

    private void BroadCastPositionOnJumpEnd()
    {
        OnJumpEnd.Invoke(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Car>(out var car))
        {
            if (isDead)
            {
                return;
            }
            transform.DOScaleY(0.1f, 0.2f);
            isDead = true;
            animator.enabled = false;
            Invoke("Die", 3);
        }
        else if (other.TryGetComponent<Coin>(out var coin))
        {
            OnGetCoin.Invoke(coin.Value);
            coin.Collected();
        }
        else if (other.TryGetComponent<Eagle>(out var eagle))
        {
            if (this.transform != eagle.transform)
            {
                this.transform.SetParent(eagle.transform);
                Invoke("Die", 3);
            }

        }

    }

    private void Die()
    {
        OnDie.Invoke();
    }
}
