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
        /// 物品列表，存储物品和数量，每条键值对均占背包中的一格
        /// </summary>
        Dictionary<IInventoryItem, int> Items { get; }
        /// <summary>
        /// 最大容量（格子数）
        /// </summary>
        int CapacityMax { set; get; }

        /// <summary>
        /// 已占用的容量（格子数）
        /// </summary>
        int CapacityUsed { set; get; }

        /// <summary>
        /// 钱数
        /// </summary>
        int Cash { set; get; }

        /// <summary>
        /// 加入物体，返回成功加入数量（1或0）
        /// </summary>
        /// <param name="item">待加入背包物</param>
        /// <returns></returns>
        int AddItem(IInventoryItem item);

        /// <summary>
        /// 加入物体，返回成功加入数量
        /// 此操作会优先堆叠未堆满的格子
        /// </summary>
        /// <param name="itemId">待加入背包物id</param>
        /// <param name="count">数量，默认是1</param>
        /// <returns></returns>
        int AddItemByID(int itemId, int count = 1);

        /// <summary>
        /// 删除一整格指定物体
        ///若指定的物体不在仓库中，则返回0
        /// </summary>
        /// <param name="item">待删除背包物</param>
        /// <returns></returns>
        int DelItem(IInventoryItem item);

        /// <summary>
        /// 删除指定数量的物体，返回受影响的物品数
        /// 若指定数量超过存量，则删除存量的物体，返回删除了的物体个数
        /// </summary>
        /// <param name="itemId">待删除背包物id</param>
        /// <param name="count">数量，默认是1</param>
        /// <returns></returns>
        int DelItemByID(int itemId, int count = 1);

        /// <summary>
        /// 清空指定ID的所有物体（可能不止一格），返回受影响的物品数
        /// </summary>
        /// <param name="itemId">待删除背包物id</param>
        /// <returns></returns>
        int DelAllItemByID(int itemId);

        /// <summary>
        /// 将某项物体的数量修改为指定值（只对可堆叠物体有效），返回新增的物体数（减少之后是负数）
        /// </summary>
        /// <param name="itemId">物品ID</param>
        /// <returns></returns>
        int SetItemCount(int itemId);

        /// <summary>
        /// 是否包含某种物体
        /// </summary>
        /// <param name="itemId">物品ID</param>
        /// <returns></returns>
        bool HasItem(int itemId);

        /// <summary>
        /// 返回仓库中指定ID的物体数量，没有则返回0
        /// </summary>
        /// <param name="itemId">物品ID</param>
        /// <returns></returns>
        int ItemCount(int itemId);

        /// <summary>
        /// 返回某格物体数量
        /// </summary>
        /// <param name="item">物品</param>
        /// <returns></returns>
        int ItemCount(IInventoryItem item);

        /// <summary>
        /// 尝试购买某格物体
        /// 若此物品索引不在指定商店中、或者需要的金额不足，购买失败，不做修改
        /// 购买成功后会扣除本仓库的金钱，添加物品
        /// </summary>
        /// <param name="shop">商店</param>
        /// <param name="item">物品</param>
        /// <returns></returns>
        bool TryBuy(IInventory shop, IInventoryItem item);

    }
}
