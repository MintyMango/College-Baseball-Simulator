using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.UI;

public class PitcherRotationController: MonoBehaviour
{
    // Prefab for player info
    public GameObject lineupEntry;
    public GameObject shortPitcherInfo;


    // Reference to the Scroll Rect that will hold the lineup players
    public GameObject lineupList;
    // List of the lineupEntry prefabs
    private List<GameObject> lineupIndex;
    

    public MasterController masterController;
    public PlayerInfoCanvasController playerController;
    private Team playerTeam;


    public void activateCanvas()
    {

        lineupIndex = new List<GameObject>();

        playerTeam = masterController.playerTeam;


        initializePitcherIndex();

        fillPitcherRotation();

        this.transform.gameObject.SetActive(true);
    }

    public void initializePitcherIndex()
    {
        for(int i = 0; i < playerTeam.getStartingPitchers().Count; i++)
        {
            // if the roster spot has not been initialized then initialize it
            if (lineupList.transform.childCount != i + 1)
            {
                GameObject pitchTemp = GameObject.Instantiate(lineupEntry, lineupList.transform);

                pitchTemp.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                pitchTemp.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);

                lineupIndex.Add(pitchTemp);

                pitchTemp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = i + 1 + ":";

                GameObject pitcherInfo = GameObject.Instantiate(shortPitcherInfo, pitchTemp.transform.GetChild(1));
            }
        }
    }

    // Fill the lineup info with the current team's lineup
    public void fillPitcherRotation()
    {
        int i = 0;
        foreach(Player player in playerTeam.getStartingPitchers())
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
    public void updateRotationInfoToTeam()
    {
        List<Player> newRotation = new List<Player>();

        // Loop through the pitcher rotation and update 
        for (int i = 0; i < lineupIndex.Count;i++)
        {
            if (lineupIndex[i].transform.GetChild(1).childCount == 0)
                continue;

            PlayerDataController temp = getPlayerInfo(lineupIndex[i]).GetComponent<PlayerDataController>();

            newRotation.Add(temp.player);
        }

        playerTeam.updateStartingRotation(newRotation);
        this.transform.gameObject.SetActive(false);
    }


    public void updateInfo(GameObject playerInfo, Player player)
    {
        PlayerDataController dataCon = getPlayerInfo(playerInfo).GetComponent<PlayerDataController>();
        dataCon.setPlayer(player);
        dataCon.updateShortText(player.number, player.name, player.getPosition(), player.getOverall(), player.getCareerBA(), player.getCareerSO());
        playerInfo.GetComponentInChildren<Button>().onClick.AddListener(() => playerController.displayPlayer(player));
    }

}
