using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchieveScroll : MonoBehaviour
{
    public Text AchieveName;
    //public Text AchieveContext;
    public Text AchieveCount;

    public List<Text> CountText;
    public Slider CountSlider;

    public Button RewardBtn;
    public Image RewardImage;

    public Sprite IStoneSprite,StoneSprite,ZemSprite;
    // Start is called before the first frame update
    private void Start()
    {
            
    }
    public void SetData(AchieveInfo Info,int index)
    {
        AchieveName.text = Info.AchieveName;
        //AchieveContext.text = Info.Context;
        AchieveCount.text = index.ToString() + "/"+ Info.Count[index].ToString();          //내 업적 카운트/총 카운트

        CountSlider.maxValue = int.Parse(Info.Count[index]);//총 카운트
        CountSlider.minValue= 0;//내 업적 카운트

        switch (Info.RewardType[index])
        {
            case "Money":
                RewardImage.sprite = StoneSprite;
                break;
            case "ShinMoney":
                RewardImage.sprite = IStoneSprite;
                break;
            case "Zem":
                RewardImage.sprite = ZemSprite;
                break;

            default:
                break;
        }

       /* for (int i = 0; i < CountText.Count; i++)
        {
            switch (Info.RewardType[i])
            {
                case "Money":
                    CountText[i].text = "얼음" + System.Environment.NewLine + "+" + Info.Reward[i];
                    break;
                case "ShinMoney":
                    CountText[i].text = "빛나는 얼음" + System.Environment.NewLine + "+" + Info.Reward[i];
                    break;
                case "Zem":
                    CountText[i].text = "잼" + System.Environment.NewLine + "+" + Info.Reward[i];
                    break;

                default:
                    break;
            }
        }*/
  
    }

}
