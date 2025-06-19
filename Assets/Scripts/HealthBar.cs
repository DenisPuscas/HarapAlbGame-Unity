using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    public Text healthCounter;

    private float currentHealth;
    public float maxHealth;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        slider.value = currentHealth / maxHealth;
        healthCounter.text = currentHealth + "/" + maxHealth;
        
        /*if (Input.GetKey(KeyCode.M))
        {
            currentHealth -= 10;
        }*/
    }
}
