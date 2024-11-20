using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CloneDetection : MonoBehaviour
{
    [SerializeField]private bool isSwappable;
    private string GroundTag = "Ground";
    private string MovingGroundTag = "MovingGround";
    private string DeathTag = "Death";
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask movinggroundLayer;
    [SerializeField] private Transform PlayerClone;
    private SpriteRenderer SpriteRenderer;

    private void Start() 
    {
        rb.isKinematic = true;
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() 
    {
        if (!SpriteRenderer.isVisible)
        {
            CantSwap();
        }
    }
 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == GroundTag||collision.tag == MovingGroundTag||collision.tag == DeathTag)
        {
            CantSwap();
        }
        else
        {
            CanSwap();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == GroundTag||collision.tag == MovingGroundTag||collision.tag == DeathTag)
        {
            CanSwap();
        }
    }
    

    private void CantSwap()
    {
        isSwappable = false;
        SpriteRenderer.color =  new Color32(255, 80 ,80 ,180);
        Debug.Log("接触中！");
    }
    private void CanSwap()
    {
        isSwappable = true;
        SpriteRenderer.color =  new Color32(100, 200, 255, 180);
        Debug.Log("接触終了！");
    }

    public bool GetSwappable()
    {
        return isSwappable;
    }


}