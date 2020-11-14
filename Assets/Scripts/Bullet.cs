using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    #region Components

    private Rigidbody2D _rb;

    #endregion

    #region Inspector

    [SerializeField] private float lifeTime;

    [SerializeField] private ColorSet colorSet;

    [SerializeField] private SpriteRenderer[] background;

    #endregion
    
    #region Private

    private Vector2 _force;

    private int _colorIndex;

    #endregion

    #region Public

    public int ColorIndex => _colorIndex;

    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        _rb.velocity = _force;

        _rb.rotation += 10;
    }

    public void Fire(Vector2 force, int colorIndex)
    {
        _force = force;

        _colorIndex = colorIndex;
        
        UpdateColor();
    }

    private void UpdateColor()
    {
        foreach (var spriteRenderer in background)
        {
            spriteRenderer.color = colorSet.colors[_colorIndex];
        }
    }
}