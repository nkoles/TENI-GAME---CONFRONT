using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class ConfrontScript : MonoBehaviour
{
    public EnemyManager enemy;
    public UnityEngine.UI.Button[] buttons;
    public Image[] images;
    public TMP_Text[] text;
    public Sprite[] parkSprites = new Sprite[3];
    public Sprite[] schoolSprites, homeSprites, clinicSprites;
    public string[] parkText = new string[3];
    public string[] schoolText, homeText, clinicText, schoolDescription, homeDescription, clinicDescription;
    public string[] parkDescription = new string[3];
    public GameObject[] inputFields;
    public int[] weapons = new int[3];
    public TMP_Text description;
    public int selectedWeapon, hoveredButton, timesClicked;
    public UnityAction click;
    public UnityAction hoverEnter;

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

    public void OnConfront()
    {

        inputFields[timesClicked].SetActive(true);

        if(timesClicked != 0)
        {
            inputFields[timesClicked - 1].SetActive(false);
            //inputFields.GetComponent<TMP_InputField>().ActivateInputField();
            TMP_InputField currentField = inputFields[timesClicked].GetComponent<TMP_InputField>();
            currentField.text = "";
            currentField.DeactivateInputField();

        }
        inputFields[timesClicked].GetComponent<TMP_InputField>().ActivateInputField();
    }

    public void OnClick()
    {

        timesClicked++;

        switch(enemy.phase)
        {
            case 1:
            {
                enemy.schoolObstacles[weapons[selectedWeapon]] = null;
                GameplayManager.Instance.tempObstacle = schoolText[weapons[selectedWeapon]];
                GameplayManager.Instance.sequence.SettupConfront();

                break;
            }

            case 2:
            {
                enemy.homeObstacles[weapons[selectedWeapon]] = null;
                GameplayManager.Instance.tempObstacle = homeText[weapons[selectedWeapon]];
                GameplayManager.Instance.sequence.SettupConfront();

                break;
            }

            case 3:
            {
                enemy.clinicObstacles[weapons[selectedWeapon]] = null;
                GameplayManager.Instance.tempObstacle = clinicText[weapons[selectedWeapon]];
                GameplayManager.Instance.sequence.SettupConfront();

                break;
            }

            case 0:
            {
                enemy.parkObstacles[weapons[selectedWeapon]] = null;
                GameplayManager.Instance.tempObstacle = parkText[weapons[selectedWeapon]];
                GameplayManager.Instance.sequence.SettupConfront();

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

            case 0:
            {
                description.text = parkDescription[weapons[hoveredButton]];
                break;
            }
        }
    }

    void OnDisable()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            click -= OnClick;
            hoverEnter -= OnPointerEnter;
            //buttons[i].gameObject.GetComponent<ConfrontButtonHandler>().hoverExit -= OnPointerExit();
            description.text = " ";
        }
    }

    void OnEnable()
    {
        click += OnClick;
        hoverEnter += OnPointerEnter;

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
                            weapons[i] = j;
                            //buttons[i].gameObject.GetComponent<ConfortButtonHandler>().hoverExit += OnPointerExit();

                            objects.Remove(objects[randObject]);
                            break;
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
                            weapons[i] = j;
                            //buttons[i].gameObject.GetComponent<ConfortButtonHandler>().hoverExit += OnPointerExit();
                            
                            objects.Remove(objects[randObject]);
                            break;
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
                            weapons[i] = j;
                            //buttons[i].gameObject.GetComponent<ConfortButtonHandler>().hoverExit += OnPointerExit();

                            objects.Remove(objects[randObject]);
                            break;
                        }
                    }
                }

                break;
            }

            case 0:
            {
                foreach(GameObject thing in enemy.parkObstacles)
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
                    for(int j = 0; j < enemy.parkObstacles.Length; j++)
                    {
                        if(objects[randObject] == enemy.parkObstacles[j])
                        {
                            images[i].sprite = parkSprites[j];
                            text[i].text = parkText[j];
                            //text[i].description = schoolDescription[j];
                            weapons[i] = j;
                            //buttons[i].gameObject.GetComponent<ConfortButtonHandler>().hoverExit += OnPointerExit();

                            objects.Remove(objects[randObject]);
                            break;
                        }
                    }
                }

                break;
            }

        }
    }
}
