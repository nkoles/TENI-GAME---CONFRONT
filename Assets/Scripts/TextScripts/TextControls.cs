using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
//using Unity.VisualScripting;

public class TextControls : MonoBehaviour
{
    public TMP_FontAsset playerFont, bossFont;
    public TextMeshProUGUI associatedTextObj;
    public int testFontSize;
    public Color testColor;

    public Color completedAffirmationCharColor;
    public Color nextAffirmationCharColor;

    public Transform attackTextStringContainer;

    public string[] testAttackStrings;
    public Transform spawnBox;

    public List<AttackTextInterraction> currentActiveWords = new List<AttackTextInterraction>();
    public int wordsDestroyedCount;

    public int damageTaken;
    public int aggressionGained;

    public GameObject defaultTextPrefab;

    public int lastHealedHP;

    private PostProcessVolume _postProcessVolume;
    private Vignette _postProcessVignette;
    
    public int aggressionAmount = 1;

    public IEnumerator Affirm(string text, float time)
    {
        yield return TransitionShader.instance.Fade(false, 3f, TransitionShader.instance.affirmationTransition);

        print("entered");
        associatedTextObj.gameObject.SetActive(true);

        associatedTextObj.text = InitialiseText(text, associatedTextObj.fontSize, Color.clear);

        string parsedString = text;

        int currentCharIdx = 0;
        float timePassed = 0;

        print(parsedString);

        print(ParseRichTags(associatedTextObj));

        for (int i = 0; i < parsedString.Length; ++i)
        {
            //yield return LerpFontSize(associatedTextObj, i, 0, 50, 1f);
            yield return LerpCharColor(associatedTextObj, i, Color.clear, Color.white, 0.1f);
        }

        StartCoroutine(LerpFontSize(associatedTextObj, 0, GetCharFontSize(associatedTextObj, 0),  GetCharFontSize(associatedTextObj, 0) * 1.2f, .3f));
        StartCoroutine(LerpCharColor(associatedTextObj, 0, GetCharColor(associatedTextObj, 0), nextAffirmationCharColor, .3f));

        print("mainloop");

        parsedString = associatedTextObj.GetParsedText();

        print(parsedString.Length - 1);

        _postProcessVignette = ScriptableObject.CreateInstance<Vignette>();

        _postProcessVignette.enabled.Override(true);
        _postProcessVignette.smoothness.Override(1f);
        _postProcessVignette.rounded.Override(true);
        _postProcessVignette.intensity.Override(0f);

        while (currentCharIdx <= parsedString.Length || timePassed < time)
        {
            //print(timePassed);
            //print(Input.inputString);
            //print(currentCharIdx);
            //print(parsedString[currentCharIdx]);

            //print(parsedString[currentCharIdx]);

            print(currentCharIdx);
            print(parsedString.Length - 1);

            _postProcessVolume = PostProcessManager.instance.QuickVolume(0 & 1 >> 5, 100, _postProcessVignette);

            _postProcessVignette.intensity.Override(Mathf.Lerp(0, 1f, timePassed/time));

            if (currentCharIdx == parsedString.Length)
            {
                lastHealedHP = (int)(time - timePassed);

                GameplayManager.Instance.UpdatePlayerHealth(-lastHealedHP);

                break;
            }

            if (timePassed > time)
            {
                lastHealedHP = 0;

                break;
            }

            char currentInput = ' ';

            foreach (var c in Input.inputString)
            {
                currentInput = c;
            }

            bool checkChar = char.ToUpper(parsedString[currentCharIdx]) == char.ToUpper(currentInput) ? true : false;

            print(currentInput);

            if (parsedString[currentCharIdx] == ' ' || checkChar)
            {
                StartCoroutine(LerpCharColor(associatedTextObj, currentCharIdx, GetCharColor(associatedTextObj, currentCharIdx), completedAffirmationCharColor, .5f));
                StartCoroutine(LerpFontSize(associatedTextObj, currentCharIdx, GetCharFontSize(associatedTextObj, currentCharIdx), GetCharFontSize(associatedTextObj, currentCharIdx) / 1.2f, .5f));

                if (currentCharIdx < parsedString.Length - 1)
                {
                    StartCoroutine(LerpCharColor(associatedTextObj, currentCharIdx + 1, Color.white, nextAffirmationCharColor, .5f));
                    StartCoroutine(LerpFontSize(associatedTextObj, currentCharIdx + 1, GetCharFontSize(associatedTextObj, currentCharIdx + 1), GetCharFontSize(associatedTextObj, currentCharIdx + 1) * 1.2f, .5f));
                }

                currentCharIdx++;
            }

            timePassed += Time.deltaTime;

            yield return null;
        }

        print("yippee");

        if (currentCharIdx == 0)
            currentCharIdx++;

        StartCoroutine(LerpCharColor(associatedTextObj, currentCharIdx-1, GetCharColor(associatedTextObj, currentCharIdx-1), completedAffirmationCharColor, .5f));
        StartCoroutine(LerpFontSize(associatedTextObj, currentCharIdx-1, GetCharFontSize(associatedTextObj, 0), GetCharFontSize(associatedTextObj, 0) / 1.2f, .5f));

        for (int i = 0; i < parsedString.Length; ++i)
        {
            StartCoroutine(LerpCharColor(associatedTextObj, i, GetCharColor(associatedTextObj, i), Color.clear, .5f));

            yield return null;
        }

        for(float i = 0; i < 1.1; i+= Time.deltaTime)
        {
            _postProcessVignette.intensity.Override(Mathf.Lerp(1f, 0, i));

            yield return null;
        }

        RuntimeUtilities.DestroyVolume(_postProcessVolume, true, false);


        yield return new WaitForSeconds(.5f);

        associatedTextObj.gameObject.SetActive(false);

        yield return TransitionShader.instance.Fade(true, 3f, TransitionShader.instance.affirmationTransition);

        print("finished");
    }

