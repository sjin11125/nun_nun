using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isLoad = false;
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
            /* if (SceneManager.GetActiveScene().name == "Main" && GameManager.BuildingArray != null)       //메인씬에서 로드하기
             {
                 Debug.Log("hello");
                 //건물로드
                 Debug.Log(GameManager.BuildingArray.Length);

                 for (int i = 0; i < GameManager.BuildingArray.Length; i++)
                 {
                     Building LoadBuilding = GameManager.BuildingArray[i].GetComponent<Building>();           // 현재 가지고 잇는 빌딩 리스트의 빌딩 컴포넌트
                     string BuildingName = LoadBuilding.Building_Image;        //현재 가지고 있는 빌딩 리스트에서 빌딩 이름 부르기
                     Debug.Log(LoadBuilding.BuildingPosition);

                     GameObject BuildingPrefab = GameManager.BuildingPrefabData[BuildingName];           // 해당 건물 프리팹
                    GameObject g= Instantiate(BuildingPrefab, new Vector3(LoadBuilding.BuildingPosition.x, LoadBuilding.BuildingPosition.y, 0), Quaternion.identity);

                     Building PrefabBuilding = BuildingPrefab.GetComponent<Building>();      //해당 건물 프리팹의 빌딩 스크립트
                                                                                             //Component tempData = BuildingPrefab.GetComponent<Building>().GetType();


                     foreach (var item in LoadBuilding.GetType().GetFields())
                     {
                         item.SetValue(PrefabBuilding, item.GetValue(LoadBuilding));
                     }
                     PrefabBuilding = LoadBuilding.DeepCopy();
                     Debug.Log("level: " + PrefabBuilding.level);
                     PrefabBuilding.Placed = true;
                     Debug.Log(PrefabBuilding.Placed);
                     Debug.Log(g.GetComponent<Building>().Placed);

                     ///----------프리팹으로 생성된 하우스 오브젝트의 빌딩 스크립트 대입해야함----------------
                     // Debug.Log(PrefabBuilding.Placed);
                     GameObject[] BuildingLevels = PrefabBuilding.Buildings();

                     if (PrefabBuilding.level == 2)
                     {
                         BuildingLevels[1].SetActive(true);
                     }
                     else if (PrefabBuilding.level == 3)
                     {
                         BuildingLevels[1].SetActive(true);
                         BuildingLevels[2].SetActive(true);
                     }
                     else
                     {
                         BuildingLevels[0].SetActive(true);
                     }

                 }
             }*/
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