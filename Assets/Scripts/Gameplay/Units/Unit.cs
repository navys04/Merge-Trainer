using System;
using System.Collections;
using UnityEngine;

public enum EUnitType
{
    Soldier,
    Archer
}

public class Unit : MergeableObject
{
    [Header("Resources per second")] 
    [SerializeField] private float _foodPerTick;
    [SerializeField] private float _feedPerTick;
    [SerializeField] private float _woodPerTick;

    [Header("Unit settings")] [SerializeField]
    private float _goldPrice;

    [SerializeField] private EUnitType _unitType;
    [SerializeField] private float _timePerResourceGeneration = 1.0f;
    [SerializeField] private float Damage = 100.0f;

    [Header("Unit price")] 
    [SerializeField] private float _woodCost;
    [SerializeField] private float _foodCost;
    [SerializeField] private float _feedCost;

    private bool _needToGenerateResources = true;

    private SellPanel _sellPanel;
    private Enemy _enemy;

    public float GetWoodCost() => _woodCost;
    public float GetFoodCost() => _foodCost;
    public float GetFeedCost() => _feedCost;

    public float GetFoodPerTick() => _foodPerTick;
    public float GetWoodPerTick() => _woodPerTick;
    public float GetFeedPerTick() => _feedPerTick;
    
    public float GetGoldPrice() => _goldPrice;
    public EUnitType GetUnitType() => _unitType;

    private void Start()
    {
        base.Start();
        
        StartCoroutine(ResourcesGenerator());
    }

    protected override void Drag()
    {
        base.Drag();

        ChangeStateMergeReadyFX(true);
        
        if (_haveRaycast)
        {
            if (_currentHit.collider.TryGetComponent(out SellPanel sellPanel))
            {
                _sellPanel = sellPanel;
            }
            
            else if (_currentHit.collider.TryGetComponent(out Enemy enemy))
            {
                _enemy = enemy;
            }
            
            else if (_sellPanel || _enemy)
            {
                _sellPanel = null;
                _enemy = null;
            
                print("cleared object");
            }
        }
    }

    private void ChangeStateMergeReadyFX(bool isPlaying)
    {
        PanelManager panelManager = PanelManager.Instance;
        foreach (var panel in panelManager.GetPanels())
        {
            if (!panel.IsPanelFree() && panel.GetObject() != this)
            {
                if (panel.GetObject().TryGetComponent(out Unit unit))
                {
                    if (unit.GetUnitType() == _unitType && unit.GetLevel() == GetLevel())
                    {
                        if (isPlaying) panel.ChangeMergeReadyFXState(isPlaying);
                    }
                }
            }
        }
    }

    protected override void Drop()
    {
        base.Drop();

        ChangeStateMergeReadyFX(false);
        
        if (_sellPanel)
        {
            _sellPanel.Sell(this);
        }

        if (_enemy)
        {
            AttackEnemy();
        }
    }

    private void AttackEnemy()
    {
        bool needToBeDestroyed = false;

        if (_enemy.Health >= Damage) needToBeDestroyed = true;
                
        _enemy.Damage(Damage);
        if (needToBeDestroyed)
        {
            _parentPanel.ClearObject();
            Destroy(this.gameObject);
        }
    }

    private IEnumerator ResourcesGenerator()
    {
        PlayerManager playerManager = PlayerManager.Instance;
        while (_needToGenerateResources)
        {
            yield return new WaitForSeconds(_timePerResourceGeneration);
            playerManager.AddFood(_foodPerTick);
            playerManager.AddFeed(_feedPerTick);
            playerManager.AddWood(_woodPerTick);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Unit unit))
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
        }
    }
}
