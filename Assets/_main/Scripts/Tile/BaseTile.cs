using Sirenix.OdinInspector;
using UnityEngine;

namespace Tile
{
    [HideMonoScript]
    public abstract class BaseTile : MonoBehaviour
    {
        [SerializeField] private Transform snapPoint;

        public Vector3 SnapPointPosition => snapPoint.position;
        
        protected virtual void OnMouseEnter()
        {
            TileSelectorHandler.SelectTile(this);
        }

        protected virtual void OnMouseExit()
        {
            TileSelectorHandler.UnselectTile(this);
        }
    }
}