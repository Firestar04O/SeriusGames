using UnityEngine;
using Photon.Pun;

public class PlayerShoot : MonoBehaviourPunCallbacks
{
    [SerializeField] private int damage = 25;
    [SerializeField] private float shootRange = 100f;
    [SerializeField] private LayerMask shootableLayer;
    [SerializeField] private GameObject muzzleFlash;

    void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        // Activar muzzle flash localmente
        if (muzzleFlash != null)
            muzzleFlash.SetActive(true);

        if (Physics.Raycast(ray, out hit, shootRange, shootableLayer))
        {
            // Llamada RPC para daño
            photonView.RPC("RPC_TakeDamage", RpcTarget.All, hit.collider.gameObject.GetPhotonView().ViewID, damage);
        }

        // Desactivar muzzle flash después de un frame
        StartCoroutine(DisableMuzzleFlash());
    }

    [PunRPC]
    private void RPC_TakeDamage(int targetViewID, int damageAmount)
    {
        PhotonView targetView = PhotonView.Find(targetViewID);
        if (targetView != null)
        {
            PlayerHealth health = targetView.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
        }
    }

    private System.Collections.IEnumerator DisableMuzzleFlash()
    {
        yield return new WaitForSeconds(0.05f);
        if (muzzleFlash != null)
            muzzleFlash.SetActive(false);
    }
}