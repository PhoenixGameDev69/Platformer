using System.Collections;
using UnityEngine;

public class Frog : MonoBehaviour, ICreature
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _wallsLayer;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private static readonly int JumpKey = Animator.StringToHash("Jump");
    private static readonly int BlinkKey = Animator.StringToHash("Blink");

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DoSomething();
    }

    public void DoSomething()
    {
        var randomNum = Random.Range(0, 5);

        switch (randomNum)
        {
            case 0:
            case 1:
            case 2:
                _animator.SetTrigger(JumpKey);
                break;
            case 3:
                _animator.SetTrigger(BlinkKey);
                break;
            case 4:
                FlipSprite();
                break;
        }

    }

    private void FlipSprite()
    {
        var newScale = transform.localScale;
        newScale.x *= -1;

        transform.localScale = newScale;
    }

    private void Jump()
    {
        var lineEnd = new Vector2(transform.position.x - 0.5f * transform.lossyScale.x, transform.position.y);
        var wall = Physics2D.Linecast(transform.position, lineEnd, _wallsLayer);

        if (wall)
        {
            FlipSprite();
        }

        _rigidbody.AddForce(new Vector2(-transform.localScale.x * _jumpForce, 0), ForceMode2D.Impulse);
    }

    public IEnumerator DeathInPoisonousPond()
    {

        _animator.enabled = false;

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
}
