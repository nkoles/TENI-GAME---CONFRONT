using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class ConfrontScript : MonoBehaviour
{
    public EnemyManager enemy;
    public Button[] buttons;
    public Image[] images;
    public TMP_Text[] text, description;
    public Sprite[] schoolSprites, homeSprites, clinicSprites;
    public string[] schoolText, homeText, clinicText, schoolDescription, homeDescriptions, clinicDescription;
    public int[] weapons;
    public int selectedWeapon;
    public UnityAction[] click;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameplayManager.Instance.enemy;
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(click[i]);
            //buttons[i].gameObject.GetComponent<EventTrigger>().OnPointerEnter.AddListener();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectedButton(int button)
    {
        selectedWeapon = button;
    }

    public void OnClick()
    {
        switch(enemy.phase)
        {
            case 1:
            {
                enemy.schoolObstacles[selectedWeapon] = null;

                break;
            }

            case 2:
            {
                enemy.homeObstacles[selectedWeapon] = null;

                break;
            }

            case 3:
            {
                enemy.clinicObstacles[selectedWeapon] = null;

                break;
            }

            default:
            {

                break;
            }
        }
    }

    void OnPointerEnter()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            switch(enemy.phase)
            {
                case 1:
                {
                    description[i].text = schoolText[weapons[i]];

                    break;
                }

                case 2:
                {
                    description[i].text = homeText[weapons[i]];

                    break;
                }

                case 3:
                {
                    description[i].text = clinicText[weapons[i]];

                    break;
                }

                default:
                {

                    break;
                }
            }
        }
    }

    void OnDisable()
    {
        for(int i = 0; i < click.Length; i++)
        {
            click[i] -= OnClick;
            buttons[i].gameObject.GetComponent<ConfrontButtonHandler>().hoverEnter -= OnPointerEnter;
            //buttons[i].gameObject.GetComponent<ConfrontButtonHandler>().hoverExit -= OnPointerExit();
        }
    }

    void OnEnable()
    {
        List<GameObject> objects = new List<GameObject>();

        switch(enemy.phase)
        {
            case 1:
            {
                foreach(GameObject thing in enemy.schoolObstacles)
                {
                    if(thing != null)
                    {
                        objects.Add(thing);
                    }
                }

                for(int i = 0; i < buttons.Length; i++)
                {
                    if(objects.Count <= 0)
                    {
                        break;
                    }

                    int randObject = Random.Range(0, objects.Count);
                    for(int j = 0; j < enemy.schoolObstacles.Length; j++)
                    {
                        if(objects[randObject] == enemy.schoolObstacles[j])
                        {
                            images[i].sprite = schoolSprites[j];
                            text[i].text = schoolText[j];
                            //text[i].description = schoolDescription[j];
                            click[i] += OnClick;
                            weapons[i] = j;

                            buttons[i].gameObject.GetComponent<ConfrontButtonHandler>().hoverEnter += OnPointerEnter;
                            //buttons[i].gameObject.GetComponent<ConfortButtonHandler>().hoverExit += OnPointerExit();
                        }
                    }
                }

                break;
            }

            case 2:
            {
                foreach(GameObject thing in enemy.homeObstacles)
                {
                    if(thing != null)
                    {
                        objects.Add(thing);
                    }
                }

                for(int i = 0; i < buttons.Length; i++)
                {
                    if(objects.Count <= 0)
                    {
                        break;
                    }

                    int randObject = Random.Range(0, objects.Count);
                    for(int j = 0; j < enemy.homeObstacles.Length; j++)
                    {
                        if(objects[randObject] == enemy.homeObstacles[j])
                        {
                            images[i].sprite = homeSprites[j];
                            text[i].text = homeText[j];
                            //text[i].description = schoolDescription[j];
                            click[i] += OnClick;
                            weapons[i] = j;

                            buttons[i].gameObject.GetComponent<ConfrontButtonHandler>().hoverEnter += OnPointerEnter;
                            //buttons[i].gameObject.GetComponent<ConfortButtonHandler>().hoverExit += OnPointerExit();
                        }
                    }
                }

                break;
            }

            case 3:
            {
                foreach(GameObject thing in enemy.clinicObstacles)
                {
                    if(thing != null)
                    {
                        objects.Add(thing);
                    }
                }

                for(int i = 0; i < buttons.Length; i++)
                {
                    if(objects.Count == 0)
                    {
                        break;
                    }
                    
                    int randObject = Random.Range(0, objects.Count);
                    for(int j = 0; j < enemy.clinicObstacles.Length; j++)
                    {
                        if(objects[randObject] == enemy.clinicObstacles[j])
                        {
                            images[i].sprite = clinicSprites[j];
                            text[i].text = clinicText[j];
                            //text[i].description = schoolDescription[j];
                            click[i] += OnClick;
                            weapons[i] = j;

                            buttons[i].gameObject.GetComponent<ConfrontButtonHandler>().hoverEnter += OnPointerEnter;
                            //buttons[i].gameObject.GetComponent<ConfortButtonHandler>().hoverExit += OnPointerExit();
                        }
                    }
                }

                break;
            }

            default:
            {
                break;
            }

        }
    }
}
