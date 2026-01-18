using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRBaseInteractable), typeof(Image))]
public class ButtonValidation : MonoBehaviour
{
    private XRBaseInteractable _interactable;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        _interactable = GetComponent<XRBaseInteractable>();
        _interactable.hoverEntered.AddListener(_ =>
        {
            Debug.Log("hovered");
            _image.color = Color.red;
        });
        _interactable.hoverEntered.AddListener(_ =>
        {
            Debug.Log("unhovered");
            _image.color = Color.white;
        });
        _interactable.activated.AddListener(_ => IconInteraction.ValidateDrawing());
        Debug.Log("listeners ajoutés!");
    }
}