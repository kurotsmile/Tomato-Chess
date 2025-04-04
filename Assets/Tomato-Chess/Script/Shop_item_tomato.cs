using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_item_tomato : MonoBehaviour
{
    public Image img_icon;
    public Image img_status;
    public Text txt_name;
    public Text txt_tip;
    public int index_skin;
    public bool is_use;

    public void click()
    {
        if(this.is_use)
            GameObject.Find("Game").GetComponent<Game>().select_skin_player(this.index_skin);
        else
            GameObject.Find("Game").GetComponent<Game>().buy_skin_player(this.index_skin);
    }
}
