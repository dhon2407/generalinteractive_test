using Buildings;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    [HideMonoScript]
    public class BuildingButton : MonoBehaviour
    {
        [SerializeField] private BuildingType buildingType;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI buildingName;

        public event UnityAction<BuildingType> OnSelect;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(()=> OnSelect?.Invoke(buildingType));
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}