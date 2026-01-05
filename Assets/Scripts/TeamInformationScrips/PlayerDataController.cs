using TMPro;
using UnityEngine;
using static Unity.Collections.Unicode;
using static UnityEngine.LowLevelPhysics2D.PhysicsLayers;

public class PlayerDataController: MonoBehaviour
{
    public TextMeshProUGUI numberText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI posText;
    public TextMeshProUGUI overallText;
    public TextMeshProUGUI gamesPlayedText;
    public TextMeshProUGUI battingAverageText;
    public TextMeshProUGUI walksText;
    public TextMeshProUGUI strikeOutsText;
    public TextMeshProUGUI runsText;
    public TextMeshProUGUI inLineupText;

    public Player player;


    public void updateText(int num, string playerName, string position, int overall, int gamesPlayed, float battingAverage, int walks, int strikeOuts, int runs, bool inLineup)
    {
        numberText.text = num.ToString();
        nameText.text = playerName;
        posText.text = position;
        overallText.text = overall.ToString();
        gamesPlayedText.text = gamesPlayed.ToString();
        battingAverageText.text = string.Format("{0:#.000}", battingAverage);
        walksText.text = walks.ToString();
        strikeOutsText.text = strikeOuts.ToString();
        runsText.text = runs.ToString();
    }

    public void updateText()
    {
        if (player != null)
        {
            numberText.text = player.number.ToString();
            nameText.text = player.name;
            posText.text = player.getPosition();
            overallText.text = player.getOverall().ToString();
            gamesPlayedText.text = player.getGamesPlayed().ToString();
            battingAverageText.text = string.Format("{0:#.000}", player.getCareerBA());
            walksText.text = player.getCareerWalks().ToString();
            strikeOutsText.text = player.getCareerSO().ToString();
            runsText.text = player.getCareerRuns().ToString();
        }
    }

    public void updateShortText(int num, string playerName, string position, int overall, float battingAverage, int strikeOuts)
    {
        numberText.text = num.ToString();
        nameText.text = playerName;
        posText.text = position;
        overallText.text = overall.ToString();
        battingAverageText.text = string.Format("{0:#.000}", battingAverage);
        strikeOutsText.text = strikeOuts.ToString();
    }

    public void updateShortText()
    {
        if (player != null)
        {
            numberText.text = player.number.ToString();
            nameText.text = player.name;
            posText.text = player.getPosition();
            overallText.text = player.getOverall().ToString();
            battingAverageText.text = string.Format("{0:#.000}", player.getCareerBA());
            strikeOutsText.text = player.getCareerSO().ToString();
        }
    }
}
