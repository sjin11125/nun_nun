using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    string Name, Tale, Property;    // 이름, 동화, 특성
    string Information, isLock;         // 설명
    string ImageName;
    Sprite Image;
    string Money;
    string Level;
    string Deco;
    public Character(string islock, string name, string tale, string property, string information, string imagename, string money,string level,string deco)     //생성자
    {
        isLock = islock;
        Name = name;
        Tale = tale;
        Property = property;
        Information = information;
        ImageName = imagename;
        Money = money;
        Level = level;
        Deco = deco;
    }
    public void SetCharImage(Sprite image)
    {
        Image = image;
    }
    public string GetCharacter(string info)
    {
        if (info == "Name")
        {
            return Name;
        }
        else if (info == "Tale")
        {
            return Tale;
        }
        else if (info == "Property")
        {
            return Property;
        }
        else if (info == "Information")
        {
            return Information;
        }
        else if (info == "isLock")
        {
            return isLock;
        }
        else if (info == "ImageName")
        {
            return ImageName;
        }
        else if (info == "Money")
        {
            return Money;
        }
        else if(info == "Level")
        {
            return Level;
        }
        else if(info == "Deco")
        {
            return Deco;
        }
        else
        {
            return null;
        }
    }       //정보 부르기
    public void SetCharacter(string info,string value)
    {
        if (info == "Name")
        {
            Name = value;
        }
        else if (info == "Tale")
        {
            Tale = value;
        }
        else if (info == "Property")
        {
            Property= value; 
        }
        else if (info == "Information")
        {
             Information = value;
        }
        else if (info == "isLock")
        {
             isLock = value;
        }
        else if (info == "ImageName")
        {
             ImageName = value;
        }
        else if (info == "Money")
        {
             Money = value;
        }
        else if (info=="Level")
        {
            Level = value;
        }
        else if (info == "Deco")
        {
            Deco = value;
        }
    }       //정보 넣기
    public Sprite GetSprite()
    {
        return Image;
    }
}