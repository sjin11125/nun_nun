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
                Debug.Log("ll");
                break;
            }
            Card character = new Card(pro_data[1], pro_data[2], pro_data[3], pro_data[4], pro_data[5], pro_data[6]);

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
    public Character[] Parse(int index)
    {
        if (GameManager.parse == false)
        {
            GameManager.parse = true;
        }
        
        List<Character> DictionaryList = new List<Character>(); //도감 캐릭터 리스트 생성.
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
            Character character = new Character(pro_data[1],pro_data[2], pro_data[3], pro_data[4], pro_data[5], pro_data[6], pro_data[7], pro_data[8], pro_data[9]);

            DictionaryList.Add(character);
        }

   
        return DictionaryList.ToArray();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
