using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Combat : CoreComponents, IDamageable
{

    [SerializeField] private float _health = 100;
    public void Damage(float damageAmount)
    {
        Debug.Log(core.transform.name + " Damaged! " + damageAmount);

        _health = _health - damageAmount;

        //playera ya da enemye erisemiyoruz state ayarlayamiyorum.
        //health ui guncellemek icin playeruicontrollera erisemiyorum
        //damage geldiginde stun yesin animasyona girsin
        //ses bozulsun
        //karakter flash effect
        //

    }

    public void LogicUpdate()
    {

    }
}