using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;
    void Start()
    {
        Debug.Log("Connecting...");
		var m_str = PhotonNetwork.PhotonServerSettings.AppSettings.ToStringFull();
		PhotonNetwork.ConnectUsingSettings();
	}
	public override void OnConnectedToMaster()
	{
		base.OnConnectedToMaster();

        Debug.Log("Connected to Server");
        PhotonNetwork.JoinLobby();
		Debug.Log("UserId : "+PhotonNetwork.LocalPlayer.UserId);
		Debug.Log("Before : "+ PhotonNetwork.LocalPlayer.CustomProperties.Count);
		var m_propertiesId = (byte)0;
		var m_propertiesLimit = false;
		while (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey(m_propertiesId))
		{
			m_propertiesId++;
			if (m_propertiesId > 100)
				m_propertiesLimit = true;
		}
		if(!m_propertiesLimit)
			PhotonNetwork.LocalPlayer.CustomProperties.Add(m_propertiesId, "ASD");
		Debug.Log("After : "+ PhotonNetwork.LocalPlayer.CustomProperties.Count);
	}
	public override void OnJoinedLobby()
	{
		base.OnJoinedLobby();
        PhotonNetwork.JoinOrCreateRoom(roomName: "Test",roomOptions: null,typedLobby: null);
        Debug.Log("We're Connected and in a room now");
	}
	public bool JoinRoom(string roomName) { return true; }
	public override void OnJoinedRoom()
	{
		base.OnJoinedRoom();

		Debug.Log("We're connected and in room!");
		var m_player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
		m_player.GetComponent<PlayerSet>().IsLocalPlayer();
	}
	public override void OnDisconnected(DisconnectCause cause)
	{
		base.OnDisconnected(cause);
	}
	private void OnDestroy()
	{
		PhotonNetwork.Disconnect();
	}
}
