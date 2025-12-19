using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerGenerator
{
    private string[] firstNames;
    private string[] lastNames;
    private string[] homeState;

    private List<Player.position> positions;
    private List<Player.position> pitchtingPos;

    public PlayerGenerator()
    {
        var sr = new StreamReader(Application.dataPath + "/DataFiles/FirstNames.txt");
        firstNames = sr.ReadToEnd().Split("\n");
        sr = new StreamReader(Application.dataPath + "/DataFiles/LastNames.txt");
        lastNames = sr.ReadToEnd().Split("\n");
        sr = new StreamReader(Application.dataPath + "/DataFiles/HomeStates.txt");
        homeState = sr.ReadToEnd().Split("\n");
        sr.Close();

        positions = new List<Player.position>();
        pitchtingPos = new List<Player.position>();

        foreach (Player.position p in Enum.GetValues(typeof(Player.position)))
        {
            if (p == Player.position.SP || p == Player.position.RP || p == Player.position.CP)
                pitchtingPos.Add(p);
            else
                positions.Add(p);
        }
    }

    public Player generatePlayer(Team team, bool isPitcher)
    {
        string name = generateName();
        int number = generateNumber(team);
        string homeState = generateHomeState();
        Player.position p;

        if (!isPitcher)
            p = generatePosition();
        else
            p = generatePitcher();

            int speed = generateSkillValue();
        int eyes = generateSkillValue();
        int strength = generateSkillValue();
        int fielding = generateSkillValue();

        float trueBA = generateBAAvgValue(eyes);
        float trueWalk = generateWalkAvgValue(eyes);
        float trueSlug = generateTrueSlugging(eyes);

        return new Player(name, number, homeState, p, .500f, trueWalk, trueSlug, speed, eyes, fielding, strength);
    }

    public string generateName()
    {
        return firstNames[UnityEngine.Random.Range(0, firstNames.Length)] + " " + lastNames[UnityEngine.Random.Range(0, lastNames.Length)];
    }

    public int generateNumber(Team team)
    {
        List<int> numberList = team.getValidNumbers();
        return numberList[UnityEngine.Random.Range(0, numberList.Count)];
    }

    public string generateHomeState()
    {
        return homeState[UnityEngine.Random.Range(0, homeState.Length)];
    }

    public Player.position generatePosition()
    {
        return positions[UnityEngine.Random.Range(0, positions.Count)];
    }

    public Player.position generatePitcher()
    {
        return pitchtingPos[UnityEngine.Random.Range(0, pitchtingPos.Count)];
    }

    public int generateSkillValue()
    {
        int skillValue = 0;

        // Generates a number between 0 and 100 that should average around 50
        for (int i = 0; i < 10; i++)
        {
            skillValue += UnityEngine.Random.Range(0, 11);
        }

        return skillValue;
    }

    public float generateBAAvgValue(int eyes)
    {
        float avgValue = 0;
        // Lowest BA a player can have
        float floor = .200f;

        // Player with 99 skill adds an extra .100 to batting avg making minimum .300
        float skillMult = .100f;

        // Generate a random value between 0 and 100 averaging 50
        for (int i = 0; i < 10; i++)
        {
           avgValue += UnityEngine.Random.Range(0, 11) / 1000f;
        }

        // Normalize the eyes skill
        float normalizedSkill = eyes / 99;

        // Generates a value between .200 and .400 that is based on the players skill level
        return avgValue + floor + (skillMult * normalizedSkill);
    }

    public float generateWalkAvgValue(int eyes)
    {
        float avgValue = 0;
        // Lowest walk percentage a player can have
        float floor = .05f;

        // Player with 99 skill adds an extra .04 to walk avg making minimum .1
        float skillMult = .04f;

        // Generate a random value between 0 and 300 averaging 150
        // Divide by 10000 to get a value between 0 and .03
        for (int i = 0; i < 30; i++)
        {
            avgValue += UnityEngine.Random.Range(0, 11) / 10000f;
        }

        // Normalize the eyes skill
        float normalizedSkill = eyes / 99;

        // Generates a value between .05 and .18 that is based on the players skill level
        // Total avg (50 eyes skill) is 0.085
        return avgValue + floor + (skillMult * normalizedSkill);
    }

    public float generateTrueSlugging(int eyes)
    {
        float avgValue = 0;
        // Lowest slug percentage a player can have
        float floor = 0.350f;

        // Player with 99 skill adds an extra .150 to walk avg making minimum .500
        float skillMult = .150f;

        // Generate a random value between 0 and 100 averaging 50
        for (int i = 0; i < 20; i++)
        {
            avgValue += UnityEngine.Random.Range(0, 11) / 1000f;
        }

        // Normalize the eyes skill
        float normalizedSkill = eyes / 99;

        // Generates a value between .350 and .550 that is based on the players skill level
        // Total avg (50 eyes skill) is 0.475
        return avgValue + floor + (skillMult * normalizedSkill);
    }
}

