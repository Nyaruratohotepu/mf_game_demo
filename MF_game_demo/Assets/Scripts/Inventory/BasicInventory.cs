using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventory
{
    public class BasicInventory : IInventory
    {
        public BasicInventory(List<InventoryBlock> blocks)
        {
            //由ItemManager调用，传入blocks，可能从文件中存取
            if (blocks != null)
                this.blocks = blocks;
            else
            {
                //默认情况只有一个空物体
                this.blocks = new List<InventoryBlock>();
                blocks.Add(new InventoryBlock(0));
            }
        }

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

        /// <summary>
        /// 清空指定ID的所有物体（可能不止一格）
        /// </summary>
        /// <param name="itemId">待删除背包物id</param>
        /// <returns>受影响的物品数</returns>
        int IInventory.DelAllItem(int itemId)
        {
            int delCount = 0;
            for (int i = blocks.Count - 1; i >= 0; i--)
            {
                if (itemId == blocks[i].ItemId)
                {
                    delCount += blocks[i].Count;
                    blocks.RemoveAt(i);
                }
            }

            return delCount;
        }

        int IInventory.DelItem(IInventoryItem item, int count)
        {
            if (item == null) return 0;
            if (count <= 0) return 0;
            int delCount = 0;
            int id = item.InventoryID;

            for (int i = blocks.Count - 1; i >= 0; i--)
            {
                if (delCount >= count) break;
                if (id == blocks[i].ItemId)
                {
                    int countNeed = count - delCount;
                    if (countNeed >= blocks[i].Count)
                    {
                        //删一整格
                        delCount += blocks[i].Count;
                        blocks.RemoveAt(i);
                    }
                    else
                    {
                        delCount += countNeed;
                        blocks[i].Count -= countNeed;
                    }
                }
            }
            return delCount;
        }

        /// <summary>
        /// 删除物体，不会超过剩余数量，优先删除数量最少的
        /// </summary>
        /// <param name="itemId">待删除背包物id</param>
        /// <param name="count">数量</param>
        /// <returns>成功删除物体数</returns>
        int IInventory.DelItem(int itemId, int count)
        {
            if (itemId <= 0) return 0;
            if (count <= 0) return 0;
            int delCount = 0;

            for (int i = blocks.Count - 1; i >= 0; i--)
            {
                if (delCount >= count) break;
                if (itemId == blocks[i].ItemId)
                {
                    int countNeed = count - delCount;
                    if (countNeed >= blocks[i].Count)
                    {
                        //删一整格
                        delCount += blocks[i].Count;
                        blocks.RemoveAt(i);
                    }
                    else
                    {
                        delCount += countNeed;
                        blocks[i].Count -= countNeed;
                    }
                }
            }
            return delCount;
        }

        /// <summary>
        /// 删除仓库中的某格物体，若block不在本仓库中则失败
        /// </summary>
        /// <param name="block">待删除格子</param>
        /// <returns>成功删除物体数</returns>
        int IInventory.DelItem(InventoryBlock block)
        {
            int count = block.Count;
            return blocks.Remove(block) ? count : 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="itemId">物品ID</param>
        /// <returns>仓库中指定ID的物体数量，没有则返回0</returns>
        int IInventory.GetItemCount(int itemId)
        {
            int count = 0;
            foreach (InventoryBlock block in blocks)
            {
                if (itemId == block.ItemId)
                    count += block.Count;
            }
            return count;
        }

        /// <summary>
        /// 返回指定Id的未堆满格子，若全部堆满则返回下标最后的一格
        /// </summary>
        /// <param name="itemId">物品Id</param>
        /// <returns>指定Id的最后一格物体</returns>
        InventoryBlock IInventory.GetTailBlock(int itemId)
        {
            int index = 0;
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].ItemId == itemId)
                {
                    index = i;
                    //没存满，一定是散格
                    if (blocks[i].CountLeft > 0) break;
                }
            }
            return blocks[index];
        }

        /// <summary>
        /// </summary>
        /// <param name="itemId">物品ID</param>
        /// <returns>是否包含某种物体</returns>
        bool IInventory.HasItem(int itemId)
        {
            bool has = false;
            foreach (InventoryBlock block in blocks)
            {
                if (block.ItemId == itemId)
                {
                    has = true;
                    break;
                }
            }
            return has;
        }

        /// <summary>
        /// 按Id升序，同Id格子数量升序排序
        /// </summary>
        void IInventory.Sort()
        {
            blocks.Sort();
        }
    }
}
