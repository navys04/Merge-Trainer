using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : SingletonBase<PlayerManager>
{
    [SerializeField] private float _food = 50;
    private float _feed = 0;
    private float _wood = 0;
    private float _gold = 0;
    private float _diamonds = 0;

    public Action<float> OnFoodChanged = delegate(float f) {  };
    public Action<float> OnFeedChanged = delegate(float f) {  };
    public Action<float> OnWoodChanged = delegate(float f) {  };
    public Action<float> OnGoldChanged = delegate(float f) {  };
    public Action<float> OnDiamondsChanged = delegate(float f) {  };

    public float GetFood() => _food;
    public float GetFeed() => _feed;
    public float GetWood() => _wood;
    public float GetGold() => _gold;
    public float GetDiamonds() => _diamonds;

    private void Start()
    {
        LoadPlayerManager();
    }

    // As parameter returns new food value
    public float AddFood(float value)
    {
        _food += value;
        OnFoodChanged?.Invoke(_food);
        return _food;
    }

    // As parameter returns new feed value
    public float AddFeed(float value)
    {
        _feed += value;
        OnFeedChanged?.Invoke(_feed);
        return _feed;
    }

    
    // As parameter returns new feed value
    public float AddWood(float value)
    {
        _wood += value;
        OnWoodChanged?.Invoke(_wood);
        return _wood;
    }
    
    // As parameter returns new feed value
    public float AddGold(float value)
    {
        _gold += value;
        OnGoldChanged?.Invoke(_gold);
        return _gold;
    }
    
    // As parameter returns new diamonds value
    public float AddDiamonds(float value)
    {
        _diamonds += value;
        OnDiamondsChanged?.Invoke(_diamonds);
        return _diamonds;
    }
    
    // As parameter returns new food value
    public float TakeFood(float value)
    {
        _food -= value;
        OnFoodChanged?.Invoke(_food);
        return _food;
    }
    
    // As parameter returns new feed value
    public float TakeFeed(float value)
    {
        _feed -= value;
        OnFeedChanged?.Invoke(_feed);
        return _feed;
    }
    
    // As parameter returns new wood value
    public float TakeWood(float value)
    {
        _wood -= value;
        OnWoodChanged?.Invoke(_wood);
        return _wood;
    }
    
    // As parameter returns new feed value
    public float TakeGold(float value)
    {
        _gold -= value;
        OnGoldChanged?.Invoke(_gold);
        return _gold;
    }
    
    // As parameter returns new diamonds value
    public float TakeDiamonds(float value)
    {
        _diamonds -= value;
        OnDiamondsChanged?.Invoke(_diamonds);
        return _diamonds;
    }

    private void LoadPlayerManager()
    {
        if (PlayerPrefs.HasKey(PlayerSave.FEED_KEY))
        {
            _feed = PlayerPrefs.GetFloat(PlayerSave.FEED_KEY);
            OnFeedChanged?.Invoke(_feed);
        }
        
        if (PlayerPrefs.HasKey(PlayerSave.FOOD_KEY))
        {
            _food = PlayerPrefs.GetFloat(PlayerSave.FOOD_KEY);
            OnFoodChanged?.Invoke(_feed);
        }
        
        if (PlayerPrefs.HasKey(PlayerSave.WOOD_KEY))
        {
            _wood = PlayerPrefs.GetFloat(PlayerSave.WOOD_KEY);
            OnWoodChanged?.Invoke(_feed);
        }
        
        if (PlayerPrefs.HasKey(PlayerSave.DIAMONDS_KEY))
        {
            _diamonds = PlayerPrefs.GetFloat(PlayerSave.DIAMONDS_KEY);
            OnDiamondsChanged?.Invoke(_feed);
        }
        
        if (PlayerPrefs.HasKey(PlayerSave.GOLD_KEY))
        {
            _gold = PlayerPrefs.GetFloat(PlayerSave.GOLD_KEY);
            OnGoldChanged?.Invoke(_feed);
        }
    }
}
