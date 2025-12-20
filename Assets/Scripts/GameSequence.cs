using System.Collections.Generic;
using UnityEngine;

public class GameSequence
{
    public enum batResult {Walk, StrikeOut, HitOut, Single, Double, Triple, HomeRun};

    public Team homeTeam;
    public Team awayTeam;
    public int homeRuns;
    public int awayRuns;
    public int inning;
    public bool topOfInning;
    public bool inbetweenInnings;

    private Player homePitcher;
    private Player awayPitcher;
    // 0 -> Home, 1 -> 1st, 2 -> 2nd, 3 -> 3rd
    private Player[] baseRunners;
    private int outCount;
    private string inningResults;

    public GameSequence(Team homeTeam, Team awayTeam)
    {
        this.homeTeam = homeTeam;
        this.awayTeam = awayTeam;
        homeRuns = 0;
        awayRuns = 0;
        inning = 1;
        topOfInning = true;
        inbetweenInnings = true;
        outCount = 0;
        inningResults = "";

        homePitcher = homeTeam.getNextStartingPitcher();
        awayPitcher = awayTeam.getNextStartingPitcher();

        homeTeam.generateLineup();
        awayTeam.generateLineup();

        baseRunners = new Player[4];
    }

    public int getInning()
    {
        return inning;
    }

    public bool getTopofInning()
    {
        return topOfInning;
    }

    public int getHomeScore()
    {
        return homeRuns;
    }

    public int getAwayScore()
    {
        return awayRuns;
    }

    public Player[] getBaserunners()
    {
        return baseRunners;
    }

    public Player getHomePitcher()
    {
        return homePitcher;
    }

    public Player getAwayPitcher()
    {
        return awayPitcher;
    }

    // Used to Auto Sim or Finish Sim the half inning
    public string simmulateInningHalf()
    {
        inningResults = "";
        while (outCount < 3)
        {
            // If home team is up to bat
            if (topOfInning)
            {
                simulateAtBat(awayTeam, homeTeam, homePitcher);
            }
            else
            {
                simulateAtBat(homeTeam, awayTeam, awayPitcher);
            }
        }

        if (!topOfInning)
            inning++;

        clearBaseRunners();
        outCount = 0;
        topOfInning = !topOfInning;
        inningResults = inningResults.Replace("\r", "");
        return inningResults;
    }

    // Only progress the simulation by the next batter
    public string simmulateNextBatter()
    {
        inningResults = "";
        inbetweenInnings = false;
        if (outCount < 3)
        {
            // If home team is up to bat
            if (topOfInning)
            {
                simulateAtBat(awayTeam, homeTeam, homePitcher);
            }
            else
            {
                simulateAtBat(homeTeam, awayTeam, awayPitcher);
                
            }
        }
        
        if (outCount == 3)
        { 
           clearBaseRunners();
            if (!topOfInning)
                inning++;

            outCount = 0;
            topOfInning = !topOfInning;
            inbetweenInnings = true;
        }
        inningResults = inningResults.Replace("\r", "");

        return inningResults;
    }

    public void simulateAtBat(Team offense, Team defense, Player pitcher)
    {
        Player batter = offense.getUpToBat();
        batResult result = batter.atBat(pitcher);

        switch (result)
        {
            case batResult.HitOut:
                outCount++;
                Player fielder = defense.lineup[Random.Range(0, defense.lineup.Count)];
                fielder.field();

                // 1/3 chance batter flies, lines, or grounds out to a fielder
                int temp = UnityEngine.Random.Range(0, 100);
                if (temp > 66)
                    inningResults += formatPlayerString(batter) + " lines out to " + formatPlayerString(fielder) + "!\n";
                else if (temp > 33)
                    inningResults += formatPlayerString(batter) + " flys out to " + formatPlayerString(fielder) + "!\n";
                else
                    inningResults += formatPlayerString(batter) + " grounds out to " + formatPlayerString(fielder) + "!\n";

                inningResults += outCount + " outs.\n";
                break;

            case batResult.StrikeOut:
                outCount++;
                pitcher.field();
                inningResults += formatPlayerString(batter) + " strikes out to " + formatPlayerString(pitcher) + "!\n";
                inningResults += outCount + " outs.\n";
                break;

            case batResult.Walk:
                inningResults += formatPlayerString(batter) + " walks!\n";
                advanceRunners(batter, 1, offense == homeTeam);
                break;

            case batResult.Single:
                inningResults += formatPlayerString(batter) + " hits a single!\n";
                advanceRunners(batter, 1, offense == homeTeam);
                break;

            case batResult.Double:
                inningResults += formatPlayerString(batter) + " hits a double!!\n";
                advanceRunners(batter, 2, offense == homeTeam);
                break;

            case batResult.Triple:
                inningResults += formatPlayerString(batter) + " hits a triple!!!\n";
                advanceRunners(batter, 3, offense == homeTeam);
                break;

            case batResult.HomeRun:
                inningResults += formatPlayerString(batter) + " hits a homerun!!!!\n";
                advanceRunners(batter, 4, offense == homeTeam);
                break;
        }
    }

    public string formatPlayerString(Player player)
    {
        switch(player.playerPos)
        {
            case Player.position.FB:
                return player.name + " (1st)";
            case Player.position.SB:
                return player.name + " (2nd)";
            case Player.position.TB:
                return player.name + " (3rd)";
            default:
                return player.name + " (" + player.playerPos + ")";
        }
        
    }

    public void advanceRunners(Player newRunner, int baseAmount, bool homeTeamUp)
    {
        baseRunners[0] = newRunner;

        for (int i = baseRunners.Length - 1; i >= 0 ; i--)
        {
            // Try to advance the runner
            try
            {
                baseRunners[i + baseAmount] = baseRunners[i];
                baseRunners[i] = null;
            }
            // If we get an index out of bounds error then the base runner returned home
            catch
            {
                if (baseRunners[i] != null)
                {
                    newRunner.addRBI();

                    if (homeTeamUp)
                    {
                        homeRuns++;
                        inningResults += baseRunners[i].name + " scores for " + homeTeam.teamName + "\n";
                    }
                    else
                    {
                        awayRuns++;
                        inningResults += baseRunners[i].name + " scores for " + awayTeam.teamName + "!\n";
                    }

                    baseRunners[i].addRun();
                    baseRunners[i] = null;
                }
            }
        }
    }
    public void clearBaseRunners()
    {
        for (int i = 0; i < baseRunners.Length; i++)
        {
            baseRunners[i] = null;
        }
    }

    public Player getMVP(Team team)
    {
        List<Player> lineup = team.getLineup();

        Player mvp = null;
        int mvpValue = -1;

        foreach (Player player in lineup)
        {
            int hits = player.getGameHits();
            int walks = player.getGameWalks();
            int rbis = player.getGameRBI();
            int putOuts = player.getGameOuts();
            int runs = player.getGameRuns();

            int tempValue = hits + walks + rbis + putOuts + runs;

            if (tempValue > mvpValue)
            {
                mvp = player;
                mvpValue = tempValue;
            }
        }

        mvp.gameMVP();
        return mvp;
    }
}
