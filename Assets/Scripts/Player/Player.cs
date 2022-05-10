using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public event UnityAction<Item> OnHasTook; 

    [SerializeField] private Movement _movement;
    [SerializeField] private Animator _animator;

    private int _runID;
    private int _jumpID;

    private void OnEnable()
    {
        _movement.LandingEvent += OnLanding;
    }

    private void OnDisable()
    {
        _movement.LandingEvent -= OnLanding;
    }

    private void Start()
    {
        _runID = Animator.StringToHash("IsRun");
        _jumpID = Animator.StringToHash("IsJump");
    }

    private void Update()
    {
        bool rightMovePressed = Input.GetKey(KeyCode.D);
        bool leftMovePressed = Input.GetKey(KeyCode.A);
        bool jumpPressed = Input.GetKeyDown(KeyCode.W);
        
        if (rightMovePressed)
        {
            _movement.MoveForward();
            PlayAnimation(_runID);
        }

        if (leftMovePressed)
        {
            _movement.MoveBackward();
            PlayAnimation(_runID);
        }

        if(jumpPressed) 
        {
            _movement.Jump();
            PlayAnimation(_jumpID);
        }

        if (!rightMovePressed && !leftMovePressed)
        {
            StopAnimation(_runID);
        }
    }

    public void GetItem(Item item) => OnHasTook?.Invoke(item);

    private void OnLanding() => StopAnimation(_jumpID);

    private void PlayAnimation(int idAnimation) => _animator.SetBool(idAnimation, true);

    private void StopAnimation(int idAnimation) => _animator.SetBool(idAnimation, false);
}
