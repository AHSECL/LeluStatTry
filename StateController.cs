using Lelu.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    State currentState;
    
    [SerializeField] public Transform placeToGo;
    


    public GameInputs inputs;
    public Animator animator;


    [Header("States")]
    public MovementState movementState;
    public AttackState attackState;
    public InteractionState interactionState;
    public SittingState sittingState;
    private CharacterEnergy characterEnergy;


    public SurvivalStats survivalStats;



    [Header("Animation States")]

    public readonly int IS_SITTING = Animator.StringToHash("isSitting");
    public readonly int IS_TURNING = Animator.StringToHash("isTurning");

    public readonly int IS_RUNNING = Animator.StringToHash("isRunning");





    private void Start()
    {
        
        animator = GetComponentInChildren<Animator>();
        inputs = GetComponent<GameInputs>();
        survivalStats = new SurvivalStats(100f, 100f, 100f);


        
        movementState = new MovementState(this, transform, inputs, animator, survivalStats);
        attackState = new AttackState(this, transform, inputs);
        interactionState = new InteractionState(this, transform, animator, inputs);
        sittingState = new SittingState(animator, this);

        currentState = movementState;
        currentState?.Enter();

    }

    private void Update()
    {
        currentState?.Update();

    }

    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void SetAnimationStates(int AnimationID, bool checkBool)
    {
        animator.SetBool(AnimationID, checkBool);
    
    }

    
   

   
}
