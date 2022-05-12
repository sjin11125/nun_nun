using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DicParsingManager : MonoBehaviour
{

    // Start is called before the first frame update

    public static Character[] Mal;
    TextAsset csvData;
    void Start()
    {
        if (GameManager.parse==false)
        {
            GameManager.parse = true;
        }
    }
    public Str[] Parse_Str(int index)
    {
        List<Str> StrList = new List<Str>();
        if (index == 2)
        {
            csvData = Resources.Load<TextAsset>("StrDogam");    //csv파일 가져옴

        }
        string[] data = csvData.text.Split(new char[] { '\n' });    //엔터 기준으로 쪼갬.
        //string[] pro_data;
        Debug.Log("data.Length:         "+data.Length);


        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] pro_data = data[i].Split(',');

            if (pro_data[0] == "end")
            {
                break;
            }
            Str str = new Str(pro_data[1], pro_data[2], pro_data[3],pro_data[4], pro_data[5]); //잠금    /   이름  /  아이템 /   이미지 /  가격  /  레벨  /  별   /  게이지 /  설명  / 보유효과  / 건물  / 골드 획득량

            Debug.Log("pro_data[3]: "+ pro_data[3]);
            str.SetChaImage(GameManager.DogamStrImageData[pro_data[3]]);
           StrList.Add(str);
        }

        for (int i = 0; i < StrList.Count; i++)
        {
            string ImageName = StrList[i].Building_Image;
            if (GameManager.DogamStrImageData.ContainsKey(ImageName))
            {
                StrList[i].SetChaImage(GameManager.DogamStrImageData[ImageName]);     //설치물 이미지 넣기     

            }
        }
        return StrList.ToArray();


    }
    public Card[] Parse_character(int index)                //누니 정보 불러옴 
    {
        List<Card> CharacterList = new List<Card>();
        if (index == 1)
        {
            csvData = Resources.Load<TextAsset>("Character");    //csv파일 가져옴

        }
        string[] data = csvData.text.Split(new char[] { '\n' });    //엔터 기준으로 쪼갬.
        //string[] pro_data;
        Debug.Log(data.Length);


        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] pro_data = data[i].Split(',');

            if (pro_data[0] == "end")
            {
                break;
            }
            Card character = new Card(pro_data[1], pro_data[2], pro_data[3], pro_data[4], pro_data[5], pro_data[6],
                                       pro_data[7], pro_data[8], pro_data[9], pro_data[10], pro_data[11], pro_data[12]);
            //잠금    /   이름  /  아이템 /   이미지 /  가격  /  레벨  /  별   /  게이지 /  설명  / 보유효과  / 건물  / 골드 획득량
            character.SetChaImage(GameManager.CharacterImageData[pro_data[4]]);
            CharacterList.Add(character);
        }

        for (int i = 0; i < CharacterList.Count; i++)
        {
            string ImageName = CharacterList[i].cardImage;
            if (GameManager.CharacterImageData.ContainsKey(ImageName))
            {
                CharacterList[i].SetChaImage(GameManager.CharacterImageData[ImageName]);     //누니 이미지 넣기     

            }
        }
        return CharacterList.ToArray();

    }

    //-----------------------건물 정보---------------------------------


    public Building[] Parse(int index)
    {
        if (GameManager.parse == false)
        {
            GameManager.parse = true;
        }
        
        List<Building> DictionaryList = new List<Building>(); //도감 캐릭터 리스트 생성.
        if (index==0)
        {
            csvData = Resources.Load<TextAsset>("Dogam");    //csv파일 가져옴
        }

        string[] data = csvData.text.Split(new char[] { '\n' });    //엔터 기준으로 쪼갬.
        //string[] pro_data;
        Debug.Log(data.Length);
      
        
        for (int i = 1; i < data.Length-1; i++)
        {
            string[] pro_data = data[i].Split(',');

            if (pro_data[0]=="end")
            {
                Debug.Log("ll");
                break;
            }
            Debug.Log("pro_data: "+ pro_data[8]);
           // Character character = new Character(pro_data[1],pro_data[2], pro_data[3], pro_data[4], pro_data[5], pro_data[6], pro_data[7], pro_data[8], pro_data[9]);
            Building building = new Building(pro_data[1], pro_data[2], pro_data[3], pro_data[4], pro_data[5], pro_data[6], pro_data[7], pro_data[8], pro_data[9], pro_data[10]);
            
            //잠금 유무     // 이름     //설명     //이미지    //가격1       //가격2      //가격3        //생성재화1         //생성재화2        //생성재화3
            DictionaryList.Add(building);
            Debug.Log("DictionaryList: "+ DictionaryList.Count);
        }

   
        return DictionaryList.ToArray();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
