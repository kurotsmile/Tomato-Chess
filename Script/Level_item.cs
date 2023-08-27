using UnityEngine;
using UnityEngine.UI;

public class Level_item : MonoBehaviour
{
    public Text txt_level;
    public int col_tomato;
    public int row_tomato;
    public int index_level;
    public Image img_bk;
    public Image img_icon_level;
    public Image[] img_star;
    public GameObject panel_star;
    private bool is_open;
    private int count_star;
    private int count_star_level;

    public void load_data()
    {
        this.count_star = PlayerPrefs.GetInt("level_star_"+this.index_level,-1);
        if (this.count_star!= - 1) this.count_star_level = this.count_star;
        if (PlayerPrefs.GetInt("level_open_" + this.index_level, 0) == 0)
            this.is_open = false;
        else
            this.is_open = true;

        if (this.index_level == 0) this.is_open = true;

        if (this.count_star == -1)
        {
            this.panel_star.SetActive(false);
        }
        else
        {
            this.panel_star.SetActive(true);
            for (int i = 0; i < img_star.Length; i++)
            {
                if (i < this.count_star)
                    this.img_star[i].color = new Color32(255, 165, 0, 255);
                else
                    this.img_star[i].color = Color.gray;
            }
        }
    }

    public void select_level()
    {
        if (this.is_open||this.index_level==0) { 
            GameObject.Find("Game").GetComponent<Game>().select_map_level_player_one(this);
        }
        else
        {
            GameObject.Find("Game").GetComponent<Game>().play_sound(3);
        }
    }

    public void set_data_level(int num_star)
    {
        int num_s = 0;
        if (num_s >= 3) num_s = 3;
        else
            num_s = num_star;

        PlayerPrefs.SetInt("level_star_" + this.index_level, num_s);
    }

    public void set_open_level()
    {
        this.is_open = true;
        PlayerPrefs.SetInt("level_open_" + this.index_level, 1);
    }

    public bool get_statu_open_level()
    {
        return this.is_open;
    }

    public int get_count_star_level()
    {
        return this.count_star_level;
    }
}
