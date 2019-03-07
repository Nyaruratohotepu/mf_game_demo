using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventory
{
    public class BasicInventory : IInventory
    {
        protected List<InventoryBlock> blocks;

        /// <summary>
        /// 物品列表，存储物品（堆），每条记录占背包（仓库）一格
        /// </summary>
        List<InventoryBlock> IInventory.Blocks
        {
            get
            {
                return blocks;
            }
        }

        private int capacity;

        /// <summary>
        /// 最大格子数
        /// </summary>
        int IInventory.Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                capacity = (value > 0) ? value : 0;
            }
        }

        /// <summary>
        /// 已占用的格子数
        /// </summary>
        int IInventory.CapacityUsed
        {
            get
            {
                return blocks.Count;
            }
        }

        /// <summary>
        /// 可用格子数
        /// </summary>
        int IInventory.CapacityLeft
        {
            get
            {
                return capacity - blocks.Count;
            }
        }

        /// <summary>
        /// 加入物体
        /// 此操作会优先堆叠未堆满的格子
        /// 若超过背包容量，则只加到最大
        /// </summary>
        /// <param name="item">待加入背包物</param>
        /// <param name="count">数量</param>
        /// <returns>成功加入数量</returns>
        int IInventory.AddItem(IInventoryItem item, int count)
        {
            if (item == null) return 0;
            int itemId = item.InventoryID;
            //已经转换了多少数量
            int deltaCount = 0;

            //先填满已有格子
            for (int i = 0; i < blocks.Count; i++)
            {
                InventoryBlock block = blocks[i];
                //跳过id不匹配和已经满了的格子
                if ((block.ItemId != itemId) || (block.CountLeft <= 0)) continue;
                else
                {

                    int countLeft = block.CountLeft;
                    //确保不超单格容量
                    int deltaCountCurrent = (countLeft > count) ? count : countLeft;

                    count -= deltaCountCurrent;
                    block.Count += deltaCountCurrent;
                    deltaCount += deltaCountCurrent;
                }
            }
            //剩下的尝试装成新的满格
            int blockMaxCount = item.StackMaxCount;
            while (count >= blockMaxCount)
            {
                // 仓库满了
                if (blocks.Count >= capacity)
                {
                    blocks.Sort();
                    return deltaCount;
                }


                blocks.Add(new InventoryBlock(itemId, blockMaxCount));
                count -= blockMaxCount;
                deltaCount += blockMaxCount;
            }
            //还有多的，装一格
            if ((count >= 0) && (blocks.Count < capacity))
            {
                blocks.Add(new InventoryBlock(itemId, count));
                deltaCount += count;
            }
            blocks.Sort();
            return deltaCount;

        }

        /// <summary>
        /// 根据id添加物体
        /// </summary>
        /// <param name="itemId">物体id</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        int IInventory.AddItem(int itemId, int count)
        {
            //已经转换了多少数量
            int deltaCount = 0;

            //先填满已有格子
            for (int i = 0; i < blocks.Count; i++)
            {
                InventoryBlock block = blocks[i];
                //跳过id不匹配和已经满了的格子
                if ((block.ItemId != itemId) || (block.CountLeft <= 0)) continue;
                else
                {

                    int countLeft = block.CountLeft;
                    //确保不超单格容量
                    int deltaCountCurrent = (countLeft > count) ? count : countLeft;

                    count -= deltaCountCurrent;
                    block.Count += deltaCountCurrent;
                    deltaCount += deltaCountCurrent;
                }
            }
            //剩下的尝试装成新的满格
            int blockMaxCount = ItemFactory.NewItem(itemId).StackMaxCount;
            while (count >= blockMaxCount)
            {
                // 仓库满了
                if (blocks.Count >= capacity)
                {
                    blocks.Sort();
                    return deltaCount;
                }

                blocks.Add(new InventoryBlock(itemId, blockMaxCount));
                count -= blockMaxCount;
                deltaCount += blockMaxCount;
            }
            //还有多的，装一格
            if ((count >= 0) && (blocks.Count < capacity))
            {
                blocks.Add(new InventoryBlock(itemId, count));
                deltaCount += count;
            }
            blocks.Sort();
            return deltaCount;
        }

        /// <summary>
        /// 添加一格物体，可能会拆包优先填满未填满的格子
        /// </summary>
        /// <param name="block">加入的格子</param>
        /// <returns>加入的数量</returns>
        int IInventory.AddItem(InventoryBlock block)
        {
            if (block == null) return 0;

            //加入的格子是满的，直接加
            if (block.CountLeft == 0)
            {
                blocks.Add(block);

                blocks.Sort();
                return block.Count;
            }

            int count = block.Count;
            int itemId = block.ItemId;
            //已经转换了多少数量
            int deltaCount = 0;

            //先填满已有格子
            for (int i = 0; i < blocks.Count; i++)
            {
                InventoryBlock thisblock = blocks[i];
                //跳过id不匹配和已经满了的格子
                if ((thisblock.ItemId != itemId) || (thisblock.CountLeft <= 0)) continue;
                else
                {

                    int countLeft = thisblock.CountLeft;
                    //确保不超单格容量
                    int deltaCountCurrent = (countLeft > count) ? count : countLeft;

                    count -= deltaCountCurrent;
                    thisblock.Count += deltaCountCurrent;
                    deltaCount += deltaCountCurrent;
                }
            }
            //有多的，装一格
            if ((count >= 0) && (blocks.Count < capacity))
            {
                blocks.Add(new InventoryBlock(itemId, count));
                deltaCount += count;
            }
            blocks.Sort();
            return deltaCount;
        }

        int IInventory.DelAllItem(int itemId)
        {
            throw new NotImplementedException();
        }

        int IInventory.DelItem(IInventoryItem item, int count)
        {
            throw new NotImplementedException();
        }

        int IInventory.DelItem(int itemId, int count)
        {
            throw new NotImplementedException();
        }

        int IInventory.DelItem(InventoryBlock block)
        {
            throw new NotImplementedException();
        }

        int IInventory.GetItemCount(int itemId)
        {
            throw new NotImplementedException();
        }

        InventoryBlock IInventory.GetTailBlock(int itemId)
        {
            throw new NotImplementedException();
        }

        bool IInventory.HasItem(int itemId)
        {
            throw new NotImplementedException();
        }

        int IInventory.SetItemCount(int itemId, int newCount)
        {
            throw new NotImplementedException();
        }

        void IInventory.Sort()
        {
            blocks.Sort();
        }
    }
}
