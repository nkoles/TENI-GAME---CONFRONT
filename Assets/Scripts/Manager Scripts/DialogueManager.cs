using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{
    public TextAsset TextFile;
    public TextMeshProUGUI textComponent;
    public string[] lines = new string[5];
    public float textspeed;
    private int index;
    public Animator anim;
    public GameObject choices;
    public EnemyManager enemy;
    public ButtonDetailHighlighting button;

    public bool friendStart;
    public bool friendEnd;

    public bool principalStart;
    public bool principalGoodEnd;
    public bool principalBadEnd;

    public bool auntStart;
    public bool auntGoodEnd;
    public bool auntBadEnd;

    public bool doctorStart;
    public bool doctorGoodEnd;
    public bool doctorBadEnd;

    public bool therapistEnd;

    public bool battleEnd;

    // Start is called before the first frame update
    void Start()
    {
        ButtonDetailHighlighting.instance.MoveButtonOutOfView(false);
        if (enemy.phase == 1)
        {
            principalStart = true;
        }
        else if (enemy.phase == 2)
        {
            auntStart = true;
        }
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

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
        if (friendStart == true)
        {
            lines[0] = "Hey, how’s it going! You said you had something you wanted to talk to me about?";
            lines[1] = "You’re trans? Wait- for real? \tSo that means you’re…";
            lines[2] = "Choices";
            lines[3] = "Huh. Cool? Sorry, I don’t really know anything about trans stuff… Is it okay if I ask you some things?";
            lines[4] = "Nice! Just stop me if I say anything dumb, alright?";
        }
        else if (friendEnd == true)
        {
            lines[0] = "Okay, that’s pretty much everything I wanted to know! Thanks for putting up with all that, haha.";
            lines[1] = "And thanks for trusting me enough to come out to me; I know these kinds of conversations are never easy, at best.";
            lines[2] = "I guess when it comes down to it, you have to remember to stick to your guns; never back down, and don’t be afraid to really push your point if it just isn’t sticking. Some people can be real stubborn, you know? In those cases, confrontation tends to be unavoidable.";
            lines[3] = "…Ah. Sorry, started rambling… in my defence, you should’ve expected that when you came out to someone on a debate team. Don’t blame me!";
            lines[4] = "Whatever – my point is, you got this, and I got your back! Now; wanna go get some food? My treat!";
        }
        else if (principalStart == true)
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
        else if(therapistEnd == true)
        {
            lines[0] = "…And so that’s what led to your referral to me; you’ve needed to have a lot of tough discussions to get to this point, haven’t you?";
            lines[1] = "Conversations like that can really feel like an uphill battle; you’re fighting not just to be heard, but to defend yourself as well, and it can turn into a one-sided affair all too easily.";
            lines[2] = "It’s completely normal to feel frustration, anxiety, sadness, and other negative emotions because of this. It can be a lot to deal with, especially since trans people typically experience these situations often.";
            lines[3] = "I’d be happy to talk about that today, if you’d like to.";
            lines[4] = "I’m here to listen.";
        }
    }

    public void PlayMusic()
    {
        if ((friendStart == true) || (friendEnd == true))
        {
            AudioManager.instance.PlayMusic("Friend (Pre-battle)");
        }
        else if ((friendStart != true) && (friendEnd != true))
        {
            AudioManager.instance.PlayMusic("Friend (Pre-battle)");
        }

        if (principalStart == true)
        {
            AudioManager.instance.PlayMusic("Principal (Pre-battle)");
        }
        else if (principalGoodEnd == true)
        {
            AudioManager.instance.PlayMusic("Principal (Post-battle)");
        }
        else if((principalStart != true) && (principalGoodEnd != true) && enemy.phase == 1)
        {
            AudioManager.instance.PlayMusic("Principal (During-battle)");
        }

        if (auntStart == true)
        {
            AudioManager.instance.PlayMusic("Aunt (Pre-battle)");
        }
        else if (auntGoodEnd == true)
        {
            AudioManager.instance.PlayMusic("Aunt (Post-battle)");
        }
        else if ((auntStart != true) && (auntGoodEnd != true) && enemy.phase == 2)
        {
            AudioManager.instance.PlayMusic("Aunt (During-battle)");
        }

        if (doctorStart == true)
        {
            AudioManager.instance.PlayMusic("Doctor (Pre-battle)");
        }
        else if (doctorGoodEnd == true)
        {
            AudioManager.instance.PlayMusic("Doctor (Post-battle)");
        }
        else if ((doctorStart != true) && (doctorGoodEnd != true) && enemy.phase == 3)
        {
            AudioManager.instance.PlayMusic("Doctor (During-battle)");
        }

        else if ((principalBadEnd == true) || (auntBadEnd == true) || (doctorBadEnd == true))
        {
            AudioManager.instance.PlayMusic("Lose");
        }

        if (therapistEnd == true)
        {
            AudioManager.instance.PlayMusic("Therapy");
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textspeed);
            anim.SetBool("Talking", true);
            if((friendStart == true) || (friendEnd == true))
            {
                AudioManager.instance.PlaySFX("FriendSfx");
            }
            else if((principalStart == true) || (principalGoodEnd == true) || (principalBadEnd == true))
            {
                AudioManager.instance.PlaySFX("PrincipalSfx");
            }
            else if ((auntStart == true) || (auntGoodEnd == true) || (auntBadEnd == true))
            {
                AudioManager.instance.PlaySFX("AuntSfx");
            }
            else if ((doctorStart == true) || (doctorGoodEnd == true) || (doctorBadEnd == true))
            {
                AudioManager.instance.PlaySFX("DoctorSfx");
            }
            else if (therapistEnd == true)
            {
                AudioManager.instance.PlaySFX("TherapistSfx");
            }
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
            if(battleEnd == true)
            {
                ButtonDetailHighlighting.instance.MoveButtonOutOfView(false);
                SceneLoadingManager.instance.LoadNextScene();
            }
            else
            {
                ButtonDetailHighlighting.instance.MoveButtonOutOfView(true);
                //gameObject.SetActive
                textComponent.text = string.Empty;
                index = 0;
                lines[0] = "";
                lines[1] = "";
                lines[2] = "";
                lines[3] = "";
                lines[4] = "";
            }
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
