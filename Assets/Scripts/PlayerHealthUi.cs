using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUi : Singleton<PlayerHealthUi>
{
    [SerializeField] private Image hit;

    [SerializeField] private Image current;

    [SerializeField] private float decreaseDelay;
    
    [SerializeField] private float decreaseSpeed;

    public void Damage(float totalHealth, float currentHealth)
    {
        var percentage = currentHealth / totalHealth;

        if (percentage < 0) percentage = 0;

        current.fillAmount = percentage;
        
        StartCoroutine(DecreaseHealth());
    }

    private IEnumerator DecreaseHealth()
    {
        yield return new WaitForSeconds(decreaseDelay);
        
        var amount = (hit.fillAmount - current.fillAmount) / decreaseSpeed;

        while (hit.fillAmount > current.fillAmount)
        {
            hit.fillAmount -= amount * Time.deltaTime;

            yield return null;
        }
    }
}