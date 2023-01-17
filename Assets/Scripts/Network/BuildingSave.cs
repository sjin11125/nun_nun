using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class BuildingSave : MonoBehaviour
{               //건물들 저장하는 스크립트
                //저장하면 구글 스프레드 시트로 전송

    // string URL = GameManager.URL;
    public static BuildingSave _Instance;
    public static BuildingSave Instance
    {
        get {
            if (_Instance == null) 
            return null;
            return _Instance;
        }
    }
  

    public bool isMe;       //내 자신의 건물을 불러오는가?
    // Start is called before the first frame update


    public void BuildingReq(BuildingDef buildingDef,Building tempBuilding=null,Action callback=null)
    {
        WWWForm form = new WWWForm();

        switch (buildingDef)
        {
            case BuildingDef.updateValue:
            case BuildingDef.addValue:

                form.AddField("order", buildingDef.ToString());
                form.AddField("building_image", tempBuilding.Building_Image);
                form.AddField("player_nickname", GameManager.NickName);
                form.AddField("BuildingPosition_x", tempBuilding.BuildingPosition.x.ToString());
                form.AddField("BuildingPosition_y", tempBuilding.BuildingPosition.y.ToString());
                form.AddField("isLock", tempBuilding.isLock);
                form.AddField("building_name", tempBuilding.Building_name);
                form.AddField("level", tempBuilding.Level);
                form.AddField("isFlied", tempBuilding.isFliped.ToString());
                form.AddField("id", tempBuilding.Id.ToString());

                //StartCoroutine(Post(form, buildingDef));        //SavePost
                break;

            case BuildingDef.removeValue:

                form.AddField("order", buildingDef.ToString());
                form.AddField("player_nickname", GameManager.NickName);

                form.AddField("id", tempBuilding.Id);
                //StartCoroutine(Post(form, buildingDef));
                break;

            case BuildingDef.getFriendBuilding:
                break;

            case BuildingDef.getMyBuilding:
                isMe = true;                    //내 건물 불러온다!!!!!!!!!!!!!!!!
                form.AddField("order", "getFriendBuilding");
                form.AddField("loadedFriend", GameManager.NickName);
                //StartCoroutine(Post(form, buildingDef,callback));
                break;

            default:
                break;
        }
    }
    
  
    public void FriendBuildindLoad()
    {
        string FriendNickname=gameObject.transform.parent.name;
        GameManager.friend_nickname = FriendNickname;           
        WWWForm form1 = new WWWForm();
        isMe = false;                   //친구 건물 불러올거지롱 메롱
        form1.AddField("order", "getFriendBuilding");
        form1.AddField("loadedFriend", FriendNickname);
       // StartCoroutine(Post(form1, BuildingDef.getFriendBuilding));
    }
   
   /* IEnumerator Post(WWWForm form,BuildingDef buildingDef,Action callback=null)
    {
        Debug.Log("불러오라");
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 반드시 using을 써야한다
            {
                yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            if (www.isDone)
            {
                switch (buildingDef)
                {
                    case BuildingDef.updateValue:
                    case BuildingDef.addValue:
                    case BuildingDef.removeValue:
                        break;

                    case BuildingDef.getFriendBuilding:
                        Response(www.downloadHandler.text, buildingDef);
                        break;

                    case BuildingDef.getMyBuilding:
                        Response(www.downloadHandler.text, buildingDef);
                        callback();
                        break;

                    default:
                        break;
                }
            } 
            else print("웹의 응답이 없습니다.");
            }
        
    }*/
    void Response(string json, BuildingDef buildingDef)                          //건물 값 불러오기
    {
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log(json);
            return;
        }
        Debug.Log("josn:      "+json);

        Newtonsoft.Json.Linq.JArray j = Newtonsoft.Json.Linq.JArray.Parse(json);

        switch (buildingDef)
        {
            case BuildingDef.getFriendBuilding:

                if (json.Equals("Null"))  //건물이 없다면
                {
                    SceneManager.LoadScene("FriendMain");
                    return;
                }

                GameManager.FriendBuildingList = new List<Building>();
  

                BuildingParse friendBuildings = new BuildingParse();

                foreach (var item in j)
                {
                    Debug.Log(item);
                    friendBuildings = JsonUtility.FromJson<BuildingParse>(item.ToString());
                    Building b = new Building();
                    b.SetValueParse(friendBuildings);

                    GameManager.FriendBuildingList.Add(b);      //친구의 건물 리스트에 삽입
                }

                Debug.Log(GameManager.FriendBuildingList.Count);
                GameManager.isLoading = true;
                SceneManager.LoadScene("FriendMain");
                break;

            case BuildingDef.getMyBuilding:
                if (json.Equals("Null"))
                {
                    SceneManager.LoadScene("Main");
                    return;
                }

                BuildingParse Buildings = new BuildingParse();
                foreach (var item in j)
                {
                    Buildings = JsonUtility.FromJson<BuildingParse>(item.ToString());
                    Building b = new Building();
                    b.SetValueParse(Buildings);

                    LoadManager.AddBuildingSubject.OnNext(b); //내 건물 리스트에 삽입

                }
                GameManager.isLoading = true;
                if (gameObject.GetComponent<LoadManager>() != null)
                {
                    gameObject.GetComponent<LoadManager>().BuildingLoad();
                }
                else
                    SceneManager.LoadScene("Main");
                break;

            default:
                break;
        }
  
    }
}

