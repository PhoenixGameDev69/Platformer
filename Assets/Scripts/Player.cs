using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(PlayerInputReader))]
public class Player : MonoBehaviour, ICreature
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _cooldownAttack;
    [SerializeField] private ColliderCheck _groundChecker;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private PlayerInputReader _inputReader;

    private float _timeToAttack;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _inputReader = GetComponent<PlayerInputReader>();

        _timeToAttack = Time.time + _cooldownAttack;
    }

    private void Update()
    {
        _animator.SetFloat("Y_Velocity", _rigidbody.velocity.y);
        _animator.SetBool("IsGrounded", _groundChecker.IsTouchingLayer);
    }

    public void HorizontalMovement(float direction)
    {
        var movement = new Vector2(_speed * direction, _rigidbody.velocity.y);
        _rigidbody.velocity = movement;

        _animator.SetFloat("X_Velocity", Mathf.Abs(direction));

        if (Mathf.Abs(direction) >= 0.1f && direction != transform.localScale.x)
        {
            FlipSprite();
        }
    }

    public void Jump()
    {
        if (_groundChecker.IsTouchingLayer)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _animator.SetTrigger("Jump");
        }
    }

    public void Attack()
    {
        if (_timeToAttack <= Time.time)
        {
            HorizontalMovement(0.0f);
            _animator.SetTrigger("Attack");
            _timeToAttack = Time.time + _cooldownAttack;
        }
    }

    public IEnumerator DeathInPoisonousPond()
    {
        _inputReader.enabled = false;
        _animator.SetTrigger("Death");

        var sprite = GetComponent<SpriteRenderer>();
        while (sprite.color.a >= 0.1f)
        {
            var newColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a);
            newColor.a = Mathf.Lerp(newColor.a, 0.0f, 0.1f);
            sprite.color = newColor;

            yield return new WaitForSeconds(0.1f);
        }

        Destroy(gameObject);
    }

    private void FlipSprite()
    {
        var newScale = new Vector3(-transform.localScale.x, 1.0f, 1.0f);
        transform.localScale = newScale;
    }
}
