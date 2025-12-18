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
        Player tempPlayer = startingPitchers[startingPitcherIndex];
        startingPitcherIndex++;
        return tempPlayer;
    }

    public Player getReliefPitcher()
    {
        return reliefPitchers[UnityEngine.Random.Range(0, reliefPitchers.Count)];
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

    public string getFourtyManRoster()
    {
        string roster = "";

        foreach(Player player in positionPlayers)
        {
            roster += player.name + " (" + player.number + ")  - " + player.playerPos + "\n";
        }

        foreach(Player player in startingPitchers)
        {
            roster += player.name + " (" + player.number + ")  - " + player.playerPos + "\n";
        }

        foreach(Player player in reliefPitchers)
        {
            roster += player.name + " (" + player.number + ")  - " + player.playerPos + "\n";
        }

        return roster;
    }
}
