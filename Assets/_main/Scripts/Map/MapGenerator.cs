using System.Collections.Generic;
using Cinematics;
using DG.Tweening;
using Sirenix.OdinInspector;
using SOData;
using Tile;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    [HideMonoScript]
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField, MinValue(0)]
        private int width = 10;
        [SerializeField, MinValue(0)]
        private int height = 10;

        [SerializeField] private float widthOffset = 0.45f;
        [SerializeField] private float heightOffset = 0.26f;

        [SerializeField] private CameraController cameraController;

        private readonly List<BaseTile> _currentTiles = new();
        private TileType[,] _tileBlueprint;
        
        private void Start()
        {
            DOTween.SetTweensCapacity(50000,1000);
            GenerateMap();
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
                    _currentTiles.Add(Instantiate(GameSettings.GetTilePrefab(_tileBlueprint[i, j]), instancePosition,
                        quaternion.identity).GetComponent<BaseTile>());

                    if ((i == width / 2) && (j == height / 2))
                        centerPosition = instancePosition;
                }
            }

            foreach (BaseTile tile in _currentTiles)
                tile.Popup();

            cameraController.MoveTo(centerPosition);
        }

        private void RemoveAllTiles()
        {
            foreach (BaseTile currentTile in _currentTiles)
            {
                if (currentTile != null)
                    currentTile.Destroy();
            }
            
            _currentTiles.Clear();
        }
    }
}

