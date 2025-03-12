/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public levelGenerator lvGen;
    public SpriteRenderer sprRender;
    public Transform checkLeft;
    public Transform checkRight;
    public Transform checkUp;
    public Transform checkDown;
    public float CheckWallRadius;
    public LayerMask wallLayer;
    
    public bool Right;
    public bool Left;
    public bool Up;
    public bool Down;

    [SerializeField] Sprite WallDefeault;
    [SerializeField] Sprite WallTurnLeftDown;
    [SerializeField] Sprite WallTurnRightDown;
    [SerializeField] Sprite WallTurnLeftUp;
    [SerializeField] Sprite WallTurnRightUp;
    [SerializeField] Sprite WallLeft;
    [SerializeField] Sprite WallRight;
    [SerializeField] Sprite WallEnd;

    public void Start(){
        sprRender = GetComponent<SpriteRenderer>();
    }
    public void FixedUpdate(){
        CheckAll();
    }

    public void CheckAll()
    {
        WallDownCheck();
        WallLeftCheck();
        WallRightCheck();
        WallUpCheck();

        if(Right == true && Left == true && Down == false){
            sprRender.sprite = WallDefeault;
        }
        else if(Right == false && Left == false && Down == false && Up == true){
            sprRender.sprite = WallEnd;
        }
        else if(Right == true && Left == false && Down == false && Up == true){
            sprRender.sprite = WallTurnLeftUp;
        }
        else if(Right == false && Left == true && Down == false && Up == true){
            sprRender.sprite = WallTurnRightUp;
        }
        else if(Right == true && Left == false && Down == false){
            sprRender.sprite = WallTurnLeftUp;
        }
        else if(Right == false && Left == true && Down == true){
            sprRender.sprite = WallTurnRightDown;
        }
        else if(Right == true && Left == false && Down == true){
            sprRender.sprite = WallTurnLeftDown;
        }
        else if(Right == false && Left == false && Down == true){
            sprRender.sprite = WallLeft;
        }
         else if(Right == true && Left == false && Up == true){
            sprRender.sprite = WallTurnLeftUp;
        }
        
        
    }

    void WallRightCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkRight.position, CheckWallRadius, wallLayer);
        if(colliders.Length > 0 ){
            Right = true;
        }
        else{
            Right = false;
        }
    }

    void WallLeftCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkLeft.position, CheckWallRadius, wallLayer);
        if(colliders.Length > 0 ){
            Left = true;
        }
        else{
            Left = false;
        }
    }

    void WallUpCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkUp.position, CheckWallRadius, wallLayer);
        if(colliders.Length > 0 ){
            Up = true;
        }
        else{
            Up = false;
        }
    }

    void WallDownCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkDown.position, CheckWallRadius, wallLayer);
        if(colliders.Length > 0 ){
            Down = true;
        }
        else{
            Down = false;
        }
    }
} */
