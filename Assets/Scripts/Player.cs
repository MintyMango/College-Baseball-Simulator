using UnityEngine;

public class Player
{
    public string name;
    public int number;
    public string homeState;
    public enum position {C, FB, SB, SS, TB, RF, CF, LF, DH, SP, RP, CP};
    public position playerPos;

    // Player Stats
    private float trueBattingAvg;
    private float trueWalkAvg;
    private float trueSluggingPercent;

    /** 0-99 stats **/
    // Running for position players and Throwing speed for Pitchers
    public int speed;
    // Plate discipline for position and Accuracy for Pitchers
    public int eyes;
    // Defense
    public int fielding;
    // Hit power for position
    public int strength;

    // Game Stats ------
    // Plate Stats
    private int plateAppearances;
    private int strikeOuts;
    private int walks;
    private int RBIs;
    private int runs;

    private int singles;
    private int doubles;
    private int triples;
    private int homeruns;
    // private int steals; <- future stat
    private int putOuts;
    private int gamesPlayed;
    // Game Stats----

    // Career Stats----
    // Plate Stats
    private int careerPlateAppearances;
    private int careerStrikeOuts;
    private int careerWalks;
    private int careerRBIs;
    private int careerRuns;

    private int careerSingles;
    private int careerDoubles;
    private int careerTriples;
    private int careerHomeruns;
    // private int steals; <- future stat
    private int careerPutOuts;

    // Awards
    private int singleGameMVP;


    // Career Stats

    public Player(string name, int number, string homeState, position playerPos, float trueBattingAvg, float trueWalkAvg, float trueSluggingPercent, int speed, int eyes, int fielding, int strength)
    {
        this.name = name.Replace("\r", "");
        this.number = number;
        this.homeState = homeState;
        this.playerPos = playerPos;
        this.trueBattingAvg = trueBattingAvg;
        this.trueWalkAvg = trueWalkAvg;
        this.trueSluggingPercent = trueSluggingPercent;
        this.speed = speed;
        this.eyes = eyes;
        this.fielding = fielding;
        this.strength = strength;


        careerSingles = 0;
        careerDoubles = 0;
        careerTriples = 0;
        careerHomeruns = 0;
        careerPlateAppearances = 0;
        careerPutOuts = 0;
        careerStrikeOuts = 0;
        careerWalks = 0;
        careerRBIs = 0;
        careerRuns = 0;
    }

    public int getOverall()
    {
        return (speed + eyes + fielding + strength) / 4;
    }

    public int getDefenseRating()
    {
        return (speed + fielding) / 2;
    }

    public int getOffenseRating()
    {
        return (speed + eyes + strength) / 3;
    }

    public GameSequence.batResult atBat(Player pitcher)
    {
        // TODO: Incorporate pitchers stats for the result

        plateAppearances++;
        // Generate a float between 1.000 and 0.000
        float result = UnityEngine.Random.Range(0, 1000) / 1000f;
        if (result <= trueBattingAvg)
        {

            // Generate a float between 1.000 and 0.000
            if (UnityEngine.Random.Range(0, 1000) / 1000f <= trueSluggingPercent)
            {
                // Float between 1-0
                float temp = rollExtraBases();

                if (temp < 0.40f)
                {
                    doubles++;
                    return GameSequence.batResult.Double;
                }
                else if (temp < 0.60f)
                {
                    triples++;
                    return GameSequence.batResult.Triple;
                }
                else
                {
                    homeruns++;
                    return GameSequence.batResult.HomeRun;
                }

            }
            else
            {
                singles++;
                return GameSequence.batResult.Single;
            }   
        }
        else if (result <= trueBattingAvg + trueWalkAvg)
        {
            walks++;
            return GameSequence.batResult.Walk;
        }
        else
        {
            float outType = Random.value;
            // TODO: Make this deterministic based on the player's skill
            if (outType <= 0.5)
            {
                strikeOuts++;
                return GameSequence.batResult.StrikeOut;
            }
            else
            {
                return GameSequence.batResult.HitOut;
            }
        }
    }

    public float rollExtraBases()
    {
        float runningTotal = 0f;

        for (int i = 0; i < 20; i++)
        {
            // 50 is the average for the stat thus the player gets benefits if they are above average
            // or penalized if they are below average
            runningTotal += (UnityEngine.Random.Range(0, 100) / 100f) * (strength / 50f);
        }

        // Return a normalized value after generating a random number 20 times
        return runningTotal / 20f;
    }

