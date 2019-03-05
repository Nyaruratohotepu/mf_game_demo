using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventory
{
    //空物体
    public class EmptyItem : IInventoryItem
    {
        int IInventoryItem.InventoryID
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        InventoryEnum.ItemType IInventoryItem.Type { get; set; }
        string IInventoryItem.InventoryName { get; set; }
        string IInventoryItem.InventoryImgDefault { get; set; }
        int IInventoryItem.StackMaxCount { get; set; }
        bool IInventoryItem.IsTradable { get; set; }
        int IInventoryItem.Price { get; set; }

        IInventoryItem IInventoryItem.Duplicate()
        {
            throw new NotImplementedException();
        }
    }
}
