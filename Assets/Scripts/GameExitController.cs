using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameExitController : MonoBehaviour
{
    public GameObject TutoManager;
    public GameObject ExitPanel;

    public void Awake()
    {
        if (int.Parse(GameManager.Instance.PlayerUserInfo.Tuto) < 14)
        {
            TutoManager.SetActive(true);
            if (TutorialsManager.itemIndex > 9)//게임갔다오고난 후
            {
                RandomSelect.isTuto = 1;
            }
            else
            {
                RandomSelect.isTuto = 0;//게임튜토 실행
            }
        }
        else//튜토 다 끝낸 상태
        {
            TutoManager.SetActive(false);
            RandomSelect.isTuto = 1;
        }
    }

   /* public void GameSave()
    {
        WWWForm form2 = new WWWForm();                      //돈 저장 -> 업적 저장
        //isMe = true;                 
        form2.AddField("order", "setMoney");
        form2.AddField("player_nickname", GameManager.NickName);
        form2.AddField("version", GameManager.CurVersion);
        form2.AddField("money", GameManager.Money.ToString()+"@"+GameManager.ShinMoney.ToString() + "@" + TutorialsManager.itemIndex+"@"+GameManager.BestScore + "@" + GameManager.Zem);

        form2.AddField("achieve", string.Join(",", CanvasManger.currentAchieveSuccess));
        form2.AddField("index", string.Join(",", CanvasManger.achieveContNuniIndex));
        form2.AddField("count", string.Join(",", CanvasManger.achieveCount));

        form2.AddField("shopbuy", string.Join(",", ShopBuyScript.Achieve12));
        form2.AddField("achieveMoney", string.Join(",", CanvasManger.AchieveMoney));
        form2.AddField("achieveShinMoney", string.Join(",", CanvasManger.AchieveShinMoney));
        form2.AddField("achieveNuniName", string.Join(",", CardUI.AchieveNuniName));
        form2.AddField("achieveFriendCount", string.Join(",", CanvasManger.AchieveFriendCount));

        StartCoroutine(SetPost(form2));
    }
   /* IEnumerator SetPost(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            /*
            if (www.isDone)
            {
            }
            else print("웹의 응답이 없습니다.");
            print("exit");
            

           
            Application.Quit();
        }
    }
    IEnumerator SetPost2(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            /*
            if (www.isDone)
            {
            }
            else print("웹의 응답이 없습니다.");
            print("exit");
            

        
            Application.Quit();
        }
    }*/


    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Home))
            {
                ExitPanel.SetActive(true);
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                ExitPanel.SetActive(true);
            }
            else if (Input.GetKey(KeyCode.Menu))
            {
                ExitPanel.SetActive(true);
            }
        }
    }
}
