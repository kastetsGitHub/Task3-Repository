using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Frog : Enemy
{
    [SerializeField] private float _distancePatrol;
    [SerializeField] private float _timePatrol;
    [SerializeField] private Animator _animator;

    private int _jumpID;

    private void Start()
    {
        _jumpID = Animator.StringToHash("IsJump");
        Patrol();
    }

    public override void Patrol()
    {
        float currentPointX = transform.position.x - _distancePatrol;
        PlayAnimation(_jumpID);
        transform.DOMoveX(currentPointX, _timePatrol).OnComplete(() => FlipDirection()).SetAutoKill(true);
    }

    private void FlipDirection()
    {
        StopAnimation(_jumpID);
        int direction = -1;
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= direction;
        _distancePatrol *= direction;
        gameObject.transform.localScale = currentScale;
        StartCoroutine(Waiting());
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(_timePatrol);
        Patrol();
    }

    private void PlayAnimation(int idAnimation) => _animator.SetBool(idAnimation, true);

    private void StopAnimation(int idAnimation) => _animator.SetBool(idAnimation, false);

    private void OnDrawGizmos()
    {
        if (_distancePatrol > 0 && !Application.IsPlaying(this))
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, transform.position - new Vector3(_distancePatrol, 0, 0));
        }
    }


}
