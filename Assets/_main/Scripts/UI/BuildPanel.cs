using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    [HideMonoScript]
    public class BuildPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CurrentBuildIndicator buildIndicator;
        [SerializeField] private RectTransform rTransform;
        [SerializeField] private bool hideOnStart;
        
        
        private float _height;
        private Vector3 _originalPosition;
        private Vector3 _hiddenPosition;
        
        private void Start()
        {
            Canvas.ForceUpdateCanvases();
            _height = rTransform.rect.height;
            _originalPosition = rTransform.position;
            _hiddenPosition = _originalPosition;
            _hiddenPosition.y -= _height;

            if (hideOnStart)
                rTransform.DOMove(_hiddenPosition, 0f);
        }
        
        [Button]
        public void Show()
        {
            rTransform.DOKill();
            rTransform.DOMove(_originalPosition, 0.5f).SetEase(Ease.OutBack);
        }

        [Button]
        public void Hide()
        {
            rTransform.DOKill();
            rTransform.DOMove(_hiddenPosition, 0.2f).SetEase(Ease.OutCubic);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            buildIndicator.Hide();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            buildIndicator.Show();
        }
    }
}