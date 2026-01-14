/*
 * HitTarget.cs
 * Gère la couleur, les sons et les collisions entre d'une cible par rapport au marteau.
 */

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HitTarget : MonoBehaviour
{
    // Les comparaisons par Tag calcule l'id de la chaîne passée en paramètre.
    // Remplacer ce paramètre par son id nous épargne ces calculs.
    private static readonly int Color1 = Shader.PropertyToID("_Color");
    // public float hitForceThreshold = 1.5f;

    public Renderer targetRenderer;
    public Color hitColor = Color.red;
    private Color _normalColor;

    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _normalColor = targetRenderer.material.GetColor(Color1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hammer"))
        {
            targetRenderer.material.SetColor(Color1, hitColor);
            _audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hammer"))
        {
            targetRenderer.material.SetColor(Color1, _normalColor);
        }
    }
}