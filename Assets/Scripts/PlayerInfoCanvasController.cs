using UnityEngine;

public class PlayerInfoCanvasController : MonoBehaviour
{
    public MasterController masterController;
    public GameObject playerEntry;

    private Team playerTeam;


    void Start()
    {
        playerTeam = masterController.playerTeam;
        populateList(playerTeam);
    }

    public void populateList(Team team)
    {
        float offset = (this.GetComponent<RectTransform>().rect.height / 2);
        int runningCount = 1;

        foreach (Player player in team.getFourtyManRoster())
        {

            GameObject temp = Instantiate(playerEntry, this.transform);

            RectTransform rt = temp.GetComponent<RectTransform>();

            offset -= temp.GetComponent<RectTransform>().rect.height;

            rt.localPosition = new Vector2(0, offset);

            temp.GetComponent<PlayerDataController>().updateText(runningCount, player.name, player.playerPos, player.getOverall(), player.getGamesPlayed(), 
                player.getCareerHits() / player.getCareerPA(), player.getCareerWalks() / player.getCareerPA(), player.getCareerSO(), player.getCareerOuts(),
                team.lineup.Contains(player));
            runningCount++;
        }
    }
}