    //PASSIVE
    public IEnumerator Attack(string[] _attackStrings, Transform centerSpawn, float spawnBoxRadius, float spawnTime, float attackDuration)
    {
        yield return BackgroundShader.instance.PassiveSceneTransition(BackgroundShader.instance.passiveBGColorChange, BackgroundShader.instance.noiseBGColorChange, .5f, true, 3f);

        BossShader.instance.runningRoutine = StartCoroutine(BossShader.instance.OndulateEdge(BossShader.instance.defaultOutlineWidth, BossShader.instance.passiveOutlineBand, spawnTime, attackDuration));

        wordsDestroyedCount = 0;

        damageTaken = 0;
        aggressionGained = 0;

        List<string> attackStrings = new List<string>();

        for(int i = 0;  i < _attackStrings.Length; ++i)
        {
            attackStrings.Add(InitialiseText(_attackStrings[i], 1, Color.white));
        }

        float timePassed = 0;
        float spawnTimePassed = 0;

        List<int> spawnedWordIndexes = new List<int>();

        Vector2 opposingSigns = Vector2.zero;

        while(true)
        {
            if (timePassed >= attackDuration || wordsDestroyedCount >= _attackStrings.Length)
                break;

            if(currentActiveWords.Count == 0 || spawnTimePassed >= spawnTime && spawnedWordIndexes.Count < _attackStrings.Length)
            {
                int randomStringIdx = Random.Range(0, attackStrings.Count);

                spawnedWordIndexes.Add(randomStringIdx);

                Vector3 dir = (Vector3)Random.insideUnitCircle;

                if (spawnedWordIndexes.Count % 2 == 0)
                {
                    dir *= opposingSigns;
                } else
                {
                    opposingSigns = new Vector2(-dir.x / dir.x, -dir.y / dir.y);
                }

                TextMeshProUGUI textObj = Instantiate(defaultTextPrefab,
                                                      centerSpawn.position,
                                                      Quaternion.identity,
                                                      attackTextStringContainer).GetComponent<TextMeshProUGUI>();

                print(textObj.gameObject.transform.parent.name);

                AttackTextInterraction textHandler = textObj.gameObject.GetComponent<AttackTextInterraction>();
                textObj.text = attackStrings[randomStringIdx];

                attackStrings.RemoveAt(randomStringIdx);

                textHandler.Initialise(dir, bossFont);

                currentActiveWords.Add(textHandler);

                spawnTimePassed = 0;

            }

            timePassed += Time.deltaTime;
            spawnTimePassed += Time.deltaTime;

            //print(timePassed);

            yield return null;
        }

        BossShader.instance.StopAllCoroutines();

        BossShader.instance.StartCoroutine(BossShader.instance.LerpOutline(BossShader.instance.defaultOutlineWidth, 1f));

        yield return new WaitForSeconds(1f);

        for(int i = currentActiveWords.Count-1; i >= 0; i--)
        {
            StartCoroutine(currentActiveWords[i].OnDestruction());
        }

        print("attack finished");

        yield return BackgroundShader.instance.PassiveSceneTransition(BackgroundShader.instance.defaultBGColor, BackgroundShader.instance.defaultNoiseColor, 1f, true, 3f);
    }

