using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomListManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform contentParent;

    private Dictionary<string, Button> roomButtons = new Dictionary<string, Button>();

    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady && !PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("En el lobby - esperando salas...");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log($"{roomList.Count} salas actualizadas");

        foreach (RoomInfo room in roomList)
        {
            if (room.RemovedFromList)
            {
                if (roomButtons.ContainsKey(room.Name))
                {
                    Destroy(roomButtons[room.Name].gameObject);
                    roomButtons.Remove(room.Name);
                }
            }
            else if (room.IsOpen && room.IsVisible)
            {
                if (!roomButtons.ContainsKey(room.Name))
                {
                    // Crear botón dinámicamente
                    GameObject buttonObj = new GameObject(room.Name);
                    buttonObj.transform.SetParent(contentParent);

                    // Layout automático
                    LayoutElement layout = buttonObj.AddComponent<LayoutElement>();
                    layout.preferredHeight = 50;

                    // Texto del botón
                    TextMeshProUGUI text = buttonObj.AddComponent<TextMeshProUGUI>();
                    text.text = $"{room.Name} ({room.PlayerCount}/{room.MaxPlayers})";
                    text.fontSize = 24;
                    text.alignment = TextAlignmentOptions.Center;
                    text.color = Color.white;

                    // Componente botón
                    Button button = buttonObj.AddComponent<Button>();
                    ColorBlock colors = button.colors;
                    colors.normalColor = new Color(0.2f, 0.2f, 0.2f);
                    colors.highlightedColor = new Color(0.3f, 0.3f, 0.3f);
                    button.colors = colors;

                    string roomName = room.Name;
                    button.onClick.AddListener(() => {
                        Debug.Log($"Uniéndose a {roomName}");
                        PhotonNetwork.JoinRoom(roomName);
                    });

                    roomButtons.Add(room.Name, button);
                }
                else
                {
                    // Actualizar texto de jugadores
                    TextMeshProUGUI text = roomButtons[room.Name].GetComponent<TextMeshProUGUI>();
                    text.text = $"{room.Name} ({room.PlayerCount}/{room.MaxPlayers})";
                }
            }
        }
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.JoinLobby();
    }
}