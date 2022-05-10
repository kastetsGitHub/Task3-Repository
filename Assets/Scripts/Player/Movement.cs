using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public event UnityAction LandingEvent;

    private Rigidbody2D _rigidbody;

    [Header("Move settings:")]
    [SerializeField] [Range(0, 100f)] private float _speed;

    [Header("Jump settings:")]
    [SerializeField] [Range(0, 5)] private float _distanceToGroundCheck;
    [SerializeField] private float _forceOfJump;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private LayerMask _layerMask;


    private void Start()
    {        
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void MoveForward() => Move(Vector2.right);
    
    public void MoveBackward() => Move(Vector2.left);

    public void Jump()
    {
        if (IsGrounded())
        {
            _rigidbody.velocity = Vector2.up * _forceOfJump;
        }
    }

    private void Move(Vector3 direction)
    {
        float angleRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(transform.rotation.x, angleRotation, transform.rotation.z);
        transform.position += _speed * Time.deltaTime * direction;
    }

    private bool IsGrounded()
    {       
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, _distanceToGroundCheck, _layerMask);
        return hit.collider != null;
    }

    private bool CheckLanding(int layerCollision) => (_layerMask.value & (1 << layerCollision)) != 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CheckLanding(collision.gameObject.layer))
        {
            LandingEvent?.Invoke();           
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - _distanceToGroundCheck), Color.magenta);
    }
}
