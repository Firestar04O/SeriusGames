using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class CreateRoomMenu : MonoBehaviour
{
    public TMP_InputField roomNameInput;
    public TextMeshProUGUI playerCountText;

    private int playerCount = 2;
    private const int MAX_PLAYERS = 10;

    public void IncrementCount()
    {
        playerCount = Mathf.Min(playerCount + 1, MAX_PLAYERS);
        playerCountText.text = playerCount.ToString();
    }

    public void DecrementCount()
    {
        playerCount = Mathf.Max(playerCount - 1, 2);
        playerCountText.text = playerCount.ToString();
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInput.text)) return;

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = (byte)playerCount;

        PhotonNetwork.CreateRoom(roomNameInput.text, options);
    }

    public void Cancel()
    {
        UIManager.Instance.ShowPanel(UIManager.Instance.mainLobbyPanel);
    }
}