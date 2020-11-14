using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerShield : MonoBehaviour
{
    #region Components

    private SpriteRenderer _renderer;

    #endregion

    #region Inspector

    [SerializeField] private ColorSet colorSet;

    [SerializeField] private ParticleSystem absorbEffect;

    #endregion

    #region Public

    public int CurrentColorIndex => _colorIndex % colorSet.colors.Length;

    #endregion

    #region Private

    private int _colorIndex = -1;

    #endregion

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();

        ChangeColor();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (enemy.ColorIndex == CurrentColorIndex)
            {
                //TODO encher a barrinha do super

                ShowEffect();

                ChangeColor();

                Destroy(other.gameObject); //TODO adicicionar no pool
            }
        }
    }

    private void ChangeColor()
    {
        _colorIndex++;

        _renderer.color = colorSet.colors[CurrentColorIndex];
    }

    private void ShowEffect()
    {
        var effect = Instantiate(absorbEffect, transform);
        
        var effectMain = effect.main;
        effectMain.startColor = colorSet.colors[CurrentColorIndex];
    }
}