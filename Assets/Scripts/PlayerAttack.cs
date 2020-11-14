using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Components

    private PlayerShield _shield;

    #endregion

    #region Inspector

    [SerializeField] private Transform firePoint;

    [SerializeField] private Bullet bullet;

    [SerializeField] private float force;

    [SerializeField] private float rate;

    #endregion

    #region Private

    private float _nextFire;

    #endregion

    private void Start()
    {
        _shield = GetComponentInChildren<PlayerShield>();
    }

    private void Update()
    {
        if (_nextFire > Time.time) return;

        var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        newBullet.Fire(-transform.right * force, _shield.CurrentColorIndex);

        _nextFire = Time.time + 1 / rate;
    }
}