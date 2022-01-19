using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *              3D合成游戏合成框架
 *              
 * 合成框架元素数据基类，只记录合成通用的数据，如有扩展，请自行继承
 * 
 * Made Time : 2021/05/26
 * Made By 宋志强
 */
namespace JQMergeFramework
{
    [System.Serializable]
    public class UnitDataBase
    {
        /// <summary>
        /// 元素放置地块的ID
        /// </summary>
        private int placeId;
        /// <summary>
        /// 元素等级
        /// </summary>
        private int unitLevel;

        public int PlaceId { get => placeId; set => placeId = value; }
        public int UnitLevel { get => unitLevel; set => unitLevel = value; }
        /// <summary>
        /// 升级
        /// </summary>
        public void UpgradeLevel()
        {
            UnitLevel += 1;
        }
    }

}
