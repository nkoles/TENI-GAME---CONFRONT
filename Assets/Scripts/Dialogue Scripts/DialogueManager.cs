using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public TextAsset TextFile;
    public TextMeshProUGUI textComponent;
    public string[] lines = new string[5];
    public float textspeed;
    private int index;
    public Animator anim;
    public GameObject choices;

    public bool principalStart;
    public bool principalGoodEnd;
    public bool principalBadEnd;

    public bool auntStart;
    public bool auntGoodEnd;
    public bool auntBadEnd;

    public bool doctorStart;
    public bool doctorGoodEnd;
    public bool doctorBadEnd;


    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                anim.SetBool("Talking", false);
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
        if (principalStart == true)
        {
            lines[0] = "Come in.";
            lines[1] = "Ah, hello, name. You wanted to talk to me about something?";
            lines[2] = "…What? Sorry, I don’t understand. You’re… transgender?";
            lines[3] = "And you want to change your register name and uniform.";
            lines[4] = "...Take a seat; we need to discuss this more.";
        }
        else if (principalGoodEnd == true)
        {
            lines[0] = "Alright, I think I understand now.";
            lines[1] = "You’ll have to forgive me; we haven’t had many transgender students before, so this sort of thing is still new to me.";
            lines[2] = "I’ll let your teachers know about the uniform and name change, and we should have the register updated soon. Is that alright with you?";
            lines[3] = "If that’s all, then you should get going. And, please…";
            lines[4] = "If you need help with anything else, just let me know, alright?";
        }
        else if(principalBadEnd == true)
        {
            lines[0] = "…I see.";
            lines[1] = "Sorry, but I just don’t think that going through with this is a good idea.";
            lines[2] = "It might be worth taking some time to reconsider all of this, before you make any rash decisions.";
            lines[3] = "If that’s all, then you should get going. And, please… ";
            lines[4] = "Think carefully about what we talked about today, alright?";
        }
        else if(auntStart == true)
        {
            lines[0] = "Taking care of the dishes, name? Ah, its only fair; you oughta help out around the house more, the work’s good for you.";
            lines[1] = "Huh? Call you “[name]”? Well, how come?";
            lines[2] = "Oh god, I forgot you subscribe to all that woke nonsense. I swear, these days it’s all ‘dyed hair’ this and ‘pronouns’ that. I just don’t get any of it, I mean, what’s the point?";
            lines[3] = "No, seriously, explain it to me. What do you even get out of all that stuff? What’s wrong with how you were before, huh?";
            lines[4] = "Go on; I’m all ears.";
        }
        else if(auntGoodEnd == true)
        {
            lines[0] = "Okay, I get the picture.";
            lines[1] = "…I mean, kind of. I can’t exactly relate, you know? All this is after my time, I’m completely out of my depth!";
            lines[2] = "Your generation’s more educated on this queer stuff than I am, but if you like it, then that’s worth something. I’ll try keep it in mind.";
            lines[3] = "It’s not my thing, but you can do what you want; not my place to judge.";
            lines[4] = "Have fun with the dishes, na- [name], sorry. That’s gonna take a bit to get used to, my bad.";
        }
        else if(auntBadEnd == true)
        {
            lines[0] = "Okay, you can stop now.";
            lines[1] = "I really don’t get any of this stuff… it’s not exactly my area of expertise, y’know?";
            lines[2] = "All this blue hair, pronoun, queer stuff is your generation’s thing, not mine.";
            lines[3] = "Ah, whatever. Just keep it to yourself I guess, but don’t expect me to get involved.";
            lines[4] = "Have fun with the dishes, name.";
        }
        else if(doctorStart == true)
        {
            lines[0] = "Welcome; please have a seat.";
            lines[1] = "Now. To my understanding, you’re looking to be added to the public list for gender affirming healthcare, and you also want to start seeing a therapist. Is that right?";
            lines[2] = "I see.";
            lines[3] = "If you don’t mind, I’d like to ask you some questions about your experiences, just to get a sense of what you’ve been dealing with to help decide the best course of action.";
            lines[4] = "Please be honest with your answers.";
        }
        else if(doctorGoodEnd == true)
        {
            lines[0] = "Thank you for your time.";
            lines[1] = "From what I can see, you’ve clearly been experiencing symptoms of gender dysphoria, so we can go through with adding you to the list for gender affirming care.";
            lines[2] = "I believe beginning to see a therapist in the meantime would also be a good idea; we’ll arrange something soon.";
            lines[3] = "If that’s all, then we can leave it there.";
            lines[4] = "We’ll get back to you about therapy arrangements as soon as possible.";
        }
        else if(doctorBadEnd == true)
        {
            lines[0] = "Thank you for your time.";
            lines[1] = "Sorry, but I don’t believe you meet the requirements for gender affirming care.";
            lines[2] = "I believe beginning to see a therapist to talk things over with would be a good idea, however; we’ll arrange something soon.";
            lines[3] = "If that’s all, then we can leave it there.";
            lines[4] = "We’ll get back to you about therapy arrangements as soon as possible.";
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textspeed);
            anim.SetBool("Talking", true);
        }
        anim.SetBool("Talking", false);
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            if (lines[index] == "Choices")
            {
                choices.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(TypeLine());
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void ChoiceList()
    {
        gameObject.SetActive(true);
        NextLine();
        choices.SetActive(false);
    }

    /*public void ReadFile()
    {
        string txt = TextFile.text;
        string[] linez = txt.Split(System.Environment.NewLine.ToCharArray());

        foreach (string line in linez)
        {
            if (!string.IsNullOrEmpty(line))
            {
                if(line.StartsWith("["))
                {
                    choices.SetActive(true);
                    gameObject.SetActive(false);
                }
                else
                {

                }
            }
        }
    }*/
}
