using System;
using System.Threading.Tasks;
using Buildings;
using Sirenix.OdinInspector;
using Tile;
using UnityEditor;
using UnityEngine;

namespace SOData
{
    [HideMonoScript]
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        private static GameSettings _instance;
        
        private static GameSettings GetInstance() =>
            _instance ? _instance : _instance = Resources.Load<GameSettings>("Game/GameSettings");

        [BoxGroup("Tile Prefabs"), SerializeField, PreviewField]
        private GameObject grassTile;
        [BoxGroup("Tile Prefabs"), SerializeField, PreviewField]
        private GameObject buildTile;
        [BoxGroup("Tile Prefabs"), SerializeField, PreviewField]
        private GameObject roadTile;

        [BoxGroup("Building Data"), SerializeField, LabelText("Barracks")]
        private BuildingData buildingBarracks;
        [BoxGroup("Building Data"), SerializeField, LabelText("Archer")]
        private BuildingData buildingArcher;
        [BoxGroup("Building Data"), SerializeField, LabelText("Wizard")]
        private BuildingData buildingWizard;

        public static GameObject GetTilePrefab(TileType tileType)
        {
            return tileType switch
            {
                TileType.Grass => GetInstance().grassTile,
                TileType.Road => GetInstance().roadTile,
                TileType.Building => GetInstance().buildTile,
                _ => throw new UnityException($"No data for tile type: {tileType}.")
            };
        }
        
        public static Sprite GetBuildingBaseSprite(BuildingType buildingType)
        {
            var settings = GetInstance();
            return buildingType switch
            {
                BuildingType.Barracks => settings.buildingBarracks.spriteData.level1,
                BuildingType.Archer => settings.buildingArcher.spriteData.level1,
                BuildingType.Wizard => settings.buildingWizard.spriteData.level1,
                _ => throw new UnityException($"No data for building type: {buildingType}.")
            };
        }
        
        private void Awake()
        {
            var guid = AssetDatabase.FindAssets($"t:{nameof(GameSettings)}");
            if (guid.Length <= 1)
            {
                return;
            }
            
            Debug.LogError("Already have a setting instance.");
            var devSettings = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(guid[0]));
            Selection.activeObject = devSettings;
            DelayDestroy();
        }
        
        private async void DelayDestroy()
        {
            await Task.Delay(1);
            DestroyImmediate(this);
        }
    }
}