using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesPanelController : MonoBehaviour
{
    [SerializeField] private Text _foodText;
    [SerializeField] private Text _feedText;
    [SerializeField] private Text _woodText;
    [SerializeField] private Text _goldText;

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
