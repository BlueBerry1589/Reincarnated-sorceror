/*
 * IconInteraction.cs
 *
 * Gère la page de sorts, la sélection, l'affichage de la zone de dessin et
 * l'exécution des sorts.
 */

using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Outline), typeof(XRBaseInteractable))]
public class IconInteraction : MonoBehaviour
{
    [SerializeField] private AudioSource hoverSource;
    [SerializeField] private AudioSource selectSource;
    [SerializeField] private Animator mascotAnimator;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] private GameEventManager manager;
    [SerializeField] private string conditionName;
    [SerializeField] private string animationName;

    [SerializeField] private GameObject text;
    [SerializeField] private GameObject kanji;
    [SerializeField] private GameObject drawingSurface;
    [SerializeField] private TextMeshProUGUI counter;

    [SerializeField] private GameObject validationButton;

    private Outline _outline;

    // Pour éviter que le joueur déclenche un sort si un est déjà en cours.
    public static bool isTriggered { get; private set; }
    public static bool isDrawingValidated { get; private set; }
    private static bool _isInDrawingPart;

    public static void ValidateDrawing()
    {
        if (_isInDrawingPart)
        {
            isDrawingValidated = true;
        }
    }

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;

        var interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);

        // activated = quand on appuie sur la gâchette
        interactable.activated.AddListener(OnActivation);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        if (isTriggered) return;
        hoverSource.Play();
        text.SetActive(true);
        _outline.enabled = true;
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        text.SetActive(false);
        _outline.enabled = false;
    }

    private void OnActivation(ActivateEventArgs args)
    {
        if (!isTriggered)
        {
            StartCoroutine(PlayAnimation());
        }
    }

    private IEnumerator PlayAnimation()
    {
        selectSource.Play();
        isTriggered = true;
        text.SetActive(false);
        kanji.SetActive(true);
        counter.gameObject.SetActive(true);

        for (var i = 3; i > 0; --i)
        {
            counter.text = i.ToString();
            yield return new WaitForSeconds(1);
            selectSource.Play();
        }

        _isInDrawingPart = true;
        counter.gameObject.SetActive(false);
        kanji.SetActive(false);
        drawingSurface.SetActive(true);
        validationButton.SetActive(true);

        // ...
        yield return new WaitUntil(() => isDrawingValidated);

        drawingSurface.SetActive(false);
        validationButton.SetActive(false);
        _isInDrawingPart = false;
        manager.DisabledCurrentTarget();
        effectSource.Play();

        if (manager.CoughSource.isPlaying)
        {
            manager.CoughSource.Stop();
        }
        else if (manager.ShakeSource.isPlaying)
        {
            manager.ShakeSource.Stop();
        }

        mascotAnimator.SetBool(conditionName, true);
        var rac = mascotAnimator.runtimeAnimatorController;
        var duration = rac.animationClips.First(clip => clip.name == animationName).length;
        yield return new WaitForSeconds(duration);

        foreach (var param in mascotAnimator.parameters)
        {
            mascotAnimator.SetBool(param.nameHash, false);
        }

        isTriggered = false;
        isDrawingValidated = false;
        manager.TriggerRandomEvent();
    }
}
