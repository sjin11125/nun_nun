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

   public bool isLoaded;      //�ǹ� �� �ҷ��Դ���

    public GameObject RewardPannel;     //�ϰ����� �ǳ�
    public BuildingSave buildingsave;

    public GameObject LoadingNuni;
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
        if (TutorialButton.isTutoButton)
        {
            Destroy(this.gameObject);
        }
        else
        {
            return;
        }
        for (int i = 0; i < CanvasManger.currentAchieveSuccess.Length; i++)
        {
           Debug.Log("CanvasManger.currentAchieveSuccess[" + i + "] : " + CanvasManger.currentAchieveSuccess[i]);
        }
        for (int j = 0; j < CanvasManger.achieveContNuniIndex.Length; j++)
        {
           Debug.Log("CanvasManger.achieveContNuniIndex[" + j + "] : " + CanvasManger.achieveContNuniIndex[j]);
        }
        for (int k = 0; k < CanvasManger.achieveCount.Length; k++)
        {
            Debug.Log("CanvasManger.achieveCount[" + k + "] : " + CanvasManger.achieveCount[k]);
        }
        GameManager.isLoading = false;
        isLoaded = false;
        GameManager.items = 0;          //������ �ʱ�ȭ

        if (SceneManager.GetActiveScene().name.Equals("Main"))
        {
            if (TutorialsManager.itemIndex > 13)
            {
                WWWForm form1 = new WWWForm();
                form1.AddField("order", "getFriendBuilding");
                form1.AddField("loadedFriend", GameManager.NickName);

                StartCoroutine(RewardStart());          //오늘 재화 받을 수 있는지

                GameManager.Instance.BestScoreSave();                   //최고점수 서버 저장
            }

            buildingsave.BuildingLoad();

          

            if (TutorialsManager.itemIndex>3)
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
    }

    public void BuildingLoad()
    {
        for (int i = 0; i < GameManager.BuildingList.Count; i++)
        {
            if (GameManager.BuildingList[i].isLock.Equals("F"))          //��ġ�ȵǾ��ִ�?
                continue;

            Building LoadBuilding = GameManager.BuildingList[i];           // ���� ������ �մ� ���� ����Ʈ�� ���� ������Ʈ
            string BuildingName = LoadBuilding.Building_Image;        //���� ������ �ִ� ���� ����Ʈ���� ���� �̸� �θ���

            GameObject BuildingPrefab = GameManager.BuildingPrefabData[BuildingName];           // �ش� �ǹ� ������
            GameObject g = Instantiate(BuildingPrefab, new Vector3(LoadBuilding.BuildingPosition.x, LoadBuilding.BuildingPosition.y, 0), Quaternion.identity, buildings.transform) as GameObject;

            Building g_Building = g.GetComponent<Building>();
            g_Building.SetValue(LoadBuilding);      //���� ������ �������� ���� ��ũ��Ʈ value ���� ������ �ִ� ��ũ��Ʈ value�� ����

            for (int j = 0; j < GameManager.BuildingArray.Length; j++)
            {
                if (g_Building.Building_Image.Equals(GameManager.BuildingArray[j].Building_Image))
                {
               
                    if (GameManager.BuildingArray[j].Cost.Length!=0)
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
                if (g_Building.Building_Image.Equals(GameManager.StrArray[j].Building_Image) )
                {
                    if (GameManager.StrArray[j].Reward[0]!=0)
                    {
                        g_Building.Reward = GameManager.StrArray[j].Reward;
                        g_Building.Cost = GameManager.StrArray[j].Cost;
                        g_Building.ShinCost = GameManager.StrArray[j].ShinCost;
                    }
                
                }

            }
            g.name = g_Building.Id;          //�̸� �缳��

            g_Building.Type = BuildType.Load;
            g_Building.Place_Initial(g_Building.Type);
            GameManager.IDs.Add(g_Building.Id);
            // g_Building.Rotation();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isLoading)
        {
            LoadingNuni.SetActive(true);

        }
        else
        {
            LoadingNuni.SetActive(false);
        }

        if (GameManager.isReward.Equals(true)&&GameManager.isLoading.Equals(true) )        
        {

            
            LoadingNuni.SetActive(false);
            GameManager.isReward = false;
            int MyReward = 0;

            List<string> RewardedNuni = new List<string>();         //보상받은 누니
            int nuni30 = 0;
            for (int i = 0; i < GameManager.BuildingList.Count; i++)            //보상획득
            {
                for (int j = 0; j < GameManager.BuildingArray.Length; j++)
                {
                    if (GameManager.BuildingList[i].Building_Image.Equals(GameManager.BuildingArray[j].Building_Image))
                    {
                        MyReward += GameManager.BuildingArray[j].Reward[GameManager.BuildingList[i].Level - 1];
                      
                    }
                   
                }

                for (int y = 0; y < GameManager.CharacterList.Count; y++)
                {
                    if (GameManager.BuildingList[i].Building_Image.Equals(GameManager.CharacterList[y].Building[0]) 
                        && GameManager.CharacterList[y].Gold != "X"
                        && GameManager.CharacterList[y].cardName!=RewardedNuni.Find(x=>x==GameManager.CharacterList[y].cardName))//건물 보상 받는 누니인가
                    {
                        MyReward += int.Parse(GameManager.CharacterList[y].Gold);
                        Debug.Log(i + "    " + GameManager.BuildingList[i].Building_Image);
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
      if (SceneManager.GetActiveScene().name.Equals("FriendMain") && isLoaded.Equals(false) )                            //ģ�� ���� ��
            {
            isLoaded = true;
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

            GameManager.isLoading = true;
        }

    }
}