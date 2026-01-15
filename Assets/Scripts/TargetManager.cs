using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public List<HitTarget> targets = new List<HitTarget>();
    private HitTarget _currentTarget;
    private int index = -1;

    void Start()
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

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, targets.Count);
        } while (randomIndex == index);
        _currentTarget = targets[Random.Range(0, targets.Count)];
        _currentTarget.SetActiveTarget(true);
    }
}
