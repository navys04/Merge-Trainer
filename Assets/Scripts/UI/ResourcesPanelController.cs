using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesPanelController : MonoBehaviour
{
    [SerializeField] private Text _foodText;
    [SerializeField] private Text _feedText;
    [SerializeField] private Text _woodText;
    [SerializeField] private Text _goldText;
    [SerializeField] private Text _diamondsText;

    private void OnEnable()
    {
        PlayerManager playerManager = PlayerManager.Instance;
        playerManager.OnFoodChanged += OnFoodChanged;
        playerManager.OnFeedChanged += OnFeedChanged;
        playerManager.OnWoodChanged += OnWoodChanged;
        playerManager.OnGoldChanged += OnGoldChanged;
        playerManager.OnDiamondsChanged += OnDiamondsChanged;
    }

    private void OnFoodChanged(float value)
    {
        _foodText.text = value.ToString();
    }
    
    private void OnFeedChanged(float value)
    {
        _feedText.text = value.ToString();
    }
    
    private void OnWoodChanged(float value)
    {
        _woodText.text = value.ToString();
    }
    
    private void OnGoldChanged(float value)
    {
        _goldText.text = value.ToString();
    }

    private void OnDiamondsChanged(float value)
    {
        _diamondsText.text = value.ToString();
    }
}
