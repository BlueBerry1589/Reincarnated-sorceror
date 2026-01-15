using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameEventManager : MonoBehaviour
{
    private enum EventKind
    {
        SpawnTarget,
        CauseShaking,
        CauseSweating,
    }
    
    [SerializeField]
    private TargetManager targetManager;
    [SerializeField]
    private Animator mascotAnimator;

    [SerializeField, Range(0f, 1f)]
    private float spawnTargetChance = 0.7f;

    private void Start()
    {
        TriggerRandomEvent();
    }

    public void TriggerRandomEvent()
    {
        targetManager.DisableCurrentTarget();
        
        var eventKind = GetRandomEvent();
        switch (eventKind)
        {
            case EventKind.SpawnTarget:
                Debug.Log("Event: Spawn Target");
                targetManager.ActivateRandomTarget();
                break;
            case EventKind.CauseShaking:
                Debug.Log("Event: Shaking");
                TriggerShakingEvent();
                break;
            case EventKind.CauseSweating:
                Debug.Log("Event: Sweating");
                TriggerSweatingEvent();
                break;
        }
    }

    private static EventKind GetRandomEvent()
    {
        var values = Enum.GetValues(typeof(EventKind));
        return (EventKind)values.GetValue(Random.Range(0, values.Length));
    }

    private void TriggerShakingEvent()
    {
        if (mascotAnimator != null)
        {
            mascotAnimator.SetBool("isShaking", true);
        }
    }
    
    private void TriggerSweatingEvent()
    {
        if (mascotAnimator != null)
        {}
    }
}