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
    const string URL = "https://script.google.com/macros/s/AKfycby5MWtjVhA7zdiUf52_jbttDSRarKxlgMTixeKHEDEIyqq9EYg3oc8S1uCukkdXmMB-xA/exec";
    public Buildingsave GD;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue()
    {
        BTosave = Tosave();
        for (int i = 0; i < BTosave.Length; i++)
        {
            WWWForm form = new WWWForm();
            form.AddField("order", "setValue");
            form.AddField("buildingPosiiton_x", BTosave[i].BuildingPosition_x.ToString());
            form.AddField("buildingPosiiton_y", BTosave[i].BuildingPosition_y.ToString());
            form.AddField("isLock", BTosave[i].isLock);
            form.AddField("building_name", BTosave[i].Building_name);
            form.AddField("cost", BTosave[i].Cost);
            form.AddField("level", BTosave[i].Level);
            form.AddField("tree", BTosave[i].Tree);
            form.AddField("ice", BTosave[i].Ice);
            form.AddField("grass", BTosave[i].Grass);
            form.AddField("snow", BTosave[i].Snow);
            form.AddField("row_size", (i+2).ToString());
            form.AddField("length", BTosave.Length.ToString());
            Debug.Log("BTosave.Length: "+BTosave.Length);
            StartCoroutine(Post(form));
        }
           
        
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            if (www.isDone) Response(www.downloadHandler.text);
            else print("웹의 응답이 없습니다.");
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

        /*print(GD.order + "을 실행했습니다. 메시지 : " + GD.msg);

        if (GD.order == "getValue")
        {
            ValueInput.text = GD.value;*/
        }
    
    public Buildingsave[] Tosave()          //저장할 값만 뺌
    {
        List<Buildingsave> buildingsave = new List<Buildingsave>();
        for (int i = 0; i < GameManager.BuildingList.Count; i++)
        {
            Building building = new Building();
            building = GameManager.BuildingList[i];

            Buildingsave building_save = new Buildingsave();
            Debug.Log(building.BuildingPosition.x.ToString());
            Debug.Log(building.BuildingPosition.y.ToString());
            building_save.BuildingPosition_x = building.BuildingPosition.x.ToString();
            building_save.BuildingPosition_y= building.BuildingPosition.y.ToString();
            building_save.isLock = building.isLock;
            building_save.Building_name = building.Building_name;
            building_save.Cost = building.Cost.ToString();
            building_save.Level = building.Level.ToString();
            building_save.Tree = building.Tree.ToString();
            building_save.Ice = building.Ice.ToString();
            building_save.Grass = building.Grass.ToString();
            building_save.Snow = building.Snow.ToString();

            buildingsave.Add(building_save);
        }
        return buildingsave.ToArray();
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
    //-----------------------------------------------------------
    
}