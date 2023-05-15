using Lelu.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionState : State
{
    
    private const string ANIMATION_CLIP_NAME = "Stand To Sit";
    
    
    private Animator animator;
    private Transform transform;
    private StateController controller;
    private GameInputs gameInputs;

   
    public InteractionState(StateController _stateController, Transform _transform, Animator _animator, GameInputs _inputs) 
    {

        animator = _animator;
        transform = _transform;
        controller = _stateController;
        gameInputs = _inputs;
    
    }
    public override void Enter()
    {
       
        
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        SitOnSofa();

    }

    private void SitOnSofa()
    {
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0); 

        if (Input.GetKeyDown(KeyCode.C))
        {
            
            controller.SwitchState(controller.movementState);

        }

        /*Go to prestted place before sitting animation. 
         * controller.placeToGo.transform.position is a empty game object which is placed in front of the sofa/bench. 
        
        */
        while (Vector3.Distance(transform.position, controller.placeToGo.transform.position) > .1f)
        {
            controller.SetAnimationStates(controller.IS_RUNNING, true);
            

            transform.position = Vector3.Lerp(transform.position, controller.placeToGo.transform.position, Time.deltaTime * 5f / Vector3.Distance(transform.position, controller.placeToGo.transform.position));
            //Lerping towards position at certain speed.
            transform.LookAt(controller.placeToGo.transform.position);


            return;

        }

       
        controller.SetAnimationStates(controller.IS_RUNNING,false);

       
        
        /* Checking if rotation is done. 
         * placeToGo.rotation's local z axis should point forward.( Blue arrow) 
         
         */
        Quaternion targetRotation = controller.placeToGo.transform.rotation;
        
        while (Quaternion.Angle(transform.rotation, targetRotation) > .1f)
        {
            controller.SetAnimationStates(controller.IS_TURNING , true);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 2.5f);
           
            
            return;
            
        }

        controller.SetAnimationStates(controller.IS_TURNING, false);
        animator.SetBool("isSquatting", true);


        // Checking if the squatting is done, if yes, play sitting animation.
        if (animatorStateInfo.IsName(ANIMATION_CLIP_NAME))
        {
            if (animatorStateInfo.normalizedTime >= 1.0f)
            {

                animator.SetBool("isSquatting", false);
                controller.SwitchState(controller.sittingState); // Switch state to sit
            }


        }

      
    }


}
