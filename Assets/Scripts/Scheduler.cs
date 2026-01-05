using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Scheduler
{
    int seriesInSeason;
    List<Conference> conferences;

    List<List<(Team, Team)>> matchups;


    /**
     * Season goes Bo3 series against conference opponent then the next game is a Bo1 against an OOC opponent (every odd index)
     * Conference games alternate home and away (OOC game attempts to follow this pattern but sometimes doesn't)
    */

    public Scheduler(int seriesInSeason, List<Conference> conferences)
    {
        this.seriesInSeason = seriesInSeason;
        this.conferences = conferences;
        matchups = new List<List<(Team, Team)>>();
    }

    public void generateMatchups()
    {

        for (int i = 0; i < seriesInSeason; i++)
        {
            List<Team> teamsScheduled = new List<Team>();
            matchups.Add(new List<(Team, Team)>());

            // Grab each conference
            foreach (Conference c in conferences)
            {
                // Then for every team in that conference get teams you haven't played yet
                foreach(Team team in c.teams)
                {
                    if (teamsScheduled.Contains(team))
                        continue;
                    else
                        teamsScheduled.Add(team);


                    // If the series count is even then we play a conference opp
                    if (i % 2 == 0)
                    {
                        List<Team> posOpponents = getConfOpponents(c, teamsScheduled);
                        Team opp = null;

                        if (posOpponents.Count != 0)
                        {
                            opp = posOpponents[UnityEngine.Random.Range(0, posOpponents.Count)];
                            teamsScheduled.Add(opp);
                        }

                        // 50% chance a team is home or away
                        if (Random.value >= 0.5f)
                            matchups[i].Add((team, opp));
                        else
                            matchups[i].Add((opp, team));

                    }
                    // Else we play an OOC opponent/rival
                    else
                    {
                        List<Team> oocOpponents = getOOCOppenents(c, teamsScheduled);
                        Team opp = null;

                        if (oocOpponents.Count != 0)
                        {
                            opp = oocOpponents[UnityEngine.Random.Range(0, oocOpponents.Count)];

                            teamsScheduled.Add(opp);
                        }

                        
                        // 50% chance a team is home or away
                        if (Random.value >= 0.5f)
                            matchups[i].Add((team, opp));
                        else
                            matchups[i].Add((opp, team));

                    }
                }
            }

            //printTeams(teamsScheduled);
        }
    }


    public void printTeams(List<Team> teams)
    {
        foreach(Team team in teams)
        {
            Debug.Log(team.teamName);
        }
    }

    public List<(Team, Team)> getNextMatchup(int seriesCount)
    {
        return matchups[seriesCount];
    }
    /**
     * Get a list of OOC Opponents passing the the conference we don't want opponents from
     */
    public List<Team> getOOCOppenents(Conference currConference, List<Team> scheduledTeams)
    {
        List<Conference> temp = new List<Conference>();
        foreach (Conference c in conferences)
            temp.Add(c); 

        temp.Remove(currConference);

        List<Team> oocOpponents = new List<Team>();

        foreach (Conference c in temp)
        {
            foreach(Team t in c.teams)
            {
                if (!scheduledTeams.Contains(t))
                    oocOpponents.Add(t);
            }
        }

        return oocOpponents;
    }

    public List<Team> getConfOpponents(Conference conference, List<Team> scheduledTeams)
    {
        List<Team> temp = new List<Team>();

        foreach(Team t in conference.teams)
        {
            if (!scheduledTeams.Contains(t))
                temp.Add(t);                
        }

        return temp;
    }

    public List<Team> getOpponents(Team team)
    {
        List<Team> opps = new List<Team>();

        foreach(List<(Team, Team)> match in matchups)
        {
            foreach((Team, Team) game in match)
            {
                if (game.Item1 == team)
                {
                    opps.Add(game.Item2);
                }
                else if (game.Item2 == team)
                {
                    opps.Add(game.Item1);
                }
            }
        }

        return opps;
    }

    public Team findNextOpponent(Team team, int seriesNum)
    {
        List<Team> opps = getOpponents(team);

        return opps[seriesNum];
    }

    public List<(Team, Team)> getNonPlayerMatchups(Team playerTeam, Team oppTeam, int seriesNum)
    {
        List<(Team, Team)> temp = new List<(Team, Team)>();

        foreach ((Team, Team) game in matchups[seriesNum])
        {
            if ((game.Item1 != playerTeam && game.Item2 != oppTeam) || (game.Item1 != oppTeam && game.Item2 != playerTeam))
            {
                temp.Add(game);
            }    
        }

        return temp;
    }
}
