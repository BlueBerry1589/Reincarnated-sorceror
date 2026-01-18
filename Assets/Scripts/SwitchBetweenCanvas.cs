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
