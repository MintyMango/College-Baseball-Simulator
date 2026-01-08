using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortalController : MonoBehaviour
{
    public GameObject transferEntry;
    public GameObject transferList;

    private List<Player> availablePlayers;


    public PlayerPortalController()
    {
        availablePlayers = new List<Player>();
    }

    public void generateNewClass()
    {

    }
}
