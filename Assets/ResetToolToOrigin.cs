/*
 * ResetToolToOrigin.cs
 *
 * Fait en sorte que l'outil en main une fois lâché revienne à sa place.
 */

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResetToolToOrigin : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        
        var interactable = GetComponent<XRBaseInteractable>();
        interactable.selectExited.AddListener(OnSelectExit);
    }

    void OnSelectExit(SelectExitEventArgs args)
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
