using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/*
 *      3D合成游戏合成框架
 *              
 *      UI逻辑处理，用于发射射线进行检测 
 * 
 * Made Time : 2021/05/26
 * Made By 宋志强
 */
namespace JQMergeFramework
{
    public class MergeTouchArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public void OnDrag(PointerEventData eventData)
        {
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            //获取射线检测到的所有物体
            RaycastHit[] hits = Physics.RaycastAll(ray);
            //射线是否检测到Place
            UnitPlaceBase place = null;
            //射线是否检测到回收站
            UnitRecycleBase recycle = null;
            //用来接收射线打到地板上
            RaycastHit hit = new RaycastHit();
            //检测射线
            foreach (var item in hits)
            {
                if (item.collider.GetComponent<UnitPlaceBase>() != null)
                {
                    place = item.collider.GetComponent<UnitPlaceBase>();
                }
                else if(item.collider.GetComponent<GroundBase>() != null)
                {
                    hit = item;
                }else if(item.collider.GetComponent<UnitRecycleBase>() != null)
                {
                    recycle = item.collider.GetComponent<UnitRecycleBase>();
                }
            }
            //如果射线未打到任何物体，则不处理
            if (hits.Length != 0)
            {
                //如果存在正在操作的物体,则开始操作
                if (MergeManager.Instance.operatingUnit != null)
                {
                    //根据射线打到地面的投影进行位移
                    Transform unit = MergeManager.Instance.operatingUnit.transform;
                    unit.position = new Vector3(hit.point.x, unit.position.y, hit.point.z);
                    //根据是否存在place，进行状态判断
                    if (place != null)
                    {
                        MergeState state = MergeManager.Instance.CheckMergeState(MergeManager.Instance.operatingUnit, place);
                        MergeManager.Instance.showPlaceEffect?.Invoke(state,place);
                    }

                    if (recycle != null)
                    {
                        MergeManager.Instance.showRecycleEffect?.Invoke(true);
                    }
                    else
                    {
                        MergeManager.Instance.showRecycleEffect?.Invoke(false);
                    }
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            //获取射线检测到的所有物体
            RaycastHit[] hits = Physics.RaycastAll(ray);
            //检测射线是否打到Unit上
            UnitBase unit = null;
            //用来接收射线打到地板上
            RaycastHit hit = new RaycastHit();
            //检测射线
            foreach (var item in hits)
            {
                if (item.collider.GetComponent<GroundBase>() != null)
                {
                    hit = item;
                }else if (item.collider.GetComponent<UnitBase>() != null)
                {
                    unit = item.collider.GetComponent<UnitBase>();
                }
            }
            //如果射线未打到任何物体，则不处理
            if (hits.Length != 0)
            {
                //如果无正在操作的物体，则拿起物体
                if(MergeManager.Instance.operatingUnit == null)
                {
                    if (unit != null)
                    {
                        unit.transform.position = new Vector3(hit.point.x, unit.transform.position.y, hit.point.z);
                        MergeManager.Instance.operatingUnit = unit;
                    }
                }
                
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            //获取射线检测到的所有物体
            RaycastHit[] hits = Physics.RaycastAll(ray);
            //检测射线是否打到Place上
            UnitPlaceBase place = null;
            //射线是否检测到回收站
            UnitRecycleBase recycle = null;
            foreach (var item in hits)
            {
                if (item.collider.GetComponent<UnitPlaceBase>() != null)
                {
                    place = item.collider.GetComponent<UnitPlaceBase>();
                }else if(item.collider.GetComponent<UnitRecycleBase>() != null)
                {
                    recycle = item.collider.GetComponent<UnitRecycleBase>();
                }
            }
            if (hits.Length != 0)
            {
                //如果有正在操作的Unit,则检测地块 
                if (MergeManager.Instance.operatingUnit != null)
                {
                    //如果检测到有地块，则进行合成对比
                    if (place != null)
                    {
                        MergeState state = MergeManager.Instance.CheckMergeState(MergeManager.Instance.operatingUnit, place);
                        MergeManager.Instance.operatingUnit.PlaceUnitByState(state, place);
                    }else if(recycle != null)
                    {
                        recycle.ForSell(MergeManager.Instance.operatingUnit);
                    }
                    //如果没有地块，则返回原处
                    else
                    {
                        MergeManager.Instance.operatingUnit.BackToOrigin();
                    }
                }
            }
            //射线未检测到，则返回原处
            else
            {
                MergeManager.Instance.operatingUnit.BackToOrigin();
            }
            MergeManager.Instance.operatingUnit = null;
        }

        private void Awake()
        {
            if(GetComponent<Image>() == null)
            {
                Image img = gameObject.AddComponent<Image>();
                img.color *= new Color(0, 0, 0, 0);
            }
        }
    }
}

