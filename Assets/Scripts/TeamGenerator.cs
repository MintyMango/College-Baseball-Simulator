using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TeamGenerator
{
    string[] teams;

    public List<Conference> conferenceList;

    private PlayerGenerator playerGenerator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TeamGenerator(PlayerGenerator playerGenerator)
    {
        var sr = new StreamReader(Application.dataPath + "/DataFiles/TeamInfo.txt");
        teams = sr.ReadToEnd().Split("\n");

        conferenceList = new List<Conference>();
        this.playerGenerator = playerGenerator;

        generateConferences();
        generateTeams();

        //printConferneces();
    }

    public void generateConferences()
    {
        foreach (MasterController.Conf conference in Enum.GetValues(typeof(MasterController.Conf)))
        {
            conferenceList.Add(new Conference(conference));
        }
    }

    public void generateTeams()
    {

        foreach (var team in teams)
        {
            if (team == "")
                break;

            string[] splitTeamInfo = team.Split(",");

            //Debug.Log(string.Format("State: {0}, Conf: {1}", splitTeamInfo[2], splitTeamInfo[3]));

            Team temp = new Team(splitTeamInfo[0], splitTeamInfo[1], splitTeamInfo[2]);

            createRoster(temp);

            MasterController.Conf confName = convertConf(splitTeamInfo[3]);

            foreach (Conference conference in conferenceList)
            {
                if (confName.Equals(conference.getConferenceABV()))
                    conference.addTeam(temp);
            }
        }
    }

    public void printConferneces()
    {
        foreach(Conference conference in conferenceList)
        {
            Debug.Log(conference.getFullConferenceName());

            foreach (Team team in conference.teams)
                Debug.Log(team.teamName);
        }
    }


    public MasterController.Conf convertConf(string conf)
    {
        if (conf.Contains("Aurora Athletic Conference"))
            return MasterController.Conf.AAC;
        else if(conf.Contains("Frontier Plains Conference"))
            return MasterController.Conf.FPC;
        else if(conf.Contains("Atlantic Crest Conference"))
            return MasterController.Conf.ACC;
        else
            return MasterController.Conf.SSC;
    }


    public void createRoster(Team team)
    {
        for (int i = 0; i < 24; i++)
        {
            team.addPlayer(playerGenerator.generatePlayer(team, false));
        }

        for (int i = 0; i < 10; i++)
        {
            Player temp = playerGenerator.generatePlayer(team, true);

            // Create 4 Starting Pitchers, 5 Relief Pitchers, and 1 Closer
            if (i >= 0 && i <= 3)
                temp.playerPos = Player.position.SP;
            else if (i >= 4 && i <= 8)
                temp.playerPos = Player.position.RP;
            else
                temp.playerPos = Player.position.CP;

            team.addPlayer(temp);
        }
    }
}
