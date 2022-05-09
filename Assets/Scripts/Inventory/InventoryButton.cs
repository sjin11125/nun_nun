using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
            if (this_building.isLock == "F")
            {
                X_Image.gameObject.SetActive(false);
            }
            else
            {
                X_Image.gameObject.SetActive(true);
            }

        }
        else if(gameObject.tag == "Inven_Nuni")
        {
            nunis= GameObject.Find("nunis");
            for (int i = 0; i < GameManager.CharacterList.Count; i++)
            {
                if (this.gameObject.name == GameManager.CharacterList[i].cardName+"(Clone)")
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
                if (nuni_child[i].gameObject.name == GameManager.CurrentBuilding.name)
                {
                    Card nuni_childs = nuni_child[i].gameObject.GetComponent<Card>();
                    Destroy(nuni_child[i]);
                }
            }


        }
        else                                    //누니가 배치 안된 상태
        {
            this_building.isLock = "F";         //배치 된 상태로 바꾸기

            for (int i = 0; i < GameManager.CharacterList.Count; i++)           //Instatntiate 해주기
            {
                if (this_nuni.cardName== GameManager.CharacterList[i].cardName)
                {
                    Instantiate(GameManager.CharacterPrefab[this_nuni.cardImage], nunis.transform);
                }

                
            }


        }
    }
    public void Click()         //버튼 클릭했을 때
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
