using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InventoryType { inventoryNPC, inventoryPlayer};
public class DisplayInventory : MonoBehaviour
{
    public InventorySO inventoryReference;
    public GameObject itemDefault;
    public Transform grid;
    public InventoryType inventoryType;
    
    private void OnEnable()
    {
        CreateDisplay(inventoryReference.listItens);
    }

    private void OnDisable()
    {
        DestroyGameObjectContainer();
    }

    private void CreateDisplay(List<Item> list)
    {
        switch (inventoryType)
        {
            case InventoryType.inventoryNPC:
                for (int i = 0; i < list.Count; i++)
                {
                    GameObject obj = Instantiate(itemDefault, grid);
                    obj.GetComponent<SetupItem>().item = inventoryReference.listItens[i];
                    obj.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => ShopManager.Instance.GetItem(obj.GetComponent<SetupItem>().item, obj));
                }
                break;
            case InventoryType.inventoryPlayer:
                for (int i = 0; i < list.Count; i++)
                {
                    GameObject obj = Instantiate(itemDefault, grid);
                    obj.GetComponent<SetupItem>().item = inventoryReference.listItens[i];

                    Destroy(obj.transform.GetChild(3).gameObject);
                    obj.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => ShopManager.Instance.GetItem(obj.GetComponent<SetupItem>().item, obj));
                }
                break;
            default:
                break;
        }
    }

    private void DestroyGameObjectContainer()
    {
        for (int i = 0; i < grid.childCount; i++)
        {
            Destroy(grid.GetChild(i).gameObject);
        }
    }
}
