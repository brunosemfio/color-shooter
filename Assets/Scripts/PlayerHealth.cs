using Cinemachine;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class PlayerHealth : MonoBehaviour
{
    #region Components

    private CinemachineImpulseSource _impulseSource;

    #endregion

    #region Inspector

    [SerializeField] private int totalHealth;

    [SerializeField] private float invulnerability;

    #endregion

    #region Private

    private int _currentHealth;

    private float _nextVulnerability;

    #endregion

    private void Start()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();

        _currentHealth = totalHealth;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;

        Destroy(other.gameObject); // TODO adicionar no pool

        if (_nextVulnerability > Time.time) return;
        _nextVulnerability = Time.time + invulnerability;

        _currentHealth--;

        _impulseSource.GenerateImpulse();

        PlayerHealthUi.Instance.Damage(totalHealth, _currentHealth);

        if (_currentHealth <= 0)
        {
            Debug.Log("PERDEU!!!");
        }
    }
}