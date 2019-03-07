using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventory
{
    /// <summary>
    /// 用于交易
    /// </summary>
    public interface ITrader
    {
        /// <summary>
        /// 交易者必须拥有仓库，只读
        /// </summary>
        IInventory inventory { get; }

        /// <summary>
        /// 钱数
        /// </summary>
        int Cash { set; get; }

        /// <summary>
        /// 从other处购买一格物体
        /// </summary>
        /// <param name="other">交易对方</param>
        /// <param name="block">交易物体</param>
        /// <returns>成功交易金额</returns>
        int Buy(ITrader other, InventoryBlock block);

        /// <summary>
        ///卖给other一格物体
        /// </summary>
        /// <param name="other">交易对方</param>
        /// <param name="block">交易物体</param>
        /// <returns>成功交易金额</returns>
        int Sell(ITrader other, InventoryBlock block);
    }
}
