/*
 * IconHoverColor.cs
 * Permet de sélectionner un sort.
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Image), typeof(XRBaseInteractable))]
public class IconHoverColor : MonoBehaviour
{
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = Color.cyan;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.color = normalColor;

        var interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(_ => _image.color = hoverColor);
        interactable.hoverExited.AddListener(_ => _image.color = normalColor);
        
        // activated = quand on appuie sur la gâchette
        interactable.activated.AddListener(_ => Debug.Log("press"));
        interactable.selectEntered.AddListener(_ => Debug.Log("grab"));
    }
}