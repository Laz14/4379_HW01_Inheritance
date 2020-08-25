using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class PowerUpBase : MonoBehaviour
{
    [SerializeField] float powerupDuration = 2;

    [SerializeField] float _movementSpeed = 3;
    protected float MovementSpeed { get => _movementSpeed; }

    [SerializeField] ParticleSystem _collectParticles;
    [SerializeField] AudioClip _collectSound;

    Rigidbody _rb;

    bool _active = false;

    Player _player;

    protected abstract void PowerUp(Player player);
    protected abstract void PowerDown(Player player);

    private void OnTriggerEnter(Collider other)
    {
        _player = other.gameObject.GetComponent<Player>();
        if (_player != null)
        {
            PowerUp(_player);
            //spawn particles and sfx because we need to disable object
            Feedback();

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            _active = true;
        }
    }

    private void Feedback()
    {
        //particles
        if (_collectParticles != null)
        {
            _collectParticles = Instantiate(_collectParticles, transform.position, Quaternion.identity);
        }
        //audio. TODO - consider object pooling for performance
        if (_collectSound != null)
        {
            AudioHelper.PlayClip2D(_collectSound, 1f);
        }
    }

    protected virtual void Movement(Rigidbody rb)
    {
        //calculate rotation
        Quaternion turnOffset = Quaternion.Euler(0, _movementSpeed, 0);
        rb.MoveRotation(_rb.rotation * turnOffset);
    }

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_active)
        {
            if (powerupDuration <= 0)
            {
                this.gameObject.SetActive(false);
                PowerDown(_player);
            }

            powerupDuration -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Movement(_rb);
    }
}
