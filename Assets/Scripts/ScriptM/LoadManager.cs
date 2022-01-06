using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;

public class LoadManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isLoad = false;

    public GameObject buildings;
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
    void Start()
    {
        if (SceneManager.GetActiveScene().name=="Main")
        {
            isLoad = true;
        }
        //재화로드
        //캐릭터 로드
        if (isLoad==true)
        {
            isLoad = false;
            for (int i = 0; i < GameManager.Items.Length; i++)
            {
                GameManager.Items[i] = false;
            }
             if (SceneManager.GetActiveScene().name == "Main" && GameManager.BuildingArray != null)       //메인씬에서 로드하기
             {
                //건물로드

                for (int i = 0; i < GameManager.BuildingArray.Length; i++)
                {
                    Building LoadBuilding = GameManager.BuildingArray[i];           // 현재 가지고 잇는 빌딩 리스트의 빌딩 컴포넌트
                    string BuildingName = LoadBuilding.Building_Image;        //현재 가지고 있는 빌딩 리스트에서 빌딩 이름 부르기
                    Debug.Log(LoadBuilding.Placed);

                    GameObject BuildingPrefab = GameManager.BuildingPrefabData[BuildingName];           // 해당 건물 프리팹
                    GameObject g = Instantiate(BuildingPrefab, new Vector3(LoadBuilding.BuildingPosition.x, LoadBuilding.BuildingPosition.y, 0), Quaternion.identity) as GameObject;

                  //  Building PrefabBuilding = BuildingPrefab.GetComponent<Building>();      //해당 건물 프리팹의 빌딩 스크립트
                                                                                            //Component tempData = BuildingPrefab.GetComponent<Building>().GetType();
                                                                                            // PrefabBuilding = LoadBuilding;          //프리팹으로 생성된 하우스 오브젝트의 빌딩 스크립트 대입                                                                   
                                                                                            //해당 건물의 프리팹 클론 생성 후 빌딩 스크립트 복제
                    
                    //CopyComponent(LoadBuilding, g);
                    Building g_Building = g.GetComponent<Building>();
                    g_Building.SetValue(LoadBuilding);      //새로 생성된 프리팹의 빌딩 스크립트 value 값을 기존에 있던 스크립트 value값 설정
                    //g.transform.SetParent(buildings.transform);     //buildings를 부모로 설정
                    Debug.Log(LoadBuilding.Building_name);
                    g.name = LoadBuilding.Building_name;            //이름 재설정

                    g_Building.Type = BuildType.Load;
                    g_Building.Place(g_Building.Type);
                    Debug.Log(g.GetComponent<Building>().isFliped);
                   // g_Building.Rotation();
                   
                }
             }

            if (SceneManager.GetActiveScene().name == "Main" && GameManager.CharacterList != null)       //메인씬에서 로드하기
            {
                for (int i = 0; i < GameManager.CharacterList.Count; i++)
                {
                    Card c = GameManager.CharacterList[i];
                    Debug.Log(c.cardImage);

                    Instantiate(GameManager.CharacterPrefab[c.cardImage]);
                }

            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }

}