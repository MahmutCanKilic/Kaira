using MoreMountains.Feedbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Combat : CoreComponents, IDamageable //IKnockbackable
{
    //private bool isKnockbackActive;
    //private float knockbackStartTime;
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _currentHealth = 100;
    [SerializeField] private bool _canTakeDamage = true;

    public bool CanTakeDamage { get { return _canTakeDamage; } set { _canTakeDamage = value; } }

    public bool IsPlayerDead { get; private set; }
    
    
    public void LogicUpdate()
    {
        //  CheckKnockback();
    }

    public void Damage(float damageAmount)
    {
        if(_canTakeDamage)
        {
            Debug.Log(core.transform.name + " Damaged! " + damageAmount);

            _currentHealth -= damageAmount;

            CheckHealth();

            //_player.PlayerEvents.OnDamage();
            //_player.PlayerUIController.UpdateHealth();
            //

            //health ui guncellemek icin playeruicontrollera erisemiyorum
            //damage geldiginde stun yesin animasyona girsin
            //ses bozulsun
            //karakter flash effect
            //
        }
    }

    public void Heal(float healAmount)
    {
        _currentHealth += healAmount;

        CheckHealth();
    }

    void CheckHealth()
    {
        if(_currentHealth < 0)
        {
            _currentHealth = 0;
            Die();
        }
        else if(_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    public void Respawn()
    {
        // _player
        //lastCheckPoint
        _currentHealth = _maxHealth;
        IsPlayerDead = false;
        //_player.PlayerEvents.OnRespawnEvent();        
    }

    public void Die()
    {
        IsPlayerDead = true;
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        // core.Movement.SetVelocity(strength, angle, direction);
        // core.Movement.CanSetVelocity = false;
        // isKnockbackActive = true;
        // knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        //if (isKnockbackActive && core.Movement.CurrentVelocity.y <= 0.01f && core.CollisionSenses.Ground)
        //{
        //    isKnockbackActive = false;
        //    core.Movement.CanSetVelocity = true;
        //}
    }
}