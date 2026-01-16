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

    private readonly Color _selectColor = Color.red;
    private Color _baseOutlineColor;
    private Image _image;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        _baseOutlineColor = _outline.effectColor;
        _image = GetComponent<Image>();

        var interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(_ => _outline.enabled = false);

        // activated = quand on appuie sur la gâchette
        interactable.activated.AddListener(OnActivation);
        interactable.deactivated.AddListener(OnDeactivation);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        _outline.enabled = true;
        hoverSource.Play();
    }

    private void OnActivation(ActivateEventArgs arg0)
    {
        _outline.effectColor = _selectColor;
        _image.color = _selectColor;
        selectSource.Play();
    }
    
    private void OnDeactivation(DeactivateEventArgs arg0)
    {
        _outline.effectColor = _baseOutlineColor;
        _image.color = Color.white;
    }
}