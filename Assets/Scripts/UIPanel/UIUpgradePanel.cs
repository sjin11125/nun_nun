using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;
public class UIUpgradePanel : UIBase
{
    public Text UpgradeTextCost;
    public Text UpgradeTextBefore;
    public Text UpgradeTextAfter;

    public Building building;
    public GameObject NoEffectPanel;
    public GameObject NoMoneyPanel;

    int MoneyCost = 0;
    int ShinMoneyCost = 0;

    private void Start()
    {
        base.Start();

        for (int j = 0; j < GameManager.BuildingArray.Length; j++)
        {
            if (building.Building_Image == GameManager.BuildingArray[j].Building_Image)
            {

                UpgradeTextBefore.text = GameManager.BuildingArray[j].Reward[building.Level - 1].ToString();     //업글 전 획득 재화
                Debug.Log("업글전: " + GameManager.BuildingArray[j].Reward[building.Level - 1]);

                MoneyCost = GameManager.BuildingArray[j].Cost[building.Level];
                ShinMoneyCost= GameManager.BuildingArray[j].ShinCost[building.Level];

                UpgradeTextAfter.text = GameManager.BuildingArray[j].Reward[building.Level].ToString();                       //업글 후 획득 재화
                Debug.Log("업글후: " + GameManager.BuildingArray[j].Reward[building.Level - 1]);

                UpgradeTextCost.text = "얼음: " + MoneyCost.ToString() + ",   빛나는 얼음: " + ShinMoneyCost.ToString() + " 이 소모됩니다.";
                break;

            }
        }

        if (UIYesBtn != null)
        {

            UIYesBtn.onClick.AsObservable().Subscribe(_ =>
            {
                Upgrade(building);

            }).AddTo(this);

        }
        if (UINoBtn != null)
        {

            UINoBtn.onClick.AsObservable().Subscribe(_ =>
            {
                Destroy(this.gameObject);

            }).AddTo(this);
        }
        if (UICloseBtn != null)
        {

            UICloseBtn.onClick.AsObservable().Subscribe(_ =>
            {
                Destroy(this.gameObject);
            }).AddTo(this);
        }
    }

    public void Upgrade(Building building)
    {
        bool isUp=false;
        if (building.Level < 2)
        {
            if (building.Building_Image == "building_level(Clone)" ||
                   building.Building_Image == "village_level(Clone)" ||
                   building.Building_Image == "flower_level(Clone)")
            {
                Debug.Log("해당 건물마자");
                for (int i = 0; i < GameManager.CharacterList.Count; i++)
                {
                    if (GameManager.CharacterList[i].cardName == "수리공누니")
                    {
                        Debug.Log("해당 누니이써");
                        isUp = true;
                        break;

                    }
                }
            }
            //GameObject UPPannel = Instantiate(UpgradePannel);
            if (building.Building_Image == "syrup_level(Clone)" ||
             building.Building_Image == "fashion_level(Clone)" ||
             building.Building_Image == "school_level(Clone)")
            {
                Debug.Log("해당 건물마자22");
                for (int i = 0; i < GameManager.CharacterList.Count; i++)
                {
                    if (GameManager.CharacterList[i].cardName == "페인트누니")
                    {
                        Debug.Log("해당 누니이써222");
                        isUp = true;
                        break;
                    }
                }
            }
            if (isUp == true)               //해당 누니 있을 때 업그레이드 O
            {
                if (GameManager.ShinMoney>=ShinMoneyCost &&           //재화 체크
                    GameManager.Money>=MoneyCost)
                {
                    GameManager.ShinMoney -= ShinMoneyCost;
                    GameManager.Money -= MoneyCost;

                    building.Level += 1;
                    building.BuildingImage.sprite = GameManager.GetDogamChaImage(building.Building_Image+building.Level.ToString());//건물이미지 바꿈
                }
                else                                             //재화가 없음
                {
                    NoMoneyPanel.SetActive(true);
                }
            }
            else               //해당 누니 없을 때 업그레이드 X
            {

                NoEffectPanel.SetActive(true);          //해당 누니가 없으면 패널뜨게
            }

        }
        return;
    }
}
