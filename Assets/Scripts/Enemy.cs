using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    #region Components

    private Rigidbody2D _rb;

    private SpriteRenderer _renderer;

    #endregion

    #region Inspector

    [SerializeField] private float moveSpeed;

    [SerializeField] private float lifeTime;

    [SerializeField] private ColorSet colorSet;

    #endregion

    #region Private

    private Transform _player;

    private Vector2 _dir;

    private int _colorIndex;

    #endregion

    #region Public

    public int ColorIndex => _colorIndex;

    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _renderer = GetComponentInChildren<SpriteRenderer>();

        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _colorIndex = Random.Range(0, colorSet.colors.Length);

        _renderer.color = colorSet.colors[_colorIndex];

        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        _dir = (_player.position - transform.position).normalized;

        FacePlayer();

        FollowPlayer();
    }

    private void FollowPlayer()
    {
        _rb.velocity = _dir * moveSpeed;
    }

    private void FacePlayer()
    {
        var angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;

        _rb.MoveRotation(angle);
    }
}