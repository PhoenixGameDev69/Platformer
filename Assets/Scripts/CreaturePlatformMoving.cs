using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CreaturePlatformMoving : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Dictionary<Collider2D, ICreature> _creaturesStanding = new Dictionary<Collider2D, ICreature>();

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out ICreature creature))
        {
            _creaturesStanding.Add(collision, creature);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_creaturesStanding.ContainsKey(collision))
        {
            _creaturesStanding.Remove(collision);
        }
    }

    private void FixedUpdate()
    {
        if (_creaturesStanding.Count != 0)
        {
            foreach (var creature in _creaturesStanding.Values)
            {
                creature.AddVelocityPlatform(_rigidbody.velocity / 1.0005f);
            }
        }
    }
}
