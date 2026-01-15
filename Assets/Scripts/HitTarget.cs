/*
 * HitTarget.cs
 * Gère la couleur, les sons et les collisions entre d'une cible par rapport au marteau.
 */

using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Collider))]
public class HitTarget : MonoBehaviour
{
    public Renderer targetRenderer;
    public GameEventManager manager;

    private AudioSource _audioSource;
    private bool _isActive;

    public void SetActive(bool state)
    {
        _isActive = state;
        GetComponent<Collider>().enabled = state;
        targetRenderer.enabled = state;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isActive && other.CompareTag("Hammer"))
        {
            manager.TriggerRandomEvent();
            _audioSource.Play();
        }
    }
}