using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private int _maxHealthPoint;
    [SerializeField] private int _healthPoint;

    [Space(10)]
    [SerializeField] private GameObject _endRoundPanel;
    [SerializeField] private GameObject _gameOverPanel;

    [Header("Components")]
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TextMeshProUGUI _healthText; 
    [SerializeField] private TextMeshProUGUI _coinAmountText;

    [Header("Buttons")]
    [SerializeField] private GameObject _healthButton;
    [SerializeField] private GameObject _damageButton;
    [SerializeField] private GameObject _speedButton;
    [SerializeField] private GameObject _atkSpeedButton;
    [SerializeField] private GameObject _moneyButton;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _healthStatText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private TextMeshProUGUI _atkSpeedText;
    [SerializeField] private TextMeshProUGUI _moneyText;

    [SerializeField] private TMP_Text money;

    private static UIController _instance;
    private GameStat stat;
    public static UIController Instance { get {  return _instance; } }

    private PlayerController player;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _healthPoint = _maxHealthPoint;
        UpdateHPUI(_healthPoint);
        UpdateCoinText(PlayerController.Instance.Statistics.Money);

        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        stat = GameObject.Find("Game Manager").GetComponent<GameStat>();
    }
    public void UpdateHPUI(int healthAmount)
    {
        _healthSlider.maxValue = _maxHealthPoint;
        _healthSlider.value = healthAmount;

        _healthText.SetText($"HP: {healthAmount}");
    }

    public void UpdateCoinText(int coinAmount)
    {
        _coinAmountText.SetText(coinAmount.ToString());
    }

    public void ChangeHealthStat(int level, int amount)
    {
        _maxHealthPoint = amount;
        UpdateHPUI(amount);
        _healthStatText.text = $"Health: {amount}";
        _healthButton.transform.GetComponentInChildren<TextMeshProUGUI>().text = $"{10 + (5 * level)} coins";
    }

    public void ChangeDamageStat(int level, int amount)
    {
        _damageText.text = $"Damage: {amount}";
        _damageButton.transform.GetComponentInChildren<TextMeshProUGUI>().text = $"{10 + (5 * level)} coins";
    }

    public void ChangeSpeedStat(int level, float amount)
    {
        _speedText.text = $"Speed: {amount}";
        _speedButton.transform.GetComponentInChildren<TextMeshProUGUI>().text = $"{10 + (5 * level)} coins";
    }
    public void ChangeAtkSpeedStat(int level, float amount)
    {
        _atkSpeedText.text = $"Attack speed: {amount}";
        _atkSpeedButton.transform.GetComponentInChildren<TextMeshProUGUI>().text = $"{10 + (5 * level)} coins";
    }

    public void ChangeMoneyStat(int level, float amount)
    {
        _moneyText.text = $"Money mul: {amount}";
        _moneyButton.transform.GetComponentInChildren<TextMeshProUGUI>().text = $"{10 + (5 * level)} coins";
    }

    public void ShowStatShop()
    {
        _endRoundPanel.SetActive(true);
        money.text = player.Statistics.Money.ToString();
        player.IsCanMoving = false;
        player._direction = Vector3.zero;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void StartNewRound()
    {
        stat.waves++;
        _endRoundPanel.SetActive(false);
        player.IsCanMoving = true;
        player._direction = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void RestartGame()
    {
        stat.SetDef();
        _endRoundPanel.SetActive(false);
        _gameOverPanel.SetActive(false);
        player.IsCanMoving = true;
        player._direction = Vector3.zero;
        Cursor.visible = false;
        UIController.Instance.UpdateHPUI(_maxHealthPoint);
    }
}
