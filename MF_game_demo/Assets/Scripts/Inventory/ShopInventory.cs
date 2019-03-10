using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventory
{

    public class ShopInventory : BasicInventory, ITrader
    {
        IInventory ITrader.inventory
        {
            get
            {
                return this;
            }
        }

        int cash=0;
        int ITrader.Cash {
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
        /// 主动调用方，调用客人的接口，客人不需要调用商店接口
        /// </summary>
        /// <param name="other"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        int ITrader.Buy(ITrader other, InventoryBlock block)
        {
            //未完成
            if (other.Equals(this)) return 0;
            return 0;
        }

        int ITrader.Sell(ITrader other, InventoryBlock block)
        {
            throw new NotImplementedException();
        }
    }
}
