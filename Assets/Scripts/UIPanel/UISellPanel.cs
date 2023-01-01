using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class UISellPanel : UIBase
{
    public Building building;
    public override void Start()
    {
        base.Start();

       // UIPanelName.text = "건물 제거";
       // UIPanelText.text = "건물을 제거하시겠습니까?";

        if (UIYesBtn!=null)
        {

            UIYesBtn.onClick.AsObservable().Subscribe(_ =>
            {
                Remove(building);
            }).AddTo(this);

        }
        if (UINoBtn!=null)
        {

            UINoBtn.onClick.AsObservable().Subscribe(_ =>
            {
                Destroy(this.gameObject);

            }).AddTo(this);
        }
        if (UICloseBtn!=null)
        {

            UICloseBtn.onClick.AsObservable().Subscribe(_ =>
            {
                Destroy(this.gameObject);
            }).AddTo(this);
        }
    }
    public void Remove(Building building)
    {
        Debug.Log("건물 제거");

        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(building.gameObject.transform.position);
        BoundsInt areaTemp = building.area;
        areaTemp.position = positionInt;
        GridBuildingSystem.current.RemoveArea(areaTemp);





        if (building.Type.Equals(BuildType.Make))     //상점에서 사고 설치X 바로 제거
        {
            GameManager.Money += building.Cost[building.Level - 1];          //자원 되돌리기
            CanvasManger.AchieveMoney += building.Cost[building.Level - 1];
            GameManager.ShinMoney += building.ShinCost[building.Level - 1];
            CanvasManger.AchieveShinMoney += building.ShinCost[building.Level - 1];
            Destroy(building.transform.gameObject);
        }
        else                                //설치하고 제거
        {
            GameManager.Money += building.Cost[building.Level - 1] / 10;          //자원 되돌리기
            CanvasManger.AchieveMoney += building.Cost[building.Level - 1] / 10;
            GameManager.ShinMoney += building.ShinCost[building.Level - 1] / 3;
            CanvasManger.AchieveShinMoney += building.ShinCost[building.Level - 1] / 3;

            LoadManager.RemoveBuildingSubject.OnNext(building);           //현재 가지고 있는 건물 목록에서 제거
            LoadManager.Instance.buildingsave.BuildingReq(BuildingDef.removeValue, building);
            Destroy(building.transform.gameObject);
        }

    }
}
