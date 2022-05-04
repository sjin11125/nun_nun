using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button X_Image;     //�ǹ� ȸ�� ��ư

    Building this_building;         //�� ��ư�� �ش��ϴ� �ǹ�
    Card this_nuni;         //�� ��ư�� �ش��ϴ� �ǹ�
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
            if (nunis ==null)
            {
                Debug.Log("nunis is null");
            }
            else
            {
                Debug.Log("nunis is not null");
            }
            for (int i = 0; i < GameManager.CharacterList.Count; i++)
            {
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
        if (this_nuni.isLock=="T")      //���ϰ� ��ġ�� ����
        {
            Debug.Log("this_nuni.isLock: T");
            this_nuni.isLock = "F";         //��ġ �ȵ� ���·� �ٲٱ�
            Transform[] nuni_child = nunis.GetComponentsInChildren<Transform>();
            Debug.Log("nuni_child.Length:   "+ nuni_child.Length);
            for (int i = 0; i < nuni_child.Length; i++)                     //���� ��Ͽ��� �ش� ���� ã�Ƽ� ���ֱ�
            {
                Debug.Log("nuni_child[i].gameObject.name: "+nuni_child[i].gameObject.name);
                if (nuni_child[i].gameObject.name == this_nuni.cardImage+"(Clone)")
                {
                    Debug.Log("that_nuni");
                    Card nuni_childs = nuni_child[i].gameObject.GetComponent<Card>();
                    nuni_childs.isLock = "F";
                    Destroy(nuni_child[i].gameObject);
                }
            }


        }
        else                                    //���ϰ� ��ġ �ȵ� ����
        {
            Debug.Log("this_nuni.isLock: F");
            Transform[] nuni_child = nunis.GetComponentsInChildren<Transform>();
            Debug.Log("nuni_child.Length:   " + nuni_child.Length);
            //Debug.Log("nuni_child[i].gameObject.name: " + nuni_child[i].gameObject.name);
            this_nuni.isLock = "T";         //��ġ �� ���·� �ٲٱ�

            for (int i = 0; i < GameManager.CharacterList.Count; i++)           //Instatntiate ���ֱ�
            {
                if (this_nuni.cardName== GameManager.CharacterList[i].cardName)
                {
                    GameManager.CharacterList[i].isLock = "T";
                   GameObject nuni= Instantiate(GameManager.CharacterPrefab[this_nuni.cardImage], nunis.transform)as GameObject;
                
                    
                }

                
            }


        }
    }
    public void Click()         //��ư Ŭ������ ��
    {
        if (this_building.isLock=="T")      //���� ��ġ�� �����ΰ�
        {
            GameManager.isEdit = false;
           
            GameManager.CurrentBuilding = null;
            GameManager.CurrentBuilding_Script = null;
            this_building.isLock = "F";         //��ġ �ȵ� ���·� �ٲٱ�
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
        else                                    //���� ��ġ�� ���°� �ƴѰ�
        {
            //this_building.isLock = "T";         //��ġ �� ���·� �ٲٱ�
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


   /* public void Place()         //�ǹ� ��ư Ŭ������ ��(��ġ)
    {
        X_Button.SetActive(false);
    }
    public void X_Place()       //�ǹ� ȸ��
    {
        X_Button.SetActive(true);
    }*/
}