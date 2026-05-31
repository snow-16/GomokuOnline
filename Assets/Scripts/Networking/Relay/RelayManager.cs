using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class RelayManager
{
    public static async Task<string> CreateRelay(int maxConnections = 2)
    {
        // 認証初期化（初回のみ）
        await Initialize();

        // リレー割り当ての作成（ホスト側がサーバーを確保）
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);
        
        // 参加コードの取得
        string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        
        // 取得した JoinCode をプレイヤーに共有する
        Debug.Log("Join Code: " + joinCode);

        // 接続処理
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
        allocation.RelayServer.IpV4,
        (ushort)allocation.RelayServer.Port,
        allocation.AllocationIdBytes,
        allocation.Key,
        allocation.ConnectionData
        );

        NetworkManager.Singleton.StartHost();

        return joinCode;
    }

    public static async Task JoinRelay(string joinCode)
    {
        await Initialize();

        // リレー割り当てに参加
        try
        {
            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            // 接続処理
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData,
                allocation.HostConnectionData
            );

            NetworkManager.Singleton.StartClient();
        }
        catch (RelayServiceException)
        {
            Debug.Log("参加コードの形式が不正です。");
        }
    }

    private async static Task Initialize()
    {
        if (UnityServices.State == ServicesInitializationState.Uninitialized)
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }
}
