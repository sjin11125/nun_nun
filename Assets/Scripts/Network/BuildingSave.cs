using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BuildingSave : MonoBehaviour
{               //건물들 저장하는 스크립트
                //저장하면 구글 스프레드 시트로 전송

    Buildingsave[] BTosave;
    const string URL = "https://script.google.com/macros/s/AKfycbwE2aIOlyClACGKGkD9rVScaXMv--pSFjqHhtRV9hS9C1bIrBJX_kOm4v3Bz4jOHekq4Q/exec";
    public Buildingsave GD;
    public string Friends;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    
    public void UpdateValue(Building update_building)
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "updateValue");
        form.AddField("buildingPosiiton_x", update_building.BuildingPosition.x.ToString());
        form.AddField("buildingPosiiton_y", update_building.BuildingPosition.y.ToString());
        form.AddField("isLock", update_building.isLock);
        form.AddField("building_name", update_building.Building_name);
        form.AddField("cost", update_building.Cost);
        form.AddField("level", update_building.Level);
        form.AddField("tree", update_building.Tree);
        form.AddField("ice", update_building.Ice);
        form.AddField("grass", update_building.Grass);
        form.AddField("snow", update_building.Snow);
        form.AddField("isFlied", update_building.isFliped.ToString());
        StartCoroutine(Post(form));
    }
    public void AddValue()
    {
        WWWForm form = new WWWForm();
        Building buildings = GetComponent<Building>();
        Debug.Log("건물저장");
        form.AddField("order", "addValue");
        form.AddField("buildingPosiiton_x", buildings.BuildingPosition.x.ToString());
        form.AddField("buildingPosiiton_y", buildings.BuildingPosition.y.ToString());
        form.AddField("isLock", buildings.isLock);
        form.AddField("building_name", buildings.Building_name);
        form.AddField("cost", buildings.Cost);
        form.AddField("level", buildings.Level);
        form.AddField("tree", buildings.Tree);
        form.AddField("ice", buildings.Ice);
        form.AddField("grass", buildings.Grass);
        form.AddField("snow", buildings.Snow);
        form.AddField("isFlied",buildings.isFliped.ToString());
        StartCoroutine(Post(form));
    }
    public void RemoveValue(string b_name)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("order", "removeValue");
        form1.AddField("remove_building", b_name);
        StartCoroutine(Post(form1));

        return;
    }
    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            /*if (www.isDone) Response(www.downloadHandler.text);
            else print("웹의 응답이 없습니다.");*/
        }
    }
    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        GD = JsonUtility.FromJson<Buildingsave>(json);

        if (GD.result == "ERROR")
        {
            print(GD.order + "을 실행할 수 없습니다. 에러 메시지 : " + GD.msg);
            return;
        }


        /*if (GD.order == "getFriend")
        {
            GameManager.Friends = GD.Friends;
            Debug.Log(GameManager.Friends[0]);
        }*/
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
    public string Level;       //건물 레벨
    public string Tree;        //나무
    public string Ice;        //얼음
    public string Grass;        //풀
    public string Snow;        //눈
    public string isFlied;        //뒤집어졌는지
                               //-----------------------------------------------------------
                               //public string[] Friends;
}