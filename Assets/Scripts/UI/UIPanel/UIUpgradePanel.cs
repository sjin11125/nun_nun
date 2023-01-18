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
    public UIUpgradePanel(GameObject UIPrefab, Building building)
    {
        /*  base.Awake();
          this.UIPrefab = UIPrefab;
          this.building = building;*/

        UIUpgradePanel r = UIPrefab.GetComponent<UIUpgradePanel>();
        r.Awake();
        r.UIPrefab = UIPrefab;
        r.building = building;

        r.InstantiatePrefab();


    }
  

    private void Start()
    {
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
                this.gameObject.transform.parent.gameObject.SetActive(false);
                Destroy(this.gameObject);

            }).AddTo(this);
        }
        if (UICloseBtn != null)
        {

            UICloseBtn.onClick.AsObservable().Subscribe(_ =>
            {
                this.gameObject.transform.parent.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }).AddTo(this);
        }
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
                foreach (var item in GameManager.Instance.CharacterList)
                {
                    if (item.Value.cardName=="수리공누니")
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
               foreach (var item in GameManager.Instance.CharacterList)
                {
                    if (item.Value.cardName=="페인트누니")
                    {
                        Debug.Log("해당 누니이써");
                        isUp = true;
                        break;
                    }
                }
            }
            if (isUp == true)               //해당 누니 있을 때 업그레이드 O
            {
                MoneyCheck();
            }
            else               //해당 누니 없을 때 업그레이드 X
            {
                UIYesNoPanel UiMoneyCheckPanel = new UIYesNoPanel(NoEffectPanel);
            }

        }
        return;
    }

    void MoneyCheck()
    {
        

        if (int.Parse(GameManager.Instance.PlayerUserInfo.ShinMoney) >= ShinMoneyCost &&           //재화 체크
            int.Parse(GameManager.Instance.PlayerUserInfo.Money) >= MoneyCost)
        {
            int ShinMoney = int.Parse(GameManager.Instance.PlayerUserInfo.ShinMoney);
            ShinMoney -= ShinMoneyCost;
            GameManager.Instance.PlayerUserInfo.ShinMoney = ShinMoney.ToString();

            int Money = int.Parse(GameManager.Instance.PlayerUserInfo.Money);
            Money -= MoneyCost;
            GameManager.Instance.PlayerUserInfo.Money = Money.ToString();

            building.Level += 1;
            building.BuildingImage.sprite = GameManager.GetDogamChaImage(building.Building_Image + building.Level.ToString());//건물이미지 바꿈
        }
        else                                             //재화가 없음
        {
            UIYesNoPanel UiMoneyCheckPanel = new UIYesNoPanel(NoMoneyPanel);

        }
    }
}
