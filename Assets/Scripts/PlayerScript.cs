using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public Rigidbody2D rb;
    public Animator anim;
    private Vector2 _movement;
    public int life;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastVertical = "LastVertical";
    private const string _lastHorizontal = "LastHorizontal";

    public float __lastHorizontal;
    public float __lastVertical;

    private GameObject Vcam;
    public Transform TreasureCheckPoint;
    public LayerMask treasures;
    public TreasureScript treasure;

    public int colectedTreasures = 0;

    [SerializeField] Transform SwordUP;
    [SerializeField] Transform SwordDOWN;
    [SerializeField] Transform SwordLEFT;
    [SerializeField] Transform SwordRIGHT;
    [SerializeField] LayerMask Enemies;
    [SerializeField] int attack;
    private bool isAttacking;
    private float attackCooldown = 0.5f; // Cooldown de ataque
    private float lastAttackTime;

    public Transform spawnPoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        Vcam = GameObject.FindWithTag("vcam");
        Vcam.GetComponent<VcamScript>().SetFollow();
    }

    void Update()
    {
        if (spawnPoint == null)
        {
            spawnPoint = GameObject.FindGameObjectsWithTag("Respawn")[0].GetComponent<Transform>();
        }

        if (life <= 0)
        {
            Debug.Log("Morri");
            transform.position = spawnPoint.position;
            life = 5;
        }

        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        rb.velocity = _movement * moveSpeed;

        anim.SetFloat(_horizontal, _movement.x);
        anim.SetFloat(_vertical, _movement.y);

        if (_movement != Vector2.zero)
        {
            anim.SetFloat(_lastHorizontal, _movement.x);
            anim.SetFloat(_lastVertical, _movement.y);
            __lastHorizontal = _movement.x;
            __lastVertical = _movement.y;
        }

        if (Input.GetButtonDown("Jump") && Time.time >= lastAttackTime + attackCooldown)
        {
            isAttacking = true;
            Attack();
            lastAttackTime = Time.time; // Atualiza o tempo do último ataque
        }

        TreasureCheck();
    }

    void FixedUpdate()
    {
        if (colectedTreasures >= 5)
        {
            Debug.Log("Ganhou");
            SceneManager.LoadScene("win", LoadSceneMode.Single);
        }
    }

    public void Rewhite()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void TreasureCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(TreasureCheckPoint.position, 0.5f, treasures);
        if (colliders.Length > 0 && !colliders[0].gameObject.GetComponent<TreasureScript>().isColected)
        {
            StartCoroutine(Find());
        }
    }

    public IEnumerator Find()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(TreasureCheckPoint.position, 0.5f, treasures);
        if (colliders.Length > 0 && !colliders[0].gameObject.GetComponent<TreasureScript>().isColected)
        {
            treasure = colliders[0].gameObject.GetComponent<TreasureScript>();
            treasure.isColected = true;
            colectedTreasures++;
            Debug.Log("Player found");
            yield return new WaitForSeconds(1);
            Destroy(colliders[0].gameObject);
        }
    }

    void Attack()
    {
        Debug.Log("entrou no ataque");
        anim.SetBool("isAttacking", true);
        anim.SetTrigger("isAttacking");

        Vector2 attackDirection = new Vector2(__lastHorizontal, __lastVertical);
        PerformAttack(attackDirection);
    }

    void PerformAttack(Vector2 direction)
    {
        Transform swordPosition = null;

        if (direction.x < 0) // Ataque para a esquerda
        {
            swordPosition = SwordLEFT;
        }
        else if (direction.x > 0) // Ataque para a direita
        {
            swordPosition = SwordRIGHT;
        }
        else if (direction.y > 0) // Ataque para cima
        {
            swordPosition = SwordUP;
        }
        else if (direction.y < 0) // Ataque para baixo
        {
            swordPosition = SwordDOWN;
        }

        if (swordPosition != null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(swordPosition.position, 1f, Enemies);
            if (colliders.Length > 0)
            {
                foreach (Collider2D enemy in colliders)
                {
                    enemy.GetComponent<EnemyScript>().takeDamage();
                }
            }
        }

        isAttacking = false; // Reseta o estado de ataque
    }

    public void takeDamage()
    {
        life--;
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("Rewhite", 0.5f);
        Debug.Log("levei");
    }
}