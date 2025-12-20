using TMPro;
using UnityEngine;

public class TeamStatController : MonoBehaviour
{
    public TextMeshProUGUI oGP;
    public TextMeshProUGUI oPA;
    public TextMeshProUGUI oH;
    public TextMeshProUGUI oOBP;
    public TextMeshProUGUI oBA;
    public TextMeshProUGUI oSLG;
    public TextMeshProUGUI oSO;
    public TextMeshProUGUI oR;
    public TextMeshProUGUI oBB;
    public TextMeshProUGUI oDB;
    public TextMeshProUGUI oHR;
    public TextMeshProUGUI oTP;
    public TextMeshProUGUI oRBI;
    public TextMeshProUGUI oTB;

    public TextMeshProUGUI dPA;
    public TextMeshProUGUI dH;
    public TextMeshProUGUI dOBP;
    public TextMeshProUGUI dBA;
    public TextMeshProUGUI dSLG;
    public TextMeshProUGUI dSO;
    public TextMeshProUGUI dR;
    public TextMeshProUGUI dERA;
    public TextMeshProUGUI dBB;
    public TextMeshProUGUI dDB;
    public TextMeshProUGUI dHR;
    public TextMeshProUGUI dTP;
    public TextMeshProUGUI dTB;

    public void updateTeamOffStats(Team team)
    {
        oGP.text = string.Format("W/L: {0} - {1}", team.wins, team.losses);
        oPA.text = string.Format("PA: {0}", team.getTotalOffABs());
        oH.text = string.Format("H: {0}", team.getTotalOffHits());
        oOBP.text = string.Format("OBP: {0:#.000}", team.getTotalOffOBP());
        oBA.text = string.Format("BA: {0:#.000}", team.getTotalOffBA());
        oSLG.text = string.Format("SLG: {0:#.000}", team.getTotalOffSLG());
        oSO.text = string.Format("SO: {0}", team.getTotalOffSO());
        oR.text = string.Format("R: {0}", team.getTotalOffRuns());
        oBB.text = string.Format("BB: {0}", team.getTotalOffBB());
        oDB.text = string.Format("2B: {0}", team.getTotalOffDoubles());
        oTB.text = string.Format("3B: {0}", team.getTotalOffTriples());
        oHR.text = string.Format("HR: {0}", team.getTotalOffHomeRuns());
        oRBI.text = string.Format("RBI: {0}", team.getTotalOffRBIs());
        oTB.text = string.Format("TB: {0}", team.getTotalOffTB());
    }

    public void updateTeamDefStats(Team team)
    {
        dPA.text = string.Format("PA: {0}", team.getTotalDefABs());
        dH.text = string.Format("H: {0}", team.getTotalDefHits());
        dOBP.text = string.Format("OBP: {0:#.000}", team.getTotalDefOBP());
        dBA.text = string.Format("BA: {0:#.000}", team.getTotalDefBA());
        dSLG.text = string.Format("SLG: {0:#.000}", team.getTotalDefSLG());
        dSO.text = string.Format("SO: {0}", team.getTotalDefSO());
        dR.text = string.Format("R: {0}", team.getTotalDefRuns());
        dBB.text = string.Format("BB: {0}", team.getTotalDefBB());
        dDB.text = string.Format("2B: {0}", team.getTotalDefDoubles());
        dTB.text = string.Format("3B: {0}", team.getTotalDefTriples());
        dHR.text = string.Format("HR: {0}", team.getTotalDefHomeRuns());
        dTB.text = string.Format("TB: {0}", team.getTotalDefTB());
        dERA.text = string.Format("ERA: {0:#.00}", team.getDefERA());
    }
}
