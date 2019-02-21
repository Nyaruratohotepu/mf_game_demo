using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Scripts.Inventory
{
    /// <summary>
    /// 此接口定义背包物具有的属性
    /// </summary>
    public interface IInventoryItem
    {
        /// <summary>
        /// 物体ID
        /// </summary>
        int InventoryID { set; get; }

        /// <summary>
        /// 在背包中显示的名称
        /// </summary>
        string InventoryName { set; get; }

        /// <summary>
        /// 背包中显示的图标
        /// </summary>
        string InventoryImgDefault { set; get; }

        /// <summary>
        /// 能否堆叠
        /// </summary>
        bool IsStackable { set; get; }

        /// <summary>
        /// 最多一堆多少个
        /// </summary>
        int StackMaxCount { set; get; }

        /// <summary>
        /// 能否交易
        /// </summary>
        bool IsTradable { set; get; }

        /// <summary>
        /// 交易价格
        /// </summary>
        int Price { set; get; }

    }
}
