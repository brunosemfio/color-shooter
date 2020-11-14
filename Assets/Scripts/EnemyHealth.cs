using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    #region Components

    private Enemy _enemy;

    #endregion

    #region Inspector

    [SerializeField] private int health;

    [SerializeField] private float invulnerability;

    [SerializeField] private ColorSet colorSet;

    [SerializeField] private ParticleSystem explosionEffect;

    [SerializeField] private ParticleSystem absorbEffect;

    #endregion

    #region Private

    private float _nextVulnerability;

    #endregion

    private void Start()
    {
        _enemy = GetComponent<Enemy>();

        var explosionEffectMain = explosionEffect.main;
        explosionEffectMain.startColor = colorSet.colors[_enemy.ColorIndex];

        var absorbEffectMain = absorbEffect.main;
        absorbEffectMain.startColor = colorSet.colors[_enemy.ColorIndex];
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_nextVulnerability > Time.time) return;
        _nextVulnerability = Time.time + invulnerability;

        if (!other.gameObject.TryGetComponent(out Bullet bullet)) return;

        if (_enemy.ColorIndex != bullet.ColorIndex && --health <= 0)
        {
            //TODO refazer isso aqui
            var explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            var explosionMain = explosion.main;
            explosionMain.stopAction = ParticleSystemStopAction.Destroy;
            explosion.Play();

            Destroy(gameObject); //TODO adicionar no pool

            return;
        }

        absorbEffect.Play();

        Destroy(other.gameObject); //TODO adicionar no pool
    }
}