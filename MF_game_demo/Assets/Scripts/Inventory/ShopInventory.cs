using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventory
{

    public class ShopInventory : BasicInventory, ITrader
    {
        public ShopInventory(List<InventoryBlock> blocks) : base(blocks)
        {

        }


        //以下为交易接口
        IInventory ITrader.inventory
        {
            get
            {
                return this;
            }
        }

        int cash = 0;
        int ITrader.Cash
        {
            get
            {
                return cash;
            }
            set
            {
                if (value > 0)
                    cash = value;
            }
        }

        /// <summary>
        /// 商店回购客人的商品，给客人钱
        /// 交易的被动方，玩家卖出物品时被玩家接口调用
        /// </summary>
        /// <param name="other">交易者</param>
        /// <param name="block">交易物体格</param>
        /// <returns>交易金额</returns>
        int ITrader.Buy(ITrader other, InventoryBlock block)
        {
            //防止自循环
            if (other.Equals(this)) return 0;

            IInventory myInventory = this as IInventory;

            return (myInventory.AddItem(block)) * (block.Item.Price);
        }

        /// <summary>
        /// 商店卖出物体给客人，收取玩家费用
        /// </summary>
        /// <param name="other">交易对象</param>
        /// <param name="block">交易物</param>
        /// <returns>交易金额</returns>
        int ITrader.Sell(ITrader other, InventoryBlock block)
        {
            if (other.Equals(this)) return 0;

            IInventory myInventory = this as IInventory;
            return (myInventory.DelItem(block)) * (block.Item.Price);
        }
    }
}
