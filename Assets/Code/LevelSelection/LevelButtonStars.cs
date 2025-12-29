using UnityEngine;
using UnityEngine.UI;

public class LevelButtonStars : MonoBehaviour
{
    public int levelIndex;
    public Image[] stars;
    public Sprite grayStar;
    public Sprite goldStar;

    void Start()
    {
        int earnedStars = PlayerPrefs.GetInt("Level_Stars_" + levelIndex, 0);

        for (int i = 0; i < stars.Length; i++)
        {
            if (i < earnedStars)
                stars[i].sprite = goldStar;
            else
                stars[i].sprite = grayStar;
        }
    }
}
