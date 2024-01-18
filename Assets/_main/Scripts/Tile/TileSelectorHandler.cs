using Buildings;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;

namespace Tile
{
    [HideMonoScript]
    public class TileSelectorHandler : MonoBehaviour
    {
        [SerializeField] private CurrentBuildIndicator buildIndicator;
        
        public static BuildingType CurrentBuildingType { get; private set; }
        public static bool BuildingActive => _instance.buildIndicator.Active;
        
        private static TileSelectorHandler _instance;
        private static BaseTile _currentTile;
        
        public static void StartBuild()
        {
            _instance.buildIndicator.BuildStarted();
        }

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.LogError("TileSelectorHandler already initialized.");
                Destroy(gameObject);
                return;
            }
            
            _instance = this;
        }

        private void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }

        public static void SelectTile(BaseTile baseTile)
        {
            if (!BuildingActive)
                return;
            
            _currentTile = baseTile;
            _instance.buildIndicator.SnapOn(baseTile.SnapPointPosition);
            _instance.buildIndicator.Buildable(_currentTile.Buildable);
            CurrentBuildingType = _instance.buildIndicator.CurrentBuildingType;
        }

        public static void UnselectTile(BaseTile baseTile)
        {
            if (!BuildingActive)
                return;
            
            _currentTile = null;
            _instance.buildIndicator.SnapOff();
            _instance.buildIndicator.Buildable(false);
        }
    }
}