using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Bad and unflexible UI manager but enough for now
    public static UIManager Instance { get; private set; }

    // Many tightly coupled direct references but anyways, maybe refactor later
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text goldsText;
    [SerializeField] private GameObject levelInfoCanvas;
    [SerializeField] private GameObject towerSelectionMenu;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject waveTimerCanvas;
    [SerializeField] private Image waveTime;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        GameManager.OnGameStarted += ShowLevelUI;
    }

    public void SetWaveTimer(bool isActive)
    {
        waveTimerCanvas.SetActive(isActive);
        // waveTimer.transform.position = 
    }

    public void SetTowerMenu(bool isActive)
    {
        towerSelectionMenu.transform.position = Node.CurrentNode.transform.position;
        towerSelectionMenu.SetActive(isActive);
    }

    private void ShowLevelUI()
    {
        startButton.SetActive(false);
        levelInfoCanvas.SetActive(true);
        UpdateCoin();
        UpdateLive();
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= ShowLevelUI;
    }

    public void SetWaveTimeFill(float amount) { waveTime.fillAmount = amount; }

    public void UpdateCoin() => goldsText.text = ((int)PlayerStats.Gold).ToString();
    public void UpdateLive() => livesText.text = PlayerStats.Lives.ToString();
}