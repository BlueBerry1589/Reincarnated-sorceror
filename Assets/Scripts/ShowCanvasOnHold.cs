/*
 * ShowCanvasOnHold.cs
 * Permet d'afficher le canvas (la page de sorts) lorsqu'on a la baguette en main.
 */

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class ShowCanvasOnHold : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;
    private XRGrabInteractable _grabInteractable;

    private void Awake()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _grabInteractable.selectEntered.AddListener(OnSelectEnter);
        _grabInteractable.selectExited.AddListener(OnSelectExit);
        
        canvas?.SetActive(false);
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        canvas?.SetActive(true);
    }

    private void OnSelectExit(SelectExitEventArgs args)
    {
        canvas?.SetActive(false);
    }
}
