using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float _force;
    [SerializeField] private float _damageAmount;
    [SerializeField] private float _destroyTime;

    public Rigidbody2D rigid { get { return rb; } }
    public float Force { get { return _force; } }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    public void InstantiateMe()
    {
        //StartCoroutine(DestroyMe());
       // rb.AddForce(_force * transform.right, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage(_damageAmount);
            }
        }       
    }

    IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(_destroyTime);

        Destroy(this);
    }
}
