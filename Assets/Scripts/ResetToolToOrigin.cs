/*
 * ResetToolToOrigin.cs
 *
 * Fait en sorte que l'outil en main une fois lâché revienne à sa place.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable), typeof(Rigidbody))]
public class ResetToolToOrigin : MonoBehaviour
{
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private Rigidbody _rb;
    private XRGrabInteractable _interactable;
    private bool _isReturning; // false par défaut
    
    [Header("Settings")]
    public float returnDuration = 0.5f;

    private void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        
        _rb =  GetComponent<Rigidbody>();
        _interactable = GetComponent<XRGrabInteractable>();
        _interactable.selectExited.AddListener(OnSelectExit);
    }

    private void OnDestroy()
    {
        _interactable.selectExited.RemoveListener(OnSelectExit);
    }

    private void OnSelectExit(SelectExitEventArgs args)
    {
        if (!_isReturning)
        {
            // On a recours à une coroutine car on veut exécuter une tâche (le retour au point de départ) sur plusieurs
            // frames sans bloquer le thread principal.
            StartCoroutine(ReturnRoutine());
        }
    }

    // Le retour au point de départ se fait par interpolation de la position et de la rotation de l'outil.
    private IEnumerator ReturnRoutine()
    {
        _isReturning = true;
        
        var initPosition = transform.position;
        var initRotation = transform.rotation;
        var t = 0.0f;

        while (t < returnDuration)
        {
            t += Time.deltaTime;
            var p = Mathf.SmoothStep(0, 1, t / returnDuration);
            transform.position = Vector3.Lerp(initPosition, _startPosition, p);
            transform.rotation = Quaternion.Lerp(initRotation, _startRotation, p);
            
            // `yield return` permet de mettre en pause la coroutine.
            // Renvoyer null équivaut à mettre en pause durant 1 frame.
            yield return null;
        }
        
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        
        // Même si Unity affiche un avertissement, ces instructions restent importantes.
        // Si one ne les applique pas, les outils rouleraient une fois lâchés.
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        _isReturning = false;
    }
}