    // Update stats to career stats
    public void takenOutOfGame()
    {
        careerSingles += singles;
        careerDoubles += doubles;
        careerTriples += triples;
        careerHomeruns += homeruns;
        careerPlateAppearances += plateAppearances;
        careerPutOuts += putOuts;
        careerStrikeOuts += strikeOuts;
        careerWalks += walks;
        careerRBIs += RBIs;
        careerRuns += runs;
    }

    public void startGame()
    {
        singles = 0;
        doubles = 0;
        triples = 0;
        homeruns = 0;
        plateAppearances = 0;
        putOuts = 0;
        strikeOuts = 0;
        walks = 0;
        RBIs = 0;
        runs = 0;
        gamesPlayed++;
    }

    public string getPosition()
    {
        switch (playerPos)
        {
            case position.FB:
                return "1B";
            case position.SB:
                return "2B";
            case position.TB:
                return "3B";
            default:
                return playerPos.ToString();
        }
    }

    public void addRBI()
    {
        RBIs++;
    }

    public void addRun()
    {
        runs++;
    }

    public void gameMVP()
    {
        singleGameMVP++;
    }

    public void field()
    {
        putOuts++;
    }

    public int getGameHits()
    {
        return (singles + doubles + triples + homeruns);
    }

    public int getGameWalks()
    {
        return walks;
    }

    public int getGamePAs()
    {
        return plateAppearances;
    }

    public int getGameSO()
    {
        return strikeOuts;
    }

    public int getGameSingles()
    {
        return singles;
    }

    public int getGameDoubles()
    {
        return doubles;
    }

    public int getGameTriples()
    {
        return triples;
    }

    public int getGameHomeRuns()
    {
        return homeruns;
    }

    public int getGameOuts()
    {
        return putOuts;
    }

    public int getGameRBI()
    {
        return RBIs;
    }

    public int getGameRuns()
    {
        return runs;
    }

    public int getCareerHits()
    {
        return (careerSingles + careerDoubles + careerTriples + careerHomeruns);
    }

    public int getCareerWalks()
    {
        return careerWalks;
    }

    public int getCareerPA()
    {
        return careerPlateAppearances;
    }

    public int getCareerSO()
    {
        return careerStrikeOuts;
    }

    public int getCareerSingles()
    {
        return careerSingles;
    }

    public int getCareerDoubles()
    {
        return careerDoubles;
    }

    public int getCareerTriples()
    {
        return careerTriples;
    }

    public int getCareerHomeRuns()
    {
        return careerHomeruns;
    }

    public int getCareerOuts()
    {
        return careerPutOuts;
    }

    public int getCareerRBI()
    {
        return careerRBIs;
    }

    public int getCareerRuns()
    {
        return careerRuns;
    }

    public int getCareerStrikeOuts()
    {
        return careerStrikeOuts;
    }

    public int getGamesPlayed()
    {
        return gamesPlayed;
    }

    public float getCareerOBP()
    {
        if (careerPlateAppearances == 0)
            return 0;

        return (getCareerHits() + careerWalks) / (float)careerPlateAppearances; 
    }

    public float getGameOBP()
    {
        if (plateAppearances == 0)
            return 0;

        return (getGameHits() + walks) / (float)plateAppearances;
    }

    public float getCareerSlugging()
    {
        if (getCareerHits() == 0)
            return 0;

        return (careerSingles + (careerDoubles * 2) + (careerTriples * 3) + (careerHomeruns * 4)) / (float)getCareerHits();
    }

    public float getGameSlugging()
    {
        if (getGameHits() == 0)
            return 0;

        return (singles + (doubles * 2) + (triples * 3) + (homeruns * 4)) / (float)getGameHits();
    }

    public float getCareerBA()
    {
        if (careerPlateAppearances == 0)
            return 0;

        return getCareerHits() / (float)careerPlateAppearances;
    }

    public float getGameBA()
    {
        if (plateAppearances == 0)
            return 0;

        return getGameHits() / (float)plateAppearances;
    }

    public int getCareerTotalBases()
    {
        return (careerSingles + (careerDoubles * 2) + (careerTriples * 3) + (careerHomeruns * 4));
    }

    public int getGameTotalBases()
    {
        return (singles + (doubles * 2) + (triples * 3) + (homeruns * 4));
    }



    // Pitcher helper methods
    public void strikeOut()
    {
        strikeOuts++;
    }

    public void walk()
    {
        walks++;
    }

    public void singleHit()
    {
        singles++;
    }

    public void doubleHit()
    {
        doubles++;
    }

    public void tripleHit()
    {
        triples++;
    }

    public void homerunHit()
    {
        homeruns++;
    }

    
}
