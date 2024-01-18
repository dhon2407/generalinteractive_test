using Map;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [HideMonoScript]
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private MapGenerator mapGenerator;
        [SerializeField] private Button startButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private TMP_InputField mapSize;
        [SerializeField] private BuildPanel buildPanel;
        [SerializeField] private CurrentBuildIndicator buildIndicator;

        private void Awake()
        {
            restartButton.transform.localScale = Vector3.zero;
            startButton.onClick.AddListener(StartDemo);
            restartButton.onClick.AddListener(RestartDemo);
        }

        private void RestartDemo()
        {
            buildIndicator.Stop();
            buildPanel.Hide();
            restartButton.transform.localScale = Vector3.zero;
            transform.localScale = Vector3.one;
            mapGenerator.Clear();
        }

        private void StartDemo()
        {
            if (!int.TryParse(mapSize.text, out int size))
                return;
            
            mapGenerator.Generate(size, size);
            restartButton.transform.localScale = Vector3.one;
            transform.localScale = Vector3.zero;
            buildPanel.Show();
        }
    }
}