    private void Awake()
    {
    }

    private void Start()
    {
        //associatedTextObj.text = InitialiseText("Testing Individual Lerps", 50, Color.white);

        //StartCoroutine(Affirm("I am beautiful", 30));

        //StartCoroutine(Attack(testAttackStrings, spawnBox, 50, 2, 10));
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //ChangeCharColor(associatedTextObj, 0, Color.red);
        //    //ChangeCharFont(associatedTextObj, 0, 50);

        //    //StartCoroutine(LerpFontSize(associatedTextObj, Random.Range(0, associatedTextObj.GetParsedText().Length), 75, .5f));
        //    //StartCoroutine(LerpCharColor(associatedTextObj, Random.Range(0, associatedTextObj.GetParsedText().Length), Color.red, .5f));
        //}
    }

    static public string ParseRichTags(TextMeshProUGUI tmproAsset)
    {
        string result = "";

        string[] splitString = tmproAsset.text.Split('<', '>');

        for(int i = 4; i < splitString.Length; i+=8)
        {
            result += splitString[i];
        }

        return result;
    }

    static public string InitialiseText(string text, float fontSize, Color color)
    {
        string result = "";
        string hexColor = ColorUtility.ToHtmlStringRGBA(color);

        foreach (var c in text)
        {
            result += "<size=" + fontSize + ">" +
                      "<color=#" + hexColor + ">" +
                      c +
                      "</color>" + "</size>";

        }

        return result;
    }

    static public void ChangeCharColor(TextMeshProUGUI tmproAsset, int charIdx, Color color)
    {
        //print(GetCharColor(tmproAsset, charIdx));

        string result = "";
        string[] splitString = tmproAsset.text.Split('<', '>');

        splitString[3 + charIdx * 8] = "color=#" + ColorUtility.ToHtmlStringRGBA(color);

        foreach (string s in splitString)
        {
            if (s != "")
            {
                if (s.Length != 1)
                    result += '<' + s + '>';
                else
                    result += s;
            }
        }

        tmproAsset.text = result;

        //print(GetCharColor(tmproAsset, charIdx));
    }

    static public void ChangeCharFontSize(TextMeshProUGUI tmproAsset, int charIdx, float fontSize)
    {
        //print(GetCharFontSize(tmproAsset, charIdx));

        string result = "";
        string[] splitString = tmproAsset.text.Split('<', '>');

        splitString[1 + charIdx * 8] = "size=" + fontSize;

        foreach (string s in splitString)
        {
            if (s != "")
            {
                if (s.Length != 1)
                    result += '<' + s + '>';
                else
                    result += s;
            }
        }

        tmproAsset.text = result;

       // print(GetCharFontSize(tmproAsset, charIdx));
    }

    static public float GetCharFontSize(TextMeshProUGUI tmproAsset, int charIdx)
    {
        string[] splitString = tmproAsset.text.Split('<', '>');

        return float.Parse(splitString[1 + 8 * charIdx].Substring(5));
    }

    static public Color GetCharColor(TextMeshProUGUI tmproAsset, int charIdx)
    {
        Color result = new Color();

        string[] splitString = tmproAsset.text.Split('<', '>');

        ColorUtility.TryParseHtmlString(splitString[3 + 8 * charIdx].Substring(6), out result);

        return result;
    }

    static public IEnumerator LerpCharColor(TextMeshProUGUI tmproAsset, int charIdx, Color startColor, Color targetColor, float time)
    {
        for (float i = 0; i < 1.1; i += Time.deltaTime / time)
        {
            ChangeCharColor(tmproAsset, charIdx, Color.Lerp(startColor, targetColor, i));

            if (i + Time.deltaTime * 2 > 1)
            {
                ChangeCharColor(tmproAsset, charIdx, Color.Lerp(startColor, targetColor, 1));

                break;
            }

            yield return null;
        }
    }

    static public IEnumerator LerpFontSize(TextMeshProUGUI tmproAsset, int charIdx, float startSize, float targetSize, float time)
    {
        for (float i = 0; i < 1.1; i += Time.deltaTime / time)
        {
            ChangeCharFontSize(tmproAsset, charIdx, Mathf.Lerp(startSize, targetSize, i));

            if(i+Time.deltaTime*2 > 1)
            {
                ChangeCharFontSize(tmproAsset, charIdx, Mathf.Lerp(startSize, targetSize, 1));

                break;
            }

            yield return null;
        }
    }
}

