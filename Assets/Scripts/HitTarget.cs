/*
 * HitTarget.cs
 *
 * Gère la couleur, les sons et les collisions d'une cible par rapport au marteau.
 */

using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitTarget : MonoBehaviour
{
    public Renderer targetRenderer;
    public GameEventManager manager;

    [SerializeField] private AudioSource hitSource;
    [SerializeField] private AudioSource painSource1;
    [SerializeField] private AudioSource painSource2;

    [SerializeField, Tooltip("Peut être None, à appliquer seulement si on veut que l'animation de choc s'exécute")]
    private Animator mascotAnimator;

    private bool _isActive;

    public void SetActive(bool state)
    {
        _isActive = state;
        GetComponent<Collider>().enabled = state;
        targetRenderer.enabled = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(_isActive && other.CompareTag("Hammer"))) return;
        manager.TriggerRandomEvent();
        if (mascotAnimator != null)
        {
            StartCoroutine(PlayHitAnimation());
        }

        hitSource.Play();
        Invoke(nameof(PlayPainSound), 0.2f);
    }

    private void PlayPainSound()
    {
        var painSource = Random.value > 0.5f ? painSource1 : painSource2;
        painSource.pitch = Random.Range(0.8f, 1.2f);
        painSource.Play();
    }

    private IEnumerator PlayHitAnimation()
    {
        mascotAnimator.SetBool("isHit", true);
        var rac = mascotAnimator.runtimeAnimatorController;
        var duration = rac.animationClips.First(clip => clip.name == "choc").length;
        yield return new WaitForSeconds(duration);

        mascotAnimator?.SetBool("isHit", false);
    }
}
