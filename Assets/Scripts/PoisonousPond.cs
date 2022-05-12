using System.Collections.Generic;
using UnityEngine;

public class PoisonousPond : MonoBehaviour
{
    private List<string> _died = new List<string>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_died.Contains(collision.name)) { return; }

        if (collision.TryGetComponent(out ICreature creature))
        {
            _died.Add(collision.name);
            StartCoroutine(creature.DeathInPoisonousPond());
        }
    }
}
