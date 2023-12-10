using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using System.Collections.Generic;

// This class is used to manage the text that is
// displayed on screen.  In situations where many
// messages are triggered one after another it
// makes sure they are played in the correct order.
public class TextManager : MonoBehaviour
{
    // This struct encapsulates the messages that are
    // sent for organising.
    public struct Instruction
    {
        public string message;      // The body of the message.
        public Color textColor;     // The color the message should be displayed in.
        public float startTime;     // The time the message should start being displayed based on when it is triggered and its delay.
    }


    public Text text;                               // Reference to the Text component that will display the message.
    public float displayTimePerCharacter = 0.1f;    // The amount of time that each character in a message adds to the amount of time it is displayed for.
    public float additionalDisplayTime = 0.5f;      // The additional time that is added to the message is displayed for.
    public static string currentSlug;

    private int minFontSize = 60; // Minimum font size
    private int maxFontSize = 80; // Maximum font size


    private List<Instruction> instructions = new List<Instruction> ();
                                                    // Collection of instructions that are ordered by their startTime.
    private float clearTime;                        // The time at which there should no longer be any text on screen.

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



    private void Update ()
    {
        // If there are instructions and the time is beyond the start time of the first instruction...
        if (instructions.Count > 0 && Time.time >= instructions[0].startTime)
        {
            // ... set the Text component to display the instruction's message in the correct color.

            string slug = StringTools.GenerateSlug(instructions[0].message);
            string local_text = LocalizationSettings.StringDatabase.GetLocalizedString("Adventure-Strings", slug);

            currentSlug = slug;
            text.text = local_text;
            text.color = instructions[0].textColor;

            AdjustFontSize();

            // Then remove the instruction.
            instructions.RemoveAt (0);
        }
        // Otherwise, if the time is beyond the clear time, clear the text component's text.
        else if (Time.time >= clearTime)
        {
            text.text = string.Empty;
        }
    }

    void AdjustFontSize()
    {
        string locale = GetLocale();
        if (locale == "ja" && text.text.Length > 15)
            maxFontSize = 60;
        else
            maxFontSize = 75;

        Debug.Log("PREFERRED WIDTH: " + text.preferredWidth);
        Debug.Log("FIRST SIZE: " + text.fontSize);

        Debug.Log("MIN AND MAX: " + minFontSize + " , " + maxFontSize);
        // Calculate normalized value based on the preferred width and maxFontSize
        float normalizedValue = Mathf.Clamp01(text.preferredWidth / maxFontSize);

        // Interpolate between minFontSize and maxFontSize based on the normalized value
        int newSize = Mathf.RoundToInt(Mathf.Lerp(minFontSize, maxFontSize, normalizedValue));

        Debug.Log("Normalized Value: " + normalizedValue);
        Debug.Log("New Size: " + newSize);


        // Apply the new font size to the Text component
        text.fontSize = newSize;
    }


    // This function is called from TextReactions in order to display a message to the screen.
    public void DisplayMessage (string message, Color textColor, float delay)
    {
        // The time when the message should start displaying is the current time offset by the delay.
        float startTime = Time.time + delay;

        // Calculate how long the message should be displayed for based on the number of characters.
        float displayDuration = message.Length * displayTimePerCharacter + additionalDisplayTime;

        // Create a new clear time...
        float newClearTime = startTime + displayDuration;

        // ... and if it is after the old clear time, replace the old clear time with the new.
        if (newClearTime > clearTime)
            clearTime = newClearTime;

        // Create a new instruction.
        Instruction newInstruction = new Instruction
        {
            message = message,
            textColor = textColor,
            startTime = startTime
        };

        // Add the new instruction to the collection.
        instructions.Add (newInstruction);

        // Order the instructions by their start time.
        SortInstructions ();
    }


    // This function orders the instructions by start time using a bubble sort.
    private void SortInstructions ()
    {
        // Go through all the instructions...
        for (int i = 0; i < instructions.Count; i++)
        {
            // ... and create a flag to determine if any reordering has been done.
            bool swapped = false;

            // For each instruction, go through all the instructions...
            for (int j = 0; j < instructions.Count; j++)
            {
                // ... and compare the instruction from the outer loop with this one.
                // If the outer loop's instruction has a later start time, swap their positions and set the flag to true.
                if (instructions[i].startTime > instructions[j].startTime)
                {
                    Instruction temp = instructions[i];
                    instructions[i] = instructions[j];
                    instructions[j] = temp;

                    swapped = true;
                }
            }

            // If for a single instruction, all other instructions are later then they are correctly ordered.
            if (!swapped)
                break;
        }
    }
}

