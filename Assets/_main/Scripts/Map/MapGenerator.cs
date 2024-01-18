using System.Collections.Generic;
using Cinematics;
using DG.Tweening;
using Helper;
using Sirenix.OdinInspector;
using SOData;
using Tile;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    [HideMonoScript]
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField]
        private int maxDimension = 150;
        
        [SerializeField, MinValue(0)]
        private int width = 10;
        [SerializeField, MinValue(0)]
        private int height = 10;

        [SerializeField] private float widthOffset = 0.45f;
        [SerializeField] private float heightOffset = 0.26f;

        [SerializeField] private CameraController cameraController;

        private readonly List<BaseTile> _currentTiles = new();
        private TileType[,] _tileBlueprint;
        private GameObjectPool<BaseTile> _grassTilePool;
        private GameObjectPool<BaseTile> _buildingTilePool;

        private void Awake()
        {
            DOTween.SetTweensCapacity(50000,1000);
            _grassTilePool = GameObjectPool<BaseTile>.CreateInstance(GameSettings.GetTilePrefab(TileType.Grass), maxDimension * maxDimension, transform);
            _buildingTilePool = GameObjectPool<BaseTile>.CreateInstance(GameSettings.GetTilePrefab(TileType.Building), maxDimension * maxDimension, transform);
        }

        [Button, HideInEditorMode]
        private void GenerateMap()
        {
            Vector3 centerPosition = cameraController.CurrentPosition;
            RemoveAllTiles();
            
            _tileBlueprint = new TileType[width,height];
            Vector3 instancePosition = Vector3.zero;
            for (int i = 0; i < width ; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    instancePosition.x = (i + j) * widthOffset;
                    instancePosition.y = (j - i) * heightOffset;
                    _tileBlueprint[i, j] = Random.Range(0f, 1f) > 0.8f ? TileType.Building : TileType.Grass;
                    var newTile = GetTile(_tileBlueprint[i, j], instancePosition);
                    _currentTiles.Add(newTile);

                    if ((i == width / 2) && (j == height / 2))
                        centerPosition = instancePosition;
                    
                    if ((i > (width / 2) - 15) && (i < (width / 2) + 15) &&
                        (j > (height / 2) - 15) && (j < (height / 2) + 15))
                    {
                        newTile.Popup();
                    }
                }
            }

            cameraController.MoveTo(centerPosition);
        }

        private BaseTile GetTile(TileType tileType, Vector3 position)
        {
            BaseTile tile;
            switch (tileType)
            {
                case TileType.Grass:
                    tile = _grassTilePool.GetObject();
                    break;
                case TileType.Building:
                    tile = _buildingTilePool.GetObject();
                    break;
                default:
                    throw new UnityException($"Unhandled tile type: {tileType}.");
            }

            tile.transform.position = position;

            return tile;
        }

        private void RemoveAllTiles()
        {
            foreach (BaseTile currentTile in _currentTiles)
            {
                if (currentTile != null)
                    currentTile.gameObject.SetActive(false);
            }
            
            _currentTiles.Clear();
        }
    }
}

