using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public int life;
    public Rigidbody2D rb2d;
    private Animator anim;
    public float speed;

    [SerializeField] private Transform frontCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask playerLayer;
    private Transform player;
    private bool isNearPlayer;
    private bool isAttacking;
    private float attackCooldown = 1.5f;
    private float lastAttackTime;

    [SerializeField] private Transform swordUP;
    [SerializeField] private Transform swordDOWN;
    [SerializeField] private Transform swordLEFT;
    [SerializeField] private Transform swordRIGHT;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (life <= 0)
        {
            Debug.Log("Morreu");
            Destroy(gameObject);
            return;
        }

        DetectPlayer();
        Move();
        UpdateAnimation();
    }

    private void DetectPlayer()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, 5f, playerLayer);
        isNearPlayer = players.Length > 0;
    }

    private void Move()
    {
        if (isNearPlayer && !isAttacking)
        {
            MoveToPlayer();
        }
        else
        {
            RandomMovement();
        }
    }

    private void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, player.position) <= 1.2f)
        {
            Attack();
        }
    }

    private void RandomMovement()
    {
        // Implementar lógica de movimento aleatório aqui, se necessário
        // Por exemplo, mudar de direção aleatoriamente após um certo tempo
    }

    private void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Collider2D[] colliders = new Collider2D[]
            {
                Physics2D.OverlapCircle(swordUP.position, 0.8f, playerLayer),
                Physics2D.OverlapCircle(swordDOWN.position, 0.8f, playerLayer),
                Physics2D.OverlapCircle(swordLEFT.position, 0.8f, playerLayer),
                Physics2D.OverlapCircle(swordRIGHT.position, 0.8f, playerLayer)
            };

            foreach (var collider in colliders)
            {
                if (collider != null)
                {
                    isAttacking = true;
                    lastAttackTime = Time.time;
                    StartCoroutine(PerformAttack());
                    break;
                }
            }
        }
    }

    private IEnumerator PerformAttack()
    {
        anim.SetTrigger("Attack"); // Supondo que você tenha um trigger de ataque na animação
        yield return new WaitForSeconds(0.5f); // Tempo para a animação de ataque

        // Verifica novamente se o jogador está na área de ataque
        Collider2D[] colliders = new Collider2D[]
        {
            Physics2D.OverlapCircle(swordUP.position, 0.8f, playerLayer),
            Physics2D.OverlapCircle(swordDOWN.position, 0.8f, playerLayer),
            Physics2D.OverlapCircle(swordLEFT.position, 0.8f, playerLayer),
            Physics2D.OverlapCircle(swordRIGHT.position, 0.8f, playerLayer)
        };

        foreach (var collider in colliders)
        {
            if (collider != null)
            {
                collider.GetComponent<PlayerScript>().takeDamage();
                break;
            }
        }

        isAttacking = false; // Reseta o estado de ataque
    }

    private void UpdateAnimation()
    {
        if (isAttacking)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", isNearPlayer);
        }
    }
}