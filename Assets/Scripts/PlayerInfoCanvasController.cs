using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoCanvasController : MonoBehaviour
{
    public MasterController masterController;
    public GameObject playerEntry;

    private Team playerTeam;

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



    void Start()
    {
        playerTeam = masterController.playerTeam;
        populateList(playerTeam);
    }

    private void OnEnable()
    {
        // Update Player Info
        foreach (PlayerDataController child in this.GetComponentsInChildren<PlayerDataController>())
            child.updateText();
    }

    public void populateList(Team team)
    {
        float offset = (this.GetComponent<RectTransform>().rect.height / 2);
        int runningCount = 1;

        foreach (Player player in team.getFourtyManRoster())
        {

            GameObject temp = Instantiate(playerEntry, this.transform);
            
            RectTransform rt = temp.GetComponent<RectTransform>();

            offset -= temp.GetComponent<RectTransform>().rect.height;

            rt.localPosition = new Vector2(0, offset);

            temp.GetComponent<PlayerDataController>().updateText(runningCount, player.name, player.playerPos, player.getOverall(), player.getGamesPlayed(), 
                player.getCareerBA(), player.getCareerWalks(), player.getCareerSO(), player.getCareerRuns(),
                team.lineup.Contains(player));
            runningCount++;

            temp.GetComponent<PlayerDataController>().player = player;

            temp.GetComponentInChildren<Button>().onClick.AddListener(() => displayPlayer(player));
        }
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

}
