using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveController : MonoBehaviour
{
    public int score;
    [SerializeField] Text ScoreText;

    private Rigidbody2D _rigidbody;
    private Animator _animator;//Интерфейс для контроля анимационной системы Mecanim.
    private Vector3 _leftFlip = new Vector3(0, 180, 0);
    private Vector2 _horizontalVelocity;
    private Vector2 _verticalVelocity;
    private float _horizontalSpeed;
    private float _verticalSpeed;
    private float _signPreviosFrame;
    private float _signCurrentFrame;
    public float MoveSpeed;


    private void Start()
    {
        score = PlayerPrefs.GetInt("Score");
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        MoveH();
        MoveV();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish")
        {
            score++;
            PlayerPrefs.SetInt("Score", score);
            SceneManager.LoadScene(1);
        }
    }
    private void Update()
    {
        ScoreText.text = score.ToString();
        _horizontalSpeed = Input.GetAxis("Horizontal");
        _verticalSpeed = Input.GetAxis("Vertical");
        Flip();
        Animate();
    }


    private void MoveV()
    {
        _horizontalVelocity.Set(_horizontalSpeed * MoveSpeed, _rigidbody.velocity.x);
        _rigidbody.velocity = _horizontalVelocity;
    }
    private void MoveH()
    {
        _verticalVelocity.Set(_verticalSpeed * MoveSpeed, _rigidbody.velocity.y);
        _rigidbody.velocity = _verticalVelocity;
    }

    private void Flip()
    {
        _signCurrentFrame = _horizontalSpeed == 0 ? _signPreviosFrame : Mathf.Sign(_horizontalSpeed);

        if (_signCurrentFrame != _signPreviosFrame)
        {
            transform.rotation = Quaternion.Euler(_horizontalSpeed < 0 ? _leftFlip : Vector3.zero);
        }
        _signPreviosFrame = -_signCurrentFrame;
    }

    private void Animate()
    {
        _animator.SetBool("IsRun", _horizontalSpeed != 0 || _verticalSpeed != 0 ? true : false);
    }
}
