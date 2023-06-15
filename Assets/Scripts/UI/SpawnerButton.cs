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
    }

    private void OnLevelUpdated(int level)
    {
        if (_isUnlocked) return;

        if (level >= _levelToOpen)
        {
            ChangeButtonState(true);
        }
    }
    
    private void SpawnUnit()
    {
        if (!_unitToSpawn || !_isUnlocked) return;

        List<MergeablePanel> panels = PanelManager.Instance.GetFreePanels();
        if (panels.Count == 0) return;
        
        MergeablePanel panel = panels[Random.Range(0, panels.Count)];
        if (!panel) return;
        
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
