using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StrParse
{
    //-------------------------파싱정보------------------------------
    public string isLock;               //잠금 유무
    public string Building_name;            //건물 이름
    public string Building_Image;          //빌딩 이미지 이름
    public string isFliped = "F";
    public string BuildingPosiiton_x;
    public string BuildingPosiiton_y;
    public string Id;
    //-----------------------------------------------------------

}
public class Str : MonoBehaviour
{
    public bool Placed = false;    //*

    public string isLock;               //잠금 유무
    public string Building_name;            //건물 이름
    public string Building_Image;          //빌딩 이미지 이름
    public string isFliped = "F";
    public string BuildingPosiiton_x;
    public string BuildingPosiiton_y;
    public string Id;
    public string Cost;         //비용
    public string Info;

    public Sprite Image;
    // Start is called before the first frame update
    GameObject Parent;

    public BuildType Type;

    public BuildingSave save;

    public int layer_y;   // 건물 레이어
    Transform[] child;

    public Transform Button_Pannel;    //*
    public Transform Rotation_Pannel;
    public Transform Remove_Pannel;

    public BoundsInt area;

    public Vector2 BuildingPosition;                //건물 위치
    public Str(string isLock, string str_name, string str_image,string info,string cost)
    {
        this.isLock = isLock;
        Building_name = str_name;
        Building_Image = str_image;
        Info = info;
        Cost = cost;
    }

