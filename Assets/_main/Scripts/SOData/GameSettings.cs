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
            switch (tileType)
            {
                case TileType.Grass:
                    return GetInstance().grassTile;
                case TileType.Road:
                    return GetInstance().roadTile;
                case TileType.Building:
                    return GetInstance().buildTile;
                default:
                    throw new UnityException($"No data for tile type {tileType}.");
            }
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