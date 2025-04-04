using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DiamondScript : MonoBehaviour
{
    public TMP_Text attackBox;
    public InputActionReference direction;
    public GameObject arrow;
    public string key;
    public bool hit, aggro;
    public Color black, violet;
    //public int damage = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction.action.started += Direction;
        direction.action.canceled += ChangeColour;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Direction(InputAction.CallbackContext context)
    {
        if(arrow != null)
        {
            //GameplayManager.Instance.ui.dialogue.text += " <b>" + arrow.GetComponent<ArrowScript>().word.text + "</b>";
            attackBox.text += " <b>" + arrow.GetComponent<ArrowScript>().word.text + "</b>";
            gameObject.GetComponent<SpriteRenderer>().color = violet;
            hit = false;
            Destroy(arrow);
            arrow = null;
            GameplayManager.Instance.UpdateEnemyHealth(GameplayManager.Instance.player.damage);
            GameplayManager.Instance.damageDealt += GameplayManager.Instance.player.damage;

            if(aggro)
            {
                //update aggression;
            }
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = black;
            StartCoroutine(Stun(2f));
        }
    }

    public void ChangeColour(InputAction.CallbackContext context)
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255,255);
    }

    public IEnumerator ColourBlack(int time)
    {
        gameObject.GetComponent<SpriteRenderer>().color = black;

        yield return new WaitForSeconds(time);
        
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255,255);
    }

    public IEnumerator Stun(float stunTime)
    {
        direction.action.started -= Direction;
        direction.action.canceled -= ChangeColour;

        yield return new WaitForSeconds(stunTime/2);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255,255);

        yield return new WaitForSeconds(stunTime/2);

        direction.action.started += Direction;
        direction.action.canceled += ChangeColour;
    }

    void Enable()
    {
        attackBox.text = "";
    }

    void OnDisable()
    {
        direction.action.started -= Direction;
        direction.action.canceled -= ChangeColour;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        
        if(collider.gameObject.GetComponent<ArrowScript>())
        {
            if(key == collider.gameObject.GetComponent<ArrowScript>().key)
            {
                if(arrow == null)
                {
                    arrow = collider.gameObject;
                    hit = true;
                }
                else if(arrow != collider.gameObject)
                {
                    Destroy(collider.gameObject);
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if(hit)
        {
            //GameplayManager.Instance.ui.dialogue.text += " <i>" + arrow.GetComponent<ArrowScript>().word.text + "</i>";
            attackBox.text += " <i>" + arrow.GetComponent<ArrowScript>().word.text + "</i>";
            StartCoroutine(ColourBlack(1));
            hit = false;
            //update player health
            Destroy(arrow);
            arrow = null;
            //Debug.Log("Damage");
        }
    }
}
