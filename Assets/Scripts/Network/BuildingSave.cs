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

    Buildingsave[] BTosave;
     string URL = GameManager.URL;
    public Buildingsave GD;
    public bool isMe;       //내 자신의 건물을 불러오는가?
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    
    public void UpdateValue(Building update_building)
    {
        Debug.Log("UpdateValue");
        WWWForm form = new WWWForm();
        form.AddField("order", "updateValue");
        Debug.Log("building_image"+update_building.Building_Image);
        form.AddField("building_image", update_building.Building_Image);
        form.AddField("player_nickname", GameManager.NickName);
        form.AddField("buildingPosiiton_x", update_building.BuildingPosition.x.ToString());
        form.AddField("buildingPosiiton_y", update_building.BuildingPosition.y.ToString());
        form.AddField("isLock", update_building.isLock);
        form.AddField("building_name", update_building.Building_name);
        form.AddField("level", update_building.Level);
        form.AddField("isFlied", update_building.isFliped.ToString());
        form.AddField("id", update_building.Id.ToString());

        StartCoroutine(SavePost(form));
    }
    
    public void AddValue()
    {
        WWWForm form = new WWWForm();
        Building buildings = GetComponent<Building>();
        Debug.Log("건물저장");
        form.AddField("order", "addValue");
        form.AddField("player_nickname", GameManager.NickName);
        form.AddField("building_image", buildings.Building_Image);
        form.AddField("buildingPosiiton_x", buildings.BuildingPosition.x.ToString());
        form.AddField("buildingPosiiton_y", buildings.BuildingPosition.y.ToString());
        form.AddField("isLock", buildings.isLock);
        form.AddField("building_name", buildings.Building_name);
        form.AddField("level", buildings.Level);
        form.AddField("isFlied",buildings.isFliped.ToString());
        form.AddField("id", buildings.Id.ToString());
        StartCoroutine(SavePost(form));
    }
    public void RemoveValue(string id)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("order", "removeValue");
        form1.AddField("player_nickname", GameManager.NickName);
        Debug.Log("ID: "+id);
        form1.AddField("id", id);
        StartCoroutine(Post(form1));

        return;
    } public void RemoveValue_str(string id)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("order", "removeValueStr");
        form1.AddField("player_nickname", GameManager.NickName);
        Debug.Log("ID: "+id);
        form1.AddField("id", id);
        StartCoroutine(Post(form1));

        return;
    }
    public void BuildingLoad()              //로그인 했을 때 건물 불러와
    {
        WWWForm form1 = new WWWForm();
        Debug.Log("건물로딩");
        isMe = true;                    //내 건물 불러온다!!!!!!!!!!!!!!!!
        form1.AddField("order", "getFriendBuilding");
        form1.AddField("loadedFriend", GameManager.NickName);
        StartCoroutine(Post(form1));
    } 
    public void FriendBuildindLoad()
    {
        string FriendNickname=gameObject.transform.parent.name;
        GameManager.friend_nickname = FriendNickname;           
        WWWForm form1 = new WWWForm();
        isMe = false;                   //친구 건물 불러올거지롱 메롱
        form1.AddField("order", "getFriendBuilding");
        form1.AddField("loadedFriend", FriendNickname);
        StartCoroutine(Post(form1));
    }
   
    IEnumerator Post(WWWForm form)
    {
        Debug.Log("불러오라");
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 반드시 using을 써야한다
            {
                yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            if (www.isDone)
            {
                
                Response(www.downloadHandler.text);
                
            }    //친구 건물 불러옴
            else print("웹의 응답이 없습니다.");
            
        }
        

    }
    IEnumerator SavePost(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
           // if (www.isDone) Response(www.downloadHandler.text);         //친구 건물 불러옴
                                                                        //else print("웹의 응답이 없습니다.");*/
        }

    }
    
    void Response(string json)                          //건물 값 불러오기
    {
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log(json);
            return;
        }
        Debug.Log("josn:      "+json);
       
        if (isMe .Equals( false) )               //친구 건물 불러오는거라면
        {
            if (json .Equals( "Null"))
            {
                SceneManager.LoadScene("FriendMain");
                return;
            }
            GameManager.FriendBuildingList = new List<Building>();
            Newtonsoft.Json.Linq.JArray j = Newtonsoft.Json.Linq.JArray.Parse(json);
            //Debug.Log("j.Count: "+j.Count);
            BuildingParse friendBuildings = new BuildingParse();
            for (int i = 0; i < j.Count; i++)
            {
                Debug.Log(i);
                friendBuildings = JsonUtility.FromJson<BuildingParse>(j[i].ToString());
                Building b = new Building();
                b.SetValueParse(friendBuildings);

                Debug.Log("X: " + friendBuildings.BuildingPosiiton_x);
                /*  new Building(friendBuildings.isLock, friendBuildings.Building_name, friendBuildings.Reward, friendBuildings.Info, 
                  friendBuildings.Building_Image, friendBuildings.Cost.ToString(), friendBuildings.Level.ToString(), friendBuildings.Tree.ToString(),
                   friendBuildings.Grass.ToString(), friendBuildings.Snow.ToString(), friendBuildings.Ice.ToString(), friendBuildings.isFliped.ToString(), 
                  friendBuildings.buildingPosiiton_x, friendBuildings.buildingPosiiton_y);*/
                GameManager.FriendBuildingList.Add(b);      //친구의 건물 리스트에 삽입

            }
            Debug.Log(GameManager.FriendBuildingList.Count);
            GameManager.isLoading = true;
            SceneManager.LoadScene("FriendMain");
        }
        else                                    //로그인했을 때 내 건물 불러오는거라면
        {
            if (json .Equals( "Null"))
            {
                SceneManager.LoadScene("Main");
                return;
            }
            GameManager.BuildingList = new List<Building>();
            Newtonsoft.Json.Linq.JArray j = Newtonsoft.Json.Linq.JArray.Parse(json);
            //Debug.Log("j.Count: "+j.Count);
            BuildingParse Buildings = new BuildingParse();
            for (int i = 0; i < j.Count; i++)
            {
                Buildings = JsonUtility.FromJson<BuildingParse>(j[i].ToString());
                Building b = new Building();
                b.SetValueParse(Buildings);
                
                /*  new Building(friendBuildings.isLock, friendBuildings.Building_name, friendBuildings.Reward, friendBuildings.Info, 
                  friendBuildings.Building_Image, friendBuildings.Cost.ToString(), friendBuildings.Level.ToString(), friendBuildings.Tree.ToString(),
                   friendBuildings.Grass.ToString(), friendBuildings.Snow.ToString(), friendBuildings.Ice.ToString(), friendBuildings.isFliped.ToString(), 
                  friendBuildings.buildingPosiiton_x, friendBuildings.buildingPosiiton_y);*/
                GameManager.BuildingList.Add(b);      //내 건물 리스트에 삽입

            }
            GameManager.isLoading = true;
            if (gameObject.GetComponent<LoadManager>() != null)
            {
                gameObject.GetComponent<LoadManager>().BuildingLoad();
            }
            else
                SceneManager.LoadScene("Main");
            
            
        }
    }
}

[Serializable]
public class Buildingsave
{
    public string order, result, msg, row_size,length;

    public string BuildingPosition_x;                //건물 위치(x좌표)
    public string BuildingPosition_y;                //건물 위치(y좌표)
    //-------------------------파싱정보------------------------------
    public string isLock;               //잠금 유무
    public string Building_name;            //건물 이름
    public string Reward;               //획득자원
    public string Info;                 //건물 설명
    public string Building_Image;          //빌딩 이미지 이름 *
    public string Cost;        //건물비용
    public string ShinCost;  
    public string Level;       //건물 레벨
    public string isFlied;        //뒤집어졌는지
                               //-----------------------------------------------------------
                               //public string[] Friends;
}