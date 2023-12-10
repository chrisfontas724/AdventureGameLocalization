using UnityEngine;

// This Reaction is used to play sounds through a given AudioSource.
// Since the AudioSource itself handles delay, this is a Reaction
// rather than an DelayedReaction.
public class AudioReaction : Reaction
{
    public AudioSource audioSource;     // The AudioSource to play the clip.
    public AudioClip audioClip;         // The AudioClip to be played.
    public float delay;                 // How long after React is called before the clip plays.

    private TextManager textManager;

    public override int Priority()
    {
        return 1;
    }


    protected override void ImmediateReaction()
    {
        // Set the AudioSource's clip to the given one and play with the given delay.

        Debug.Log("Current text is: " + TextManager.currentSlug);

        // TODO: This is where to swap out audio sources for localized versions.
        audioSource.clip = audioClip;
        audioSource.PlayDelayed(delay);
    }
}