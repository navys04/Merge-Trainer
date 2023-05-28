using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpawnerButton : MonoBehaviour
{
    [SerializeField] private GameObject _unitToSpawn;
    
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SpawnUnit);
    }

    private void SpawnUnit()
    {
        if (!_unitToSpawn) return;

        MergeablePanel panel = PanelManager.Instance.GetFreePanel();
        if (!panel) return;
        
        Vector3 position = panel.transform.position;
        Vector3 newObjectPos = new Vector3(position.x,
            position.y + 1, position.z);
        
        GameObject newObject = Instantiate(_unitToSpawn, newObjectPos,
            panel.transform.rotation, panel.transform);

        MergeableObject mergeableObject = newObject.GetComponent<MergeableObject>();
        mergeableObject.SetParentPanel(panel);
        
        panel.SetObject(mergeableObject);
    }
}
