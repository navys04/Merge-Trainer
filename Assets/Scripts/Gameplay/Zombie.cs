using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MergeableObject
{
    [SerializeField] private int _level;
    [SerializeField] private GameObject _nextLevelZombie;

    private ZombiePanel _parentZombiePanel;
    private ZombiePanel _hoveredZombiePanel;

    public int GetLevel() => _level;
    
    protected override void Drag()
    {
        base.Drag();
        
        LayerMask mask = LayerMask.GetMask("ZombiePanel");
        Ray zombieRay = new Ray(transform.position, transform.up * -1);
        if (Physics.Raycast(zombieRay, out RaycastHit hit, Mathf.Infinity, mask))
        {
            if (hit.collider.TryGetComponent(out ZombiePanel panel))
            {
                if (panel.IsPanelFree()) return;
                if (panel.GetZombie().GetLevel() != _level) return;
                if (panel == _parentZombiePanel) return;

                _hoveredZombiePanel = panel;
                print("found another zombie");
            }
            
            else if (_hoveredZombiePanel != null) _hoveredZombiePanel = null;
        }
    }

    protected override void Drop()
    {
        Vector3 newObjectPos;
        
        if (!_hoveredZombiePanel)
        {
            newObjectPos = new Vector3(_parentZombiePanel.transform.position.x,
                _parentZombiePanel.transform.position.y + 1, _parentZombiePanel.transform.position.z);

            transform.position = newObjectPos;
            return;
        }

        newObjectPos = new Vector3(_hoveredZombiePanel.transform.position.x,
            _hoveredZombiePanel.transform.position.y + 1, _hoveredZombiePanel.transform.position.z);
        
        GameObject newZombie = Instantiate(_nextLevelZombie, newObjectPos,
            _hoveredZombiePanel.transform.rotation, _hoveredZombiePanel.transform);

        Zombie zombieComponent = newZombie.GetComponent<Zombie>();
        zombieComponent.SetParentPanel(_hoveredZombiePanel);
        
        _parentZombiePanel.ClearZombie();

        Destroy(_hoveredZombiePanel.GetZombie().gameObject);
        _hoveredZombiePanel.ClearZombie();
        _hoveredZombiePanel.SetZombie(zombieComponent);

        Destroy(gameObject);
    }

    public void SetParentPanel(ZombiePanel zombiePanel) => _parentZombiePanel = zombiePanel;
}
