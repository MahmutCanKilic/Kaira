using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : BaseHumanoid
{
    #region States
    public PlayerGroundedState GroundState { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerRespawnState RespawnState { get; private set; }
    public PlayerDyingState DyingState { get; private set; }
    // public PlayerDashAttack DashAttackState { get; private set; }
    public PlayerPrimaryAttackState PrimaryAttackState { get; private set; }
    public PlayerSecondaryAttackState SecondaryAttackState { get; private set; }
    #endregion

    #region playerData

    [Header("Playerdata")]
    [SerializeField] protected PlayerData _playerData;
    public PlayerData PlayerData { get { return _playerData; } }

    #endregion

    #region Components

    public InputHandler InputHandler { get; private set; }
    //public PlayerCameraController PlayerCameraController { get; private set; }
    public PlayerEvents PlayerEvents { get; private set; }

    #endregion

    #region Ability Variables

    [SerializeField] private bool _isAbilityDone;
    public bool IsAbilityDone { get { return _isAbilityDone; } set { _isAbilityDone = value; } }

    #endregion

    public override void Awake()
    {
        base.Awake();

        #region components

        InputHandler = GetComponent<InputHandler>();
        PlayerEvents = GetComponentInChildren<PlayerEvents>();

        #endregion      

        #region player instance and initializes

        PlayerInteractor.player = this;

        #endregion
    }

    public override void Start()
    {
        base.Start();

        #region states

        IdleState = new PlayerIdleState(this, stateMachine, _playerData, "idle");
        WalkState = new PlayerWalkState(this, stateMachine, _playerData, "walk");
        DashState = new PlayerDashState(this, stateMachine, _playerData, "dash");
        LedgeClimbState = new PlayerLedgeClimbState(this, stateMachine, _playerData, "ledgeClimb"); //suan kapali 07.09.2022
        WallJumpState = new PlayerWallJumpState(this, stateMachine, _playerData, "inAir");
        WallSlideState = new PlayerWallSlideState(this, stateMachine, _playerData, "wallslide");

        RespawnState = new PlayerRespawnState(this, stateMachine, _playerData, "respawning");
        DyingState = new PlayerDyingState(this, stateMachine, _playerData, "dying");
        //DashAttackState = new PlayerDashAttack(this, stateMachine, _playerData, "dashAttack");
        PrimaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, _playerData, "primaryAttack");
        SecondaryAttackState = new PlayerSecondaryAttackState(this, stateMachine, _playerData, "secondaryAttack");

        GroundState = new PlayerGroundedState(this, stateMachine, _playerData, "grounded");
        JumpState = new PlayerJumpState(this, stateMachine, _playerData, "jump");
        InAirState = new PlayerInAirState(this, stateMachine, _playerData, "inAir");

        #endregion

        SetupJumpVariables();

        #region player instance and initializes

        // PlayerCameraController = PlayerCameraController.instance;

        stateMachine.Initialize(GroundState);

        #endregion
    }

    public override void Update()
    {
        base.Update();

        #region update other scripts

        InputHandler.HandleAllInputs();

        #endregion

        Debug.DrawRay(PlayerInteractor.Hips.position, transform.right * PlayerData.WallCheckDist, Color.cyan);
        Debug.DrawRay(PlayerInteractor.LedgeCheckHor.position, Vector2.down * (PlayerInteractor.LedgeCheckHor.position.y - PlayerInteractor.Hips.position.y), Color.black);              
    }

    public override void FixedUpdate() { base.FixedUpdate(); }      

    //bu burada kaldi kekwait
    private void SetupJumpVariables()
    {
        float timeToApex = PlayerData.MaxJumpTime / 2;
        Core.Movement.Gravity = (-2 * PlayerData.MaxJumpHeight) / Mathf.Pow(timeToApex, 2);
        Core.Movement.InitialJumpVelocity = (2 * PlayerData.MaxJumpHeight) / timeToApex;

        Core.Movement.JumpGravity = Core.Movement.Gravity;
    } 
}
//berkaynpc