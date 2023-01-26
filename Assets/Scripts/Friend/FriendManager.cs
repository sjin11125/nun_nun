using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UniRx;


public class FriendManager : UIBase
{

    public GameObject Content;


    [SerializeField]
    public List< FriendBtn> FriendBtns;

    public InputField SearchObject;
    public Text NoFriendTxt;
  
    public override void Start()
    {
        base.Start();

        foreach (var FriendBtns in FriendBtns)
        {
            FriendBtns.Btn.OnClickAsObservable().Subscribe(_=> {
                Friend_Exit();      //목록 초기화
                NoFriendTxt.gameObject.SetActive(false);
                SearchObject.gameObject.SetActive(false);

                switch (FriendBtns.FriendUIDef)
                {
                    case FriendDef.GetFriend:                   //친구 목록 가져오기

                        FirebaseLogin.Instance.GetFriend(GameManager.Instance.PlayerUserInfo.Uid).ContinueWith((task)=>{
                            if (!task.IsFaulted)
                            {
                                if (task.Result != null)
                                {
                                    Debug.Log("친구 목록 받아온 결과: " + task.Result);

                                    try
                                    {

                                        Newtonsoft.Json.Linq.JArray Result = Newtonsoft.Json.Linq.JArray.Parse(task.Result);

                                        foreach (var item in Result)
                                        {
                                            Debug.Log("item: " + item.ToString());
                                            FriendInfo itemFriend = JsonUtility.FromJson<FriendInfo>(item.ToString());
                                            //Debug.Log("item: " + JsonUtility.ToJson(item))

                                            UnityMainThreadDispatcher.Instance().Enqueue(() => {
                                                

                                                GameObject FriendUI = Instantiate(FriendBtns.Prefab, Content.transform);       //친구 UI 띄우기
                                                FriendUI.name = itemFriend.FriendName;
                                                FriendUI.GetComponent<FriendInfoUI>().SetFriendInfo(itemFriend);                //친구 버튼 세팅

                                            });
                                            //LoadManager.Instance.MyFriends.Add(itemFriend.f_nickname, itemFriend);      //친구 딕셔너리에 추가
                                        }
                                       
                                    }
                                    catch (Exception e)
                                    {
                                        Debug.LogError(e.Message);
                                        throw;
                                    }

                                }
                                else
                                {
                                    UnityMainThreadDispatcher.Instance().Enqueue(() =>
                                    {


                                        NoFriendTxt.gameObject.SetActive(true);
                                        Debug.Log("task is null");
                                    });
                                }
                            }
                        });
                        break;
                    case FriendDef.RequestFriend:
                        FirebaseLogin.Instance.GetRequestFriend(GameManager.Instance.PlayerUserInfo.Uid).ContinueWith((task) => {
                            if (!task.IsFaulted)
                            {
                                if (task.Result != null)
                                {
                                    Debug.Log("친구 목록 받아온 결과: " + task.Result);

                                    try
                                    {

                                        Newtonsoft.Json.Linq.JArray Result = Newtonsoft.Json.Linq.JArray.Parse(task.Result);

                                        foreach (var item in Result)
                                        {
                                            Debug.Log("item: " + item.ToString());
                                            FriendInfo itemFriend = JsonUtility.FromJson<FriendInfo>(item.ToString());
                                            //Debug.Log("item: " + JsonUtility.ToJson(item))

                                            UnityMainThreadDispatcher.Instance().Enqueue(() => {


                                                GameObject FriendUI = Instantiate(FriendBtns.Prefab, Content.transform);       //친구 UI 띄우기
                                                FriendUI.name = itemFriend.FriendName;
                                                FriendUI.GetComponent<FriendPlusUI>().SetFriendInfo(itemFriend);                //친구 버튼 세팅

                                            });
                                            //LoadManager.Instance.MyFriends.Add(itemFriend.f_nickname, itemFriend);      //친구 딕셔너리에 추가
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Debug.LogError(e.Message);
                                        throw;
                                    }

                                }
                                else
                                {
                                    UnityMainThreadDispatcher.Instance().Enqueue(() =>
                                    {


                                        NoFriendTxt.gameObject.SetActive(true);
                                        Debug.Log("task is null");
                                    });
                                }
                            }
                        });
                        break;
                    case FriendDef.SearchFriend:

                        SearchObject.gameObject.SetActive(true);            //검색창 띄우기

                        SearchObject.OnEndEditAsObservable().Subscribe(_=> {
                            Debug.Log("입력끝 "+ SearchObject.text);
                            Friend_Exit();      //목록 초기화
                            FirebaseLogin.Instance.GetSearchFriend(SearchObject.text).ContinueWith((task) => {
                                if (!task.IsFaulted)
                                {
                                    if (task.Result != null)//누니 넣기
                                    {
                                        Debug.Log("친구 목록 받아온 결과: " + task.Result);

                                        try
                                        {
                                            if (task.Result!=null)
                                            {
                                                FriendInfo SearchFriendInfo = JsonUtility.FromJson<FriendInfo>(task.Result);
                                               
                                                UnityMainThreadDispatcher.Instance().Enqueue(() => {


                                                    GameObject FriendUI = Instantiate(FriendBtns.Prefab, Content.transform);       //친구 UI 띄우기
                                                    FriendUI.name = SearchObject.text;
                                                    FriendUI.GetComponent<FriendPlusUI>().SetFriendInfo(SearchFriendInfo);                //친구 버튼 세팅

                                                });
                                            }
                                            else
                                            {
                                                NoFriendTxt.gameObject.SetActive(true);
                                            }
                                           

                                        }
                                        catch (Exception e)
                                        {
                                            Debug.LogError(e.Message);
                                            throw;
                                        }

                                    }
                                    else
                                    {
                                        Debug.Log("task is null");
                                    }
                                }
                            });
                        });
                        break;
                    case FriendDef.RecommendFriend:
                        break;
                    default:
                        break;
                }
            }).AddTo(this);
        }
    }
    public void Friend_Exit()           //목록 초기화
    {
        Transform[] child = Content.GetComponentsInChildren<Transform>();           //�ϴ� �ʱ�ȭ
        for (int k = 1; k < child.Length; k++)
        {
            Destroy(child[k].gameObject);
        }
    }

}
