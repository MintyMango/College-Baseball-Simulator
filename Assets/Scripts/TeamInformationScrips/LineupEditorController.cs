using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineupEditorController : MonoBehaviour
{
    // Prefab for player info
    public GameObject rosterEntry;
    public GameObject lineupEntry;

    // Reference to the Scroll Rect that will hold the lineup players
    public GameObject lineupList;
    // List of the lineupEntry prefabs
    private GameObject[] lineupIndex;
    
    // Reference to the Scroll Rect that will hold the roster players
    public GameObject rosterList;
    // List of the rosterEntry prefabs
    private List<GameObject> rosterIndex;

    public MasterController masterController;
    public PlayerInfoCanvasController playerController;
    private Team playerTeam;


    public void Start()
    {
        lineupIndex = new GameObject[9];
        rosterIndex = new List<GameObject>();

        playerTeam = masterController.playerTeam;

        
        createRosterList();

        fillStartingLineup();
        fillRoster();
    }

    public void createRosterList()
    {
        for (int i = 0; i < playerTeam.rosterSize; i++)
        {
            GameObject temp = GameObject.Instantiate(rosterEntry, rosterList.transform);

            temp.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            temp.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            rosterIndex.Add(temp);
        }
    }

    // Fill the lineup info with the current team's lineup
    public void fillStartingLineup()
    {
        int i = 0;
        foreach(Player player in playerTeam.getLineup())
        {
            updateInfo(lineupIndex[i], player);

            i++;
        }
    }

    // Get the reference to the playerInfo gameobject
    public GameObject getPlayerInfo(GameObject parent)
    {
        return parent.transform.GetChild(1).GetChild(0).gameObject;
    }

    // Player has finished editing their lineup and now update the lineup in the team
    public void updateLineupInfoToTeam()
    {
        List<Player> newLineup = new List<Player>();

        // Loop through the 9 lineup slots and grab the player reference to build a new lineup
        for (int i = 0; i < lineupIndex.Length;i++)
        {
            PlayerDataController temp = getPlayerInfo(lineupIndex[i]).GetComponent<PlayerDataController>();

            newLineup.Add(temp.player);
        }

        playerTeam.updateLineup(newLineup);
    }

    // Fill the Roster spots so the player can choose who they want on the lineup
    public void fillRoster()
    {
        int i = 0;
        foreach (Player player in playerTeam.getRoster())
        {
            if (player.playerPos == Player.position.SP || player.playerPos == Player.position.CP)
                continue;

            updateInfo(rosterIndex[i], player);
            i++;
        }

        // Adjust the height of the list to fit the roster info
        rosterList.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 150 * i + 50);
    }

    public void updateInfo(GameObject playerInfo, Player player)
    {
        PlayerDataController dataCon = getPlayerInfo(playerInfo).GetComponent<PlayerDataController>();
        dataCon.setPlayer(player);
        dataCon.updateShortText(player.number, player.name, player.getPosition(), player.getOverall(), player.getCareerBA(), player.getCareerSO());
        playerInfo.GetComponentInChildren<Button>().onClick.AddListener(() => playerController.displayPlayer(player));
    }

}
