/*
 * IconInteraction.cs
 * Permet de sélectionner un sort.
 */

using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Outline), typeof(XRBaseInteractable), typeof(Image))]
public class IconInteraction : MonoBehaviour
{
    [SerializeField] private AudioSource hoverSource;
    [SerializeField] private AudioSource selectSource;
    [SerializeField] private Animator mascotAnimator;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] private GameEventManager manager;
    [SerializeField] private string conditionName;
    [SerializeField] private string animationName;

    private Outline _outline;
    // Pour éviter que le joueur déclenche le sort s'il est déjà en cours.
    public bool isTriggered { get; private set; }

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;

        var interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(_ => _outline.enabled = false);

        // activated = quand on appuie sur la gâchette
        interactable.activated.AddListener(OnActivation);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        hoverSource.Play();
        _outline.enabled = true;
    }

    private void OnActivation(ActivateEventArgs args)
    {
        if (isTriggered) return;

        isTriggered = true;
        StartCoroutine(PlayAnimation());
        manager.DisabledCurrentTarget();
        selectSource.Play();
        effectSource.Play();
    }
    
    private IEnumerator PlayAnimation()
    {
        if (manager.CoughSource.isPlaying)
        {
            manager.CoughSource.Stop();
        }
        mascotAnimator.SetBool(conditionName, true);
        var rac = mascotAnimator.runtimeAnimatorController;
        var duration = rac.animationClips.First(clip => clip.name == animationName).length;
        yield return new WaitForSeconds(duration);

        foreach (var param in mascotAnimator.parameters)
        {
            mascotAnimator.SetBool(param.nameHash, false);
        }
        isTriggered = false;

        manager.TriggerRandomEvent();
    }
}