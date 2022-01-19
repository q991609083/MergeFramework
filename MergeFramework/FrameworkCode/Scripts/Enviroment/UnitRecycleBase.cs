using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *      3D合成游戏合成框架
 *              
 *      回收站逻辑
 * 
 * Made Time : 2021/05/26
 * Made By 宋志强
 */
namespace JQMergeFramework
{
    public class UnitRecycleBase : MonoBehaviour
    {
        /// <summary>
        /// 展示效果
        /// </summary>
        public virtual void ShowEffect(bool state)
        {

        } 
        /// <summary>
        /// 出售方法，具体自行修改
        /// </summary>
        /// <param name="unit"></param>
        public virtual void ForSell(UnitBase unit)
        {
            unit.unitPlace.unitBase = null;
            Destroy(unit.gameObject);
        }

        protected virtual void Awake()
        {
            MergeManager.Instance.showRecycleEffect += ShowEffect;
        }

        protected virtual void OnDestroy()
        {
            MergeManager.Instance.showRecycleEffect -= ShowEffect;
        }
    }
}

