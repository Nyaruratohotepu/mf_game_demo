using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventory
{
    class PlayerInventory : BasicInventory, ITrader
    {



        public PlayerInventory(List<InventoryBlock> blocks) : base(blocks)
        {

        }

        /// <summary>
        /// 主武器
        /// </summary>
        public Weapon MainWeapon { get; set; }
        /// <summary>
        /// 副武器
        /// </summary>
        public Weapon SecondWeapon { get; set; }
        /// <summary>
        /// 主武器剩余弹药
        /// </summary>
        public int AmmoCount
        {
            get
            {
                if (MainWeapon != null)
                    return Inventory.GetItemCount(MainWeapon.AmmoItemId);
                else
                    return 0;
            }
        }

        /// <summary>
        /// 尝试消耗主武器对应的弹药，当弹药不足时失败
        /// </summary>
        /// <param name="count">扣除数量</param>
        /// <returns>是否扣除成功</returns>
        public bool ConsumeAmmo(int count)
        {
            if (MainWeapon == null) return false;

            if (AmmoCount >= count)
            {
                Inventory.DelItem(MainWeapon.AmmoItemId, count);
                return true;
            }
            else
                return false;
        }

        //以下为仓库接口
        public IInventory Inventory
        {
            get
            {
                return this as IInventory;
            }
        }

        //以下为交易接口
        public ITrader CurrentShop
        {
            set; get;
        }
        IInventory ITrader.inventory
        {
            get
            {
                return this as IInventory;
            }
        }
        private int cash = 0;
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
        /// 从商店购买商品，由ItemManager捕获操作后调用
        /// </summary>
        /// <param name="other">商店</param>
        /// <param name="block">商品格</param>
        /// <returns>交易金额</returns>
        int ITrader.Buy(ITrader other, InventoryBlock block)
        {
            //看看现金够不够
            int cashNeed = block.Count * block.Item.Price;
            //不够 交易失败
            if (cashNeed > cash) return 0;

            //价钱由商店报
            int cashDelta = other.Sell(this, block);
            cash -= cashDelta;

            if (cashDelta != 0)
            {
                (this as IInventory).AddItem(block);
            }

            return cashDelta;
        }

        /// <summary>
        /// 手上物体卖给商店，由ItemManager捕获操作后调用
        /// </summary>
        /// <param name="other">商店</param>
        /// <param name="block">商品格</param>
        /// <returns>交易金额</returns>
        int ITrader.Sell(ITrader other, InventoryBlock block)
        {
            int cashDelta = 0;
            if ((this as IInventory).DelItem(block) != 0)
            {
                //价格由商店报
                cashDelta = other.Buy(this, block);
            }
            cash += cashDelta;
            return cashDelta;
        }
    }
}
