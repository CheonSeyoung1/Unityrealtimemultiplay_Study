using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
public class PhotonTest
{
    private static List<LoadBalancingClient> tempClients = new List<LoadBalancingClient>();
    public static void OnServerJoinTempClients(byte clientEa = 100)
    {
        foreach (var tempClient in tempClients)
        {
            if (tempClient is null) continue;
            tempClient.Disconnect();
        }
        tempClients.Clear();

        LoadBalancingClient[] m_clients = new LoadBalancingClient[clientEa];
        var m_masterClient = PhotonNetwork.NetworkingClient;
        var m_appId = string.Empty;

        for (var i = 0; i < clientEa; i++)
        {
            m_appId = i.ToString();
            m_clients[i] = new LoadBalancingClient();

            m_clients[i].MasterServerAddress = m_masterClient.MasterServerAddress;
            m_clients[i].AppId = m_appId;
            m_clients[i].LoadBalancingPeer.TransportProtocol = m_masterClient.LoadBalancingPeer.TransportProtocol;
            m_clients[i].EnableLobbyStatistics = m_masterClient.EnableLobbyStatistics;
            m_clients[i].ProxyServerAddress = m_masterClient.ProxyServerAddress;
            m_clients[i].NameServerPortInAppSettings = m_masterClient.NameServerPortInAppSettings;
            if (m_clients[i].ConnectToNameServer()) Debug.Log("Connected Sever Id : " + m_clients[i].AppId);
        }
        tempClients.AddRange(m_clients);
    }
}
