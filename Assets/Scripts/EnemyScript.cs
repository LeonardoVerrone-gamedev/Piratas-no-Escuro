using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int life;
    public Vector3 direction;

    public Transform target;
    public bool near;

    public Rigidbody2D rb;
    public float speed;

    public Animator anim;

    public LayerMask PlayerLayer;
    public bool closeEnought;

    public bool walking;
    public bool attacking;

    public Transform UPCheck;
    public Transform DOWNCheck;
    public Transform LEFTCheck;
    public Transform RIGHTCheck;

    public bool Started = false;



    

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        if(life <= 0){
            Debug.Log("morreu");
            Destroy(gameObject);
        }

        
        if(target != null){
            direction = target.position;
            
            Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if(near){
                if(!closeEnought){
                    rb.MovePosition(pos);
            }else{
                rb.velocity = Vector2.zero;
            }
            }

            if(Vector2.Distance(target.position, transform.position) <= 1f){
                closeEnought = true;
                walking = false;
                if(!Started){
                    StartCoroutine(Attack());
                    Started = true;
                }
                
            }else{
                closeEnought = false;
                walking = true;
                attacking = false;
            }


            if(walking){
                if(target.position.y > transform.position.y){
                    anim.SetBool("WalkingUP", true);
                    anim.SetBool("WalkingDOWN", false);
                }else{
                    anim.SetBool("WalkingDOWN", true);
                    anim.SetBool("WalkingUP", false);
                }
            }


            Collider2D[] colFind = Physics2D.OverlapCircleAll(transform.position, 7f, PlayerLayer);

            if(colFind.Length > 0){
                near = true;
            }else{
                near = false;
            }


        }else{
            target = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Transform>();
        }
    }

    public IEnumerator Attack(){
            Debug.Log("ShouldAttack");
            yield return new WaitForSeconds(0.5f);
            attacking = true;
            

            Collider2D[] up = Physics2D.OverlapCircleAll(UPCheck.position, 0.5f, PlayerLayer);
            Collider2D[] down = Physics2D.OverlapCircleAll(DOWNCheck.position, 0.5f, PlayerLayer);
            Collider2D[] left = Physics2D.OverlapCircleAll(LEFTCheck.position, 0.5f, PlayerLayer);
            Collider2D[] right = Physics2D.OverlapCircleAll(RIGHTCheck.position, 0.5f, PlayerLayer);

            if(up.Length > 0 && up[0].gameObject.GetComponent<Transform>().tag == "Player" && attacking){
                anim.Play("Base Layer.ATTACKING UP", 0);
          
                up[0].gameObject.GetComponent<PlayerScript>().takeDamage();
                
            }
            if(down.Length > 0 && down[0].gameObject.GetComponent<Transform>().tag == "Player" && attacking){
                anim.Play("Base Layer.ATTACKING DOWN", 0);
            
                down[0].gameObject.GetComponent<PlayerScript>().takeDamage();
                
            }
            if(left.Length > 0 && left[0].gameObject.GetComponent<Transform>().tag == "Player" && attacking){
                left[0].gameObject.GetComponent<PlayerScript>().takeDamage();
                anim.Play("Base Layer.ATTACKING LEFT", 0);
            }
            if(right.Length > 0 && right[0].gameObject.GetComponent<Transform>().tag == "Player" && attacking){
                right[0].gameObject.GetComponent<PlayerScript>().takeDamage();
                anim.Play("Base Layer.ATTACKING RIGHT", 0);
            }
            Started = false;
            yield return new WaitForSeconds(0.5f);
            attacking = false;
            //StopCoroutine(Attack());
        }

    public void takeDamage(){
        GetComponent<SpriteRenderer>().color = Color.red;
        life--;
        Invoke("Rewhite", 0.5f);
    }

    public void Rewhite(){
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
