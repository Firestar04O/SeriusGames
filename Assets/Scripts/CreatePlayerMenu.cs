using UnityEngine;
using Photon.Pun;
using TMPro;

public class CreatePlayerMenu : MonoBehaviour
{
    public TMP_InputField nicknameInput;

    public void AcceptNickname()
    {
        string name = nicknameInput.text;
        if (string.IsNullOrEmpty(name)) name = "Player";

        PhotonNetwork.NickName = name;
        UIManager.Instance.ShowPanel(UIManager.Instance.mainLobbyPanel);
    }
}