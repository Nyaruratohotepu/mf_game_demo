using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Inventory;
namespace Assets.Scripts
{
    public class ItemManager
    {
        /// <summary>
        /// 由ID生成新的InventoryItem实例，未完成
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        IInventoryItem GetNewInventoryItemByID(int itemId)
        {
            switch (itemId)
            {
                default:
                    return null;
            }
        }

    }
}
