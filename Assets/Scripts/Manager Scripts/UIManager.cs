using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text descriptionText;
    public GameObject battleMenu;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayBattleMenu()
    {
        battleMenu.SetActive(true);
    }
}
