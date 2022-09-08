using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestAdmin : MonoBehaviour
{
    [SerializeField] private PlayerHandler _player;

    #region Damage/Heal
    [Header("Damage / Heal")]
    [SerializeField] private float _damageAmount = 20f;
    [SerializeField] private float _healAmount = 20f;

    #endregion

   
    void Start()
    {
        //bunun yerine playerstatic yapilabilirdi
        if(_player == null)
        {
            GameObject tmp = GameObject.FindGameObjectWithTag("Player");
            _player=tmp.GetComponent<PlayerHandler>(); 
        }
    }

    void Update()
    {
        if(Keyboard.current.yKey.wasPressedThisFrame)
        {
            _player.Core.Combat.Damage(_damageAmount);
        }

        if(Keyboard.current.uKey.wasPressedThisFrame)
        {
            _player.Core.Combat.Heal(_healAmount);
        }

        if(Keyboard.current.rKey.wasPressedThisFrame)
        {
            _player.Core.Combat.Respawn();
        }

        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            _player.Core.Combat.Die();
        }
    }


}
