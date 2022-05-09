using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InventoryButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button X_Image;     //건물 회수 버튼

    Building this_building;         //이 버튼에 해당하는 건물
    Card this_nuni;         //이 버튼에 해당하는 건물
    GridBuildingSystem gridBuildingSystem;

    public GameObject buildings;
    public GameObject nunis;
    void Start()
    {
        if (gameObject.tag=="Inven_Building")
        {
            buildings = GameObject.Find("buildings");
            for (int i = 0; i < GameManager.BuildingList.Count; i++)
            {
                if (this.gameObject.name == GameManager.BuildingList[i].Building_name)
                {
                    this_building = GameManager.BuildingList[i];
                    gridBuildingSystem = gameObject.transform.parent.parent.GetComponent<GridBuildingSystem>();
                }
            }
            Debug.Log(this_building.isLock);
           /* if (this_building.isLock == "F")
            {
                X_Image.gameObject.SetActive(false);
            }
            else
            {
                X_Image.gameObject.SetActive(true);
            }*/

        }
        else if(gameObject.tag == "Inven_Nuni")
        {
            nunis= GameObject.Find("nunis");
            for (int i = 0; i < GameManager.CharacterList.Count; i++)
            {
                //Debug.Log("GameManager.CharacterList[i].cardImage: "+ GameManager.CharacterList[i].cardImage);
                if (this.gameObject.name == GameManager.CharacterList[i].cardImage)
                {
                    Debug.Log("this Nuni");
                    this_nuni = GameManager.CharacterList[i];
                    gridBuildingSystem = gameObject.transform.parent.parent.GetComponent<GridBuildingSystem>();
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void nuni_Click()
    {
        if (this_nuni.isLock=="T")      //누니가 배치된 상태
        {
            this_nuni.isLock = "F";         //배치 안된 상태로 바꾸기
            Transform[] nuni_child = nunis.GetComponentsInChildren<Transform>();

            for (int i = 0; i < nuni_child.Length; i++)                     //누니 목록에서 해당 누니 찾아서 없애기
            {
             //   Debug.Log("nuni_child[i].gameObject.name: " + nuni_child[i].gameObject.name);
             //   Debug.Log("this_nuni.cardImage: " + this_nuni.cardImage + "(Clone)");fsdfssfsdfdfs
                if (nuni_child[i].gameObject.name == this_nuni.cardImage+"(Clone)")
                {
                    Card nuni_childs = nuni_child[i].gameObject.GetComponent<Card>();
                    nuni_childs.isLock = "F";
                    Destroy(nuni_child[i].gameObject);
                }
            }


        }
        else                                    //누니가 배치 안된 상태
        {
            this_nuni.isLock = "T";         //배치 된 상태로 바꾸기

            for (int i = 0; i < GameManager.CharacterList.Count; i++)           //Instatntiate 해주기
            {
                Debug.Log("this_nuni.cardName: "+ this_nuni.cardName);
                Debug.Log("GameManager.CharacterList[i].cardName: "+ GameManager.CharacterList[i].cardName);
                if (this_nuni.cardName== GameManager.CharacterList[i].cardName)
                {
                    GameManager.CharacterList[i].isLock = "T";
                    Instantiate(GameManager.CharacterPrefab[this_nuni.cardImage], nunis.transform);
                }

                
            }


        }
        StartCoroutine(NuniSave(this_nuni));          //구글 스크립트에 업데이트
    }
    IEnumerator NuniSave(Card nuni)                //누니 구글 스크립트에 저장
    {

        WWWForm form1 = new WWWForm();
        form1.AddField("order", "nuniSave");
        form1.AddField("player_nickname", GameManager.NickName);
        form1.AddField("nuni", nuni.cardName + ":T");



        yield return StartCoroutine(Post(form1));                        //구글 스크립트로 초기화했는지 물어볼때까지 대기


    }
    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            if (www.isDone) NuniResponse(www.downloadHandler.text);
            //else print("웹의 응답이 없습니다.");*/
        }

    }
    void NuniResponse(string json)                          //누니 불러오기
    {
        //List<QuestInfo> Questlist = new List<QuestInfo>();
        Debug.Log(json);
        if (json == "null")
        {
            return;
        }
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log(json);
            return;
        }
        //누니 이름 받아서 겜메 모든 누니 배열에서 누니 정보 받아서 넣기


    }
    public void Click()         //건축물 버튼 클릭했을 때
    {
        if (this_building.isLock=="T")      //현재 배치된 상태인가
        {
            GameManager.isEdit = false;
           
            GameManager.CurrentBuilding = null;
            GameManager.CurrentBuilding_Script = null;
            this_building.isLock = "F";         //배치 안된 상태로 바꾸기
            X_Image.gameObject.SetActive(true);

            Transform[] building_child=buildings.GetComponentsInChildren<Transform>();

            Destroy(GameManager.CurrentBuilding);
            for (int i = 0; i < building_child.Length; i++)
            {
                if (building_child[i].gameObject.name ==GameManager.CurrentBuilding.name)
                {
                    Building building_childs = building_child[i].gameObject.GetComponent<Building>();
                    Destroy(building_childs);
                }
            }

            for (int i = 1; i < building_child.Length; i++)
            {
                if (building_child[i].gameObject.name==gameObject.name)
                {
                    Building building_childs= building_child[i].gameObject.GetComponent<Building>();
                    building_childs.isLock = "F";
                    building_childs.BuildingPosiiton_x = "0";
                    building_childs.BuildingPosiiton_y = "0";

                    building_childs.save.UpdateValue(building_childs);
                    Destroy(building_child[i].gameObject);
                }
                
            }
        }
        else                                    //현재 배치된 상태가 아닌가
        {
            //this_building.isLock = "T";         //배치 된 상태로 바꾸기
            GameManager.InvenButton =this.GetComponent<Button>();
            
            X_Image.gameObject.SetActive(false);
            Debug.Log("image: " + this_building.Building_Image);
            GameObject buildingprefab = GameManager.BuildingPrefabData[this_building.Building_Image];

            GameManager.CurrentBuilding  = buildingprefab;
            Building b = buildingprefab.GetComponent<Building>();
            Building c = GameManager.CurrentBuilding.GetComponent<Building>();
            c = b.GetComponent<Building>().DeepCopy();
            c.SetValue(b);
            GameManager.CurrentBuilding_Script = this_building;

            //GameManager.CurrentBuilding.name = this_building.Building_Image;
            GameManager.isEdit = true;
            gridBuildingSystem.Inven_Move(GameManager.CurrentBuilding.transform);


        }
    }


   /* public void Place()         //건물 버튼 클릭했을 때(배치)
    {
        X_Button.SetActive(false);
    }
    public void X_Place()       //건물 회수
    {
        X_Button.SetActive(true);
    }*/
}
