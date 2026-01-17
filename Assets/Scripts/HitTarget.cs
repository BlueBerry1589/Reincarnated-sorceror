/*
 * HitTarget.cs
 * Gère la couleur, les sons et les collisions entre d'une cible par rapport au marteau.
 */

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitTarget : MonoBehaviour
{
    public Renderer targetRenderer;
    public GameEventManager manager;

    [SerializeField] private AudioSource hitSource;
    [SerializeField] private AudioSource painSource1;
    [SerializeField] private AudioSource painSource2;
    
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
        hitSource.Play();
        Invoke(nameof(PlayPainSound), 0.2f);
    }

    private void PlayPainSound()
    {
        var painSource = Random.value > 0.5f ? painSource1 : painSource2;
        painSource.pitch = Random.Range(0.8f, 1.2f);
        painSource.Play();
    }
}