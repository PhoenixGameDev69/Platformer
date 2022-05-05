using UnityEngine;

public class PlayerInputReader : MonoBehaviour
{
    private Player _player;
    private float _direction;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _direction = Input.GetAxisRaw(GlobalStringVars.HorizontalAxis);
        _player.HorizontalMovement(_direction);

        if (Input.GetButtonDown(GlobalStringVars.JumpAxis))
        {
            _player.Jump();
        }

        if (Input.GetButtonDown(GlobalStringVars.AttackAxis))
        {
            _player.Attack();
        }

    }
}
