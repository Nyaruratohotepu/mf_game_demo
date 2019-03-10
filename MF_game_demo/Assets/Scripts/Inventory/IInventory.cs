using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventory
{
    /// <summary>
    /// 此接口定义背包（仓库）具有的功能
    /// </summary>
    public interface IInventory
    {
        /// <summary>
        /// 物品列表，存储物品（堆），每条记录占背包（仓库）一格
        /// </summary>
        List<InventoryBlock> Blocks { get; }

        /// <summary>
        /// 最大格子数
        /// </summary>
        int Capacity { set; get; }

        /// <summary>
        /// 已占用的格子数
        /// </summary>
        int CapacityUsed { get; }

        /// <summary>
        /// 可用格子数
        /// </summary>
        int CapacityLeft { get; }

        /// <summary>
        /// 加入物体
        /// 此操作会优先堆叠未堆满的格子
        /// 若超过背包容量，则只加到最大
        /// </summary>
        /// <param name="item">待加入背包物</param>
        /// <param name="count">数量</param>
        /// <returns>成功加入数量</returns>
        int AddItem(IInventoryItem item, int count=1);

        /// <summary>
        /// 根据id添加物体
        /// </summary>
        /// <param name="itemId">物体id</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        int AddItem(int itemId, int count=1);

        /// <summary>
        /// 添加一格物体，可能会拆包优先填满未填满的格子
        /// </summary>
        /// <param name="block">加入的格子</param>
        /// <returns>加入的数量</returns>
        int AddItem(InventoryBlock block);

        /// <summary>
        /// 删除物体，不会超过剩余数量，优先删除数量最少的
        /// </summary>
        /// <param name="item">待删除背包物</param>
        /// <param name="count">数量</param>
        /// <returns>成功删除物体数</returns>
        int DelItem(IInventoryItem item,int count=1);

        /// <summary>
        /// 删除物体，不会超过剩余数量，优先删除数量最少的
        /// </summary>
        /// <param name="itemId">待删除背包物id</param>
        /// <param name="count">数量</param>
        /// <returns>成功删除物体数</returns>
        int DelItem(int itemId, int count = 1);

        /// <summary>
        /// 删除仓库中的某格物体，若block不在本仓库中则失败
        /// </summary>
        /// <param name="block">待删除格子</param>
        /// <returns>成功删除物体数</returns>
        int DelItem(InventoryBlock block);


        /// <summary>
        /// 清空指定ID的所有物体（可能不止一格）
        /// </summary>
        /// <param name="itemId">待删除背包物id</param>
        /// <returns>受影响的物品数</returns>
        int DelAllItem(int itemId);

        /// <summary>
        /// </summary>
        /// <param name="itemId">物品ID</param>
        /// <returns>仓库中指定ID的物体数量，没有则返回0</returns>
        int GetItemCount(int itemId);

        /// <summary>
        /// </summary>
        /// <param name="itemId">物品ID</param>
        /// <returns>是否包含某种物体</returns>
        bool HasItem(int itemId);

        /// <summary>
        /// 按Id升序，同Id格子数量升序排序
        /// </summary>
        void Sort();

        /// <summary>
        /// 返回指定Id的未堆满格子，若全部堆满则返回下标最后的一格
        /// </summary>
        /// <param name="itemId">物品Id</param>
        /// <returns>指定Id的最后一格物体</returns>
        InventoryBlock GetTailBlock(int itemId);

    }
}
