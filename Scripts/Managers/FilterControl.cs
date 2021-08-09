using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class FilterControl : MonoBehaviour {

    public AudioMixer filterMixer;

    private Coroutine transitionCoroutine;
    private AudioMixerSnapshot endSnapshot;

    public static FilterControl Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void TransitionSnapshots(AudioMixerSnapshot from, AudioMixerSnapshot to, float transitionTime)
    {
            EndTransition();
            transitionCoroutine = StartCoroutine(TransitionSnapshotsCoroutine(from, to, transitionTime));
    }

    IEnumerator TransitionSnapshotsCoroutine(AudioMixerSnapshot from, AudioMixerSnapshot to, float transitionTime)
    {
            // transition values
            int steps = 20;
            float timeStep = (transitionTime / (float)steps);
            float transitionPercentage = 0.0f;
            float startTime = 0f;
        
            // set up snapshots
            endSnapshot = to;
            AudioMixerSnapshot[] snapshots = new AudioMixerSnapshot[2]{from, to};
            float[] weights = new float[2];
        
            // stepped-transition
            for (int i = 0; i < steps; i++)
            {
                    transitionPercentage = ((float)i) / steps;
                    weights[0] = 1.0f - transitionPercentage;
                    weights[1] = transitionPercentage;
                    filterMixer.TransitionToSnapshots(snapshots, weights, 0f);
                
                    // this is required because WaitForSeconds doesn't work when Time.timescale == 0
                    startTime = Time.realtimeSinceStartup;
                    while(Time.realtimeSinceStartup < (startTime + timeStep))
                    {
                            yield return null;
                    }
            }
        
            // finalize
            EndTransition();
    }
    
    void EndTransition()
    {
            if ( (transitionCoroutine == null) || (endSnapshot == null) )
            {
                    return;
            }
        
            StopCoroutine(transitionCoroutine);
            endSnapshot.TransitionTo(0f);
    }
}
