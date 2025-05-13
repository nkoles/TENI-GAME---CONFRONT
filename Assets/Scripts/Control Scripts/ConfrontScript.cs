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
    public TMP_Text[] text;
    public Sprite[] parkSprites;
    public Sprite[] schoolSprites, homeSprites, clinicSprites;
    public string[] parkText;
    public string[] schoolText, homeText, clinicText, schoolDescription, homeDescription, clinicDescription;
    public string[] parkDescription;
    public int[] weapons;
    public TMP_Text description;
    public int selectedWeapon, hoveredButton;
    public UnityAction[] click = new UnityAction[3];
    public UnityAction[] hoverEnter = new UnityAction[3];

    // Start is called before the first frame update
    void Awake()
    {
        enemy = GameplayManager.Instance.enemy;
        for(int i = 0; i < buttons.Length; i++)
        {
            //buttons[i].onClick.AddListener(click[i]);
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
                GameplayManager.Instance.tempObstacle = schoolText[selectedWeapon];
                GameplayManager.Instance.sequence.SettupConfront();

                break;
            }

            case 2:
            {
                enemy.homeObstacles[selectedWeapon] = null;
                GameplayManager.Instance.tempObstacle = schoolText[selectedWeapon];
                GameplayManager.Instance.sequence.SettupConfront();

                break;
            }

            case 3:
            {
                enemy.clinicObstacles[selectedWeapon] = null;
                GameplayManager.Instance.tempObstacle = schoolText[selectedWeapon];
                GameplayManager.Instance.sequence.SettupConfront();

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
        switch(enemy.phase)
        {
            case 1:
            {
                description.text = schoolDescription[weapons[hoveredButton]];

                break;
            }

            case 2:
            {
                description.text = homeDescription[weapons[hoveredButton]];

                break;
            }

            case 3:
            {
                description.text = clinicDescription[weapons[hoveredButton]];

                break;
            }

            default:
            {

                break;
            }
        }
    }

    void OnDisable()
    {
        for(int i = 0; i < click.Length; i++)
        {
            click[i] -= OnClick;
            hoverEnter[i] -= OnPointerEnter;
            //buttons[i].gameObject.GetComponent<ConfrontButtonHandler>().hoverExit -= OnPointerExit();
            description.text = " ";
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

                            hoverEnter[i] += OnPointerEnter;
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

                            hoverEnter[i] += OnPointerEnter;
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

                            hoverEnter[i] += OnPointerEnter;
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
