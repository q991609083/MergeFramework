using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JQMergeFramework
{
    public class UnitBase : MonoBehaviour
    {
        [HideInInspector]
        public UnitDataBase unitData;
        [HideInInspector]
        public UnitPlaceBase unitPlace;

        public UnityEngine.UI.Text text;

        public Vector3 offset = new Vector3(0,1,0);

        public virtual void InitUnitBase(UnitDataBase data, UnitPlaceBase place)
        {
            unitData = data;
            unitPlace = place;
            BackToOrigin();
        }
        /// <summary>
        /// 返回原点
        /// </summary>
        public virtual void BackToOrigin()
        {
            transform.position = unitPlace.transform.position + offset;
            text.text = unitData.UnitLevel.ToString();
        }
        /// <summary>
        /// 元素升级
        /// </summary>
        public virtual void UpgradeUnit()
        {
            unitData.UpgradeLevel();
            BackToOrigin();
        }
        public virtual void PlaceUnitByState(MergeState state,UnitPlaceBase place)
        {
            switch (state)
            {
                case MergeState.Merge:
                    {
                        unitPlace.unitBase = null;
                        place.unitBase.UpgradeUnit();
                        RemoveMine();
                        break;
                    }
                case MergeState.Exchage:
                    {
                        UnitBase other = place.unitBase;
                        unitPlace.ChangeUnit(other);
                        place.ChangeUnit(this);
                        BackToOrigin();
                        other.BackToOrigin();
                        break;
                    }
                case MergeState.Place:
                    {
                        unitPlace.unitBase = null;
                        place.ChangeUnit(this);
                        BackToOrigin();
                        break;
                    }
                case MergeState.Origin:
                    {
                        BackToOrigin();
                        break;
                    }
            }
        }

        public virtual void RemoveMine()
        {
            Destroy(gameObject);
        }
    }
}

