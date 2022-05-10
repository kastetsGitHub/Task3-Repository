using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Opossum : Enemy
{
    [SerializeField] private float _timePatrol;
    [SerializeField] private float _distancePatrol;
    [SerializeField] private Vector2 _startDirectionPatrol;

    private void Start()
    {
        Patrol();
    }

    public override void Patrol()
    {
        float currentPointX = transform.position.x - _distancePatrol;
        transform.DOMoveX(currentPointX, _timePatrol).OnComplete(() => FlipDirection()).SetAutoKill(true);
    }

    private void FlipDirection()
    {
        int direction = -1;
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= direction;
        _distancePatrol *= direction;
        gameObject.transform.localScale = currentScale;
        Patrol();
    }

    private void OnDrawGizmos()
    {
        if (_distancePatrol > 0 && !Application.IsPlaying(this))
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, transform.position - new Vector3(_distancePatrol, 0, 0));
        }
    }
}
