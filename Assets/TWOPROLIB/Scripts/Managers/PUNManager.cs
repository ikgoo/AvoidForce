//using ExitGames.Client.Photon;
//using Photon.Pun;
//using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace TWOPROLIB.Scripts.Managers
{
    public class PUNManager //: MonoBehaviourPunCallbacks
    {
    //    public Text punStatus;
    //    private string gameVersion = "0.1.0";

    //    /// <summary>
    //    /// 화면 디버그 출력용
    //    /// </summary>
    //    [Tooltip("화면 디버그 출력용")]
    //    public bool debug;

    //    #region UNITY

    //    public static PUNManager Instance = null;
    //    void Start()
    //    {
    //        if (Instance != null)
    //        {
    //            Destroy(gameObject);
    //            return;
    //        }

    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);

    //        if (punStatus != null)
    //        {
    //            punStatus.gameObject.SetActive(debug);
    //        }

    //    }

    //    void FixedUpdate()
    //    {
    //        if (debug)
    //        {
    //            if (punStatus != null)
    //                punStatus.text = PhotonNetwork.NetworkClientState.ToString();
    //        }
    //    }
    //    #endregion


    //    #region PUN CALLBACKS

    //    /// <summary>
    //    /// Photon에 접속 성공
    //    /// </summary>
    //    public override void OnConnectedToMaster()
    //    {
    //        if (debug)
    //            Debug.Log("OnConnectedToMaster");

    //        if (punCallBackIF == null)
    //        {
    //            // 게임 씬에 메뉴부분으로 이동
    //            GameManager.Instance.ChangeGameState(TWOPROLIB.ScriptableObjects.GameStateType.MainMenu);
    //        }
    //        else
    //            punCallBackIF.PUNConntected(true);
    //    }

    //    /// <summary>
    //    /// phton 접속 끊김
    //    /// </summary>
    //    /// <param name="cause"></param>
    //    public override void OnDisconnected(DisconnectCause cause)
    //    {
    //        if (debug)
    //            Debug.Log("OnDisconnected");

    //        // 접속이 끊긴 것에 대한 멘트가 필요함

    //        ScreenManager.Instance.ChangeScene(ScreenManager.ScenePage.LOADING);

    //        //PUNConnection();
    //    }

    //    /// <summary>
    //    /// 해당 룸에 접속 성공
    //    /// </summary>
    //    public override void OnJoinedRoom()
    //    {
    //        if (debug)
    //        {
    //            Debug.Log("OnJoinedRoom");
    //            Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber.ToString());
    //        }

    //        GameManager.Instance.ChangeGameState(TWOPROLIB.ScriptableObjects.GameStateType.Ready);
    //        GameManager.Instance.SpawnPlayer();
    //    }

    //    /// <summary>
    //    /// 랜던 룸에 접속 실패
    //    /// 현재 방이 없거나 비어 있는 방이 없을 경우 실패일 것으로 추정
    //    /// </summary>
    //    /// <param name="returnCode"></param>
    //    /// <param name="message"></param>
    //    public override void OnJoinRandomFailed(short returnCode, string message)
    //    {
    //        if (debug)
    //            Debug.Log("OnJoinRandomFailed");

    //        RoomOptions options = new RoomOptions { MaxPlayers = 4 };
    //        PhotonNetwork.CreateRoom(null, options);
    //    }

    //    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    //    {
    //        if (debug)
    //            Debug.Log("OnRoomListUpdate");
    //    }

    //    /// <summary>
    //    /// 플레이어가 room들어 왔을때
    //    /// </summary>
    //    /// <param name="newPlayer"></param>
    //    public override void OnPlayerEnteredRoom(Player newPlayer)
    //    {
    //        if (PhotonNetwork.IsMasterClient)
    //        {
    //            GameManager.Instance.SendCurrentGameStatus();
    //        }

    //        if (debug)
    //            Debug.Log("OnPlayerEnteredRoom:" + newPlayer.ActorNumber.ToString());
    //    }

    //    public override void OnJoinedLobby()
    //    {
    //        if (debug)
    //            Debug.Log("OnJoinedLobby=============");
    //    }

    //    /// <summary>
    //    /// 플레이어가 room에서 나갔을때
    //    /// </summary>
    //    /// <param name="other"></param>
    //    public override void OnPlayerLeftRoom(Player other)
    //    {
    //        GameManager.Instance.PlayerLeftRoom(other);

    //        if (debug)
    //        {
    //            Debug.Log("OnPlayerLeftRoom:" + other.ActorNumber.ToString());
    //            Debug.Log(other);
    //        }
    //    }

    //    //public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
    //    //{
    //    //    if(changedProps.ContainsKey("viewidx"))
    //    //    {
    //    //        GameManager.Instance.lsPlayer[(int)changedProps["viewidx"]].setPlayer(target);
    //    //        if(PhotonNetwork.LocalPlayer.Equals(target))
    //    //        {

    //    //            GameManager.Instance.localPlayer = GameManager.Instance.lsPlayer[(int)changedProps["viewidx"]];

    //    //        }
    //    //    }

    //    //}


    //    #endregion

    //    #region Public Methed

    //    PUNCallBackIF punCallBackIF;

    //    /// <summary>
    //    /// 최초 PUN 접속 처리
    //    /// </summary>
    //    public void PUNConnection(PUNCallBackIF punCallBackIF = null)
    //    {
    //        if (punCallBackIF != null)
    //            this.punCallBackIF = punCallBackIF;

    //        PhotonNetwork.SendRate = 8;
    //        PhotonNetwork.QuickResends = 3;

    //        PhotonNetwork.AutomaticallySyncScene = true;
    //        PhotonNetwork.GameVersion = gameVersion;
    //        PhotonNetwork.LocalPlayer.NickName = "test_" + Random.Range(0, 100).ToString();

    //        PhotonNetwork.ConnectUsingSettings();
    //    }

    //    public void JoinRoom()
    //    {
    //        if (PhotonNetwork.IsConnected)
    //        {
    //            PhotonNetwork.JoinRandomRoom();
    //        }
    //        else
    //        {
    //            PhotonNetwork.GameVersion = gameVersion;
    //            PhotonNetwork.ConnectUsingSettings();
    //        }
    //    }

    //    #endregion
    }
}
