using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonValidation : MonoBehaviour
{
    [SerializeField] private GameObject hoverImage;
    private Image _image;
    private XRBaseInteractable _interactable;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _interactable = GetComponent<XRBaseInteractable>();
        _interactable.hoverEntered.AddListener(_ =>
        {
            var color = _image.color;
            color.a = 0;
            _image.color = color;
            hoverImage.SetActive(true);
        });
        _interactable.hoverExited.AddListener(_ =>
        {
            var color = _image.color;
            color.a = 1;
            _image.color = color;
            hoverImage.SetActive(false);
        });
        _interactable.activated.AddListener(_ => IconInteraction.ValidateDrawing());
    }
}