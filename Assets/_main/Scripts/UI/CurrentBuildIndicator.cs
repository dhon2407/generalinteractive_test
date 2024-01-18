using Buildings;
using Sirenix.OdinInspector;
using SOData;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [HideMonoScript]
    public class CurrentBuildIndicator : MonoBehaviour
    {
        [SerializeField] private Image imageIndicator;
        [SerializeField] private BuildingButton[] buildingButtons;
        [SerializeField] private Color buildableColor = Color.green;
        [SerializeField] private Color unBuildableColor = Color.red;
        
        public BuildingType CurrentBuildingType { get; private set; }
        public bool Active => _active;

        private bool _active;
        private Camera _camera;
        private Vector3 _snapPosition;
        private bool _snapOn;

        public void Show()
        {
            if (!_active)
                return;
            
            transform.localScale = Vector3.one;
        }
        
        public void Hide()
        {
            transform.localScale = Vector3.zero;
        }
        
        public void SnapOn(Vector3 worldPosition)
        {
            _snapOn = true;
            _snapPosition = _camera.WorldToScreenPoint(worldPosition);
        }
        
        public void SnapOff()
        {
            _snapOn = false;
        }
        
        public void Buildable(bool buildable)
        {
            imageIndicator.color = buildable ? buildableColor :unBuildableColor;
        }

        private void Awake()
        {
            _camera = Camera.main;
            imageIndicator.color = unBuildableColor;
            _active = false;
            Hide();
            
            foreach (BuildingButton button in buildingButtons)
                button.OnSelect += OnSelectNewBuild;
        }

        private void OnSelectNewBuild(BuildingType buildingType)
        {
            _active = true;
            Show();
            imageIndicator.sprite = GameSettings.GetBuildingBaseSprite(buildingType);
            CurrentBuildingType = buildingType;
        }

        private void Update()
        {
            if (_active)
                transform.position = CurrentPosition;
        }

        private Vector3 CurrentPosition => _snapOn ? _snapPosition : Input.mousePosition;

        public void Stop()
        {
            _active = false;
            Hide();
        }
    }
}