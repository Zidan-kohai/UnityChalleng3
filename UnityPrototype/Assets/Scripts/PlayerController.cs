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

    // Setting variables to lerp the player between two positions
    private Vector3 startPos = new Vector3(-9, 0, 0);
    private Vector3 endPos = new Vector3(-4, 0, 0);

    // Setting variables for the lerp duration of the player
    private float timeToLerp = 1f;
    private float lerpSpeed = 0;

    private Rigidbody _playerRb;
    private Animator _playerAnim;
    private bool _isOnGround = true;
    [SerializeField] MoveLeft Bg;

    public float Score;
    public bool GameOver;
    public int AmountJump;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _playerAnim.SetFloat("Speed_f", 0.3f);
        Physics.gravity *= _gravityModifier;
        Score = 0f;
        StartCoroutine(PlayIntro());

    }


    IEnumerator PlayIntro()
    {
        GameOver = true;

        _dirtParticle.Stop();

        while (lerpSpeed < timeToLerp)
        {
            transform.position = Vector3.Lerp(startPos, endPos, lerpSpeed / timeToLerp);

            lerpSpeed += Time.deltaTime;

            yield return null;
        }

        transform.position = endPos;

        _dirtParticle.Play();

        GameOver = false;

        _playerAnim.SetFloat("Speed_f", 1f);
    }
    void Update()
    {
        dash();
        if (Input.GetKeyDown(KeyCode.Space) && (_isOnGround || AmountJump < 2) && !GameOver)
        {
            Jump();
        }
    }

    public void dash()
    {
        if(Input.GetKey(KeyCode.LeftShift) && !GameOver && _isOnGround){
            _playerAnim.speed = 1.5f;
            Score += Time.deltaTime * 2f;
            Bg.Speed = 45f;
        }else if(!Input.GetKey(KeyCode.LeftShift) || GameOver || !_isOnGround)
        {
            _playerAnim.speed = 1f;
            Bg.Speed = 30f;
            if(!GameOver)
                Score += Time.deltaTime;
        }
    }
    private void Jump()
    {
            AmountJump++;
            _playerRb.AddForce(Vector3.up * (_jumpForce / AmountJump), ForceMode.Impulse);
            _isOnGround = false;
            _playerAnim.SetTrigger("Jump_trig");
            _dirtParticle.Stop();
            _audioSource.PlayOneShot(_jumpSound);
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
