using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
  [SerializeField] public float maxHealth;
  [SerializeField] private float currentHealth;
  public FloatEvent OnDamageTaken;
  public FloatEvent OnHeal;
  public UnityEvent OnDeath;

  // test values REMOVE BEFORE MERGING
  [SerializeField]
  float testHealAmount = 5f;
  [SerializeField]
  float testDamageAmount = -5f;
  // test code end
  
  /// <summary>
  /// Initializes max health to current health.
  /// </summary>
  void Start()
  {
      currentHealth = maxHealth;
  }

  void Update()
  {
    // Test code REMOVE BEFORE MERGING
    if (Input.GetKeyDown(KeyCode.Q)) 
    {
      ChangeHealth(testHealAmount);
    }
    if (Input.GetKeyDown(KeyCode.E)) 
    {
      ChangeHealth(testDamageAmount);
    }
    // Test code end
  }

  /// <summary>
  /// Changes health by the value specified amount. Also invokes events associated with the health changes.
  /// </summary>
  /// <param name="amount">Net amount to change health by. Negative changes cause damage. Positive changes cause healing.</param>
  public void ChangeHealth(float amount) 
  {
    currentHealth += amount;

    // Prevent healing past max health
    if(currentHealth > maxHealth)
    {
      currentHealth = maxHealth;
    }

    /// If healing occured the OnHeal event will be invoked.
    /// If health dropped below 0 OnDeath will be invoked.
    /// Otherwise if damage occured OnDamageTaken will be invoked.
    if(amount > 0) 
    {
      OnHeal.Invoke(amount);
    }
    else if(currentHealth <= 0)
    {
      OnDeath.Invoke();
    }
    else if(amount < 0) 
    {
      // amount is negated so that a positive value is passed as the event parameter.
      OnDamageTaken.Invoke(-amount);
    }
  }

  // Test code REMOVE BEFORE MERGE
  public void HealResponse(float value)
  {
    Debug.Log("Heal " + value);
  }
  public void DeathResponse()
  {
    Debug.Log("Death comes");
  }
  public void DamageResponse(float value)
  {
    Debug.Log("Damage " + value);
  }
  // Test code end
}
