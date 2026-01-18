/*
 * AnimateHandOnInput.cs
 *
 * Permet d'adapter la position des doigts (du moins la progression des animations) du joueur lors d'un pincement ou
 * d'un grab.
 */

using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;

    // Rider met un avertissement mais il vaut mieux ne pas renommer cette variable, car
    // cela supprimerait les animators déjà assignées au modèles des mains.
    public Animator HandAnimator;

    // Update is called once per frame
    private void Update()
    {
        var triggerValue = pinchAnimationAction.action.ReadValue<float>();
        var gripValue = gripAnimationAction.action.ReadValue<float>();
        HandAnimator.SetFloat("Trigger", triggerValue);
        HandAnimator.SetFloat("Grip", gripValue);
    }
}
