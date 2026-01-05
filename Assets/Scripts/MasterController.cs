using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MasterController : MonoBehaviour
{
    
    public GameController gameController;
    public PlayerGenerator playerGenerator;
    public TeamGenerator teamGenerator;
    public enum Conf { AAC, FPC, ACC, SSC }

    public TextMeshProUGUI oppInfoText;

    public CanvasController canvasController;
    public GameObject gameCanvas;
    public GameObject mainCanvas;
    public GameObject teamInfo;
    public GameObject playerInfo;
    public int seriesInASeason;

    public Team playerTeam;
    private Team oppTeam;
    private Scheduler scheduler;
    private List<Conference> conferences;
    // Keeps track of how many games we have done in a bo3/bo1
    private int currGameCount;

    // Index of what series we are on
    private int currSeriesNum;

    void Start()
    {
        teamGenerator = new TeamGenerator(new PlayerGenerator());
        conferences = teamGenerator.conferenceList;

        currGameCount = 0;
        currSeriesNum = 0;
        scheduler = new Scheduler(seriesInASeason, conferences);

        //test();

        // Player is just the first team in the first conference for now
        playerTeam = conferences[0].teams[0];

        scheduler.generateMatchups();

        findNextOpponent();
    }    

    public void test()
    {
 
        foreach (Team team in conferences[0].teams)
        {
            Debug.Log(team.teamName);
        }  
    }

    public void findNextOpponent()
    {
        oppTeam = scheduler.findNextOpponent(playerTeam, currSeriesNum);

        getOppInfo(oppTeam);
    }

    public void startNextGame()
    {
        gameController.startGame(playerTeam, oppTeam, true);
        canvasController.swapCanvas(mainCanvas, gameCanvas);
    }

    public void gameFinished()
    {
        simulateRestOfGames();

        currGameCount++;

        // We are in a conference game
        if (currSeriesNum % 2 == 0)
        {
            // If we finished the Bo3 find the next Opponent
            if (currGameCount == 3)
            {
                currSeriesNum++;
                currGameCount = 0;
                findNextOpponent();
            }
            // Else keep the same opponent
            else
            {
                getOppInfo(oppTeam);
            }
        }
        // OOC games are BO1s
        else
        {
            currSeriesNum++;
            currGameCount = 0;
            findNextOpponent();
        }

        canvasController.swapCanvas(gameCanvas, mainCanvas);
    }

    public void simulateRestOfGames()
    {
        List<(Team, Team)> games = scheduler.getNonPlayerMatchups(playerTeam, oppTeam, currSeriesNum);


        foreach ((Team, Team) game in games)
        {

            // If either team is null then a BYE happened
            if (game.Item1 == null || game.Item2 == null)
                continue;

            Debug.Log("Game Happn");

            gameController.startGame(game.Item1, game.Item2, false);
        }
    }

    public void getOppInfo(Team opp)
    {
        oppInfoText.text = "Next opponent: " + opp.collegeName + " (" + opp.wins + "-" + opp.losses + ")";
    }

    public void openTeamInfo()
    {
        teamInfo.SetActive(true);
    }

    public void closeTeamInfo()
    {
        teamInfo.SetActive(false);
    }

    public void openPlayerInfo()
    {
        playerInfo.SetActive(true);
    }

    public void closePlayerInfo()
    {
        playerInfo.SetActive(false);
    }
}
