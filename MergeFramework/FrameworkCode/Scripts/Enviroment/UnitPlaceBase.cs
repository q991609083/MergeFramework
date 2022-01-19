using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *      3D合成游戏合成框架
 *              
 *放置元素的平台
 * 
 * Made Time : 2021/05/26
 * Made By 宋志强
 */
namespace JQMergeFramework { 
    public class UnitPlaceBase : MonoBehaviour
    {
        public int placeId;
        [HideInInspector]
        public UnitBase unitBase = null;
        /// <summary>
        /// 初始化元素
        /// </summary>
        /// <param name="data"></param>
        public void InitUnit(UnitDataBase data)
        {
            GameObject unitObj = MergeManager.Instance.GetUnitObj();
            UnitBase unit = unitObj.GetComponent<UnitBase>();
            unitBase = unit;
            unit.InitUnitBase(data, this);
        }
        /// <summary>
        /// 更换地块绑定的Unit
        /// </summary>
        /// <param name="unit"></param>
        public void ChangeUnit(UnitBase unit)
        {
            unitBase = unit;
            unit.unitPlace = this;
        }
        /// <summary>
        /// 根据给定的状态，显示特效
        /// </summary>
        /// <param name="state"></param>
        public void ShowState(MergeState state,UnitPlaceBase place)
        {

        }

        protected virtual void Awake()
        {
            unitBase = null;
            MergeManager.Instance.placeList.Add(this);
            MergeManager.Instance.showPlaceEffect += ShowState;
        }

        protected virtual void OnDestroy()
        {
            if (MergeManager.Instance.placeList.Contains(this))
            {
                MergeManager.Instance.placeList.Remove(this);
            }
            MergeManager.Instance.showPlaceEffect -= ShowState;
        }
    }
}
