using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpawnerButton : MonoBehaviour
{
    [SerializeField] private GameObject _unitToSpawn;
    [SerializeField] private int _levelToOpen = 1;

    [SerializeField] private Sprite _buttonLockedSprite;
    [SerializeField] private Sprite _buttonUnlockedSprite;

    [SerializeField] private Image _iconImage;
    [SerializeField] private Text _unitTypeText;
    
    private Button _button;

    private bool _isUnlocked;


    

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SpawnUnit);
        
        MergeGameManager.Instance.OnLevelUpdated += OnLevelUpdated;

        ChangeButtonState(_levelToOpen == 1);
    }

    private void ChangeButtonState(bool unlocked)
    {
        Sprite image = unlocked ? _buttonUnlockedSprite : _buttonLockedSprite;
        
        _iconImage.sprite = image;
        _isUnlocked = unlocked;
        _button.interactable = _isUnlocked;

        if (_isUnlocked)
        {
            _unitTypeText.text = _unitToSpawn.GetComponent<Unit>().GetUnitType().ToString();
        }
    }

    private void OnLevelUpdated(int level)
    {
        if (_isUnlocked) return;

        if (level >= _levelToOpen)
        {
            ChangeButtonState(true);
        }
    }

    private bool CheckUnitPrice()
    {
        Unit unit = _unitToSpawn.GetComponent<Unit>();

        float foodCost = unit.GetFoodCost();
        float feedCost = unit.GetFeedCost();
        float woodCost = unit.GetWoodCost();

        PlayerManager playerManager = PlayerManager.Instance;

        return foodCost > playerManager.GetFood() || feedCost > playerManager.GetFeed() ||
               woodCost > playerManager.GetWood();
    }
    
    private void SpawnUnit()
    {
        if (!_unitToSpawn || !_isUnlocked) return;
        
        if (CheckUnitPrice()) return;

        Unit unit = _unitToSpawn.GetComponent<Unit>();

        List<MergeablePanel> panels = PanelManager.Instance.GetFreePanels();
        if (panels.Count == 0) return;
        
        MergeablePanel panel = panels[Random.Range(0, panels.Count)];
        if (!panel) return;
        
        PlayerManager playerManager = PlayerManager.Instance;
        playerManager.TakeFood(unit.GetFoodCost());
        playerManager.TakeFeed(unit.GetFeedCost());
        playerManager.TakeWood(unit.GetWoodCost());
        
        Vector3 position = panel.transform.position;
        Vector3 newObjectPos = new Vector3(position.x,
            position.y, position.z);
        
        GameObject newObject = Instantiate(_unitToSpawn, newObjectPos,
            panel.transform.rotation, panel.transform);

        MergeableObject mergeableObject = newObject.GetComponent<MergeableObject>();
        mergeableObject.SetParentPanel(panel);
        
        panel.SetObject(mergeableObject);
    }
}
