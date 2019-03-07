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
                return 0;
            }
        }

        InventoryEnum.ItemType IInventoryItem.Type
        {
            get
            {
                return InventoryEnum.ItemType.Other;
            }
        }

        string IInventoryItem.InventoryName
        {
            get
            {
                return "测试物体";
            }
        }

        string IInventoryItem.InventoryImgDefault
        {
            get
            {
                return "";
            }
        }


        bool IInventoryItem.IsStackable
        {
            get
            {
                return true;
            }
        }

        int IInventoryItem.StackMaxCount
        {
            get
            {
                return 10;
            }
        }

        bool IInventoryItem.IsTradable
        {
            get
            {
                return true;
            }
        }

        int IInventoryItem.Price
        {
            get
            {
                return 1;
            }
        }
    }
}
