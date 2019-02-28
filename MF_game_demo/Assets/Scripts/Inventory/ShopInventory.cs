using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventory
{

    public class ShopInventory : IInventory
    {
        List<IInventoryItem> items;
        List<IInventoryItem> IInventory.Items
        {
            get
            {
                return items;
            }
        }
        /// <summary>
        ///此属性不会被使用，商店容量不做判断
        /// </summary>
        int IInventory.CapacityMax { get; set; }

        int IInventory.CapacityUsed
        {
            get
            {
                return items.Count;
            }
        }


        private int cash = 0;
        /// <summary>
        /// 只增不减，无限
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        int IInventory.AddItem(IInventoryItem item, int count)
        {
            throw new NotImplementedException();
        }

        int IInventory.DelAllItemByID(int itemId)
        {
            throw new NotImplementedException();
        }

        int IInventory.DelItem(IInventoryItem item)
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

        public IInventory customer { get; set; }

        /// <summary>
        /// 卖出某格物体，扣除消费者金钱，若消费者资金不足则失败
        /// 注意，此项操作不影响自身钱数
        /// </summary>
        /// <param name="item">物品格子</param>
        /// <returns>是否卖出成功</returns>
        public bool SellOut(IInventoryItem item)
        {
            return false;
        }

        /// <summary>
        /// 从消费者手上买入某物体，给予消费者金钱
        /// 注意，此项操作不影响自身钱数
        /// </summary>
        /// <param name="item">物品</param>
        /// <returns></returns>
        public bool BuyIn(IInventoryItem item)
        {
            return false;
        }
    }
}
