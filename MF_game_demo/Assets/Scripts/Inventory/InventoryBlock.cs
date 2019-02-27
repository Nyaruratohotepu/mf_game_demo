using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventory
{
    /// <summary>
    /// 一格背包物
    /// </summary>
    public class InventoryBlock
    {
        private Type type;

        /// <summary>
        /// 物品ID
        /// </summary>
        public int ItemID { get; set; }

        private List<IInventoryItem> items;
        /// <summary>
        /// 物品
        /// </summary>
        public List<IInventoryItem> Items
        {
            get
            {
                return items;
            }
        }
        /// <summary>
        /// 物品数量
        /// </summary>
        public int ItemCount
        {
            get
            {
                return Items.Count;
            }
        }

        /// <summary>
        /// 添加物品，若加入后数量超出堆叠上限，则加入到最大值
        /// </summary>
        /// <param name="count">加入数量，不可为负值</param>
        /// <returns>添加成功的数量</returns>
        //public int Add(int count)
        //{
        //    if (count <= 0) return 0;
        //    int restCount = items[0].StackMaxCount - items.Count;
        //    int addCount = (restCount < count) ? restCount : count;
        //    //使用反射机制获取类型
        //    items.Add()
        //}
    }
}
