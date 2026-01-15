/*
 * TargetManager.cs
 * Permet d'instancier une cible différente et aléatoire.
 */

using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public List<HitTarget> targets = new();
    private HitTarget _currentTarget;
    private int _index = -1;

    // Awake est appelé avant Start.
    // Si on restait sur Start, la méthode Start de GameEventManager serait appelée en premier,
    // et donc Start ici remettrait les cibles inactives, entraînant un soft-lock.
    private void Awake()
    {
        foreach (var target in targets)
        {
            target.SetActive(false);
        }
    }

    public void DisableCurrentTarget()
    {
        if (_currentTarget != null)
        {
            _currentTarget.SetActive(false);
        }
    }

    public void ActivateRandomTarget()
    {
        if (targets.Count == 0) return;

        // On veut une cible différente à chaque fois
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, targets.Count);
        } while (randomIndex == _index);
        
        
        _index = randomIndex;
        _currentTarget = targets[_index];
        _currentTarget.SetActive(true);
    }
}
