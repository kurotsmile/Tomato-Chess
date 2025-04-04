using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_shop : MonoBehaviour
{
    public Sprite icon_shop;
    public GameObject item_shop_prefab;

    public Sprite[] s_icon;
    public Sprite[] s_icon2;
    public string[] s_name;
    public string[] s_tip;

    public Sprite sp_buy;
    public Sprite sp_select;

    private int index_skin_buy_item;

    public void show_shop()
    {
        Carrot.Carrot_Box box_shop=this.GetComponent<Game>().carrot.Create_Box("Stores and Accessories", this.icon_shop);

        for(int i = 0; i < this.s_icon.Length; i++)
        {
            GameObject i_shop = Instantiate(this.item_shop_prefab);
            i_shop.transform.SetParent(box_shop.area_all_item);
            i_shop.transform.transform.localScale = new Vector3(1f, 1f, 1f);
            i_shop.GetComponent<Shop_item_tomato>().txt_name.text = s_name[i];
            i_shop.GetComponent<Shop_item_tomato>().txt_tip.text = s_tip[i];
            i_shop.GetComponent<Shop_item_tomato>().img_icon.sprite= s_icon[i];
            i_shop.GetComponent<Shop_item_tomato>().index_skin = i;
            if (i == 0 || i == 3)
            {
                i_shop.GetComponent<Shop_item_tomato>().img_status.sprite = this.sp_select;
                i_shop.GetComponent<Shop_item_tomato>().is_use = true;
            }
            else {
                if (PlayerPrefs.GetInt("tomato_" + i, 0) == 0)
                {
                    i_shop.GetComponent<Shop_item_tomato>().img_status.sprite = this.sp_buy;
                    i_shop.GetComponent<Shop_item_tomato>().is_use = false;
                }
                else
                {
                    i_shop.GetComponent<Shop_item_tomato>().img_status.sprite = this.sp_select;
                    i_shop.GetComponent<Shop_item_tomato>().is_use = true;
                }
            }
        }

        this.GetComponent<Game>().play_sound();
    }

    public void set_index_buy(int index_skin)
    {
        this.index_skin_buy_item = index_skin;
    }

    public void buy_success_skin()
    {
        PlayerPrefs.SetInt("tomato_" + this.index_skin_buy_item, 1);
        this.GetComponent<Game>().select_skin_player(this.index_skin_buy_item);
    }
}
