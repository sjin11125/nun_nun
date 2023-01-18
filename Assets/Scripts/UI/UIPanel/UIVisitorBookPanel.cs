using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIVisitorBookPanel : UIBase
{
    //
    //InputField
    public InputField MessageInputField;
    public GameObject MessagePrefab;
    public Transform Content;
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
        {

            FirebaseLogin.Instance.GetVisitorBook(GameManager.Instance.PlayerUserInfo.Uid).ContinueWith((task) =>{

                if (!task.IsFaulted)
                {
                    if (task.Result!=null)
                    {
                        Debug.Log("task.Result: "+task.Result);
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

            }); //내 uid로 방명록 부르기
        }
        else                //친구씬이라면
        {
            //친구 uid로 방명록 부르기
        }
        //InputField 구독
    }

}
