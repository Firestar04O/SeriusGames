using UnityEngine;
using Photon.Pun;
using TMPro;

public class CurrentRoomMenu : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI roomInfoText;
    public GameObject startButton;

    void OnEnable()
    {
        roomInfoText.text = $"Nombre de la Sala: {PhotonNetwork.CurrentRoom.Name}\n" +
                            $"Nombre del creador: {PhotonNetwork.NickName}\n" +
                           $"Número de jugadores ( {PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers} )";

        // Solo el master ve el botón comenzar
        startButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(1); // GameScene
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        UIManager.Instance.ShowPanel(UIManager.Instance.mainLobbyPanel);
    }

    public override void OnLeftRoom()
    {
        Application.Quit();
    }
}