using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : BaseHumanoid
{
    public bool _showDebug = true;

    #region States
    public PlayerGroundedState GroundState { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    // public PlayerJumpAttack JumpAttackState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    // public PlayerLandState LandState { get; private set; }
    // public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    // public PlayerDashAttack DashAttackState { get; private set; }
    public PlayerPrimaryAttackState PrimaryAttackState { get; private set; }
    // public PlayerAttackState SecondaryAttackState { get; private set; }
    // public PlayerPushPullState PushPullState { get; private set; }
    // public PlayerCarryOnHeadState CarryOnHeadState { get; private set; }
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
        DashState = new PlayerDashState(this, stateMachine, _playerData, "dash"); //"inAir" olabilir
        //LandState = new PlayerLandState(this, stateMachine, _playerData, "land");
        //WallClimbState = new PlayerWallClimbState(this, stateMachine, _playerData, "wallClimb");
        LedgeClimbState = new PlayerLedgeClimbState(this, stateMachine, _playerData, "ledgeClimb");
        WallJumpState = new PlayerWallJumpState(this, stateMachine, _playerData, "inAir");
        WallSlideState = new PlayerWallSlideState(this, stateMachine, _playerData, "wallslide");

        //DashAttackState = new PlayerDashAttack(this, stateMachine, _playerData, "dashAttack");
        PrimaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, _playerData, "primaryAttack");
        //SecondaryAttackState = new PlayerAttackState(this, stateMachine, _playerData, "attack");
        //JumpAttackState = new PlayerJumpAttack(this, stateMachine, _playerData, "jumpAttack");

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

        AnimationController.animator.SetBool("isMoving", Core.Movement.IsMovementPressed);

        Debug.DrawRay(PlayerInteractor.Hips.position, transform.right * PlayerData.WallCheckDist, Color.cyan);
        Debug.DrawRay(PlayerInteractor.LedgeCheckHor.position, Vector2.down * (PlayerInteractor.LedgeCheckHor.position.y - PlayerInteractor.Hips.position.y), Color.black);    
    }


    public override void FixedUpdate() { base.FixedUpdate(); }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Core.Movement.SlopeHitNormal = hit.normal;
        Core.Movement.SlopeHit = hit.point;
        Debug.DrawRay(Core.Movement.SlopeHit, Core.Movement.SlopeHitNormal * 5, Color.yellow);
    }

    //bu burada kaldi kekwait
    private void SetupJumpVariables()
    {
        float timeToApex = PlayerData.MaxJumpTime / 2;
        Core.Movement.Gravity = (-2 * PlayerData.MaxJumpHeight) / Mathf.Pow(timeToApex, 2);
        Core.Movement.InitialJumpVelocity = (2 * PlayerData.MaxJumpHeight) / timeToApex;

        Core.Movement.JumpGravity = Core.Movement.Gravity;
        //JumpVelocity_Y = _initialJumpVelocity * 0.5f; //jumpvelocity baslangicta -lere iniyor duzelt
    }

}