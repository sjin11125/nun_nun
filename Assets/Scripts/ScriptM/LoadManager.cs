using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Reflection;
using UnityEngine.Networking;
using System;

public class LoadManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isLoad = false;

    public GameObject buildings;
    public GameObject nunis;

    bool isLoaded;      //건물 다 불러왔는지

    public GameObject RewardPannel;     //일괄수익 판넬
    //public GameObject 
    Component CopyComponent(Component original, GameObject destination)
    {
        System.Type type = original.GetType();

        Component copy = destination.AddComponent(type);
        Debug.Log(copy.GetType());
        // Copied fields can be restricted with BindingFlags
        FieldInfo[] fields = type.GetFields();
        foreach (FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy;
    }
    IEnumerator MoneyPost(WWWForm form)
    {
        Debug.Log("불러오라");
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            if (www.isDone)
            {

                MoneyResponse(www.downloadHandler.text);

            }  
            else print("웹의 응답이 없습니다.");
        }

    }
    void MoneyResponse(string json)                          //자원 값 불러오기
    {
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log(json);
            return;
        }
        Debug.Log("현재돈:      " + json);
        string[] moneys = json.Split('@');
        Debug.Log(moneys[0] +"    "+ moneys[1]);//
        GameManager.Money =int.Parse(moneys[0].ToString());
        GameManager.ShinMoney= int.Parse(moneys[1].ToString());

        Debug.Log("돈: " + GameManager.Money);
        Debug.Log("발광석: " + GameManager.ShinMoney);

        StartCoroutine(RewardStart());  //일괄수확 가능한지
    }
    IEnumerator Post(WWWForm form)
    {
        Debug.Log("불러오라");
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
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
   
   
    void Response(string json)                          //건물 값 불러오기
    {
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log(json);
            return;
        }
        Debug.Log("josn:      " + json);

        if (json == "Null")
        {
            return;
        }
        GameManager.BuildingList = new List<Building>();
        Newtonsoft.Json.Linq.JArray j = Newtonsoft.Json.Linq.JArray.Parse(json);
        //Debug.Log("j.Count: "+j.Count);
        BuildingParse Buildings = new BuildingParse();
        for (int i = 0; i < j.Count; i++)
        {
            Debug.Log(i);
            Buildings = JsonUtility.FromJson<BuildingParse>(j[i].ToString());
            Building b = new Building();
            b.SetValueParse(Buildings);

            /*  new Building(friendBuildings.isLock, friendBuildings.Building_name, friendBuildings.Reward, friendBuildings.Info, 
              friendBuildings.Building_Image, friendBuildings.Cost.ToString(), friendBuildings.Level.ToString(), friendBuildings.Tree.ToString(),
               friendBuildings.Grass.ToString(), friendBuildings.Snow.ToString(), friendBuildings.Ice.ToString(), friendBuildings.isFliped.ToString(), 
              friendBuildings.buildingPosiiton_x, friendBuildings.buildingPosiiton_y);*/
            GameManager.BuildingList.Add(b);      //내 건물 리스트에 삽입

        }
        Debug.Log("GameManager.BuildingList[0]" + GameManager.BuildingList[0].BuildingPosiiton_x);

        Debug.Log("GameManager.BuildingList[0]" + GameManager.BuildingList[0].BuildingPosiiton_x);
        isLoaded = true;
    }
    public IEnumerator RewardStart()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "questTime");
        form.AddField("player_nickname", GameManager.NickName);
        yield return StartCoroutine(RewardPost(form));
    }

    IEnumerator RewardPost(WWWForm form)
    {
        Debug.Log("RewardPost");
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            if (www.isDone) Reward_response(www.downloadHandler.text);
            else print("웹의 응답이 없습니다.");
        }

    }

    void Reward_response(string json)
    {
        Debug.Log("날짜: " + json);
        string time = json;
        if (time != DateTime.Now.ToString("yyyy.MM.dd"))     //오늘날짜가 아니냐 일괄수확 가능
        {
            Debug.Log("마지막으로 수확했던 날짜: " + time);
            Debug.Log("오늘날짜: " + DateTime.Now.ToString("yyyy.MM.dd"));
            GameManager.isReward = true;
        }
        else
        {
            GameManager.isReward = false;               //오늘날짜면 수확 불가능
        }
        Debug.Log("수확가능여부: " + GameManager.isReward);
    }
    //재화로드
    //캐릭터 로드
    void Start()
    {
        isLoaded = false;
        GameManager.items = 0;          //아이템 초기화

        if (SceneManager.GetActiveScene().name == "Main")
        {

            WWWForm form1 = new WWWForm();
            Debug.Log("건물로딩");
            //isMe = true;                    //내 건물 불러온다!!!!!!!!!!!!!!!!
            form1.AddField("order", "getFriendBuilding");
            form1.AddField("loadedFriend", GameManager.NickName);

            isLoad = true;
            /* WWWForm form2 = new WWWForm();
             Debug.Log("설치물로딩");
             //isMe = true;                    //내 설치물 불러온다!!!!!!!!!!!!!!!!
             form2.AddField("order", "getFriendStr");
             form2.AddField("loadedFriend", GameManager.NickName);
             StartCoroutine(Post_Str(form2)); */


            StartCoroutine(Post(form1));

            WWWForm form2 = new WWWForm();
            Debug.Log("자원로딩");
            //isMe = true;                    //자원 불러오기
            form2.AddField("order", "getMoney");
            form2.AddField("player_nickname", GameManager.NickName);

            StartCoroutine(MoneyPost(form2));

            if (TutorialsManager.itemIndex>13)
            {
                Camera.main.GetComponent<Transform>().position = new Vector3(0, 0, -10);
            }

        }

        Debug.Log("누니갯수: "+GameManager.CharacterList.Count);

        if (SceneManager.GetActiveScene().name == "Main" && GameManager.CharacterList != null)       //메인씬에서 로드하기(누니)
        {
            /*for (int j = 0; j < GameManager.CharacterList.Count; j++)
            {
                Debug.Log(GameManager.CharacterList[j].name);
            }*/
            Debug.Log("GameManager.: " + GameManager.CharacterList.Count);
            for (int i = 0; i < GameManager.CharacterList.Count; i++)
            {
                Debug.Log("not t.: " + i);
                Card c = GameManager.CharacterList[i];
                if (c.isLock == "T")
                {
                    GameObject nuni = Instantiate(GameManager.CharacterPrefab[c.cardImage], nunis.transform);
                    Card nuni_card = nuni.GetComponent<Card>();
                    nuni_card.SetValue(c);
                }
                else
                {
                    Debug.Log("not t.: " + c.cardName + "   " + c.isLock);
                }
            }

        }
    }



    // Update is called once per frame
    void Update()
    {

        if (GameManager.isReward == true)          //일괄수확 할수있니?
        {
            GameManager.isReward = false;
            int MyReward = 0;
            for (int i = 0; i < GameManager.BuildingList.Count; i++)
            {
                for (int j = 0; j < GameManager.BuildingArray.Length; j++)
                {
                    if (GameManager.BuildingList[i].Building_Image == GameManager.BuildingArray[j].Building_Image)
                        MyReward += GameManager.BuildingArray[j].Reward[GameManager.BuildingList[i].Level - 1];
                }
            }
            GameManager.Money += MyReward;


            WWWForm form1 = new WWWForm();
            form1.AddField("order", "questSave");
            form1.AddField("player_nickname", GameManager.NickName);
            form1.AddField("time", DateTime.Now.ToString("yyyy.MM.dd"));

            StartCoroutine(Post(form1));//구글 시트에 오늘날짜 업데이트 해주기

            Debug.Log("내돈: "+MyReward);
            RewardPannel.SetActive(true);
            Text[] rewardText = RewardPannel.GetComponentsInChildren<Text>();
            rewardText[1].text = MyReward.ToString();
        }
        if (isLoad == true)
        {
            isLoad = false;
            //isLoad = false;
            for (int i = 0; i < GameManager.Items.Length; i++)
            {
                GameManager.Items[i] = false;
            }
            if (SceneManager.GetActiveScene().name == "Main" && GameManager.BuildingList != null)       //메인씬에서 로드하기(내 마을)
            {
                //건물로드
                Debug.Log("GameManager.BuildingList.Count: " + GameManager.BuildingList.Count);

                for (int i = 0; i < GameManager.BuildingList.Count; i++)
                {
                    if (GameManager.BuildingList[i].isLock == "F")          //배치안되어있니?
                        continue;

                    Building LoadBuilding = GameManager.BuildingList[i];           // 현재 가지고 잇는 빌딩 리스트의 빌딩 컴포넌트
                    string BuildingName = LoadBuilding.Building_Image;        //현재 가지고 있는 빌딩 리스트에서 빌딩 이름 부르기
                    Debug.Log(LoadBuilding.Placed);
                    Debug.Log("BuildingName: " + BuildingName);
                    GameObject BuildingPrefab = GameManager.BuildingPrefabData[BuildingName];           // 해당 건물 프리팹
                    GameObject g = Instantiate(BuildingPrefab, new Vector3(LoadBuilding.BuildingPosition.x, LoadBuilding.BuildingPosition.y, 0), Quaternion.identity, buildings.transform) as GameObject;

                    //  Building PrefabBuilding = BuildingPrefab.GetComponent<Building>();      //해당 건물 프리팹의 빌딩 스크립트
                    //Component tempData = BuildingPrefab.GetComponent<Building>().GetType();
                    // PrefabBuilding = LoadBuilding;          //프리팹으로 생성된 하우스 오브젝트의 빌딩 스크립트 대입                                                                   
                    //해당 건물의 프리팹 클론 생성 후 빌딩 스크립트 복제

                    //CopyComponent(LoadBuilding, g);
                    Building g_Building = g.GetComponent<Building>();
                    g_Building.SetValue(LoadBuilding);      //새로 생성된 프리팹의 빌딩 스크립트 value 값을 기존에 있던 스크립트 value값 설정
                    Debug.Log("IDIDIDIDID:  " + LoadBuilding.BuildingPosiiton_x);                                      //g.transform.SetParent(buildings.transform);     //buildings를 부모로 설정

                    //Debug.Log("gm_Building.Building_Image: " + GameManager.BuildingArray[0].Building_Image);
                    for (int j = 0; j < GameManager.BuildingArray.Length; j++)
                    {
                        if (g_Building.Building_Image == GameManager.BuildingArray[j].Building_Image)
                        {
                            g_Building.Reward = GameManager.BuildingArray[j].Reward;
                            g_Building.Cost = GameManager.BuildingArray[j].Cost;
                            g_Building.ShinCost = GameManager.BuildingArray[j].ShinCost;
                        }

                    }
                    for (int j = 0; j < GameManager.StrArray.Length; j++)
                    {
                        if (g_Building.Building_Image == GameManager.StrArray[j].Building_Image)
                        {
                            g_Building.Reward = GameManager.StrArray[j].Reward;
                            g_Building.Cost = GameManager.StrArray[j].Cost;
                            g_Building.ShinCost = GameManager.StrArray[j].ShinCost;
                        }

                    }
                    Debug.Log("ididkjflsnmfld:      " + g_Building.Building_name);
                    g.name = g_Building.Id;          //이름 재설정

                    g_Building.Type = BuildType.Load;
                    g_Building.Place_Initial(g_Building.Type);
                    GameManager.IDs.Add(g_Building.Id);
                    Debug.Log(g.GetComponent<Building>().isFliped);
                    // g_Building.Rotation();

                }
            }
          
            else if (SceneManager.GetActiveScene().name == "FriendMain")                            //친구 마을 씬
            {
                for (int i = 0; i < GameManager.FriendBuildingList.Count; i++)
                {
                    Building LoadBuilding = GameManager.FriendBuildingList[i];           // 현재 가지고 잇는 빌딩 리스트의 빌딩 컴포넌트
                    string BuildingName = LoadBuilding.Building_Image;        //현재 가지고 있는 빌딩 리스트에서 빌딩 이름 부르기
                    Debug.Log(BuildingName);

                    foreach (var item in GameManager.BuildingPrefabData)
                    {
                        Debug.Log(item.Key);
                    }
                    Debug.Log(LoadBuilding.BuildingPosiiton_x);
                    Debug.Log(BuildingName);
                    GameObject g = Instantiate(GameManager.BuildingPrefabData[BuildingName], new Vector3(float.Parse(LoadBuilding.BuildingPosiiton_x), float.Parse(LoadBuilding.BuildingPosiiton_y), 0), Quaternion.identity) as GameObject;

                    Building g_Building = g.GetComponent<Building>();
                    g_Building.SetValue(LoadBuilding);
                    //g.transform.position=new Vector3(LoadBuilding.BuildingPosition.x,LoadBuilding.BuildingPosition.y, 0);
                    Debug.Log(LoadBuilding.Building_name);
                    g.name = LoadBuilding.Id;            //이름 재설정

                    g_Building.Type = BuildType.Load;
                    g_Building.Place(g_Building.Type);
                    Debug.Log(g.GetComponent<Building>().isFliped);
                    // g_Building.Rotation();

                }
            }

        }

    }
}