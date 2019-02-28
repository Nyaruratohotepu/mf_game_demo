using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventory
{
    public class PlayerInventory : IInventory
    {

        private List<IInventoryItem> items;
        List<IInventoryItem> IInventory.Items
        {
            get
            {
                return items;
            }
        }

        private int capacityMax=30;
        int IInventory.CapacityMax
        {
            get
            {
                return capacityMax;
            }
            set
            {
                if (value >= capacityUsed)
                    capacityMax = value;
            }
        }
        private int capacityUsed=0;
        int IInventory.CapacityUsed
        {
            get
            {
                return capacityUsed;
            }
        }

        private int cash=0;
        int IInventory.Cash
        {
            get
            {
                return cash;
            }
            set
            {
                if (value >= 0)
                    cash = value;
            }
        }

        int IInventory.AddItem(IInventoryItem item,int count)
        {
            if (item == null)
                return 0;
            if (item.StackMaxCount > 0)
            {

                //可堆叠，未完成
                return 0;
                    
            }
            else
            {
                //不可堆叠
                if (capacityUsed >= capacityMax)
                    return 0;
                else
                {
                    items.Add(item);
                    return 1;
                }
            }
                
        }

        int IInventory.DelAllItemByID(int itemId)
        {
            throw new NotImplementedException();
        }


        int IInventory.DelItemByID(int itemId, int count)
        {
            throw new NotImplementedException();
        }

        bool IInventory.HasItem(int itemId)
        {
            throw new NotImplementedException();
        }

        int IInventory.ItemCount(int itemId)
        {
            throw new NotImplementedException();
        }


        int IInventory.SetItemCount(int itemId)
        {
            throw new NotImplementedException();
        }

        int IInventory.DelItem(IInventoryItem item)
        {
            throw new NotImplementedException();
        }

        public PlayerInventory()
        {
            items = new List<IInventoryItem>();
            
        }
    }
}
