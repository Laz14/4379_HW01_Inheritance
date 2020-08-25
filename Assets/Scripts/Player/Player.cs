using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BallMotor))]
public class Player : MonoBehaviour
{
    //TODO offload health into a Health.cs script
    [SerializeField] int _maxHealth = 3;
    [SerializeField] Text _treasureText;
    int _currentHealth;

    BallMotor _ballMotor;
    int _treasureCount = 0;

    public bool _isInvincible = false;
    Color _originalColor;

    private void Awake()
    {
        _ballMotor = GetComponent<BallMotor>();
        _originalColor = this.GetComponent<Renderer>().material.color;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void FixedUpdate()
    {
        ProcessMovement();  
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth += Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log("Player's health: " + _currentHealth);
    }

    public void DecreaseHealth(int amount)
    {
        if (!_isInvincible)
        {
            _currentHealth -= amount;
            Debug.Log("Player's health: " + _currentHealth);
            if (_currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);
        //play particles
        //play sounds
    }

    public void AddTreasure(int amount)
    {
        _treasureCount += amount;
        _treasureText.text = "Treasures: " + _treasureCount;
    }

    public void Bounce(Transform point, float force)
    {
        Vector3 direction = -1 * (point.position - this.transform.position).normalized;
        _ballMotor.Bounce(direction, force);
        Debug.Log("Bounced " + direction.ToString() + " at force of " + force);
    } 

    private void ProcessMovement()
    {
        //TODO move into Input script
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        _ballMotor.Move(movement);
    }

    public void ChangeColor(Color color)
    {
        this.GetComponent<Renderer>().material.color = color;
    }

    public void RevertColor()
    {
        this.GetComponent<Renderer>().material.color = _originalColor;
    }
}
