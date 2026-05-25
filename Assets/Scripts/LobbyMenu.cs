using UnityEngine;
using Photon.Pun;
using TMPro;

public class LobbyMenu : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;

    void OnEnable()
    {
        playerNameText.text = $"Jugador: {PhotonNetwork.NickName}";
    }

    public void OpenCreateRoom()
    {
        UIManager.Instance.ShowPanel(UIManager.Instance.createRoomPanel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}