using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;
using System;

public class UIVisitorBookPanel : UIBase
{
    //
    //InputField
    public InputField MessageInputField;
    public GameObject MessagePrefab;
    public Transform Content;

    string Uid;

    public Dictionary<string, VisitorBookInfo> VisitorBookMessages;
    public UIVisitorBookPanel(GameObject UIPrefab)
    {
        UIVisitorBookPanel r = UIPrefab.GetComponent<UIVisitorBookPanel>();
        r.Awake();
        r.UIPrefab = UIPrefab;

        r.InstantiatePrefab();
    }

    override public void Start()
    {
        base.Start();
        VisitorBookMessages = new Dictionary<string, VisitorBookInfo>();

        
        if (SceneManager.GetActiveScene().name=="Main")             //내 마을 씬 이라면
            Uid = GameManager.Instance.PlayerUserInfo.Uid;
        else                //친구씬이라면
            Uid = LoadManager.Instance.FriendUid;


        FirebaseLogin.Instance.GetVisitorBook(Uid).ContinueWith((task) => {

            if (!task.IsFaulted)
            {
                if (task.Result != null)
                {
                    Debug.Log("task.Result: " + task.Result);
                    Newtonsoft.Json.Linq.JArray Result = Newtonsoft.Json.Linq.JArray.Parse(task.Result);
                    UnityMainThreadDispatcher.Instance().Enqueue(() =>
                    {
                        foreach (var item in Result)
                        {

                            VisitorMessage Message = Instantiate(MessagePrefab, Content).GetComponent<VisitorMessage>();
                            /*  VisitorBookMessages.Add(JsonUtility.FromJson<VisitorBookInfo>(item.ToString()).FriendTime,
                                  JsonUtility.FromJson<VisitorBookInfo>(item.ToString()));*/

                            VisitorBookInfo TempInfo = JsonUtility.FromJson<VisitorBookInfo>(item.ToString());

                            Message.SetMessage(TempInfo.FriendName, TempInfo.FriendMessage, TempInfo.FriendTime, TempInfo.FriendImage);

                        }
                    });
                }
            }

        });//친구 uid로 방명록 부르기


        MessageInputField.OnEndEditAsObservable().Subscribe(_ => {
            VisitorBookInfo newMessage = new VisitorBookInfo(GameManager.Instance.PlayerUserInfo.Uid,
                                                            MessageInputField.text,
                                                            DateTime.Now.ToString(),
                                                            GameManager.Instance.PlayerUserInfo.Image);
            newMessage.Uid= Uid;

            FirebaseLogin.Instance.SetVisitorBook(newMessage);

            VisitorMessage Message = Instantiate(MessagePrefab, Content).GetComponent<VisitorMessage>();
            Message.SetMessage(newMessage.FriendName, newMessage.FriendMessage, newMessage.FriendTime, newMessage.FriendImage);

        });  //InputField 구독
    }

}
