using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *      3D合成游戏合成框架
 *              
 * 合成管理器，用来管理合成逻辑的判断
 * 
 * Made Time : 2021/05/26
 * Made By 宋志强
 */
namespace JQMergeFramework
{
    /// <summary>
    /// 枚举，用来检测元素之间合成的状态
    /// </summary>
    public enum MergeState
    {
        Merge,
        Exchage,
        Place,
        Origin,
    }
    public class MergeManager : Singleton<MergeManager>
    {
        /// <summary>
        /// 当前是否有正在操作的Unit
        /// </summary>
        public UnitBase operatingUnit = null;
        /// <summary>
        /// 地块数组
        /// </summary>
        public List<UnitPlaceBase> placeList = new List<UnitPlaceBase>();
        /// <summary>
        /// 用来展示place效果的委托
        /// </summary>
        public TwoParamCallback<MergeState,UnitPlaceBase> showPlaceEffect;
        /// <summary>
        /// 用来展示垃圾箱的委托
        /// </summary>
        public OneParamCallback<bool> showRecycleEffect;
        /// <summary>
        /// 检查合成状态
        /// </summary>
        /// <param name="unitBase">元素</param>
        /// <param name="place">射线打到的地块</param>
        /// <returns></returns>
        public MergeState CheckMergeState(UnitBase unitBase,UnitPlaceBase place)
        {
            if(place.unitBase == null)
            {
                return MergeState.Place;
            }
            if(place.unitBase == unitBase)
            {
                return MergeState.Origin;
            }
            if(place.unitBase.unitData.UnitLevel == unitBase.unitData.UnitLevel)
            {
                return MergeState.Merge;
            }
            return MergeState.Exchage;
        }
        /// <summary>
        ///  创建一个元素到地块上
        /// </summary>
        /// <returns></returns>
        public bool CreateUnitOnPlace()
        {
            for(int i = 0; i < placeList.Count; i++)
            {
                if(placeList[i].gameObject.activeInHierarchy && placeList[i].unitBase == null)
                {
                    CreateUnit(placeList[i]);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 在指定Place上创建一个Unit，默认Level为1，自行修改
        /// </summary>
        /// <param name="place"></param>
        private void CreateUnit(UnitPlaceBase place)
        {
            UnitDataBase data = new UnitDataBase();
            data.UnitLevel = 1;
            place.InitUnit(data);
        }
        /// <summary>
        /// 获取Unit的GameObject
        /// 代码自行补充
        /// </summary>
        /// <returns></returns>
        public GameObject GetUnitObj()
        {
            
            return null;
        }
    }
    /// <summary>
    /// 无参无返回值通用委托
    /// </summary>
    public delegate void NoParamCallback();
    /// <summary>
    /// 单参无返回值通用委托
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    public delegate void OneParamCallback<T>(T t);
    /// <summary>
    /// 双参无返回值通用委托
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Y"></typeparam>
    /// <param name="t"></param>
    /// <param name="y"></param>
    public delegate void TwoParamCallback<T, Y>(T t, Y y);


}
