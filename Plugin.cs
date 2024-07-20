using BepInEx;
using System;
using UnityEngine;
using Utilla;

namespace GorillaTime
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        private GUIStyle labelStyle;
        private float screenHeight;
        private Font customFont;

        public void Start()
        {
            screenHeight = Screen.height;

            LoadCustomFont();
        }

        private void LoadCustomFont()
        {
            try
            {
                // Correct font path for built-in fonts
                customFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
                if (customFont == null)
                {
                    Debug.LogError("Failed to load the font.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"An error occurred while loading the font: {ex.Message}");
            }
        }

        public void OnGUI()
        {
            if (customFont == null)
            {
                Debug.LogWarning("Custom font is not loaded.");
                return;
            }

            if (labelStyle == null)
            {
                InitializeLabelStyle();
            }

            // Update GUI skin label settings
            GUI.skin.label.fontSize = 70;
            Rect labelRect = new Rect(70, screenHeight - 100, 800, 200);
            string formattedTime = DateTime.Now.ToString("HH:mm:ss");
            GUI.Label(labelRect, formattedTime, labelStyle);
        }

        private void InitializeLabelStyle()
        {
            labelStyle = new GUIStyle(GUI.skin.label)
            {
                normal = { textColor = Color.black },
                font = customFont,
                fontSize = 70
            };
        }
    }
}
