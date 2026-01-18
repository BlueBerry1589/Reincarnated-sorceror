using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonValidation : MonoBehaviour
{
    private XRBaseInteractable _interactable;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _interactable = GetComponent<XRBaseInteractable>();
        _interactable.hoverEntered.AddListener(_ =>
        {
            _image.color = Color.red;
        });
        _interactable.hoverEntered.AddListener(_ =>
        {
            _image.color = Color.white;
        });
        _interactable.activated.AddListener(_ => IconInteraction.ValidateDrawing());
    }
}