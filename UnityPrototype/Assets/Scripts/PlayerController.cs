using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityModifier;

    [Header("Particle")]
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private ParticleSystem _dirtParticle;

    [Header("Sound")]
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _crashSound;
    private AudioSource _audioSource;

    public bool GameOver;
    private bool _isOnGround = true;
    public int AmountJump;
    private Rigidbody _playerRb;
    private Animator _playerAnim;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        Physics.gravity *= _gravityModifier;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !GameOver)
        {
            if (_isOnGround || AmountJump < 2)
            {
                AmountJump++;
                _playerRb.AddForce(Vector3.up * (_jumpForce / AmountJump), ForceMode.Impulse);
                _isOnGround = false;
                _playerAnim.SetTrigger("Jump_trig");
                _dirtParticle.Stop();
                _audioSource.PlayOneShot(_jumpSound);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!GameOver)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _isOnGround = true;
                AmountJump = 0;
                _dirtParticle.Play();
            }
            else if (collision.gameObject.CompareTag("Obstacle"))
            {
                GameOver = true;
                _playerAnim.SetBool("Death_b", true);
                _playerAnim.SetInteger("DeathType_int", 1);
                _explosionParticle.Play();
                _dirtParticle.Stop();
                _audioSource.PlayOneShot(_crashSound);
            }
        }

    }
}
