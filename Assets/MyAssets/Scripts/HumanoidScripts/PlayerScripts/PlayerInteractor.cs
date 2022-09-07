using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class PlayerInteractor : MonoBehaviour
{
    public PlayerHandler player { get; set; }

    #region BodyParts

    [SerializeField] private Transform _head;
    [SerializeField] private Transform _hips;
    [SerializeField] private Transform _legs;
    [SerializeField] private Transform _ledgeCheckHor;

    public Transform Head { get { return _head; } }
    public Transform Hips { get { return _hips; } }
    public Transform Legs { get { return _legs; } }
    public Transform LedgeCheckHor { get { return _ledgeCheckHor; } }

    #endregion  

    #region Climb

    [Header("Climbing")]
    [SerializeField] private LayerMask climbMask;

    public LayerMask ClimbMask { get { return climbMask; } }

    #endregion

    #region Enemy

    [Header("Damage Enemy")]
    [SerializeField] private float _damageDistance = 1f;
    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private Transform _upAttack;
    [SerializeField] private Transform _downAttack;
    [SerializeField] private Transform _forwardAttack;

    #endregion

    private void Update() { }

    #region check functions

    public void CheckDamage(float damage, float direction)
    {
        Collider2D[] enemies;

        if (direction > 0)
        {
            enemies = Physics2D.OverlapCircleAll(_upAttack.position, _damageDistance, _enemyLayerMask);
        }
        else if (direction < 0)
        {
            enemies = Physics2D.OverlapCircleAll(_downAttack.position, _damageDistance, _enemyLayerMask);
        }
        else
        {
            enemies = Physics2D.OverlapCircleAll(_forwardAttack.position, _damageDistance, _enemyLayerMask);
        }

        if(enemies.Length>0)
        {
            foreach (var enemy in enemies)
            {
                IDamageable damageable = enemy.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.Damage(player.PlayerData.DamageAmount);
                }
            }
        }     
    }

    public bool CheckIfTouchingWall(float distance)
    {
        if (BodyInteractor(Hips.position, climbMask, distance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckIfTouchingLedge(float distance,LayerMask layerMask)
    {
        if(!BodyInteractor(Head.position, layerMask, distance))
        {
            return true;
        }
        else
        { 
            return false;
        }
    }

    public bool CheckIfCanEndClimb(float distance)
    {
        if (!BodyInteractor(Head.position, climbMask, distance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    #region other functions

    //public Vector2 DetermineCornerPosition(float distance)
    //{
    //    RaycastHit2D xHit = Physics2D.Raycast(Hips.position, transform.right, distance, climbMask);
    //    float xDist = xHit.distance;
    //    
    //   // float yDist = yHit.distance;
    //}

    #endregion

    public RaycastHit2D RayHit(Vector3 whicPart,Vector3 direction, LayerMask getMask, float distance)
    {
        RaycastHit2D _rayHit = Physics2D.Raycast(whicPart, direction, distance, getMask);

        return _rayHit;
    }

    #region Body Interactor

    public bool BodyInteractor(Vector3 whichPart, LayerMask getMask, float interactionDist)
    {
        bool bodyInteract = Physics2D.Raycast(whichPart, transform.right, interactionDist, getMask);
        Debug.DrawRay(whichPart, transform.right, Color.magenta);

        if (bodyInteract)
        {
            Debug.DrawRay(whichPart, transform.right, Color.green);
        }
        else
        {
            Debug.DrawRay(whichPart, transform.right, Color.magenta);
        }

        return bodyInteract;
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_forwardAttack.position, _damageDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_upAttack.position, _damageDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_downAttack.position,_damageDistance);
    }
}
