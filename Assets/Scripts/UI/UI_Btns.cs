using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UI_Btns : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Top")]
    [SerializeField]
    public MainUIBtn[] TopBtns;

    [Header("Bottom")]
    [SerializeField]
    public MainUIBtn[] BttomBtns;

    void Start()
    {
        foreach (var item in TopBtns)
        {
            item.Btn.OnClickAsObservable().Subscribe(_=> {
                switch (item.UiBtnName)
                {
                    case UIBtn.ProfilBtn:
                        UIProfilePanel ProfilePanel = new UIProfilePanel(item.UIPanelPrefab);
                        break;
                    case UIBtn.SettingBtn:
                        break;
          
                        break;
                    default:
                        break;
                }

            }).AddTo(this);
        }
        foreach (var item in BttomBtns)
        {
            


            item.Btn.OnClickAsObservable().Subscribe(_=> {
                switch (item.UiBtnName)
                {
                    case UIBtn.MessageBtn:
                        break;

                    case UIBtn.FriendBtn:
                        UIFriendPanel FriendPanel = new UIFriendPanel(item.UIPanelPrefab);
                        break;

                    case UIBtn.StoreBtn:
                        UIStorePanel StorePanel = new UIStorePanel(item.UIPanelPrefab);
                        break;

                    case UIBtn.InventoryBtn:
                        UIInventoryPanel InventoryPanel = new UIInventoryPanel(item.UIPanelPrefab);
                        break;

                    case UIBtn.RankingBtn:
                        break;

                    case UIBtn.AchievementBtn:
                        UIAchievePanel AchievePanel = new UIAchievePanel(item.UIPanelPrefab);

                        break;

                    case UIBtn.StartBtn:
                        LoadingSceneController.Instance.LoadScene(SceneName.Game,GameManager.Instance.PlayerUserInfo.Uid);
                        break;

                    default:
                        break;
                }


            }).AddTo(this);
        }
    }

}
