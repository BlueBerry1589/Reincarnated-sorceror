using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    
    public Animator HandAnimator;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float TriggerValue = pinchAnimationAction.action.ReadValue<float>();
        float GripValue= gripAnimationAction.action.ReadValue<float>();
        HandAnimator.SetFloat("Trigger",TriggerValue);
        HandAnimator.SetFloat("Grip", GripValue);
    }
}
