using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] PlayerHandler _player;

    [SerializeField] private int _weaponIndex;
    [SerializeField] private Weapon[] _weapons;
    [SerializeField] private Weapon _currentWeapon;

    public Weapon CurrentWeapon { get { return _currentWeapon; } }

   
    private void Awake()
    {
        _player = GetComponentInParent<PlayerHandler>();
    }

    private void Start()
    {
        _currentWeapon = _weapons[0];
    }

    private void Update()
    {
        if(_player.InputHandler.IsWeaponUp)
        {
            UpdateWeapon(1);
        }
        else if(_player.InputHandler.IsWeaponDown)
        {
            UpdateWeapon(-1);
        }
    }

    void UpdateWeapon(int mult)
    {
        _weaponIndex = _weaponIndex + (1 * mult);

        if(_weaponIndex > _weapons.Length - 1)
        {
            _weaponIndex = 0;
        }
        else if(_weaponIndex < 0)
        {
            _weaponIndex = _weapons.Length - 1;
        }

        _currentWeapon = _weapons[_weaponIndex];
    }
}
