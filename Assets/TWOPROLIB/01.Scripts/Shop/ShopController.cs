using System.Collections.Generic;
using UnityEngine;
using TWOPROLIB.Scripts.Managers;
using UnityEngine.UI;

namespace TWOPROLIB.Scripts.Shop
{
    [System.Serializable]
    public class Item
    {
        public string itemName;
        public Sprite icon;
        public float price = 1f;
    }

    public class ShopController : MonoBehaviour
    {
        public List<Item> itemList;
        public Transform contentPanel;
        public ShopController otherShop;
        public Text myGoldDisplay;
        public float gold = 20f;

        public GameObject prefabButtonGameObject;

        void Start()
        {
            RefreshDisplay();
        }

        public void RefreshDisplay()
        {
            myGoldDisplay.text = "Gold: " + gold.ToString();
            RemoveButtons();
            AddButtons();
        }

        private void AddButtons()
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                Item item = itemList[i];
                GameObject newButton = PrefabPoolManager.Instance.GetObjectForType(prefabButtonGameObject.name);
                newButton.transform.SetParent(contentPanel);
                newButton.SetActive(true);
                newButton.transform.localScale = Vector3.one;

                BtnShopItem btnShopItem = newButton.GetComponent<BtnShopItem>();
                btnShopItem.Setup(item, this);
            }
        }

        private void RemoveButtons()
        {
            while(contentPanel.childCount > 0)
            {
                GameObject toRemove = transform.GetChild(0).gameObject;
                toRemove.transform.SetParent(null);
                toRemove.SetActive(false);
            }
        }

        public void TryTransferItemToOtherShop(Item item)
        {
            if(otherShop.gold >= item.price)
            {
                gold += item.price;
                otherShop.gold -= item.price;
                AddItem(item, otherShop);
                RemoveItem(item, this);

                RefreshDisplay();
                otherShop.RefreshDisplay();
            }
        }

        private void AddItem(Item itemToAdd, ShopController shopController)
        {
            shopController.itemList.Add(itemToAdd);
        }

        private void RemoveItem(Item itemToRemove, ShopController shopController)
        {
            for(int i = shopController.itemList.Count-1; i >= 0; i--)
            {
                if (shopController.itemList[i] == itemToRemove)
                {
                    shopController.itemList.RemoveAt(i);
                }
            }
        }
    }
}