    public Str()
    {
    }
    public void SetChaImage(Sprite image)
    {
        Image = image;
    }
    public void SetValue(Str getBuilding)
    {
        isLock = getBuilding.isLock;
        Building_name = getBuilding.Building_name;
        Building_Image = getBuilding.Building_Image;
        isFliped = getBuilding.isFliped;
        BuildingPosiiton_x = getBuilding.BuildingPosiiton_y;
        BuildingPosiiton_y = getBuilding.BuildingPosiiton_y;
        Id = getBuilding.Id;
    }
    public Str DeepCopy()
    {
        Str StrCopy = new Str();
        StrCopy.isLock= isLock;               //잠금 유무
        StrCopy.Building_name = Building_name;            //건물 이름
        StrCopy.Building_Image= Building_Image;          //빌딩 이미지 이름
        StrCopy.isFliped = isFliped;
        StrCopy.BuildingPosiiton_x=BuildingPosiiton_x;
        StrCopy. BuildingPosiiton_y=BuildingPosiiton_y;
        StrCopy.Id = Id ;
        return StrCopy;
    }
    public void RefreshStrList()               //빌딩 리스트 새로고침
    {
        for (int i = 0; i < GameManager.StrList.Count; i++)
        {
            if (GameManager.StrList[i].Building_name == Building_name)
            {
                GameManager.StrList[i] = this.DeepCopy();
            }
        }
        GridBuildingSystem.isSave = true;
    }
    public void Rotation()          //건물 회전
    {
        bool isflip_bool;

        if (isFliped == "F")
            isflip_bool = false;
        else
            isflip_bool = true;

        SpriteRenderer spriterenderer = GetComponentInChildren<SpriteRenderer>();
        spriterenderer.flipX = isflip_bool;

      
                if (isFliped == "T")
                {
                    isFliped = "F";
                }
                else
                    isFliped = "T";
        RefreshStrList();//설치물 리스트 새로고침
    }
    void Awake()
    {
        Parent = GameObject.Find("buildings");
    }
    void Start()
    {
        bool isflip_bool;

        if (isFliped == "F")
            isflip_bool = false;
        else
            isflip_bool = true;

        
        save = GetComponent<BuildingSave>();
        //TimeText = GameObject.Find("Canvas/TimeText"); //게임오브젝트 = 캔버스에 있는 TimeText로 설정
        if (Type == BuildType.Make)
        {
            Building_Image = gameObject.name;       //이름 설정
        }

        //Placed = false;

        child = GetComponentsInChildren<Transform>();


        //Text countdownText = GetComponent<Text>();

        layer_y = 10;
        child[1].GetComponent<SpriteRenderer>().sortingOrder = layer_y;


        
        if (isflip_bool == true)
        {
            Rotation();
        }
    }
    void Update()
    {
        // layer_y = 1;             //레이어 설정



        // text.text = currentTime.ToString("0.0");
        //TimeText.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.30f, 1.4f, 0)); //Timer위치

        //이제 추가해야할 것은 건물을 눌렀을때 시간이 뜨도록 하기 (이거는 나중에)
        //건물이 생성되면 시간도 생성되어야 함 (이것도 나중에)


        // 시간이 흐르는 것이 계속 저장되도록 하기


        // 아이콘을 누르면 재화 + 
        // current Time이 일정시간 밑으로 떨어졌을 때 수확 아이콘 생성


        if (Placed == true)       // 건물 배치가 확정
        {
            Button_Pannel.gameObject.SetActive(false);     // 배치하는 버튼 사라지게
            Rotation_Pannel.gameObject.SetActive(false);        //회전 버튼 사라지게
            Remove_Pannel.gameObject.SetActive(false);
        }
        else
        {

            Button_Pannel.gameObject.SetActive(true);
            Rotation_Pannel.gameObject.SetActive(true);
            Remove_Pannel.gameObject.SetActive(true);
        }
    }
    public bool CanBePlaced()           //건물이 놓여질 수 있는지 체크
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);     //현재위치
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;


        if (GridBuildingSystem.current.CanTakeArea(areaTemp))
        {
            return true;
        }
        return false;
    }
    public void Remove(Str building)
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        //Debug.Log()
        GameManager.Money += int.Parse( Cost);          //자원 되돌리기

        GridBuildingSystem.current.RemoveArea(areaTemp);
        if (Type == BuildType.Make)      //상점에서 사고 설치X 바로 제거
        {
            Destroy(gameObject);
        }
        else                                //설치하고 제거
        {
            StrListRemove();
            save.RemoveValue(Id);
            Destroy(gameObject);
        }
        GameManager.isUpdate = true;
    }
    public void Place_Initial(BuildType buildtype)
    {
        Vector3 vec = new Vector3(float.Parse(BuildingPosiiton_x), float.Parse(BuildingPosiiton_y), 0);
        area.position = GridBuildingSystem.current.gridLayout.WorldToCell(vec);
        BoundsInt areaTemp = area;
        //areaTemp.position = positionInt;
        Placed = true;      // 배치 했니? 네
        GridBuildingSystem.current.TakeArea(areaTemp);      //타일 맵 설정
        transform.position = vec;
    }
    public void Place(BuildType buildtype)         //건물 배치
    {
        Debug.Log("Place()");

        Vector3 vec = transform.position;
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(vec);
        BoundsInt areaTemp = area;
        Debug.Log(areaTemp.position);
        //areaTemp.position = positionInt;
        //Debug.Log(areaTemp.position);
        Placed = true;      // 배치 했니? 네

        GridBuildingSystem.current.TakeArea(areaTemp);      //타일 맵 설정

        //currentTime = startingTime;
        //원래 업데이트 부분
        BuildingPosition = transform.position;          //위치 저장
        layer_y = (int)(-transform.position.y / 0.6);             //레이어 설정
        isLock = "T";           //배치했다

        if (layer_y == 0 || layer_y == 1)
        {
            layer_y = 2;
        }
        Str BuildingCurrent = gameObject.GetComponent<Str>();


        if (buildtype == BuildType.Make)                       //새로 만드는 건가?
        {

            Building_name = gameObject.name;
            Debug.Log("BuildingPosiiton_x: " + BuildingPosiiton_x);
            //GameManager.BuildingNumber[Building_Image]++; //해당 건물의 갯수 추가
            Id = GameManager.IDGenerator();
            gameObject.name = Id;      //이름 재설정
            StrListAdd();      //현재 가지고 있는 건물 리스트에 추가
            buildtype = BuildType.Empty;
            Debug.Log("새로만듬");

        }
        else if (buildtype == BuildType.Load)                    //로드할때
        {
            buildtype = BuildType.Empty;
        }
        else if (buildtype == BuildType.Move)               //이동할 때
        {
            Debug.Log("Move");
            gameObject.name = GameManager.CurrentBuilding_Script.Id;
            Id = GameManager.CurrentBuilding_Script.Id;
            Building_name = GameManager.CurrentBuilding_Script.Building_name;
            isLock = "T";
            RefreshStrList();

            buildtype = BuildType.Empty;

            save.UpdateValue(this);
        }
        else
        {
            save.UpdateValue(this);
        }

        gameObject.transform.parent = Parent.transform;
        GridBuildingSystem.current.temp_gameObject = null;
    }
    public void StrListRemove()
    {
        for (int i = GameManager.StrList.Count - 1; i >= 0; i--)
        {
            if (GameManager.StrList[i].Building_name == Building_name)
            {
                Debug.Log("Remove: " + GameManager.StrList[i].Building_name);
                GameManager.StrList.RemoveAt(i);
                for (int p = 0; p < GameManager.StrList.Count; p++)
                {
                    Debug.Log("Current: " + GameManager.StrList[p].Building_name);
                }
                return;
            }

        }

        GridBuildingSystem.isSave = true;

    }
    public void StrListAdd()
    {
        GameManager.StrList.Add(this.DeepCopy());      //현재 가지고 있는 빌딩 리스트에 추가

        GameManager.StrArray = GameManager.StrList.ToArray();
        Debug.Log("GameManager.StrArray: " + GameManager.StrArray.Length);

        GameManager.CurrentStr = null;
        //

        save.AddValue(this);
        //GameManager.isUpdate = true;
    }
}




