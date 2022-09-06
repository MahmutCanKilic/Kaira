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

    #region 

    [Header("Climbing")]
    [SerializeField] private LayerMask climbMask;

    public LayerMask ClimbMask { get { return climbMask; } }

    #endregion

    private void Awake()
    {
        //player = GetComponentInParent<PlayerHandler>(); //playerdan direkt tanittik
    }

    private void Update()
    {
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

   //public bool BodyInteractor(Vector3 whichPart, LayerMask getMask, float interactionDist, out RaycastHit2D sendHit)
   //{
   //    bool bodyInteract = Physics2D.Raycast(whichPart, transform.right, out sendHit, interactionDist, getMask);
   //
   //    if (bodyInteract)
   //    {
   //        Debug.DrawRay(whichPart, transform.right, Color.green);
   //    }
   //    else
   //    {
   //        Debug.DrawRay(whichPart, transform.right, Color.magenta);
   //    }
   //
   //    return bodyInteract;
   //}

    #endregion
}
