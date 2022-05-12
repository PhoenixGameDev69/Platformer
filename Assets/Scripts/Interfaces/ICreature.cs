using System.Collections;
using UnityEngine;

public interface ICreature
{
    public IEnumerator DeathInPoisonousPond();
    public void AddVelocityPlatform(Vector2 velocity);
    
}
