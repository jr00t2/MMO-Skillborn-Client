using UnityEngine;
using System.Collections.Generic;

public class Networkmanager : MonoBehaviour {
    public bool connecting = false;

    List<string> chatMessages;
    int maxChatMessages = 5;

    int guildId = 0;

    void Start() {
        PhotonNetwork.player.name = PlayerPrefs.GetString("Username", "Awesome Dude");
        chatMessages = new List<string>();
    }
    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("Skillborn - Nemesis");
    }
    void OnDestroy()
    {
        PlayerPrefs.SetString("Username", PhotonNetwork.player.name);
        
    }

    public void AddChatMessage(string m)
    {
        GetComponent<PhotonView>().RPC("AddChatMessage_RPC", PhotonTargets.AllBuffered, m);
    }

    [PunRPC]
    void AddChatMessage_RPC(string m)
    {
        while (chatMessages.Count >= maxChatMessages)
        {
            chatMessages.RemoveAt(0);
        }
        chatMessages.Add(m);
    }
    void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed");
        PhotonNetwork.CreateRoom("test");
    }

    void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

        connecting = false;
        //SpawnMyPlayer();
    }

    //void OnGUI() {
    //    GUILayout.Label( PhotonNetwork.connectionStateDetailed.ToString() );

    //    if(PhotonNetwork.connected == false && connecting == false ) {
    //        // We have not yet connected, so ask the player for online vs offline mode.
    //        GUILayout.BeginArea( new Rect(0, 0, Screen.width, Screen.height) );
    //        GUILayout.BeginHorizontal();
    //        GUILayout.FlexibleSpace();
    //        GUILayout.BeginVertical();
    //        GUILayout.FlexibleSpace();

    //        GUILayout.BeginHorizontal();
    //        GUILayout.Label("Username: ");
    //        PhotonNetwork.player.name = GUILayout.TextField(PhotonNetwork.player.name);
    //        GUILayout.EndHorizontal();

    //        if( GUILayout.Button("Single Player") ) {
    //            connecting = true;
    //            PhotonNetwork.offlineMode = true;
    //            OnJoinedLobby();
    //        }

    //        if( GUILayout.Button("Multi Player") ) {
    //            connecting = true;
    //            Connect ();
    //        }

    //        GUILayout.FlexibleSpace();
    //        GUILayout.EndVertical();
    //        GUILayout.FlexibleSpace();
    //        GUILayout.EndHorizontal();
    //        GUILayout.EndArea();
    //    }
    //}
}