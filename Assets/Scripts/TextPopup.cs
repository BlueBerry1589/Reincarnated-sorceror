/*
 * VRTextPopup.cs
 *
 * Permet à la feuille d'afficher les instructions lorsqu'elle est pointée.
 */

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRTextPopup : MonoBehaviour
{
    public GameObject infoCanvas; // Sera assigné depuis l'éditeur

    private void Awake()
    {
        infoCanvas.SetActive(false);

        var interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(_ => infoCanvas.SetActive(true));
        interactable.hoverExited.AddListener(_ => infoCanvas.SetActive(false));
    }
}
