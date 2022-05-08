using UnityEngine;

public class PoisonousPond : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICreature creature))
        {
            StartCoroutine(creature.DeathInPoisonousPond());
        }
    }
}
