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
        if (GameManager.parse.Equals(false))
        {
            GameManager.parse = true;
        }
    }
    public Card[] Parse_character(int index)                //누니 정보 불러옴 
    {
        List<Card> CharacterList = new List<Card>();
        if (index .Equals( 1))
        {
            csvData = Resources.Load<TextAsset>("GameData/Character");    //csv파일 가져옴

        }
        string[] data = csvData.text.Split(new char[] { '\n' });    //엔터 기준으로 쪼갬.


        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] pro_data = data[i].Split(',');

            if (pro_data[0] .Equals( "end"))
            {
                break;
            }
            if (pro_data[10].Contains("@"))
            {
                pro_data[10]= pro_data[10].Replace("@",",");
            }
            Card character = new Card(pro_data[1], pro_data[2], pro_data[3], pro_data[4], pro_data[5], pro_data[6],
                                       pro_data[7], pro_data[8], pro_data[9], pro_data[10], pro_data[11], pro_data[12], pro_data[13]);
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
        if (GameManager.parse .Equals( false))
        {
            GameManager.parse = true;
        }
        
        List<Building> DictionaryList = new List<Building>(); //도감 캐릭터 리스트 생성.
        List<Building> DictionaryStrList = new List<Building>(); //도감 캐릭터 리스트 생성.
        if (index.Equals(0))
        {
            csvData = Resources.Load<TextAsset>("GameData/Dogam");    //csv파일 가져옴
        }

        string[] data = csvData.text.Split(new char[] { '\n' });    //엔터 기준으로 쪼갬.

      
        
        for (int i = 1; i < data.Length-1; i++)
        {
            string[] pro_data = data[i].Split(',');

            if (pro_data[0].Equals("end"))
            {
                break;
            }
           // Character character = new Character(pro_data[1],pro_data[2], pro_data[3], pro_data[4], pro_data[5], pro_data[6], pro_data[7], pro_data[8], pro_data[9]);
            Building building = new Building(pro_data[1], pro_data[2], pro_data[3], pro_data[4], pro_data[5], pro_data[6], pro_data[7], pro_data[8], pro_data[9], pro_data[10], pro_data[12]);
            building.Level = 1;
            //잠금 유무     // 이름     //설명     //이미지    //가격1       //가격2      //가격3        //생성재화1         //생성재화2        //생성재화3      //설치물인지
            if (pro_data[11] .Equals( "T") )         //설치물인가
            {
                DictionaryStrList.Add(building);
            }
            else                                //설치물 아닌가                               
            {
                DictionaryList.Add(building);
            }

        }
        GameManager.StrArray = DictionaryStrList.ToArray();
     
        return DictionaryList.ToArray();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
