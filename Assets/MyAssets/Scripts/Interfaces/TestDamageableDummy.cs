using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamageableDummy : MonoBehaviour, IDamageable
{
    public void Damage(float damageAmount)
    {
        Debug.Log(transform.name + " hasar yedim " + damageAmount);
    }
}
