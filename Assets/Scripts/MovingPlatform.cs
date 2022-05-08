using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector2 _startPosition;

    private SliderJoint2D _sliderJoint;
    private JointMotor2D _startedMotor;
    private JointMotor2D _targetMotor;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _sliderJoint = GetComponent<SliderJoint2D>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _startPosition = transform.position;
    }

    private void Start()
    {
        _startedMotor = _sliderJoint.motor;
        _targetMotor = new JointMotor2D()
        {
            motorSpeed = _startedMotor.motorSpeed * -1.0f,
            maxMotorTorque = _startedMotor.maxMotorTorque
        };
    }

    private void Update()
    {
        if (_rigidbody.velocity == Vector2.zero)
        {
            ChangeMotor();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }

    private void ChangeMotor()
    {
        var distanceToMax = Vector2.Distance(transform.position, _sliderJoint.connectedAnchor);
        var distanceToMin = Vector2.Distance(transform.position, _startPosition);

        if (distanceToMax <= 0.1f)
        {
            _sliderJoint.motor = _targetMotor;
        }
        else if (distanceToMin <= 0.1f)
        {
            _sliderJoint.motor = _startedMotor;
        }
    }
}
