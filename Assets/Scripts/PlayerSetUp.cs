using UnityEngine;
using Unity.Cinemachine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private CinemachineCamera virtualCamera;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private AudioListener audioListener;

    void Start()
    {
        if (photonView.IsMine)
        {
            // Activar cámara y UI solo para el jugador local
            if (playerCamera != null)
                playerCamera.SetActive(true);

            if (virtualCamera != null)
                virtualCamera.Priority = 10;

            if (playerUI != null)
                playerUI.SetActive(true);

            if (audioListener != null)
                audioListener.enabled = true;
        }
        else
        {
            // Desactivar para jugadores remotos
            if (playerCamera != null)
                playerCamera.SetActive(false);

            if (virtualCamera != null)
                virtualCamera.Priority = 0;

            if (audioListener != null)
                audioListener.enabled = false;
        }
    }
}