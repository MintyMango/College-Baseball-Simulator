using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Conference
{
    public MasterController.Conf conf;
    public List<Team> teams;

    public Conference(MasterController.Conf conf)
    {

        this.conf = conf;

        teams = new List<Team>();
    }



    public void addTeam(Team team)
    {
        teams.Add(team);
    }

    public List<Team> getTeams()
    {
        return teams;
    }

    public MasterController.Conf getConferenceABV()
    {
        return conf;
    }

    public string getFullConferenceName()
    {
        switch(conf)
        {
            case MasterController.Conf.AAC:
                return "Aurora Athletic Conference";
            case MasterController.Conf.FPC:
                return "Frontier Plains Conference";
            case MasterController.Conf.ACC:
                return "Atlantic Crest Conference";
            case MasterController.Conf.SSC:
                return "Sunspire Conference";
        }

        return "";
    }
}
