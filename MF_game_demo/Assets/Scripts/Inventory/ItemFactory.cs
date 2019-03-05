using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Weapons;

namespace Assets.Scripts.Inventory
{
    //物体工厂，提供itemId到物体对象的转换
    public static class ItemFactory
    {
        public static IInventoryItem NewItem(int itemId)
        {
            switch (itemId)
            {
                default:
                    return new EmptyItem();
            }
        }
    }
}
