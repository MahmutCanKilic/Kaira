using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/PlayerData/BaseData")]
public class PlayerData : ScriptableObject
{
    //#region Move Variables
    //
    //[Header("Move State")]
    ////[SerializeField] private float _walkSpeed = 2;
    ////[SerializeField] private float _sprintSpeed = 6;
    ////
    ////public float WalkSpeed { get { return _walkSpeed; } }
    ////public float SprintSpeed { get { return _sprintSpeed; } }
    //
    //#endregion

    #region Combat

    [SerializeField] private float _damageAmount = 10f;
    [SerializeField] private float _damageDistance = 1f;
    public float DamageAmount { get { return _damageAmount; } }
    public float DamageDistance { get { return _damageDistance; } }
    #endregion

    #region Jump Variables with getters and setters 

    [Header("Jumping Variables")]
    [SerializeField] private int _amountOfJumps = 1;
    [SerializeField] private float _maxJumpHeight = 4.0f;
    [SerializeField] private float _maxJumpTime = 0.75f;

    public int AmountOfJumps { get { return _amountOfJumps; } }
    public float MaxJumpHeight { get { return _maxJumpHeight; } }
    public float MaxJumpTime { get { return _maxJumpTime; } }

    #endregion

    #region InAirState Variables

    [Header("InAirState Variables")]
    [SerializeField] private float _coyoteTime = 0.25f;
    [SerializeField] private float _fallMultiplier = 2f;
    public float CoyoteTime { get { return _coyoteTime; } }
    public float FallMultiplier { get { return _fallMultiplier; } }


    #endregion

    #region Dash Variables

    [Header("Dash Variables")]
    [SerializeField] private int _amountOfDashes = 1;
    [SerializeField] private float _dashSpeed = 2f;     //playerdata**
    [SerializeField] private float _dashAttackVelocity = 2f;       //playerdata**
    public int AmountOfDashes { get { return _amountOfDashes; } }
    public float DashVelocity { get { return _dashSpeed; } } //playerdata**
    public float DashAttackVelocity { get { return _dashAttackVelocity; } } //playerdata**

    #endregion

    #region Climbing Variables

    [Header("Climbing Variables")]
    [SerializeField] private float _wallCheckDist = 0.5f;
    [SerializeField] private float _wallSlideVelocity = 3f;
    [SerializeField] private float _wallJumpVelocityY = 20;
    [SerializeField] private float _wallJumpVelocityX = 20;
    [SerializeField] private Vector2 _wallJumpAngle = new Vector2(1,2);
    public float WallCheckDist { get { return _wallCheckDist; } }
    public float WallSlideVelocity { get { return _wallSlideVelocity; } }
    public float WallJumpVelocityY { get { return _wallJumpVelocityY; } }
    public float WallJumpVelocityX { get { return _wallJumpVelocityX; } }
    public Vector2 WallJumpAngle { get { return _wallJumpAngle; } }

    #endregion

    #region LedgeClimb

    [Header("Ledge Climb")]
    [SerializeField] private Vector2 _startOffSet;
    [SerializeField] private Vector2 _stopOffset;

    public Vector2 StartOffSet { get { return _startOffSet; } }
    public Vector2 StopOffset { get { return _stopOffset; } }

    #endregion

}
