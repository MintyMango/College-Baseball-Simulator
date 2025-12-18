using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MasterController : MonoBehaviour
{
    
    public GameController gameController;
    public PlayerGenerator playerGenerator;

    void Start()
    {
        playerGenerator = new PlayerGenerator();

        Team homeTeam = new Team("Pandas", "UrMoms House");
        Team awayTeam = new Team("Sharks", "The Ocean");

        createFourtyManRoster(homeTeam);
        createFourtyManRoster(awayTeam);

        gameController.startGame(homeTeam, awayTeam);

        //test();
    }

    public void test()
    {
        Dictionary<float, int> counter = new Dictionary<float, int>();

        for (int i = 0; i < 1000; i++)
        {
            float temp = playerGenerator.generateBAAvgValue(0);
            if (!counter.ContainsKey(temp))
                counter.Add(temp, 0);
            
            counter[temp]++;
        }

        var sortedKeys = counter.Keys.ToList();
        sortedKeys.Sort();

        foreach (float key in sortedKeys)
        {
            Debug.Log(key + " : " + counter[key]);
        }
    }



    public void createFourtyManRoster(Team team)
    {
        for (int i = 0; i < 30; i++)
        {
            team.addPlayer(playerGenerator.generatePlayer(team, false));
        }

        for (int i = 0; i < 10;  i++)
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
