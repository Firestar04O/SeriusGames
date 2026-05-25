using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestConnect : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject panelCreatePlayer;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // Presiona C para crear sala de prueba
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 4;
            options.IsVisible = true;
            options.IsOpen = true;
            PhotonNetwork.CreateRoom("TestSala_" + Random.Range(0, 999), options);
        }
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado a Photon Master Server");
        PhotonNetwork.JoinLobby(); // Importante: Unirse al lobby
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Conectado al lobby");
        UIManager.Instance.ShowPanel(panelCreatePlayer);
    }

    // IMPORTANTE: Al crear sala, asegurar que se abandona el lobby
    public void OnClickCreateRoom()
    {
        if (!PhotonNetwork.IsConnectedAndReady) return;

        // Salir del lobby antes de crear sala (Photon lo hace automático, pero por las dudas)
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }

        string roomName = GameObject.Find("RoomNameInput")?.GetComponent<TMP_Text>()?.text ?? "Sala_" + Random.Range(0, 999);

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.IsVisible = true;
        options.IsOpen = true;

        PhotonNetwork.CreateRoom(roomName, options);
    }

    // Al salir de la sala, volver al lobby
    public override void OnLeftRoom()
    {
        Debug.Log("Salió de la sala, volviendo al lobby...");
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // Este método se llama automáticamente cuando hay cambios en las salas
        Debug.Log($"Actualización de salas: {roomList.Count} cambios");

        // Aquí puedes actualizar tu UI de lista de salas
        // (Si tienes un RoomListManager, él también recibirá este callback)
    }


    public override void OnCreatedRoom()
    {
        Debug.Log($"Sala creada: {PhotonNetwork.CurrentRoom.Name}");
        UIManager.Instance.ShowPanel(UIManager.Instance.currentRoomPanel);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Unido a sala: {PhotonNetwork.CurrentRoom.Name}");
        UIManager.Instance.ShowPanel(UIManager.Instance.currentRoomPanel);
    }

    public void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}