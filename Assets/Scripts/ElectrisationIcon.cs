/*
 * ElectrisatinIcon.cs
 * Contient les instructions pour déclencher le sort d'électrisation.
 */

using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRBaseInteractable))]
public class ElectrisationIcon : MonoBehaviour
{
    [SerializeField] private Animator mascotAnimator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameEventManager manager;
    private XRBaseInteractable _interactable;

    // Pour éviter que le joueur déclenche le sort s'il est déjà en cours.
    private bool _isTriggered;

    private void Start()
    {
        _interactable = GetComponent<XRBaseInteractable>();
        _interactable.activated.AddListener(OnActivation);
    }

    private void OnActivation(ActivateEventArgs args)
    {
        if (_isTriggered) return;

        _isTriggered = true;
        var rac = mascotAnimator.runtimeAnimatorController;
        var duration = rac.animationClips.First(clip => clip.name == "electrisation").length;

        mascotAnimator?.SetBool("isElectrised", true);
        audioSource.Play();

        StartCoroutine(PlayAnimation(duration));
    }

    private IEnumerator PlayAnimation(float duration)
    {
        yield return new WaitForSeconds(duration);
        mascotAnimator?.SetBool("isShaking", false);
        mascotAnimator?.SetBool("isElectrised", false);
        _isTriggered = false;

        manager.TriggerRandomEvent();
    }
}