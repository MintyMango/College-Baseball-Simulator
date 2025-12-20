using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    public List<Player> positionPlayers;
    public List<Player> startingPitchers;
    public List<Player> reliefPitchers;
    public List<Player> lineup;
    public string teamName;
    public string teamLocation;
    public int wins;
    public int losses;
    public int rosterSize;


    private int startingPitcherIndex;
    private int lineupIndex;
    private List<int> validNumbers;

    public Team(string teamName, string teamLocation)
    {
        this.teamName = teamName;
        this.teamLocation = teamLocation;
        positionPlayers = new List<Player>();
        startingPitchers = new List<Player>();
        reliefPitchers = new List<Player>();
        lineup = new List<Player>();
        startingPitcherIndex = 0;
        wins = 0;
        losses = 0;
        rosterSize = 34;

        validNumbers = new List<int>();
        for (int i = 0; i < 100; i++)
        {
            validNumbers.Add(i);
        }
    }


    public void addPlayer(Player player)
    {
        validNumbers.Remove(player.number);

        switch(player.playerPos)
        {
            case Player.position.SP:
                startingPitchers.Add(player); 
                break;
            case Player.position.RP:
                reliefPitchers.Add(player);
                break;
            default:
                positionPlayers.Add(player); 
                break;
        }
    }

    public List<Player> getLineup()
    {
        return lineup;
    }

    public List<int> getValidNumbers()
    {
        return validNumbers;
    }

    public Player getStartingPitcher()
    {
        return startingPitchers[startingPitcherIndex];
    }

    public Player getNextStartingPitcher()
    {
        if (startingPitchers.Count == startingPitcherIndex)
            startingPitcherIndex = 0;

        Player tempPlayer = startingPitchers[startingPitcherIndex];
        tempPlayer.startGame();
        startingPitcherIndex++;
        return tempPlayer;
    }

    public Player getReliefPitcher()
    {
        Player temp = reliefPitchers[UnityEngine.Random.Range(0, reliefPitchers.Count)];
        temp.startGame();
        return temp;
    }

    public void generateLineup()
    {
        List<Player.position> foundPositions = new List<Player.position>();

        foreach (Player player in positionPlayers)
        {
            if (!foundPositions.Contains(player.playerPos))
            {
                foundPositions.Add(player.playerPos);
                lineup.Add(player);
                player.startGame();
            }
        }
        
        /**
        foreach (Player.position pos in Enum.GetValues(typeof(Player.position)))
        {

        }
        **/
    }

    public void endGame()
    {
        foreach (Player player in lineup)
        {
            player.takenOutOfGame();
        }
    }

    public Player getUpToBat()
    {
        if (lineupIndex == lineup.Count)
            lineupIndex = 0;

        return lineup[lineupIndex++];
    }

    public Player[] getRoster()
    {
        Player[] roster = new Player[rosterSize];
        int tempIndex = 0;

        foreach(Player player in positionPlayers)
        {
            roster[tempIndex] = player;
            tempIndex++;
        }

        foreach(Player player in startingPitchers)
        {
            roster[tempIndex] = player;
            tempIndex++;
        }

        foreach (Player player in reliefPitchers)
        {
            roster[tempIndex] = player;
            tempIndex++;
        }

        return roster;
    }

    public int getOverallRating()
    {
        int overall = 0;
        int playerCount = 0;

        foreach (Player player in positionPlayers)
        {
            overall += player.getOverall();
            playerCount++;
        }

        foreach (Player player in startingPitchers)
        {
            overall += player.getOverall();
            playerCount++;
        }

        foreach (Player player in reliefPitchers)
        {
            overall += player.getOverall();
            playerCount++;
        }

        return overall / playerCount;
    }

    public int getOffenseRating()
    {
        int overall = 0;
        int playerCount = 0;

        foreach (Player player in positionPlayers)
        {
            overall += player.getOffenseRating();
            playerCount++;
        }

        return overall / playerCount;
    }

    public int getDefenseRating()
    {
        int overall = 0;
        int playerCount = 0;

        foreach (Player player in positionPlayers)
        {
            overall += player.getDefenseRating();
            playerCount++;
        }

        foreach (Player player in startingPitchers)
        {
            overall += player.getDefenseRating();
            playerCount++;
        }

        foreach (Player player in reliefPitchers)
        {
            overall += player.getDefenseRating();
            playerCount++;
        }

        return overall / playerCount;
    }
}
