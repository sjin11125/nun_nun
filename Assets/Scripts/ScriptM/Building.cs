using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.Rendering;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;
//using UnityEngine.EventSystems;

[Serializable]
public class BuildingParse
{
    //-------------------------파싱정보------------------------------
    public string isLock;               //잠금 유무
    public string Building_name;            //건물 이름
    public string Info;                 //건물 설명
    public int[] Reward = new int[3] { 0, 0, 0 };            //획득자원
    public string Building_Image;          //빌딩 이미지 이름 *
    public int[] Cost = new int[3] { 0, 0, 0 };          //건물 비용
    public int[] ShinCost = new int[3] { 0, 0, 0 };          //건물 비용
    public int Level = 1;       //건물 레벨
    public string isFliped = "F";
    public string BuildingPosiiton_x;
    public string BuildingPosiiton_y;
    public string Id;
    public string str;      //설치물인지
    //-----------------------------------------------------------

}
public class Building : MonoBehaviour
{
    public Button BuildingBtn;
    public Image BuildingImage;
    public BuildingName BuildingNameEnum;
    #region BuildingProperties
    //*
    public bool Placed = false;    //*
    public BoundsInt area;
    

    public Transform Coin_Button;      //*

    public float currentTime = 0;      //*
    public float startingTime = 60f;   //*

    [SerializeField]
    public List<UIEdit> BuildEditBtn;    // 건축모드 UI들
    [SerializeField]
    public List<GameObject> UIPanels;    //  UI Panel들
    public GameObject VisitorBookUIPanel;       //방명록 UI Panel

    public bool isCoin = false;        //*
    public bool isCountCoin = false;   //*
    public int CountCoin = 0;      //*

    public Vector2 BuildingPosition;                //건물 위치
    //-------------------------파싱정보------------------------------
    public string isLock;               //잠금 유무
    public string Building_name;            //건물 이름s
    public int[] Reward =new int[3] { 0, 0, 0 };               //획득자원
    public string Info;                 //건물 설명
    public string Building_Image;          //빌딩 이미지 이름 *
    public int[] Cost = new int[3] { 0, 0, 0 };        //건물비용
    public int[] ShinCost = new int[3] { 0, 0, 0 };
    public int Level = 1;       //건물 레벨
    public string isFliped = "F";
    public string BuildingPosiiton_x;
    public string BuildingPosiiton_y;
    public string Id;
    public string str;      //설치물인지
    //-----------------------------------------------------------

    public int layer_y;   // 건물 레이어
    

    GameObject Parent;

    public GameObject[] buildings;     // 레벨별 건물
    public Sprite[] buildings_image;    //레벨별 건물 이미지(2,3레벨)

    
    public BuildType Type;

    public BuildingSave save;
    
    bool isUp;

    

    float second = 0;
    IDisposable longClickStream;
    IDisposable timerStream=null;

    public Subject<Vector3> OnMovePosition = new Subject<Vector3>();

