using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class vidaObjeto : MonoBehaviourPun
{
    public int health = 100;
    public float damage;
    public float attackSpeed;

    // Para health slider

    public float damageLerpDuration;
    private float currentHealth;
    private float targetHealth;
    private Coroutine damageCoroutine;

    HealthUI healthUI;

    private void Awake()
    {
        healthUI = GetComponent<HealthUI>();
        currentHealth = health;
        targetHealth = health;

        healthUI.Start3DSlider(health);
    }

    // Testing

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            takeDamage(gameObject, damage);
        }
    }

    // Usar en el reparar
    public void SetVida(int nuevaVida)
    {
        if ((currentHealth + health) >= health)
        {
            currentHealth = health;
        }
        else
        {
            currentHealth = nuevaVida;
        }
    }


    public void takeDamage(GameObject target, float damageAmount)
    {
        vidaObjeto targetStats = target.GetComponent<vidaObjeto>();
        targetStats.targetHealth -= damageAmount;

        if (targetStats.targetHealth <= 0)
        {
            PhotonView targetPhotonView = target.GetComponent<PhotonView>();
            
            if(targetPhotonView.IsMine){
                PhotonNetwork.Destroy(target);
            }
            
        }
        else if (targetStats.damageCoroutine == null)
        {
            targetStats.StartLerpHealth();
        }
    }

    private void StartLerpHealth()
    {
        if (damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(LerpHealth());
        }
    }

    private IEnumerator LerpHealth()
    {
        float elapsedTime = 0;
        float initialHealth = currentHealth;
        float target = targetHealth;

        while (elapsedTime < damageLerpDuration)
        {
            currentHealth = Mathf.Lerp(initialHealth, target, elapsedTime / damageLerpDuration);
            UpdateHealthUI();

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentHealth = target;
        UpdateHealthUI();

        damageCoroutine = null;
    }


    private void UpdateHealthUI()
    {
        healthUI.Update3DSlider(currentHealth);
    }


}
