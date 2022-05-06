using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAlive 
{
    public void TakeDamage(float damage);
    public void Death();
    public void Heal(float heal);
}