    public Vector3 pos;
    new public Transform transform
    {
        get
        {
            Debug.Log("====== Fetching transform");
            return base.transform;
        }
    }
    #endregion
    public Building()
    {
    }
    #region 생성자
    public Building(string islock, string buildingname, string info, string image, string cost,string cost2,string cost3, string Reward, string Reward2, string Reward3, string isStr)           //파싱할 때 쓰는 생성자
    {//잠금 유무     // 이름     //설명     //이미지    //가격1       //가격2      //가격3        //생성재화1         //생성재화2        //생성재화3
        
        isLock = islock;
        Building_name = buildingname;

        this.Reward[0] =int.Parse(Reward) ;                 //생성재화
        this.Reward[1] = int.Parse(Reward2);
        this.Reward[2] = int.Parse(Reward3);

        Info = info;                //건물 설명

        Building_Image = image;     //건물 이미지

        string[] Cost=cost.ToString().Split('*');           
        string[] Cost2=cost2.ToString().Split('*');
        string[] Cost3=cost3.ToString().Split('*');

        this.Cost[0] = int.Parse(Cost[0]);              //비용(골드)
        this.Cost[1] = int.Parse(Cost2[0]);
        this.Cost[2] = int.Parse(Cost3[0]);



        this.ShinCost[0] = int.Parse(Cost[1]);                //비용(발광석)
        this.ShinCost[1] = int.Parse(Cost2[1]);
        this.ShinCost[2] = int.Parse(Cost3[1]);

        this.str = isStr;
        Level = 1;

    }
  /*  public Building(string islock, string buildingname,string reward,string info,string image,string cost, string shinCost, string level,string isfliped,string building_x,string building_y)           //파싱할 때 쓰는 생성자
    {
        isLock = islock;
        Building_name = buildingname;
        Reward = reward;
        Info = info;
        Building_Image = image;
        Cost =int.Parse(cost);
        ShinCost = int.Parse(shinCost);
        Level =int.Parse(level);
        isFliped = isfliped;
        BuildingPosiiton_x = building_x;
        BuildingPosiiton_y= building_y;


    }*/
    public void SetValue(Building getBuilding)
    {
        isLock = getBuilding.isLock;
        Building_name = getBuilding.Building_name;
        Building_Image = getBuilding.Building_Image;
        BuildingPosition = getBuilding.BuildingPosition;
        Placed = getBuilding.Placed;
        Type = getBuilding.Type;
        BuildingNameEnum = getBuilding.BuildingNameEnum;
        //area = getBuilding.area;
        currentTime = getBuilding.currentTime;
        startingTime = getBuilding.startingTime;
        isCoin = getBuilding.isCoin;
        isCountCoin = getBuilding.isCountCoin;
        CountCoin = getBuilding.CountCoin;
        Cost = getBuilding.Cost;
        ShinCost = getBuilding.ShinCost;
        layer_y = getBuilding.layer_y;
        Level = getBuilding.Level;
        isFliped = getBuilding.isFliped;
       BuildingPosiiton_x = getBuilding.BuildingPosiiton_x;
        BuildingPosiiton_y = getBuilding.BuildingPosiiton_y;
        Reward = getBuilding.Reward;
        Id = getBuilding.Id;
    }
    public void SetValueParse(BuildingParse parse)
    {
        isLock = parse.isLock;               //잠금 유무
        Building_name = parse.Building_name;            //건물 이름
        Reward = parse.Reward;               //획득자원
        Info = parse.Info;                 //건물 설명
        Building_Image = parse.Building_Image;          //빌딩 이미지 이름 *
        Cost = parse.Cost;        //건물비용
        ShinCost = parse.ShinCost;
        Level = parse.Level;       //건물 레벨
        isFliped = parse.isFliped;
        BuildingPosiiton_x = parse.BuildingPosiiton_x;
        BuildingPosiiton_y = parse.BuildingPosiiton_y;
        BuildingPosition = new Vector2(float.Parse(parse.BuildingPosiiton_x),float.Parse( parse.BuildingPosiiton_y));
        Id = parse.Id;
    }
    public Building DeepCopy()
    {
        Building BuildingCopy = new Building();
        BuildingCopy.BuildingPosiiton_x = BuildingPosiiton_x;
        BuildingCopy.BuildingPosiiton_y = BuildingPosiiton_y;
        BuildingCopy.isLock = isLock;
        BuildingCopy.Building_name = this.Building_name;
        BuildingCopy.Building_Image = this.Building_Image;
        BuildingCopy.BuildingNameEnum = this.BuildingNameEnum;
        //Debug.Log(BuildingCopy.Building_Image.name);
        BuildingCopy.BuildingPosition = this.BuildingPosition;
        BuildingCopy.Placed = this.Placed;
        BuildingCopy.area = this.area;
        BuildingCopy.currentTime = this.currentTime;
        BuildingCopy.startingTime = this.startingTime;
        BuildingCopy.isCoin = this.isCoin;
        BuildingCopy.isCountCoin = this.isCountCoin;
        BuildingCopy.CountCoin = this.CountCoin;
        
        BuildingCopy.layer_y = this.layer_y;
        BuildingCopy.Level = this.Level;

        BuildingCopy.Cost = this.Cost;
        BuildingCopy.ShinCost = this.ShinCost;
        BuildingCopy.Id = this.Id;
        BuildingCopy.isFliped = isFliped;
        return BuildingCopy;
    }
    #endregion
    private void Update()
    {
        Debug.Log(gameObject.transform.position+ "        "+transform.gameObject.name );
    }
    public void Rotation()          //건물 회전
    {
        Debug.Log("회전");
        if (isFliped == "F")        //회전 안했는가
        {
            BuildingBtn.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

            isFliped = "T";
        }
        else                      //회전 햇는가
        {
            BuildingBtn.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            isFliped = "F";
        }
    }
    float time = 0;
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
        Debug.Log("이동한 위치는 "+ transform.position);
        time = 0;
        if (Type==BuildType.Make)
        {
            while (time!=3)
            {
                StartCoroutine(Wait());

                
               
            }
            Debug.Log("기다린 후 이동한 위치는 " + transform.position);
            //  StartCoroutine(Wait());

        }
        Debug.Log("걸린 시간은 " + time);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        time += 1f;
    }
    void Start()
    {
        OnMovePosition.Subscribe(_=>
        {
            try
            {
                transform.position = _;
                Debug.Log("ㅅ바ㅣㅣㅏㅡ리ㅏㄴㅇㄹ" + transform.position);
            }
            catch(Exception e)
            {
                Debug.LogError(e.Message);
            }

        }).AddTo(this);


        currentTime = (int)startingTime;
        save = GetComponent<BuildingSave>();

       // BuildingImage = BuildingBtn.gameObject.GetComponent<Image>();      //버튼 이미지 받아옴
        //TimeText = GameObject.Find("Canvas/TimeText"); //게임오브젝트 = 캔버스에 있는 TimeText로 설정
        if (Type .Equals( BuildType.Make))
        {
            gameObject.name= Building_Image  ;       //이름 설정
        }

        Coin_Button.gameObject.SetActive(false);

        if (Placed)
        {
            foreach (var item in BuildEditBtn)        //건축모드 버튼들 다 비활성화
            {
                item.btn.gameObject.SetActive(false);
            }
        }

        longClickStream = BuildingBtn.OnPointerDownAsObservable().    //건물 버튼을 꾹 누르는 상태에서 마우스 다운 스트림
                              Subscribe(_ =>
                              {
                               
                                      if(!GridBuildingSystem.isEditing.Value)                       //현재 건설모드가 아니라면
                                          {
                                              timerStream = Observable.FromCoroutine(BuildingEditTimer).Subscribe(_ =>      //일정 시간 지난 후 건설모드 On
                                              {
                                                  //GameManager.isEdit = true;
                                                  //Debug.Log("건설모드 ON");
                                                  Type = BuildType.Move;
                                                  Placed = false;
                                                  GridBuildingSystem.OnEditMode.OnNext(this);       //이 건물의 정보를 넘겨줌
                                                  if (BuildEditBtn.Count!=0)
                                                  {
                                                      foreach (var item in BuildEditBtn)        //건축모드 버튼들 다 활성화
                                                      {
                                                          item.btn.gameObject.SetActive(true);

                                                          if (item.buildUIType==BuildUIType.Upgrade
                                                          &&Level==2)        //최대 레벨인 경우 업그레이드 버튼 활성화 X                                                     
                                                          item.btn.interactable = false;
                                                      }
                                                  }
                                              }).AddTo(this);
                                          }
                                     
                                      // longClickStream.Dispose();          //타이머 구독해지

                                      //1.3초
                                  
                               }).AddTo(this);
       
        var longClickUpStream = BuildingBtn.OnPointerUpAsObservable().Subscribe(_=>     //마우스 업 스트림
            {
                second = 0;

                if (timerStream!=null)
                timerStream.Dispose();

                if (!GridBuildingSystem.isEditing.Value)        //건축모드가 아니라면
                {
                    switch (BuildingNameEnum)
                    {
                        case BuildingName.NuniTree:         //생명의나무을 클릭했을 때
                            SceneManager.LoadScene("Shop");
                            break;

                        case BuildingName.Village:      //마을회관을 클릭했을 때
                           Instantiate(VisitorBookUIPanel, GridBuildingSystem.Canvas.transform);

                            break;
                        default:
                            Type = BuildType.Move;
                            Placed = false;
                            GridBuildingSystem.OnEditMode.OnNext(this);       //이 건물의 정보를 넘겨줌
                            if (BuildEditBtn.Count != 0)
                            {
                                foreach (var item in BuildEditBtn)        //건축모드 버튼들 다 활성화
                                {
                                    item.btn.gameObject.SetActive(true);

                                    if (item.buildUIType == BuildUIType.Upgrade
                                    && Level == 2)        //최대 레벨인 경우 업그레이드 버튼 활성화 X                                                     
                                        item.btn.interactable = false;
                                }
                            }
                            break;
                    }
                }
                //longClickStream.Dispose();

        }).AddTo(this);

        if (BuildEditBtn.Count != 0)
        {


            foreach (var item in BuildEditBtn)                          //건축모드 버튼들 구독
            {
                item.btn.OnClickAsObservable().Subscribe(_ =>
                {

                    switch (item.buildUIType)
                    {
                        case BuildUIType.Make:          //확정 버튼을 눌렀는지
                            if (CanBePlaced())      //배치될 수 있는지 체크
                            {
                                if (Type == BuildType.Move)           //건축모드일때(옮기기)
                                {
                                    Place(Type);

                                    GridBuildingSystem.OnEditModeOff.OnNext(this);


                                    foreach (var item in BuildEditBtn)        //건축모드 버튼들 다 비활성화
                                    {
                                        item.btn.gameObject.SetActive(false);
                                    }
                                }
                                if (Type == BuildType.Make)           //상점모드일때(사기)
                                {
                                    Place(Type);
                                    GridBuildingSystem.OnEditModeOff.OnNext(this);
                                    foreach (var item in BuildEditBtn)        //건축모드 버튼들 다 비활성화
                                    {
                                        item.btn.gameObject.SetActive(false);
                                    }
                                }

                            }
                            break;
                        case BuildUIType.Remove:          //제거 버튼을 눌렀는지
                              UISellPanel uiSellPanel=Instantiate(UIPanels[0],GridBuildingSystem.Canvas.transform).GetComponent<UISellPanel>();
                                uiSellPanel.building = this;
                            break;

                        case BuildUIType.Rotation:          //회전 버튼을 눌렀는지
                            Rotation();                     //회전해주기    
                            LoadManager.ReBuildingSubject.OnNext(this);//건물 리스트 새로고침
                            break;

                        case BuildUIType.Upgrade:          //업그레이드 버튼을 눌렀는지
                            UIUpgradePanel uiUpgradePanel = Instantiate(UIPanels[1], GridBuildingSystem.Canvas.transform).GetComponent<UIUpgradePanel>();
                            uiUpgradePanel.building = this;
                            break;

                        default:
                            break;
                    }
                }).AddTo(this);
            }
        }

        switch (Level)
        {
            case 1:
                break;
            case 2:
                BuildingImage.sprite = GameManager.GetDogamChaImage(Building_Image+Level.ToString());
                break;
            case 3:                     //레벨3은 아직 보류
                break;
            default:
                break;
        }
      
        if (isFliped == "F")        //회전 안했는가
            BuildingBtn.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        else                      //회전 햇는가
            BuildingBtn.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

    }
    IEnumerator BuildingEditTimer()
    {
        while (true)
        {

            yield return new WaitForSeconds(0.1f);
            second += 0.1f;
            Debug.Log(second);
            if (second >= 1.2f)
            {
                second = 0;
                Debug.Log("냐하");
                break;
            }
        }
    }
    #region 기획보류
    public void Coin() //재화부분
    {

        currentTime -= 1 * Time.deltaTime;
        //currentTime = (int)currentTime_1;

        if ((int)currentTime <= 0)
        {
            currentTime = 0;
        }

        if (((int)currentTime % 5 ).Equals( 0) && (int)currentTime != startingTime && isCountCoin .Equals( false) )     //생성되고 5초 마다 재화생성 (건물마다 다르다!)
        {
            isCountCoin = true;
            //CountCoin += 1;

            Coin_Button.gameObject.SetActive(true);
        }
        else if ((int)currentTime % 5 != 0)
        {
            isCountCoin = false;
        }

        // 재화를 누르면 current Time 초기화 or 0이 되면 이미지 MAX coin으로 변환 후 수확하면 currentTime = startingTime




    }

    public void Coin_OK()       //재화 버튼 누르는 함수
    {
        //currentTime =  startingTime;
        isCoin = true;      //코인 먹었음
        GameManager.Money += Reward[Level-1];
        CanvasManger.AchieveMoney += Reward[Level - 1];

       currentTime = (int)startingTime;

        isCoin = true;

        if (currentTime .Equals( 0))
        {
            //수정필요
            currentTime = (int)startingTime;
            //Max 이미지로 바뀜
        }
        Coin_Button.gameObject.SetActive(false);
    }

    #endregion





    #region Build Methods
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
    
    public void Place_Initial(BuildType buildtype)
    {
        Vector3 vec = new Vector3(float.Parse(BuildingPosiiton_x), float.Parse(BuildingPosiiton_y), 0);
        area.position = GridBuildingSystem.current.gridLayout.WorldToCell(vec);
        BoundsInt areaTemp = area;
        //areaTemp.position = positionInt;
        Placed = true;      // 배치 했니? 네
        BuildingPosition = vec;          //위치 저장
        GridBuildingSystem.current.TakeArea(areaTemp);      //타일 맵 설정
        transform.position = vec;

    }
    public void Place(BuildType buildtype)         //건물 배치
    {

        Vector3 vec = transform.position;
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(vec);
        BoundsInt areaTemp = area; 

        Placed = true;      // 배치 했니? 네
        Debug.Log(buildings.Length);

       


        BuildingPosition = transform.position;          //위치 저장
        layer_y = (int)-transform.position.y;      //레이어 설정
        isLock = "T";           //배치했다


        switch (buildtype)
        {
            case BuildType.Empty:
                break;

            case BuildType.Load:
                buildtype = BuildType.Empty;
                break;

            case BuildType.Move:
                GridBuildingSystem.current.TakeArea(areaTemp);      //타일 맵 설정
                Debug.Log("Move");
                isLock = "T";
                LoadManager.ReBuildingSubject.OnNext(this);//건물 리스트 새로고침

                Type = BuildType.Empty;

                // save.UpdateValue(this);
                save.BuildingReq(BuildingDef.updateValue, this);
                break;

            case BuildType.Rotation:
                break;

            case BuildType.Make:
                GridBuildingSystem.current.ClearArea();
                Debug.Log("BuildingPosiiton_x: " + BuildingPosiiton_x);
                //GameManager.BuildingNumber[Building_Image]++; //해당 건물의 갯수 추가
                Id = GameManager.IDGenerator();         //건물 id 생성
                gameObject.name = Id;      //이름 재설정
                BuildingListAdd();      //현재 가지고 있는 건물 리스트에 추가
                Type = BuildType.Empty;
                Debug.Log("새로만듬");
                break;

            case BuildType.Upgrade:
                break;

            case BuildType.Remove:
                break;

            default:
                // save.UpdateValue(this);
                save.BuildingReq(BuildingDef.updateValue, this);
                break;
        }
  

       // gameObject.transform.parent = Parent.transform;
       // GridBuildingSystem.current.temp_gameObject = null;
    }
    public void BuildingListRemove()            //현재 가지고 있는 빌딩 제거
    {

        LoadManager.RemoveBuildingSubject.OnNext(this);
        GridBuildingSystem.isSave = true;
        return;
    }
    public void BuildingListAdd()
    {
        LoadManager.AddBuildingSubject.OnNext(this);     //현재 가지고 있는 빌딩 리스트에 추가

        //GameManager.BuildingArray = GameManager.BuildingList.ToArray();
        Debug.Log("GameManager.BuildingArray: "+ GameManager.BuildingArray.Length);

        GameManager.CurrentBuilding = null;
        //

        save.BuildingReq(BuildingDef.addValue, this);
        //GameManager.isUpdate = true;
    }
    #endregion
    
}

