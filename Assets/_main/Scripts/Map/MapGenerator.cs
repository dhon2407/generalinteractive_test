using System.Collections.Generic;
using Cinematics;
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

        private readonly List<GameObject> _currentTiles = new();
        private TileType[,] _tileBlueprint;
        
        private void Start()
        {
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
                    _currentTiles.Add(Instantiate(GameSettings.GetTilePrefab(_tileBlueprint[i,j]), instancePosition, quaternion.identity));

                    if ((i == width / 2) && (j == height / 2))
                        centerPosition = instancePosition;
                }
            }

            cameraController.MoveTo(centerPosition);
        }

        private void RemoveAllTiles()
        {
            foreach (GameObject currentTile in _currentTiles)
            {
                if (currentTile != null)
                    Destroy(currentTile);
            }
            
            _currentTiles.Clear();
        }
    }
}

