using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicTurret : MonoBehaviour
{    
    [SerializeField] private int _projectileCount;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _cooldown = 1;

    private void Start()
    {
        if(_projectile.GetComponent<Projectile>() !=null)
        {
            StartCoroutine(Shooting());
        }
        else
        {
            Debug.LogError("Projectile Objesinde Projectile Scripti eksik");
        }
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(_cooldown);
            Instantiate(_projectile.gameObject, transform.position, transform.rotation);

            Projectile tmp = _projectile.GetComponent<Projectile>();
            tmp.rigid.AddForce(tmp.transform.right * tmp.Force,ForceMode2D.Impulse);
        }    
    }
}
