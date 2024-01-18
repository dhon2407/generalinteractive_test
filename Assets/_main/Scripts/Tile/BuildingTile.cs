using Buildings;
using DG.Tweening;
using SOData;
using UnityEngine;

namespace Tile
{
    public class BuildingTile : BaseTile
    {
        [SerializeField] private SpriteRenderer buildingSprite;
        
        public override bool Buildable => _buildable;

        private bool _buildable = true;
        
        private void Awake()
        {
            buildingSprite.DOFade(0, 0);
        }

        private void OnDestroy()
        {
            buildingSprite.DOKill();
            buildingSprite.transform.DOKill();
        }

        private void OnMouseUpAsButton()
        {
            if (!TileSelectorHandler.BuildingActive)
                return;
            
            StartBuilding(TileSelectorHandler.CurrentBuildingType);
        }

        private void StartBuilding(BuildingType buildingType)
        {
            Debug.Log($"Clicked this building {name} -> {buildingType}");

            _buildable = false;
            buildingSprite.sprite = GameSettings.GetBuildingBaseSprite(buildingType);
            buildingSprite.DOFade(0.5f, 0f);
            buildingSprite.transform.DOShakePosition(10, 0.01f).OnComplete(() =>
            {
                buildingSprite.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutElastic);
                buildingSprite.DOFade(1f, 0f);
            });

            TileSelectorHandler.StartBuild();
        }
    }
}