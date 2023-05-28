using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellPanel : MonoBehaviour
{
    public void Sell(Unit unit)
    {
        PlayerManager.Instance.AddGold(unit.GetGoldPrice());
        
        unit.GetParentPanel().ClearObject();
        Destroy(unit.gameObject);
    }
}
