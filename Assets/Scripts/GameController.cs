using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameSequence currGame;

    // GUI Things
    public TextMeshProUGUI inningDisplay;
    public GameObject firstBase;
    public GameObject secondBase;
    public GameObject thirdBase;
    public GameObject topInningArrow;
    public GameObject bottomInningArrow;
    public TextMeshProUGUI inningCount;
    public TextMeshProUGUI scoreText;

    public GameObject playButtons;
    public GameObject backToMenu;

    private bool playerGame;
    private bool inSimulation;
    private Team homeTeam;
    private Team awayTeam;

    public void startGame(Team homeTeam,  Team awayTeam, bool playerGame)
    {
        this.playerGame = playerGame;
        inSimulation = false;

        

        this.homeTeam = homeTeam;
        this.awayTeam = awayTeam;

        currGame = new GameSequence(homeTeam, awayTeam);

        if (playerGame)
        {
            if (inningDisplay != null)
                inningDisplay.text = "";

            playButtons.SetActive(true);
            backToMenu.SetActive(false);
            updateBaseUI();

        }
        else
        {
            simulateGame();
        }

    }



    public void endGame()
    {
        if (playerGame)
        {
            inningDisplay.text = "Game over!\n";

            if (currGame.getHomeScore() > currGame.getAwayScore())
            {
                inningDisplay.text += homeTeam.teamName + " beat " + awayTeam.teamName + " " + currGame.getHomeScore() + " - " + currGame.getAwayScore() + "!\n";
                homeTeam.wins++;
                awayTeam.losses++;
            }
            else
            {
                inningDisplay.text += awayTeam.teamName + " beat " + homeTeam.teamName + " " + currGame.getAwayScore() + " - " + currGame.getHomeScore() + "!\n";
                homeTeam.losses++;
                awayTeam.wins++;
            }


            inningDisplay.text += "Home Pitcher SOs: " + currGame.getHomePitcher().getGameOuts() + "\n";
            inningDisplay.text += "Away Pitcher SOs: " + currGame.getAwayPitcher().getGameOuts() + "\n";


            inningDisplay.text += formatMVPText(currGame.getMVP(homeTeam), homeTeam) + "\n";
            inningDisplay.text += formatMVPText(currGame.getMVP(awayTeam), awayTeam) + "\n";

            playButtons.SetActive(false);
            backToMenu.SetActive(true);
        }
        else
        {
            if (currGame.getHomeScore() > currGame.getAwayScore())
            {
                Debug.Log("Home Team wins!");
                homeTeam.wins++;
                awayTeam.losses++;
            }
            else
            {
                Debug.Log("Away Team wins!");
                homeTeam.losses++;
                awayTeam.wins++;
            }

            currGame.getMVP(homeTeam);
            currGame.getMVP(awayTeam);
        }

        homeTeam.endGame();
        awayTeam.endGame();

        currGame = null;
    }

    // TODO: Game is not ending early enough when away team is winning (still simulates the top half of the 10th)
    public bool isGameOver()
    {
        // Game is in the 9th inning or later
        if (currGame.getInning() >= 9)
        {
            // Non walk off win for Home Team
            if (currGame.getHomeScore() > currGame.getAwayScore() && !currGame.getTopofInning())
                return true;
            // Walk off win or Away team wins
            else
            {
                // Away Team wins
                if (currGame.getTopofInning() && currGame.getAwayScore() > currGame.getHomeScore() && currGame.getInning() != 9)
                    return true;
                // Walk off win
                else if (currGame.getHomeScore() > currGame.getAwayScore())
                    return true;
            }
        }

        return false;
    }

    public void simulateGame()
    {

        while (!isGameOver())
        {
            if (!inSimulation)
            {
                clearUI();
            }

            currGame.simmulateInningHalf();
        }

        inSimulation = false;
        updateBaseUI();
        endGame();
    }

    // Simulate the full half inning at once
    public void nextHalfInning()
    {
        string inningString = "";

        if (isGameOver())
        {
            endGame();
        }
        else
        {
            if (!inSimulation)
            {
                clearUI();
            }

            if (playerGame)
            {
                inningString += currGame.simmulateInningHalf();

                inningDisplay.text += inningString;
            }
            else
            {
                currGame.simmulateInningHalf();
            }

            inSimulation = false;
            updateBaseUI();
        }
    }

    // Simulate the next action in the inning
    public void simulateNextStep()
    {
        if (isGameOver())
        {
            endGame();
        }
        else
        {
            if (!inSimulation)
            {
                clearUI();
                inSimulation = true;
            }

            inningDisplay.text += currGame.simmulateNextBatter();
            updateBaseUI();

            // If 3 outs is reached then we have ended the inning and the simulation
            if (currGame.inbetweenInnings)
            {
                inSimulation = false;
            }
        }
    }

    public void updateBaseUI()
    {
        if (currGame.getTopofInning())
        {
            inningCount.text = currGame.getInning().ToString();
            bottomInningArrow.gameObject.SetActive(false);
            topInningArrow.gameObject.SetActive(true);
        }
        else
        {
            inningCount.text = currGame.getInning().ToString();
            bottomInningArrow.gameObject.SetActive(true);
            topInningArrow.gameObject.SetActive(false);
        }

        Player[] baserunners = currGame.getBaserunners();

        firstBase.GetComponent<SpriteRenderer>().color = Color.white;
        secondBase.GetComponent<SpriteRenderer>().color = Color.white;
        thirdBase.GetComponent<SpriteRenderer>().color = Color.white;

        if (baserunners[1] != null)
        {
            firstBase.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (baserunners[2] != null)
        {
            secondBase.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (baserunners[3] != null)
        {
            thirdBase.GetComponent<SpriteRenderer>().color = Color.yellow;
        }

        scoreText.text = currGame.getHomeScore() + " - " + currGame.getAwayScore();

    }

    public void clearUI()
    {
        inningDisplay.text = "";
    }

    public string formatMVPText(Player mvp, Team team)
    {
        string mvpText = team.teamName + "'s MVP: " + mvp.name + ": \n";
            
        mvpText += mvp.getGameHits() + " - " + (mvp.getGamePAs() - mvp.getGameWalks());

        if (mvp.getGameSingles() > 0)
        {
            mvpText += ", " + mvp.getGameSingles() + " 1B";
        }
        if (mvp.getGameDoubles() > 0)
        {
            mvpText += ", " + mvp.getGameDoubles() + " 2B";
        }
        if (mvp.getGameTriples() > 0)
        {
            mvpText += ", " + mvp.getGameTriples() + " 3B";
        }
        if (mvp.getGameHomeRuns() > 0)
        {
            mvpText += ", " + mvp.getGameHomeRuns() + " HR";
        }
        if (mvp.getGameWalks() > 0)
        {
            mvpText += ", " + mvp.getGameWalks() + " BB";
        }
        if (mvp.getGameRBI() > 0)
        {
            mvpText += ", " + mvp.getGameRBI() + " RBI";
        }
        if (mvp.getGameRuns() > 0)
        {
            mvpText += ", " + mvp.getGameRuns() + " R";
        }

        mvpText = mvpText.Replace("\r", "");

        return mvpText;
    }
}
