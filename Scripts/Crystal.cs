using UnityEngine;
using UnityEngine.UI;

public class Crystal : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;

    public float rotationSpeed;

    void Update()
    {
        Vector3 _rotation = new Vector3(0, rotationSpeed, 0);
        transform.eulerAngles += _rotation * Time.deltaTime;
    }

    // IDamageable
    public void TakeDamage(int _value)
    {
        health -= _value;

        // Call UI function
        MyUI.healthValueChanged.Invoke(health);

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);

        SceneLoader.Died(true);
        SceneLoader.ChangeSceneStatic("StartScreen");
    }
}