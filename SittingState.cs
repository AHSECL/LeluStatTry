using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingState : State
{   
    Animator animator;
    StateController stateController;
    public SittingState(Animator animator, StateController stateController) 
    
    {
        this.animator = animator;
        this.stateController = stateController;

    
    }
    public override void Enter()
    {
        animator.SetBool("isSitting", true);
        
    }

    public override void Exit()
    {
        
        animator.SetBool("isSitting", false);
        
       
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            
            stateController.SwitchState(stateController.movementState);

        }
    }
}
