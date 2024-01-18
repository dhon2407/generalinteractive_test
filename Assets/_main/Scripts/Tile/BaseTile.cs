using DG.Tweening;
using Helper;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tile
{
    [HideMonoScript]
    public abstract class BaseTile : PoolableMonobehaviour
    {
        [SerializeField] private Transform snapPoint;
        [SerializeField] private Vector3 originalScale;

        public Vector3 SnapPointPosition => snapPoint.position;
        public abstract bool Buildable { get; }

        public void Popup()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(originalScale, Random.Range(0.3f, 1f));
        }

        public void Destroy()
        {
            transform.DOKill();
            Destroy(gameObject);
        }

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