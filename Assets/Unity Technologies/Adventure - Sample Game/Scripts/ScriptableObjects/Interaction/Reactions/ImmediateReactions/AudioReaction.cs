using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

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

        float volumeMultiplier = 1.0f;
        string language = GetLocale();
        if (language == "zh-Hans")
            volumeMultiplier = 100.0f;

        // TODO: This is where to swap out audio sources for localized versions.
        AudioClip localizedClip = LocalizationSettings.AssetDatabase.GetLocalizedAsset<AudioClip>("Adventure-Asset", TextManager.currentSlug) ?? audioClip;

        audioSource.volume *= volumeMultiplier;
        audioSource.clip = localizedClip;
        audioSource.PlayDelayed(delay);

       // audioSource.volume /= volumeMultiplier;
    }



    string GetLocale()
    {
        // Get the current locale
        Locale currentLocale = LocalizationSettings.SelectedLocale;

        // Get the language code
        string languageCode = currentLocale.Identifier.Code;

        // Print or use the language code as needed
        Debug.Log("Current Language Code: " + languageCode);

        return languageCode;
    }
}