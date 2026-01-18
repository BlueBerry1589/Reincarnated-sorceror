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

    [SerializeField] private TargetManager targetManager;
    [SerializeField] private Animator mascotAnimator;
    [SerializeField] private AudioSource coughSource;
    public AudioSource CoughSource => coughSource;

    private EventKind? _previousEvent;

    private void Start()
    {
        TriggerRandomEvent();
    }

    public void DisabledCurrentTarget()
    {
        targetManager.DisableCurrentTarget();
    }

    public void TriggerRandomEvent()
    {
        targetManager.DisableCurrentTarget();

        var eventKind = GetRandomEvent(_previousEvent);
        _previousEvent = eventKind;

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

    private static EventKind GetRandomEvent(EventKind? previous = null)
    {
        var values = Enum.GetValues(typeof(EventKind));

        // On ne veut pas invoquer plusieurs fois le même sort à la suite.
        EventKind result;
        do
        {
            result = (EventKind)values.GetValue(Random.Range(0, values.Length));
        } while (result == previous && previous != EventKind.SpawnTarget);

        return result;
    }

    private void TriggerShakingEvent()
    {
        mascotAnimator.SetBool("isShaking", true);
    }

    private void TriggerSweatingEvent()
    {
        mascotAnimator.SetBool("isCoughing", true);
        Invoke(nameof(PlayCoughingSound), 0.2f);
    }

    private void PlayCoughingSound()
    {
        coughSource.Play();
    }
}