using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TWOPROLIB.Scripts.Shop
{
    public class BtnShopItem : MonoBehaviour
    {
        public Button button;
        public Text nameLabel;
        public Text priceLabel;
        public Image iconImage;

        private Item item;
        private ShopController shopController;

        void Start()
        {
            
        }

        public void Setup(Item currentItem, ShopController shopController)
        {
            this.item = currentItem;
            nameLabel.text = item.itemName;
            priceLabel.text = item.price.ToString();
            iconImage.sprite = item.icon;

            this.shopController = shopController;
        }

        public void HandleClick()
        {
            shopController.TryTransferItemToOtherShop(item);
        }

    }
}
