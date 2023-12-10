using System;
using UnityEngine;
using System.Text;
using System.Text.RegularExpressions;

/**
 * provides a quick and dirty performance counter
**/
public class StringTools : MonoBehaviour
{
	public static string GenerateSlug(string phrase)
    {
        // Remove the accents:
        string str = RemoveAccent(phrase).ToLower();
        // Remove invalid characters:
        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
        // Convert multiple spaces into one space:
        str = Regex.Replace(str, @"\s+", " ").Trim();
        // Trim the string to 45 characters:
        //str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
        // Replace the spaces with hyphens:
        str = Regex.Replace(str, @"\s", "-");
        return str;
    }

    public static string RemoveAccent(string txt)
    {
        byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
        return System.Text.Encoding.ASCII.GetString(bytes);
    }

}
