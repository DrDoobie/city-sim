using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObj inventory;

    //public int X_START, Y_START;
    //public int X_SPACE_BETWEEN_ITEMS, Y_SPACE_BETWEEN_ITEMS;
    //public int NUMBER_OF_COLUMN;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    void Start()
    {
        CreateDisplay();
    }

    void Update()
    {
        UpdateDisplay();
    }

    public void CreateDisplay()
    {
        for(int i = 0; i < inventory.container.Count; i++)
        {
            var obj = Instantiate(inventory.container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);

            //obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            obj.GetComponentInChildren<Text>().text = inventory.container[i].amount.ToString("n0");

            itemsDisplayed.Add(inventory.container[i], obj);
        }
    }

    /*public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0.0f);
    }*/

    public void UpdateDisplay()
    {
        for(int i = 0; i < inventory.container.Count; i++)
        {
            if(itemsDisplayed.ContainsKey(inventory.container[i]))
            {
                itemsDisplayed[inventory.container[i]].GetComponentInChildren<Text>().text = inventory.container[i].amount.ToString("n0");

            } else {
                var obj = Instantiate(inventory.container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);

                //obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

                obj.GetComponentInChildren<Text>().text = inventory.container[i].amount.ToString("n0");

                itemsDisplayed.Add(inventory.container[i], obj);
            }
        }
    }
}
