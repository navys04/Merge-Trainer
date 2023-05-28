using TMPro;
using UnityEngine;

public class ResourcesPanelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _foodText;
    [SerializeField] private TextMeshProUGUI _feedText;
    [SerializeField] private TextMeshProUGUI _woodText;
    [SerializeField] private TextMeshProUGUI _goldText;

    private void OnEnable()
    {
        PlayerManager playerManager = PlayerManager.Instance;
        playerManager.OnFoodChanged += OnFoodChanged;
        playerManager.OnFeedChanged += OnFeedChanged;
        playerManager.OnWoodChanged += OnWoodChanged;
        playerManager.OnGoldChanged += OnGoldChanged;
    }

    private void OnFoodChanged(float value)
    {
        _foodText.text = "food : " + value;
    }
    
    private void OnFeedChanged(float value)
    {
        _feedText.text = "feed : " + value;
    }
    
    private void OnWoodChanged(float value)
    {
        _woodText.text = "wood : " + value;
    }
    
    private void OnGoldChanged(float value)
    {
        _goldText.text = "gold : " + value;
    }
}
