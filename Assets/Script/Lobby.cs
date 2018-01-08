using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class Lobby : NetworkLobbyManager {


  
    // Use this for initialization
    void Start () {
        MMStart();
        MMListMatches();
		
	}
    void MMStart()
    {
        Debug.Log("@ MMStart");
        this.StartMatchMaker();
    }

    void MMListMatches()
    {
        Debug.Log("@ MMlistMatches");
        this.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);

    }

    public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        Debug.Log("@ OnMatchList");
        base.OnMatchList(success, extendedInfo, matchList);

        if (!success)
            Debug.Log("This failed" + extendedInfo);
        else
            if (matchList.Count > 0)
        {
            Debug.Log("Successfully listed matches. 1st match:" + matchList[0]);
            MMJoinMatch();
        }
        else
        {
            MMCreateMatch(matchList[0]);
        }
    }

    void MMCreateMatch(MatchInfoSnapshot firstMatch)
    {
        Debug.Log("@ MMCreateMatch");
        this.matchMaker.JoinMatch(firstMatch.networkId,"","","",0,0,OnMatchJoined);

    }

    public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        Debug.Log("@ OnMatchJoined");
        base.OnMatchJoined(success, extendedInfo, matchInfo);

        if(!success)
        {
            Debug.Log("Failed to join Match "+ extendedInfo);
        }
        else
        {
            Debug.Log("Succsfully joined match:" +matchInfo.networkId);
        }
    }

    void MMJoinMatch()
    {
        Debug.Log("@ MMJoinMatch");

        this.matchMaker.CreateMatch("MM", 15, true, "", "", "", 0, 0, OnMatchCreate);
    }

    public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        Debug.Log("@ OnMatchCreate");
        base.OnMatchCreate(success, extendedInfo, matchInfo);

        if(!success)
        {
            Debug.Log("Failed to create match " + extendedInfo);
        }
        else
        {
            Debug.Log("Succsfully Created match:" + matchInfo.networkId);
        }
    }

}
