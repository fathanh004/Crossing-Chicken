using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Chicken : MonoBehaviour
{
    [SerializeField] AudioSource bwackAudioSource;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float moveDuration;
    [SerializeField] float jumpPower;
    [SerializeField] int leftMoveLimit;
    [SerializeField] int rightMoveLimit;
    [SerializeField] int backMoveLimit;
    [SerializeField] Animator animator;
    private bool isUnmoveable = false;

    public UnityEvent<Vector3> OnJumpEnd;
    public UnityEvent<int> OnGetCoin;
    public UnityEvent OnDie;
    public UnityEvent OnMovingToBackLimit;

    //for mobile input
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;


    float timer;
    // Update is called once per frame  

    private void Start()
    {
        AudioListener.volume = 1;
    }

    void Update()
    {
        if (isUnmoveable)
        {
            return;
        }

        //for mobile input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                HandleSwipe();
            }
        }

        //if no input in 5 seconds, do idle animation
        if (Input.anyKeyDown || Input.touchCount > 0)
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
            Tree.AllPositions.Contains(targetPosition))
        {
            targetPosition = transform.position;
        }

        if (targetPosition.z == backMoveLimit)
        {
            OnMovingToBackLimit.Invoke();
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
        //horizontalSize = Mathf.FloorToInt(horizontalSize * 2/5);
        leftMoveLimit = (-horizontalSize / 2);
        rightMoveLimit = (horizontalSize / 2);
        backLimit += 5;
        backMoveLimit = backLimit;
    }

    private void BroadCastPositionOnJumpEnd()
    {
        OnJumpEnd.Invoke(transform.position);
    }

    public void PlayBwackSound()
    {
        bwackAudioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Car>(out var car))
        {
            if (isUnmoveable)
            {
                return;
            }
            transform.DOScaleY(0.1f, 0.2f);
            if (isUnmoveable == false)
            {
                audioSource.Play();
            }
            isUnmoveable = true;
            animator.enabled = false;
            Invoke("Die", 2);
        }
        else if (other.TryGetComponent<Coin>(out var coin))
        {
            OnGetCoin.Invoke(coin.Value);
            coin.Collected();
        }
        else if (other.TryGetComponent<Eagle>(out var eagle))
        {
            if (this.transform.parent == null)
            {
                this.transform.SetParent(eagle.transform);
                Invoke("Die", 2);
            }

        }
        else if (other.TryGetComponent<Train>(out var train))
        {
            if (isUnmoveable)
            {
                return;
            }
            transform.DOScaleY(0.1f, 0.2f);
            if (isUnmoveable == false)
            {
                audioSource.Play();
            }
            isUnmoveable = true;
            animator.enabled = false;
            Invoke("Die", 2);
        }

    }

    private void Die()
    {
        AudioListener.volume = 0;
        OnDie.Invoke();
    }

    public void SetUnmoveable(bool value)
    {
        isUnmoveable = value;
    }

    void HandleSwipe()
    {
        Vector3 direction = Vector3.zero;
        Vector2 swipeDirection = endTouchPosition - startTouchPosition;

        if (swipeDirection.magnitude < 20)
        {
            return;
        }

        swipeDirection.Normalize();

        

        if (swipeDirection.y > 0.5f)
        {
            direction += Vector3.forward;
        }
        else if (swipeDirection.y < -0.5f)
        {
            direction += Vector3.back;
        }
        else if (swipeDirection.x < -0.5f)
        {
            direction += Vector3.left;
        }
        else if (swipeDirection.x > 0.5f)
        {
            direction += Vector3.right;
        }

        if (direction != Vector3.zero)
        {
            Move(direction);
        }
    }
}
