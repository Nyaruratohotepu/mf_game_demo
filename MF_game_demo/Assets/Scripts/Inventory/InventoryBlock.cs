using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventory
{
    public class InventoryBlock : IComparable<InventoryBlock>
    {
        /// <summary>
        /// 物体
        /// </summary>
        public IInventoryItem Item { set; get; }

        /// <summary>
        /// 本格物体Id
        /// </summary>
        public int ItemId
        {
            get { return Item.InventoryID; }
        }

        /// <summary>
        /// 堆叠数量，在0和最大堆叠数之间
        /// </summary>
        private int count = 0;
        public int Count
        {
            set
            {
                if (value < 0)
                {
                    count = 0;
                }
                else
                {
                    count = (value > Item.StackMaxCount) ? Item.StackMaxCount : value;
                }
            }
            get
            {
                return count;
            }
        }

        /// <summary>
        /// 本格剩余容量
        /// </summary>
        public int CountLeft
        {
            get
            {
                return Item.StackMaxCount - count;
            }
        }

        /// <summary>
        /// 容量上限 
        /// </summary>
        public int CountMax
        {
            get
            {
                return Item.StackMaxCount;
            }
        }
        /// <summary>
        /// 顺序：升序排ID，同ID时降序排数量
        /// </summary>
        /// <param name="other"></param>
        /// <returns>-1表示this应该在other前面</returns>
        int IComparable<InventoryBlock>.CompareTo(InventoryBlock other)
        {
            int thisId = Item.InventoryID;
            int thatId = other.Item.InventoryID;

            //升序排Id
            if (thisId != thatId)
                return thisId.CompareTo(thatId);
            //同ID比数量，数量小的在后面
            else
                return other.Count.CompareTo(this.Count);
        }

        /// <summary>
        /// 使用一个IInventoryItem构造一个物品格
        /// </summary>
        /// <param name="item">物品</param>
        /// <param name="count">数量</param>
        public InventoryBlock(IInventoryItem item, int count = 1)
        {
            this.Item = item;
            this.Count = count;
        }

        /// <summary>
        /// 使用一个ItemId构造一个物品格
        /// </summary>
        /// <param name="item">物品</param>
        /// <param name="count">数量</param>
        public InventoryBlock(int itemId, int count = 1)
        {
            this.Item = ItemFactory.NewItem(itemId);
            this.Count = count;
        }
    }
}
