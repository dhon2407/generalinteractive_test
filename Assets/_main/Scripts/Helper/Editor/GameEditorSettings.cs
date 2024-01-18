using SOData;
using UnityEditor;
using UnityEngine;

namespace Helper.Editor
{
    public class GameEditorSettings : UnityEditor.Editor
    {
        [MenuItem("Tools/Game Settings")]
        public static void EditSettings() => SelectSettings(nameof(GameSettings), "Game Settings");

        private static void SelectSettings(string settingType, string settingName)
        {
            var guid = AssetDatabase.FindAssets($"t:{settingType}");
            if (guid.Length > 0)
            {
                var settings = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(guid[0]));
                Selection.activeObject = settings;
                Debug.Log($"Editing {settingName} Settings");
            }
            else
            {
                Debug.LogWarning($"No instance of {settingName} Settings on project.");
            }
        }
    }
}