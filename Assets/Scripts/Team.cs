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
    public string collegeName;
    public string teamName;
    public string teamLocation;
    public int wins;
    public int losses;
    public int rosterSize;

    public Conference conference;
    public Team rival;
    public Team oocRival;

    private int startingPitcherIndex;
    private int lineupIndex;
    private List<int> validNumbers;

    public Team(string collegeName, string teamName, string teamLocation)
    {
        this.collegeName = collegeName;
        this.teamName = teamName;
        this.teamLocation = teamLocation;
        positionPlayers = new List<Player>();
        startingPitchers = new List<Player>();
        reliefPitchers = new List<Player>();
        lineup = new List<Player>();
        startingPitcherIndex = 0;
        wins = 0;
        losses = 0;
        // 34 is the current roster cap on NCAA baseball teams
        rosterSize = 34;

        //this.rival = rival;
        //this.oocRival = oocRival;

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

    public int getPositionPlayerCount()
    {
        int count = 0;

        foreach (Player player in getRoster())
        {
            if (player.playerPos == Player.position.SP || player.playerPos == Player.position.CP)
                continue;

            count++;
        }

        return count;
    }

    public void updateLineup(List<Player> lineup)
    {
        this.lineup = lineup;
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


    public int getGamesPlayed()
    {
        return wins + losses;
    }

    public int getTotalOffABs()
    {
        int overall = 0;

        foreach (Player player in positionPlayers)
        {
            overall += player.getCareerPA();
        }

        return overall;
    }

    public int getTotalOffHits()
    {
        int overall = 0;

        foreach (Player player in positionPlayers)
        {
            overall += player.getCareerHits();
        }

        return overall;
    }

    public float getTotalOffOBP()
    {
        float overall = 0f;
        int playerCount = 0;

        foreach (Player player in positionPlayers)
        {
            overall += player.getCareerOBP();
            playerCount++;
        }

        return overall / (float)playerCount;
    }

    public float getTotalOffBA()
    {
        float overall = 0f;
        int playerCount = 0;

        foreach (Player player in positionPlayers)
        {
            overall += player.getCareerBA();
            playerCount++;
        }

        return overall / (float)playerCount;
    }

    public float getTotalOffSLG()
    {
        float overall = 0f;
        int playerCount = 0;

        foreach (Player player in positionPlayers)
        {
            overall += player.getCareerSlugging();
            playerCount++;
        }

        return overall / (float)playerCount;
    }

    public int getTotalOffSO()
    {
        int overall = 0;

        foreach (Player player in positionPlayers)
        {
            overall += player.getCareerSO();
        }

        return overall;
    }

    public int getTotalOffBB()
    {
        int overall = 0;

        foreach (Player player in positionPlayers)
        {
            overall += player.getCareerWalks();
        }

        return overall;
    }

    public int getTotalOffDoubles()
    {
        int overall = 0;

        foreach (Player player in positionPlayers)
        {
            overall += player.getCareerDoubles();
        }

        return overall;
    }

    public int getTotalOffTriples()
    {
        int overall = 0;

        foreach (Player player in positionPlayers)
        {
            overall += player.getCareerTriples();
        }

        return overall;
    }

    public int getTotalOffHomeRuns()
    {
        int overall = 0;

        foreach (Player player in positionPlayers)
        {
            overall += player.getCareerHomeRuns();
        }

        return overall;
    }

    public int getTotalOffRBIs()
    {
        int overall = 0;

        foreach (Player player in positionPlayers)
        {
            overall += player.getCareerRBI();
        }

        return overall;
    }

    public int getTotalOffTB()
    {
        int overall = 0;

        foreach (Player player in positionPlayers)
        {
            overall += player.getCareerTotalBases();
        }

        return overall;
    }

    public int getTotalOffRuns()
    {
        int overall = 0;

        foreach (Player player in positionPlayers)
        {
            overall += player.getCareerRuns();
        }

        return overall;
    }




    public int getTotalDefABs()
    {
        int overall = 0;

        foreach (Player player in startingPitchers)
        {
            overall += player.getCareerPA();
        }
        foreach (Player player in reliefPitchers)
        {
            overall += player.getCareerPA();
        }

        return overall;
    }

    public int getTotalDefHits()
    {
        int overall = 0;

        foreach (Player player in startingPitchers)
        {
            overall += player.getCareerHits();
        }
        foreach (Player player in reliefPitchers)
        {
            overall += player.getCareerHits();
        }

        return overall;
    }

    public float getTotalDefOBP()
    {
        float overall = 0f;
        int playerCount = 0;

        foreach (Player player in startingPitchers)
        {
            overall += player.getCareerOBP();
            playerCount++;
        }
        foreach (Player player in reliefPitchers)
        {
            overall += player.getCareerOBP();
            playerCount++;
        }

        return overall / (float)playerCount;
    }

    public float getTotalDefBA()
    {
        float overall = 0f;
        int playerCount = 0;

        foreach (Player player in startingPitchers)
        {
            overall += player.getCareerBA();
            playerCount++;
        }
        foreach (Player player in reliefPitchers)
        {
            overall += player.getCareerBA();
            playerCount++;
        }

        return overall / (float)playerCount;
    }

    public float getTotalDefSLG()
    {
        float overall = 0f;
        int playerCount = 0;

        foreach (Player player in startingPitchers)
        {
            overall += player.getCareerSlugging();
            playerCount++;
        }
        foreach (Player player in reliefPitchers)
        {
            overall += player.getCareerSlugging();
            playerCount++;
        }

        return overall / (float)playerCount;
    }

    public int getTotalDefSO()
    {
        int overall = 0;

        foreach (Player player in startingPitchers)
        {
            overall += player.getCareerSO();
        }
        foreach (Player player in reliefPitchers)
        {
            overall += player.getCareerSO();
        }

        return overall;
    }

    public int getTotalDefBB()
    {
        int overall = 0;

        foreach (Player player in startingPitchers)
        {
            overall += player.getCareerWalks();
        }
        foreach (Player player in reliefPitchers)
        {
            overall += player.getCareerWalks();
        }

        return overall;
    }

    public int getTotalDefDoubles()
    {
        int overall = 0;

        foreach (Player player in startingPitchers)
        {
            overall += player.getCareerDoubles();
        }
        foreach (Player player in reliefPitchers)
        {
            overall += player.getCareerDoubles();
        }

        return overall;
    }

    public int getTotalDefTriples()
    {
        int overall = 0;

        foreach (Player player in startingPitchers)
        {
            overall += player.getCareerTriples();
        }
        foreach (Player player in reliefPitchers)
        {
            overall += player.getCareerTriples();
        }

        return overall;
    }

    public int getTotalDefHomeRuns()
    {
        int overall = 0;

        foreach (Player player in startingPitchers)
        {
            overall += player.getCareerHomeRuns();
        }
        foreach (Player player in reliefPitchers)
        {
            overall += player.getCareerHomeRuns();
        }

        return overall;
    }

    public int getTotalDefRBIs()
    {
        int overall = 0;

        foreach (Player player in startingPitchers)
        {
            overall += player.getCareerRBI();
        }
        foreach (Player player in reliefPitchers)
        {
            overall += player.getCareerRBI();
        }

        return overall;
    }

    public int getTotalDefTB()
    {
        int overall = 0;

        foreach (Player player in startingPitchers)
        {
            overall += player.getCareerTotalBases();
        }
        foreach (Player player in reliefPitchers)
        {
            overall += player.getCareerTotalBases();
        }

        return overall;
    }

    public int getTotalDefRuns()
    {
        int overall = 0;

        foreach (Player player in startingPitchers)
        {
            overall += player.getCareerRuns();
        }
        foreach (Player player in reliefPitchers)
        {
            overall += player.getCareerRuns();
        }

        return overall;
    }

    public float getDefERA()
    {
        if (getGamesPlayed() == 0)
            return 0;

        return getTotalDefRuns() / (9 * getGamesPlayed());
    }
}
