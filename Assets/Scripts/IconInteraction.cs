/*
 * IconInteraction.cs
 * Permet de sélectionner un sort.
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Outline), typeof(XRBaseInteractable), typeof(Image))]
public class IconHoverColor : MonoBehaviour
{
    [SerializeField] private AudioSource hoverSource;
    [SerializeField] private AudioSource selectSource;

    private Outline _outline;

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
        selectSource.Play();
    }
}