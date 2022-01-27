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
    const string URL = "https://script.google.com/macros/s/AKfycbx5Jjb7xxxC4ZRQQ6HXI6_aq23zyWAFnO2C07CaR7UqKVUIRXvkiLzSmvmtVbPPZAInyQ/exec?format=tsv&gid=0";
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
            form.AddField("Position_x", BTosave[i].BuildingPosition_x.ToString());
            form.AddField("Position_y", BTosave[i].BuildingPosition_y.ToString());
            form.AddField("isLock", BTosave[i].isLock);
            form.AddField("Building_name", BTosave[i].Building_name);
            form.AddField("Cost", BTosave[i].Cost);
            form.AddField("Level", BTosave[i].Level);
            form.AddField("Tree", BTosave[i].Tree);
            form.AddField("Ice", BTosave[i].Ice);
            form.AddField("Grass", BTosave[i].Grass);
            form.AddField("Snow", BTosave[i].Snow);

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
        //System.Text.Encoding.UTF8.GetString(GD, 3, GD.Length - 3);


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

            building_save.BuildingPosition_x = building.BuildingPosition.x;
            building_save.BuildingPosition_y= building.BuildingPosition.y;
            building_save.isLock = building.isLock;
            building_save.Building_name = building.Building_name;
            building_save.Cost = building.Cost;
            building_save.Level = building.Level;
            building_save.Tree = building.Tree;
            building_save.Ice = building.Ice;
            building_save.Grass = building.Grass;
            building_save.Snow = building.Snow;

            buildingsave.Add(building_save);
        }
        return buildingsave.ToArray();
    }
}

[Serializable]
public class Buildingsave
{
    public float BuildingPosition_x;                //건물 위치(x좌표)
    public float BuildingPosition_y;                //건물 위치(y좌표)
    //-------------------------파싱정보------------------------------
    public string isLock;               //잠금 유무
    public string Building_name;            //건물 이름
    public string Reward;               //획득자원
    public string Info;                 //건물 설명
    public string Building_Image;          //빌딩 이미지 이름 *
    public int Cost;        //건물비용
    public int Level = 1;       //건물 레벨
    public int Tree;        //나무
    public int Ice;        //얼음
    public int Grass;        //풀
    public int Snow;        //눈
    //-----------------------------------------------------------
    public string result, order,msg;
    
}