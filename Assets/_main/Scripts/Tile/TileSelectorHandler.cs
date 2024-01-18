using Sirenix.OdinInspector;
using UI;
using UnityEngine;

namespace Tile
{
    [HideMonoScript]
    public class TileSelectorHandler : MonoBehaviour
    {
        [SerializeField] private CurrentBuildIndicator buildIndicator;
        
        private static TileSelectorHandler _instance;
        
        private static BaseTile _currentTile;

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
            _currentTile = baseTile;
            _instance.buildIndicator.SnapOn(baseTile.SnapPointPosition);
            _instance.buildIndicator.Buildable(_currentTile.Buildable);
        }

        public static void UnselectTile(BaseTile baseTile)
        {
            _currentTile = null;
            _instance.buildIndicator.SnapOff();
            _instance.buildIndicator.Buildable(false);
        }
    }
}