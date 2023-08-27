using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tomato_col_item : MonoBehaviour
{
    public Text txt_number;
    public Image img_bk_number;
    private bool is_two_player = false;
    public int x_tomato;
    public int y_tomato;
    public void click()
    {
        if(this.is_two_player==false){
            GameObject.Find("Game").GetComponent<Game>().select_tomato_orange();
            GameObject.Find("Game").GetComponent<Game>().select_colum_show_tip(this.x_tomato);
        }
    }

    public void set_type_player(bool is_two)
    {
        this.is_two_player = is_two;
    }
}
