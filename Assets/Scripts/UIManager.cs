using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Paneles")]
    public GameObject createPlayerPanel;
    public GameObject mainLobbyPanel;
    public GameObject createRoomPanel;
    public GameObject currentRoomPanel;

    void Awake()
    {
        Instance = this;
    }

    public void ShowPanel(GameObject panel)
    {
        createPlayerPanel.SetActive(false);
        mainLobbyPanel.SetActive(false);
        createRoomPanel.SetActive(false);
        currentRoomPanel.SetActive(false);

        panel.SetActive(true);
    }
}