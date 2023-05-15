using Lelu.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class MovementState : State

{

    
    [Header("CamRelativeMovement")]
    private float smoothDampAnlge = 0.1f;
    private float turnSmoothVelocity;
    private float charSpeed = 5f;

    [Header("Animation")]
    private readonly int IS_WALKING = Animator.StringToHash("isRunning"); // readonly signed once and locked in.
    private bool isRunning;


    private StateController stateController;
    private Transform transform;
    private GameInputs gameInputs;
    private Animator animator;
    private CharacterEnergy characterEnergy;
    private SurvivalStats survivalStats;




    // constructor method allows us to get Players needed components. Transform component for instance, we need that in order to move the character in this class. 
    // Useful for MovementState
    public MovementState(StateController _stateController, Transform _transform, GameInputs _gameInputs, Animator _animator, SurvivalStats _survivalStats) 
    {
        this.stateController = _stateController;
        this.survivalStats = _survivalStats;
        transform = _transform;
        gameInputs = _gameInputs;
        animator = _animator;
    }

    public override void Enter()
    {
        
        
    }

    public override void Exit() 
    {

      

    }

    public override void Update()
    {
       
        CamRelativeMovement();

    }

    private void CamRelativeMovement()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            stateController.SwitchState(stateController.interactionState);
        }

        // Getting Input values as x,y and converting them vector3
        Vector3 moveDirectionInput = new Vector3(gameInputs.movementInputs.x, 0, gameInputs.movementInputs.y);


        //Camera Relative Movement
        if (moveDirectionInput.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(moveDirectionInput.x, moveDirectionInput.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothDampAnlge);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            isRunning = true;
            transform.position += moveDir * charSpeed * Time.deltaTime;

            survivalStats.ModifyEnergy(5f * Time.deltaTime);
            


        }
        else
        {
            isRunning = false;

            survivalStats.ModifyEnergy(5f * Time.deltaTime);
        }

        animator.SetBool(IS_WALKING, isRunning);
        animator.SetFloat("run", survivalStats.GetEnergyLevel());
    }





}
