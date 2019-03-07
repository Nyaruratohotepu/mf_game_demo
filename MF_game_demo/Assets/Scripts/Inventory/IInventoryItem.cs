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
        int InventoryID { get; }

        /// <summary>
        /// 物体类型
        /// </summary>
        InventoryEnum.ItemType Type { get; }

        /// <summary>
        /// 在背包中显示的名称
        /// </summary>
        string InventoryName { get; }

        /// <summary>
        /// 背包中显示的图标
        /// </summary>
        string InventoryImgDefault { get; }

        /// <summary>
        /// 能否堆叠
        /// </summary>
        bool IsStackable { get; }

        /// <summary>
        /// 最多一堆多少个
        /// </summary>
        int StackMaxCount { get; }

        /// <summary>
        /// 能否交易
        /// </summary>
        bool IsTradable { get; }

        /// <summary>
        /// 交易价格
        /// </summary>
        int Price { get; }
    }
}
