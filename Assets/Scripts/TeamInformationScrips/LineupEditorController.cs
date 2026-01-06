using NUnit.Framework;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.UI;

public class LineupEditorController : MonoBehaviour
{
    // Prefab for player info
    public GameObject rosterEntry;
    public GameObject lineupEntry;
    public GameObject shortPlayerInfo;


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

    private bool opened = false;

    public void activateCanvas()
    {

        if (!opened)
        {
            lineupIndex = new GameObject[9];
            rosterIndex = new List<GameObject>();

            playerTeam = masterController.playerTeam;


            initializeLineupIndex();
            createRosterList();

            opened = true;

        }

        fillStartingLineup();

        fillRoster();

        this.transform.gameObject.SetActive(true);
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

    public void initializeLineupIndex()
    {
        for(int i = 0; i < lineupIndex.Length; i++)
        {
  
            lineupIndex[i] = lineupList.transform.GetChild(i).gameObject;
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
            if (lineupIndex[i].transform.GetChild(1).childCount == 0)
                continue;

            PlayerDataController temp = getPlayerInfo(lineupIndex[i]).GetComponent<PlayerDataController>();

            newLineup.Add(temp.player);
        }

        playerTeam.updateLineup(newLineup);
        this.transform.gameObject.SetActive(false);
    }

    // Fill the Roster spots so the player can choose who they want on the lineup
    public void fillRoster()
    {
        int i = 0;
        foreach (Player player in playerTeam.getRoster())
        {
            if (player.playerPos == Player.position.SP || player.playerPos == Player.position.CP || player.playerPos == Player.position.RP)
                continue;

            if (rosterIndex[i].transform.GetChild(1).childCount == 0)
            {
                GameObject temp = GameObject.Instantiate(shortPlayerInfo, rosterIndex[i].transform.GetChild(1).transform);

                temp.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                temp.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            }

            updateInfo(rosterIndex[i], player);

            i++;
        }

        // Adjust the height of the list to fit the roster info
        rosterList.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 150 * i + 250);
    }

    public void updateInfo(GameObject playerInfo, Player player)
    {
        PlayerDataController dataCon = getPlayerInfo(playerInfo).GetComponent<PlayerDataController>();
        dataCon.setPlayer(player);
        dataCon.updateShortText(player.number, player.name, player.getPosition(), player.getOverall(), player.getCareerBA(), player.getCareerSO());
        playerInfo.GetComponentInChildren<Button>().onClick.AddListener(() => playerController.displayPlayer(player));
    }

}
