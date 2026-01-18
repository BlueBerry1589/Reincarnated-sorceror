/*
 * SwitchBetweenCanvas.cs
 *
 * Permet de basculer entre deux canvas. Dans notre cas, pour basculer entre
 * les crédits et le menu principal.
 */

using UnityEngine;

public class SwitchBetweenCanvas : MonoBehaviour
{
    [SerializeField] private GameObject otherCanvas;

    public void SwitchCanvas()
    {
        otherCanvas.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
