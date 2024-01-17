using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField]
        private GameObject tile;
    
        [SerializeField, MinValue(0)]
        private int width = 10;
        [SerializeField, MinValue(0)]
        private int height = 10;

        [SerializeField] private float widthOffset = 0.45f;
        [SerializeField] private float heightOffset = 0.26f;

        private readonly List<GameObject> _currentTiles = new();
        
        private void Start()
        {
            GenerateMap();
        }

        [Button, HideInEditorMode]
        private void GenerateMap()
        {
            RemoveAllTiles();
            Vector3 instancePosition = Vector3.zero;
            for (int i = 0; i < width ; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    instancePosition.x = (i + j) * widthOffset;
                    instancePosition.y = (j - i) * heightOffset;
                    _currentTiles.Add(Instantiate(tile, instancePosition, quaternion.identity));
                }
            }
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

