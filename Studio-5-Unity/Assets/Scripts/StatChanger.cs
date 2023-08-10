using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatChanger : MonoBehaviour
{
    public TMP_Text statText; //Stat that will be increased/decreased - eg; Strength
    public TMP_Text remainingText; //Skill points remaining to spend

    //Increments the stat value and decrements the points available to spend
    public void increaseStat()
    {
        int remainPoints;
        int stats;

        int.TryParse(remainingText.text, out remainPoints);
        int.TryParse(statText.text, out stats);

        if (remainPoints > 0)
        {
            stats++;
            statText.text = stats.ToString();
            remainPoints--;
            remainingText.text = remainPoints.ToString();
        }
    }

    //Decrements the stat value and increments the points available to spend
    public void decreaseStat()
    {
        int remainPoints;
        int stats;

        int.TryParse(remainingText.text, out remainPoints);
        int.TryParse(statText.text, out stats);

        if (stats > 0)
        {
            stats--;
            statText.text = stats.ToString();
            remainPoints++;
            remainingText.text = remainPoints.ToString();
        }
    }
}
