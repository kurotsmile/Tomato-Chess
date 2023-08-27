using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_one_player : MonoBehaviour
{
    public int num_level;
    public GameObject level_item_prefab;
    public Transform area_all_item_level;
    private List<Level_item> list_level_item;
    private Level_item level_item_cur;
    private int index_level_cur = 1;
    public Color32 color_level_cur;
    public Color32 color_level_open;
    public Sprite icon_level_open;
    public Sprite icon_level_cur;
    public Text txt_map_star;

    public void create_map()
    {
        this.index_level_cur = PlayerPrefs.GetInt("index_level_cur",0);
        this.list_level_item = new List<Level_item>();
        this.gameObject.SetActive(true);

        for(int i = 0; i < this.num_level; i++)
        {
            GameObject obj_level = Instantiate(this.level_item_prefab);
            obj_level.transform.SetParent(this.area_all_item_level);
            obj_level.transform.localScale = new Vector3(1f, 1f, 1f);
            obj_level.GetComponent<Level_item>().txt_level.text = "Level " + (i+1);
            obj_level.GetComponent<Level_item>().col_tomato = i + 4;
            obj_level.GetComponent<Level_item>().row_tomato = i + 3;
            obj_level.GetComponent<Level_item>().index_level = i ;
            this.list_level_item.Add(obj_level.GetComponent<Level_item>());
        }

        this.check_info_all_level();
    }

    public void check_info_all_level()
    {
        int count_star_map = 0;
        for (int i = 0; i < this.list_level_item.Count; i++)
        {
            this.list_level_item[i].load_data();
            if (this.list_level_item[i].get_statu_open_level() || i == 0)
            {
                this.list_level_item[i].GetComponent<Level_item>().img_bk.color = this.color_level_open;
                this.list_level_item[i].GetComponent<Level_item>().img_icon_level.sprite = this.icon_level_open;
            }

            if (index_level_cur== i)
            {
                this.list_level_item[i].GetComponent<Level_item>().img_bk.color = this.color_level_cur;
                this.list_level_item[i].GetComponent<Level_item>().img_icon_level.sprite = this.icon_level_cur;
            }
            count_star_map += this.list_level_item[i].GetComponent<Level_item>().get_count_star_level();
        }

        this.txt_map_star.text = count_star_map.ToString();
        GameObject.Find("Game").GetComponent<Game>().carrot.game.update_scores_player(count_star_map);
    }

    public Level_item get_next_level()
    {
        int index_next = this.level_item_cur.index_level + 1;
        return this.list_level_item[index_next];
    }

    public Level_item get_cur_level()
    {
        return this.level_item_cur;
    }

    public void set_cur_level(Level_item lv_item)
    {
        this.index_level_cur = lv_item.index_level;
        PlayerPrefs.SetInt("index_level_cur", lv_item.index_level);
        this.level_item_cur = lv_item;
    }
}
