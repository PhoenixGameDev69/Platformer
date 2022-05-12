using System;
using UnityEngine;

[RequireComponent(typeof(SliderJoint2D), typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float _distanceToChangeDirection;

    private bool _isForward = true;

    private Vector2 _startPosition;
    private Vector2 _endPosition;

    private Rigidbody2D _rigidbody;
    private SliderJoint2D _sliderJoint;
    private JointMotor2D _startMotor;
    private JointMotor2D _returnedMotor;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sliderJoint = GetComponent<SliderJoint2D>();
        _startMotor = _sliderJoint.motor;

        _returnedMotor = new JointMotor2D()
        {
            motorSpeed = _startMotor.motorSpeed * -1.0f,
            maxMotorTorque = _startMotor.maxMotorTorque
        };

        _startPosition = transform.position;
        _endPosition = _sliderJoint.connectedAnchor;
    }

    private void Update()
    {
        if (_rigidbody.velocity == Vector2.zero)
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        if(_isForward)
        {
            var distance = Vector2.Distance(transform.position, _endPosition);

            if(distance <= _distanceToChangeDirection)
            {
                _isForward = false;
                _sliderJoint.motor = _returnedMotor;
            }
        }
        else
        {
            var distance = Vector2.Distance(transform.position, _startPosition);

            if (distance <= _distanceToChangeDirection)
            {
                _isForward = true;
                _sliderJoint.motor = _startMotor;
            }
        }
    }
}
