/*
 * IconHoverColor.cs
 * Permet de sélectionner un sort.
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Outline), typeof(XRBaseInteractable))]
public class IconHoverColor : MonoBehaviour
{
    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;

        var interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(_ => _outline.enabled = true);
        interactable.hoverExited.AddListener(_ => _outline.enabled = false);
        
        // activated = quand on appuie sur la gâchette
        interactable.activated.AddListener(_ => Debug.Log("press"));
    }
}