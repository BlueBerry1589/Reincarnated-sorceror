/*
 * IconInteraction.cs
 * Permet de sélectionner un sort.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Outline), typeof(XRBaseInteractable), typeof(Image))]
public class IconHoverColor : MonoBehaviour
{
    [SerializeField] private AudioSource hoverSource;
    [SerializeField] private AudioSource selectSource;

    private Outline _outline;
    private GameObject _canvas;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        _canvas = transform.parent.gameObject;

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
        // On veut que le canvas disparaisse *après* que le son se soit fini de jouer
        StartCoroutine(DisabledAfterSoundEnds());
    }

    private IEnumerator DisabledAfterSoundEnds()
    {
        yield return new WaitForSeconds(selectSource.clip.length);
        _canvas.SetActive(false);
    }
}