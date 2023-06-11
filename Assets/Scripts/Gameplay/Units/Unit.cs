using System.Collections;
using UnityEngine;

public enum EUnitType
{
    Soldier,
    Horseman,
    SiegeWeapon
}

public class Unit : MergeableObject
{
    [Header("Resources per second")]
    [SerializeField] private float _foodPerTick;
    [SerializeField] private float _feedPerTick;
    [SerializeField] private float _woodPerTick;

    [Header("Unit settings")] 
    [SerializeField] private float _goldPrice;
    [SerializeField] private EUnitType _unitType;
    [SerializeField] private float _timePerResourceGeneration = 1.0f;
    [SerializeField] private float Damage = 100.0f;

    private bool _needToGenerateResources = true;

    private SellPanel _sellPanel;
    private Enemy _enemy;

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
        }
    }

    protected override void Drop()
    {
        base.Drop();

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
}
