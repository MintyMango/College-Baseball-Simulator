using TMPro;
using UnityEngine;
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


    public void updateText(int num, string playerName, Player.position position, int overall, int gamesPlayed, float battingAverage, int walks, int strikeOuts, int runs, bool inLineup)
    {
        numberText.text = num.ToString();
        nameText.text = playerName;
        posText.text = position.ToString();
        overallText.text = overall.ToString();
        gamesPlayedText.text = gamesPlayed.ToString();
        battingAverageText.text = string.Format("{0:#.000}", battingAverage);
        walksText.text = walks.ToString();
        strikeOutsText.text = strikeOuts.ToString();
        runsText.text = runs.ToString();
    }

    public void updateText()
    {
        numberText.text = player.number.ToString();
        nameText.text = player.name;
        posText.text = player.playerPos.ToString();
        overallText.text = player.getOverall().ToString();
        gamesPlayedText.text = player.getGamesPlayed().ToString();
        battingAverageText.text = string.Format("{0:#.000}", player.getCareerBA());
        walksText.text = player.getCareerWalks().ToString();
        strikeOutsText.text = player.getCareerSO().ToString();
        runsText.text = player.getCareerRuns().ToString();
    }

}
