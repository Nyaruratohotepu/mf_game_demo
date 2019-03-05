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
        List<IInventoryItem> Items { get; }

        /// <summary>
        /// 最大容量（格子数）
        /// </summary>
        int CapacityMax { set; get; }

        /// <summary>
        /// 已占用的容量（格子数）
        /// </summary>
        int CapacityUsed { get; }

        /// <summary>
        /// 钱数
        /// </summary>
        int Cash { set; get; }

        /// <summary>
        /// 加入物体
        /// 此操作会优先堆叠未堆满的格子
        /// 若超过背包容量，则只加到最大
        /// </summary>
        /// <param name="item">待加入背包物</param>
        /// <param name="count">数量</param>
        /// <returns>成功加入数量</returns>
        int AddItem(IInventoryItem item, int count);

        /// <summary>
        /// 根据id添加物体
        /// </summary>
        /// <param name="itemId">物体id</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        int AddItem(int itemId, int count);


        /// <summary>
        /// 删除一整格指定物体
        /// </summary>
        /// <param name="item">待删除背包物</param>
        /// <returns>删除数量，若指定的物体不在仓库中，则返回0</returns>
        int DelItem(IInventoryItem item);

        /// <summary>
        /// 删除指定数量的物体
        /// </summary>
        /// <param name="itemId">待删除背包物id</param>
        /// <param name="count">数量，默认是1</param>
        /// <returns>受影响的物品数；若指定数量超过存量，则删除存量的物体，返回删除了的物体个数</returns>
        int DelItemByID(int itemId, int count = 1);

        /// <summary>
        /// 清空指定ID的所有物体（可能不止一格）
        /// </summary>
        /// <param name="itemId">待删除背包物id</param>
        /// <returns>受影响的物品数</returns>
        int DelAllItemByID(int itemId);

        /// <summary>
        /// 将某项物体的数量修改为指定值（只对可堆叠物体有效）
        /// </summary>
        /// <param name="itemId">物品ID</param>
        /// <returns>新增的物体数（减少之后是负数）</returns>
        int SetItemCount(int itemId);

        /// <summary>
        /// </summary>
        /// <param name="itemId">物品ID</param>
        /// <returns>是否包含某种物体</returns>
        bool HasItem(int itemId);

        /// <summary>
        /// </summary>
        /// <param name="itemId">物品ID</param>
        /// <returns>仓库中指定ID的物体数量，没有则返回0</returns>
        int ItemCount(int itemId);

    }
}
