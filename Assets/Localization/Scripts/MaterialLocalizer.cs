using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class MaterialLocalizer : MonoBehaviour
{
    public Material targetMaterial; // Specify the material you want to change
    public string attributeName = "TextureAttributeName"; // Specify the attribute name you want to use

    public Texture chineseTexture;
    public Texture japaneseTexture;
    public Texture englishTexture;

    Texture currentTexture = null;

    void ChangeMaterialTexture()
    {
        if (currentTexture)
        {
            DebugMaterialAttributes(targetMaterial);
            targetMaterial.SetTexture(attributeName, currentTexture);
        }
    }

    void Update()
    {
        string language = GetLocale();
        Texture newTexture = null;
        if (language == "en")
            newTexture = englishTexture;
        else if (language == "ja")
            newTexture = japaneseTexture;
        else if (language == "zh-Hans")
            newTexture = chineseTexture;
        

        if (newTexture != currentTexture && newTexture != null)
        {
            currentTexture = newTexture;
            ChangeMaterialTexture();
        }
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


    void DebugMaterialAttributes(Material material)
    {
        Shader shader = material.shader;

        if (shader != null)
        {
            int propertyCount = ShaderUtil.GetPropertyCount(shader);

            Debug.Log("Material Properties for Shader: " + shader.name);

            for (int i = 0; i < propertyCount; i++)
            {
                string propertyName = ShaderUtil.GetPropertyName(shader, i);
                ShaderUtil.ShaderPropertyType propertyType = ShaderUtil.GetPropertyType(shader, i);

                string propertyInfo = propertyName + " (" + propertyType + ")";
                Debug.Log(propertyInfo);
            }
        }
        else
        {
            Debug.LogError("Material does not have a valid shader.");
        }
    }
}
