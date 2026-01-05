using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LineupEditorController : MonoBehaviour
{
    // Prefab for player info
    public GameObject shortPlayerInfo;

    public GameObject lineupList;
    private GameObject[] lineupIndex;
    private Player[] lineupPlayers;
    
    public GameObject rosterList;
    private List<GameObject> rosterIndex;

    public MasterController masterController;
    private Team playerTeam;


    public void Start()
    {
        lineupIndex = new GameObject[9];
        lineupPlayers = new Player[9];
        createLineupList();
        rosterIndex = new List<GameObject>();

        playerTeam = masterController.playerTeam;


    }

    // Link the lineup gameobjects to our created list
    public void createLineupList()
    {
        for (int i = 0; i < lineupIndex.Length; i++)
        {
            lineupIndex[i] = transform.GetChild(i).gameObject;
        }
    }

    // Fill the lineup info with the current team's lineup
    public void fillStartingLineup()
    {
        int i = 0;
        foreach(Player player in playerTeam.getLineup())
        {
            PlayerDataController dataCon = getPlayerInfo(lineupIndex[i]).GetComponent<PlayerDataController>();

            dataCon.updateShortText(player.number, player.name, player.getPosition(), player.getOverall(), player.getCareerBA(), player.getCareerSO());

            lineupPlayers[i] = player;
            i++;
        }
    }

    // Get the reference to the playerInfo gameobject
    public GameObject getPlayerInfo(GameObject parent)
    {
        return parent.transform.GetChild(2).GetChild(0).gameObject;
    }

    // Player has finished editing their lineup and now update the lineup in the team
    public void updateLineupInfoToTeam()
    {
        List<Player> newLineup = new List<Player>();

        for(int i = 0; i < lineupPlayers.Length;i++)
        {
            newLineup.Add(lineupPlayers[i]);
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


        }
    }


}
