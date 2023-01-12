using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Reflection;
using UnityEngine.Networking;
using System;
using UniRx;

public class LoadManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isLoad = false;

    public GameObject buildings;
    public GameObject nunis;

   public bool isLoaded;      //�ǹ� �� �ҷ��Դ���

    public GameObject RewardPannel;     //�ϰ����� �ǳ�
    public BuildingSave buildingsave;

   public GameObject LoadingPanel;
    public  Dictionary<string, Building> MyBuildings = new Dictionary<string, Building>();          //내가 가지고 있는 빌딩들(id, Building)
      public  Dictionary<string, GameObject> MyBuildingsPrefab = new Dictionary<string, GameObject>();          //내가 가지고 있는 빌딩들(id, Building)
    
    public static Subject<Building> ReBuildingSubject = new Subject<Building>();
    public static Subject<Building> AddBuildingSubject = new Subject<Building>();
    public static Subject<Building> RemoveBuildingSubject = new Subject<Building>();

    public static GameObject Currnetbuildings;
    public static LoadManager _Instance;


    public static LoadManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                return null;
            }
            return _Instance;
        }
    }
    public void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }

    }
    //public GameObject 
    Component CopyComponent(Component original, GameObject destination)
    {
        System.Type type = original.GetType();

        Component copy = destination.AddComponent(type);
        // Copied fields can be restricted with BindingFlags
        FieldInfo[] fields = type.GetFields();
        foreach (FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy;
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
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // �ݵ�� using�� ����Ѵ�
        {
            yield return www.SendWebRequest();
            if (www.isDone) Reward_response(www.downloadHandler.text);
            else print("���� ������ �����ϴ�.");
        }

    }
    IEnumerator TimePost(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // �ݵ�� using�� ����Ѵ�
        {
            yield return www.SendWebRequest();
        }

    }
    
    void Reward_response(string json)
    {
        
        string time = json;
        Debug.Log("보상 "+time);
        if (time != DateTime.Now.ToString("yyyy.MM.dd"))     //���ó�¥�� �ƴϳ� �ϰ���Ȯ ����
        {
            GameManager.isReward = true;
            
            GetReward();
        }
        else
        {
            GameManager.isReward = false;               //���ó�¥�� ��Ȯ �Ұ���
        }
    }
    //��ȭ�ε�
    //ĳ���� �ε�
    void Start()
    {
       

        isLoaded = false;
        GameManager.items = 0;          //������ �ʱ�ȭ

        ReBuildingSubject.Subscribe(building=>                  //건물 리스트 새로고침
        {
            MyBuildings[building.Id] = building.DeepCopy();
        }).AddTo(this); 

        RemoveBuildingSubject.Subscribe(building=>                  //건물 리스트 빼기
        {
            MyBuildings.Remove(building.Id); 
        }).AddTo(this);  

        AddBuildingSubject.Subscribe(building=>                  //건물 리스트 더하기
        {
            MyBuildings.Add(building.Id, building);
        }).AddTo(this);
        Debug.Log("튜토는 " + GameManager.Instance.PlayerUserInfo.Tuto);
        if (SceneManager.GetActiveScene().name.Equals("Main"))          //메인씬이면
        {
            
                if (int.Parse(GameManager.Instance.PlayerUserInfo.Tuto) > 13)
                {
                /*WWWForm form1 = new WWWForm();
                form1.AddField("order", "getFriendBuilding");
                form1.AddField("loadedFriend", GameManager.NickName);*/
                UiLoadingPanel UILoadingPanel = new UiLoadingPanel(LoadingPanel);

                    FirebaseLogin.Instance.GetBuilding(GameManager.Instance.PlayerUserInfo.Uid).ContinueWith((task) =>
                    {
                        Debug.Log("task.Result: " + task.Result);
                        if (!task.IsFaulted)
                        {
                            if (task.Result != null)//건물 넣기
                            {
                                Debug.Log("task.Result: " + task.Result);
                            }
                            else
                            {
                                Debug.Log("task is null");
                            }
                           
                        }
                    });
                UILoadingPanel.DestroyGameObject();
                //GameManager.Instance.BestScoreSave();                   //최고점수 서버 저장
                }
            
           /* Action action ;
            LoadManager.Instance.buildingsave.BuildingReq(BuildingDef.getMyBuilding,null,action=()=> {
                StartCoroutine(RewardStart());
                }
            );          //오늘 재화 받을 수 있는지}) ;*/



                if (int.Parse(GameManager.Instance.PlayerUserInfo.Tuto) > 3)
            {
                Camera.main.GetComponent<Transform>().position = new Vector3(0, 0, -10);
            }
            /*
            for (int i = 0; i < GameManager.Items.Length; i++)
            {
                GameManager.Items[i] = false;
            }*/
        }


        if (SceneManager.GetActiveScene().name.Equals("Main") && GameManager.CharacterList!=null )       //���ξ����� �ε��ϱ�(����)
        {
            for (int i = 0; i < GameManager.CharacterList.Count; i++)
            {
                Card c = GameManager.CharacterList[i];
                if (c.isLock.Equals("T"))
                {
                    GameObject nuni = Instantiate(GameManager.CharacterPrefab[c.cardImage], nunis.transform);
                    Card nuni_card = nuni.GetComponent<Card>();
                    nuni_card.SetValue(c);
                }
            }

        }

        if (SceneManager.GetActiveScene().name.Equals("FriendMain") )                            //친구 마을 씬이냐
        {
            for (int i = 0; i < GameManager.FriendBuildingList.Count; i++)
            {
                Building LoadBuilding = GameManager.FriendBuildingList[i];           // ���� ������ �մ� ���� ����Ʈ�� ���� ������Ʈ
                string BuildingName = LoadBuilding.Building_Image;        //���� ������ �ִ� ���� ����Ʈ���� ���� �̸� �θ���


                GameObject BuildingPrefab = GameManager.BuildingPrefabData[BuildingName];
                GameObject g = Instantiate(BuildingPrefab, new Vector3(LoadBuilding.BuildingPosition.x, LoadBuilding.BuildingPosition.y, 0), Quaternion.identity, buildings.transform) as GameObject;

                Building g_Building = g.GetComponent<Building>();
                g_Building.SetValue(LoadBuilding);
                //g.transform.position=new Vector3(LoadBuilding.BuildingPosition.x,LoadBuilding.BuildingPosition.y, 0);

                g.name = LoadBuilding.Id;            //�̸� �缳��

                g_Building.Type = BuildType.Load;
                g_Building.Place_Initial(g_Building.Type);

                // g_Building.Rotation();

            }
            Destroy(LoadingPanel);
        }


    }

    public void BuildingLoad()
    {
        foreach (var item in MyBuildings)
        {
            if (item.Value.isLock.Equals("F"))          //��ġ�ȵǾ��ִ�?
                continue;

            //string BuildingName = LoadBuilding.Building_Image;        //���� ������ �ִ� ���� ����Ʈ���� ���� �̸� �θ���
            try
            {

            Building g_Building = InstantiateBuilding(item.Value);         //건물 Instatiate

                
            g_Building.Type = BuildType.Load;
            g_Building.Place_Initial(g_Building.Type);

            MyBuildings[g_Building.Id].SetValue(g_Building); 
            MyBuildings[g_Building.Id].area = g_Building.area;
            GameManager.IDs.Add(g_Building.Id);
                // g_Building.Rotation();

            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                throw;
            }
        }
        Destroy(LoadingPanel);
    }
    public Building InstantiateBuilding(Building building)
    {
        try
        {

        GameObject BuildingPrefab = GameManager.BuildingPrefabData[building.Building_Image];           // �ش� �ǹ� ������
        if (building.Type != BuildType.Make)
        {
                Currnetbuildings = Instantiate(BuildingPrefab, new Vector3(building.BuildingPosition.x, building.BuildingPosition.y, 0), Quaternion.identity, buildings.transform) as GameObject;
        }
        else
        {
          Currnetbuildings=  Instantiate(BuildingPrefab, new Vector3(0, 0, 0), Quaternion.identity, buildings.transform) as GameObject;
        }
        if (building.Type != BuildType.Make)
        {


            MyBuildingsPrefab.Add(building.Id, Currnetbuildings);                   //내 건물 프리팹 딕셔너리에 추가

            Building g_Building = Currnetbuildings.GetComponent<Building>();
                if (g_Building.isStr)       //건축물이라면
                    building.isStr = true;

                g_Building.SetValue(building);      //���� ������ �������� ���� ��ũ��Ʈ value ���� ������ �ִ� ��ũ��Ʈ value�� ����
                

            for (int j = 0; j < GameManager.BuildingArray.Length; j++)
            {
                if (g_Building.Building_Image.Equals(GameManager.BuildingArray[j].Building_Image))
                {

                    if (GameManager.BuildingArray[j].Cost.Length != 0)
                    {
                        //g_Building.Reward = GameManager.BuildingArray[j].Reward;
                        for (int p = 0; p < GameManager.BuildingArray[j].Reward.Length; p++)
                        {
                            g_Building.Reward[p] = GameManager.BuildingArray[j].Reward[p];
                        }

                        g_Building.Cost = GameManager.BuildingArray[j].Cost;
                        g_Building.ShinCost = GameManager.BuildingArray[j].ShinCost;
                    }

                }

            }

            for (int j = 0; j < GameManager.StrArray.Length; j++)
            {
                if (g_Building.Building_Image.Equals(GameManager.StrArray[j].Building_Image))
                {
                    if (GameManager.StrArray[j].Reward[0] != 0)
                    {
                        g_Building.Reward = GameManager.StrArray[j].Reward;
                        g_Building.Cost = GameManager.StrArray[j].Cost;
                        g_Building.ShinCost = GameManager.StrArray[j].ShinCost;
                    }

                }

            }
                Currnetbuildings.name = g_Building.Id;          //�̸� �缳��

            return g_Building;
        }

        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            throw;
        }
        return null;

    }
    public void RemoveBuilding(string Id)
    {
        Destroy(MyBuildingsPrefab[Id]);
      
        MyBuildingsPrefab.Remove(Id);

    }
    
    public void GetReward()
    {
        GameManager.isReward = false;
        int MyReward = 0;

        List<string> RewardedNuni = new List<string>();         //보상받은 누니

        foreach (var item in MyBuildings)
        {


            for (int j = 0; j < GameManager.BuildingArray.Length; j++)
            {
                if (item.Value.Building_Image.Equals(GameManager.BuildingArray[j].Building_Image))
                {
                    MyReward += GameManager.BuildingArray[j].Reward[item.Value.Level - 1];

                }

            }

            for (int y = 0; y < GameManager.CharacterList.Count; y++)
            {
                if (item.Value.Building_Image.Equals(GameManager.CharacterList[y].Building[0])
                    && GameManager.CharacterList[y].Gold != "X"
                    && GameManager.CharacterList[y].cardName != RewardedNuni.Find(x => x == GameManager.CharacterList[y].cardName))//건물 보상 받는 누니인가
                {
                    MyReward += int.Parse(GameManager.CharacterList[y].Gold);
                    Debug.Log("    " + item.Value.Building_Image);
                    Debug.Log(y + "    " + GameManager.CharacterList[y].cardName);
                    RewardedNuni.Add(GameManager.CharacterList[y].cardName);

                }

            }

        }

        /*for (int i = 0; i < GameManager.CharacterList.Count; i++)
        {

            if (GameManager.CharacterList[i].cardName== "꾸러기누니"||
                GameManager.CharacterList[i].cardName == "꽃단누니" ||
                GameManager.CharacterList[i].cardName == "어린이누니" ||
                GameManager.CharacterList[i].cardName == "학생누니" )
            {
                nuni50++;
            }
            if (GameManager.CharacterList[i].cardName == "셰프누니" ||
                GameManager.CharacterList[i].cardName == "패션누니" )
            {
                nuni30++;
            }
            MyReward += nuni50 * 50 + nuni30 * 30;

        }*/
        GameManager.Money += MyReward;
        CanvasManger.AchieveMoney += MyReward;


        WWWForm form1 = new WWWForm();
        form1.AddField("order", "questSave");
        form1.AddField("player_nickname", GameManager.NickName);
        form1.AddField("time", DateTime.Now.ToString("yyyy.MM.dd"));

        StartCoroutine(TimePost(form1));//���� ��Ʈ�� ���ó�¥ ������Ʈ ���ֱ�

        if (TutorialsManager.itemIndex > 13)//Ʃ�丮���� ��������
        {
            RewardPannel.SetActive(true);
            Text[] rewardText = RewardPannel.GetComponentsInChildren<Text>();
            rewardText[1].text = MyReward.ToString();
        }
    }
    void Update()
    {
       
       
      
    }
}