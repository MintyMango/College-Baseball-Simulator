using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoCanvasController : MonoBehaviour
{
    public MasterController masterController;
    public TeamStatController teamStatController;
    public GameObject playerEntry;

    private bool listFilled = false;
    private Team playerTeam;

    // Team Info GUI
    public TextMeshProUGUI collegeName;
    public TextMeshProUGUI teamName;
    public TextMeshProUGUI overallRating;
    public TextMeshProUGUI offenseRating;
    public TextMeshProUGUI defenseRating;



    // Player Info GUI Variables
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI strength;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI eyes;
    public TextMeshProUGUI fielding;

    public TextMeshProUGUI cGP;
    public TextMeshProUGUI cPA;
    public TextMeshProUGUI cHits;
    public TextMeshProUGUI cOBP;
    public TextMeshProUGUI cBA;
    public TextMeshProUGUI cSLG;
    public TextMeshProUGUI cSO;
    public TextMeshProUGUI cPO;
    public TextMeshProUGUI cBB;
    public TextMeshProUGUI cDB;
    public TextMeshProUGUI cHR;
    public TextMeshProUGUI cTP;
    public TextMeshProUGUI cRBI;
    public TextMeshProUGUI cTB;

    public TextMeshProUGUI gPA;
    public TextMeshProUGUI gHits;
    public TextMeshProUGUI gOBP;
    public TextMeshProUGUI gBA;
    public TextMeshProUGUI gSLG;
    public TextMeshProUGUI gSO;
    public TextMeshProUGUI gPO;
    public TextMeshProUGUI gBB;
    public TextMeshProUGUI gDB;
    public TextMeshProUGUI gHR;
    public TextMeshProUGUI gTP;
    public TextMeshProUGUI gRBI;
    public TextMeshProUGUI gTB;

    private void OnEnable()
    {
        playerTeam = masterController.playerTeam;
        if (!listFilled)
            populateList();

        // Update Player Info
        foreach (PlayerDataController child in this.GetComponentsInChildren<PlayerDataController>())
            child.updateText();

        setTeamInfo(playerTeam);
        teamStatController.updateTeamOffStats(playerTeam);
        teamStatController.updateTeamDefStats(playerTeam);
    }

    // Initial setup for the 34 roster spots
    public void populateList()
    {
        float offset = (this.GetComponent<RectTransform>().rect.height / 2);

        for (int i = 0; i < 34; i++)
        {
            GameObject temp = Instantiate(playerEntry, this.transform);
            
            RectTransform rt = temp.GetComponent<RectTransform>();

            offset -= temp.GetComponent<RectTransform>().rect.height;

            rt.localPosition = new Vector2(0, offset);
        }

        listFilled = true;
    }

    public void setTeamInfo(Team team)
    {
        collegeName.text = team.collegeName;
        teamName.text = team.teamName;
        overallRating.text = "Overall: " + team.getOverallRating();
        offenseRating.text = "Offense: " + team.getOffenseRating();
        defenseRating.text = "Defense: " + team.getDefenseRating();

    }

    public void fillTeamList()
    {
        if (!listFilled)
            populateList();

        Team team = masterController.playerTeam;
        for (int i = 0; i < team.rosterSize; i++)
        {

            Transform temp = this.transform.GetChild(i);
            temp.gameObject.SetActive(true);
            Player player = team.getRoster()[i];


            temp.GetComponent<PlayerDataController>().updateText(i, player.name, player.getPosition(), player.getOverall(), player.getGamesPlayed(),
                player.getCareerBA(), player.getCareerWalks(), player.getCareerSO(), player.getCareerRuns(),
                team.lineup.Contains(player));

            temp.GetComponent<PlayerDataController>().player = player;

            temp.GetComponentInChildren<Button>().onClick.AddListener(() => displayPlayer(player));

        }

        masterController.openTeamInfo();
    }

    public void displayPlayer(Player player)
    {
        masterController.openPlayerInfo();

        playerName.text = player.name;

        cGP.text = "G: " + player.getGamesPlayed();
        cPA.text = "AB: " + player.getCareerPA();
        cHits.text = "H: " + player.getCareerHits();
        cOBP.text = string.Format("OBP: {0:#.000}", player.getCareerOBP());
        cBA.text = string.Format("BA: {0:#.000}",player.getCareerBA());
        cSLG.text = string.Format("SLG: {0:#.000}", player.getCareerSlugging());
        cSO.text = "SO: " + player.getCareerStrikeOuts();
        cPO.text = "PO: " + player.getCareerOuts();
        cBB.text = "BB: " + player.getCareerWalks();
        cDB.text = "2B: " + player.getCareerDoubles();
        cTP.text = "3B: " + player.getCareerTriples();
        cHR.text = "HR: " + player.getCareerHomeRuns();
        cRBI.text = "RBI: " + player.getCareerRBI();
        cTB.text = "TB: " + player.getCareerTotalBases();

        gPA.text = "AB: " + player.getGamePAs();
        gHits.text = "H: " + player.getGameHits();
        gOBP.text = string.Format("OBP: {0:#.000}", player.getGameOBP());
        gBA.text = string.Format("BA: {0:#.000}", player.getGameBA());
        gSLG.text = string.Format("SLG: {0:#.000}", player.getGameSlugging());
        gSO.text = "SO: " + player.getGameSO();
        gPO.text = "PO: " + player.getGameOuts();
        gBB.text = "BB: " + player.getGameWalks();
        gDB.text = "2B: " + player.getGameDoubles();
        gTP.text = "3B: " + player.getGameTriples();
        gHR.text = "HR: " + player.getGameHomeRuns();
        gRBI.text = "RBI: " + player.getGameRBI();
        gTB.text = "TB: " + player.getGameTotalBases();

        strength.text = "Strength: " + player.strength;
        speed.text = "Speed: " + player.speed;
        eyes.text = "Eyes: " + player.eyes;
        fielding.text = "Fielding: " + player.fielding;
    }

    public void displayLineup()
    {
        if (!listFilled)
            populateList();

        Team team = masterController.playerTeam;
        masterController.teamInfo.SetActive(true);

        for (int i = 0; i < 34; i++)
        {
            Transform temp = this.transform.GetChild(i);

            if (i <= 9)
            {
                Player player = null;

                if (i == 9)
                    player = team.getStartingPitcher();
                else
                    player = team.lineup[i];

                temp.GetComponent<PlayerDataController>().updateText(player.number, player.name, player.getPosition(), player.getOverall(), player.getGamesPlayed(),
                   player.getCareerBA(), player.getCareerWalks(), player.getCareerSO(), player.getCareerRuns(),
                   team.lineup.Contains(player));

                temp.gameObject.SetActive(true);
            }
            else
            {
                temp.gameObject.SetActive(false);
            }
        }
    }
}
