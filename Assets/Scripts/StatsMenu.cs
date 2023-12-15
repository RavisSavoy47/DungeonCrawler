using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class StatsMenu : MonoBehaviour
{
    public TextMeshProUGUI playerHPText;
    public TextMeshProUGUI playerATKText;
    public TextMeshProUGUI playerEXPText;
    public CanvasRenderer canvas;
    public PlayerControler player;
    public Button button;

    
    private void Start()
    {
        playerHPText = canvas.GetComponentInChildren<TextMeshProUGUI>();
        playerATKText = canvas.GetComponentInChildren<TextMeshProUGUI>();
        playerEXPText = canvas.GetComponentInChildren<TextMeshProUGUI>();

        //when the button is clicked
        if (button.tag == "hp")
            button.onClick.AddListener(increaseHP);
        if (button.tag == "atk")
            button.onClick.AddListener(increaseatk);
    }
    private void Update()
    {
        //displays the stats in text
        if(canvas.tag == "hp")
            playerHPText.text = player.maxHealth.ToString();
        if (canvas.tag == "atk")
            playerATKText.text = player.swordAttack.damage.ToString();
        if (canvas.tag == "exp")
            playerEXPText.text = player.currentExp.ToString();
        if (canvas.tag == "cost")
            playerEXPText.text = player.cost.ToString();
    }

    private void increaseHP()
    {
        //increases player hp if they have the cost amount
        if(player.currentExp !>= player.cost)
        {
            player.maxHealth++;
            player.currentExp -= 10;
            player.cost += 10;
        }
    }
    private void increaseatk()
    {
        //increases player atk if they have cost amount
        if (player.currentExp !>= player.cost)
        {
            player.swordAttack.damage++;
            player.currentExp -= 10;
            player.cost += 10;
        }
    }
}
