using TMPro;
using UnityEngine;

public class PlayerDataController: MonoBehaviour
{
    public TextMeshProUGUI numberText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI posText;
    public TextMeshProUGUI overallText;
    public TextMeshProUGUI gamesPlayedText;
    public TextMeshProUGUI battingAverageText;
    public TextMeshProUGUI walkAverageText;
    public TextMeshProUGUI strikeOutsText;
    public TextMeshProUGUI putOutsText;
    public TextMeshProUGUI inLineupText;

    public void updateText(int num, string playerName, Player.position position, int overall, int gamesPlayed, float battingAverage, float walkAverage, int strikeOuts, int putOuts, bool inLineup)
    {
        numberText.text = num.ToString();
        nameText.text = playerName;
        posText.text = position.ToString();
        overallText.text = overall.ToString();
        gamesPlayedText.text = gamesPlayed.ToString();
        battingAverageText.text = battingAverage.ToString();
        walkAverageText.text = walkAverage.ToString();
        strikeOutsText.text = strikeOuts.ToString();
        putOutsText.text = putOuts.ToString();
        inLineupText.text = inLineup.ToString();
    }

}
