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

    private void Start()
    {
        foreach (var target in targets)
        {
            target.SetActiveTarget(false);
        }

        ActivateRandomTarget();
    }

    public void ActivateRandomTarget()
    {
        if (targets.Count == 0) return;
        if (_currentTarget != null)
        {
            _currentTarget.SetActiveTarget(false);
        }

        // On veut une cible différente à chaque fois
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, targets.Count);
        } while (randomIndex == _index);
        
        _index = randomIndex;
        _currentTarget = targets[_index];
        _currentTarget.SetActiveTarget(true);
    }
}
