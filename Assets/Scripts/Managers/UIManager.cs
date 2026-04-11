using FlexTimer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Refactor later
    public static UIManager Instance { get; private set; }

    // Many tightly coupled direct references, maybe refactor later
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text goldsText;
    [SerializeField] private GameObject levelInfoCanvas;
    [SerializeField] private GameObject towerSelectionMenu;
    [SerializeField] private GameObject waveTimerCanvas;
    [SerializeField] private Image waveTime;
    [SerializeField] private GameObject timeRewardCanvas;
    [SerializeField] private TMP_Text timeRewardText;
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject towerControlCanvas;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TMP_Text upgradePriceText;
    [SerializeField] private TMP_Text destroyRefundText;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private GameObject nodes;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        nodes.SetActive(false);
        levelInfoCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    private void OnEnable()
    {
        LevelManager.OnLevelStarted += ShowLevelUI;
    }

    public void SetTowerMenu(bool isActive)
    {
        towerSelectionMenu.transform.position = Node.CurrentNode.transform.position;
        towerSelectionMenu.SetActive(isActive);
    }

    public void AnimateTimeReward(int goldReward)
    {
        timeRewardText.text = "+  " + goldReward.ToString();
        timeRewardCanvas.SetActive(true);
        timeRewardCanvas.GetComponent<Animator>().SetTrigger("ShouldReward");
        TimerManager.RegisterEvent(2f, () => timeRewardCanvas.SetActive(false));
    }

    public void SetTowerControlPanel(bool isActive)
    {
        if (Tower.CurrentTower.CurrentLevel < 3)
        {
            upgradePriceText.text = "-" + Tower.CurrentTower.TowerDatas[Tower.CurrentTower.CurrentLevel].price.ToString();
            upgradeButton.gameObject.SetActive(true);
        }
        else { upgradeButton.gameObject.SetActive(false); }
        destroyRefundText.text = "+" + ((int)(Tower.CurrentTower.TowerDatas[Tower.CurrentTower.CurrentLevel - 1].price * 0.3)).ToString();
        towerControlCanvas.transform.position = new Vector3(Tower.CurrentTower.transform.position.x, Tower.CurrentTower.transform.position.y - 0.2f, 0);
        towerControlCanvas.SetActive(isActive);
    }

    private void ShowLevelUI()
    {
        mainMenuCanvas.SetActive(false);
        levelInfoCanvas.SetActive(true);
        UpdateCoin();
        UpdateLive();
        nodes.SetActive(true);
    }

    private void OnDisable()
    {
        LevelManager.OnLevelStarted -= ShowLevelUI;
    }

    public void SetWaveTimer(bool isActive) => waveTimerCanvas.SetActive(isActive);
    public void SetWaveTimeFill(float amount) => waveTime.fillAmount = amount;
    public void UpdateCoin() => goldsText.text = ((int)PlayerStats.Gold).ToString();
    public void UpdateLive() => livesText.text = PlayerStats.Lives.ToString();
    public void UpdateWave() => waveText.text = "Wave: " + WaveManager.Instance.CurrentWave.ToString() + "/" + LevelManager.Instance.LevelData.waves.Length.ToString();
}