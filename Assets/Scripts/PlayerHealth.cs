using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerHealth : MonoBehaviourPunCallbacks
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (!photonView.IsMine) return;

        currentHealth -= damage;

        // Llamada RPC para actualizar UI de todos
        photonView.RPC("RPC_UpdateHealth", RpcTarget.All, currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    [PunRPC]
    private void RPC_UpdateHealth(int newHealth)
    {
        currentHealth = newHealth;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthText != null && photonView.IsMine)
        {
            healthText.text = $"Vida: {currentHealth}/{maxHealth}";
        }
    }

    private void Die()
    {
        if (photonView.IsMine)
        {
            // Respawn o desactivar personaje
            photonView.RPC("RPC_Respawn", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_Respawn()
    {
        // Implementar lógica de respawn
        currentHealth = maxHealth;
        UpdateHealthUI();
    }
}