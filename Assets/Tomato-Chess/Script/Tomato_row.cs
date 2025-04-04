using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tomato_row : MonoBehaviour
{
    public Sprite[] sp_tomato;
    public GameObject Tomato_item;
    public Transform area_all_tomato;
    public Text txt_number_tomato;
    private List<Tomato_item> list_tomato;
    public Image bk_number;
    public Tomato_col_item col_head;
    public int y;

    private int count_tomato_on;
    private int count_tomato_off;
    private bool is_one_player;

    public void create_tomato(int number_tomato,int y_tomato,bool is_one_p=true)
    {
        this.list_tomato = new List<Tomato_item>();
        for (int i=0;i< number_tomato; i++){
            GameObject obj_item_tomato = Instantiate(this.Tomato_item);
            obj_item_tomato.transform.SetParent(this.area_all_tomato);
            obj_item_tomato.transform.localScale = new Vector3(1f, 1f, 1f);
            obj_item_tomato.GetComponent<Tomato_item>().x = i;
            obj_item_tomato.GetComponent<Tomato_item>().y = y_tomato;
            int r_tomato = Random.Range(0, 2);
            if (r_tomato == 0) {
                obj_item_tomato.GetComponent<Tomato_item>().img_tomato.sprite = this.sp_tomato[0];
                obj_item_tomato.GetComponent<Tomato_item>().set_type(true);
                this.count_tomato_on++;
            }
            else {
                obj_item_tomato.GetComponent<Tomato_item>().img_tomato.sprite = this.sp_tomato[1];
                obj_item_tomato.GetComponent<Tomato_item>().set_type(false);
                this.count_tomato_off++;
            }
            this.is_one_player = is_one_p;
            obj_item_tomato.GetComponent<Tomato_item>().set_one_player(is_one_p);
            this.list_tomato.Add(obj_item_tomato.GetComponent<Tomato_item>());
        }

        this.check_num_tomato();
    }


    private void check_num_tomato()
    {
        int count_tomato_red = 0;
        for (int i = 0; i < this.list_tomato.Count; i++) if (this.list_tomato[i].get_type()) count_tomato_red++;
        this.txt_number_tomato.text = count_tomato_red.ToString();
    }

    public List<Tomato_item> get_list_tomato()
    {
        return this.list_tomato;
    }

    public int get_count_tomato_on()
    {
        return this.count_tomato_on;
    }

    public int get_count_tomato_off()
    {
        return this.count_tomato_off;
    }

    public void click_number_row()
    {
        if(this.is_one_player){
            GameObject.Find("Game").GetComponent<Game>().select_tomato_red();
            GameObject.Find("Game").GetComponent<Game>().select_row_show_tip(this.y);
        }
    }

    public void set_model_draft(bool status_draft)
    {
        for(int i = 0; i < this.list_tomato.Count; i++)
        {
            this.list_tomato[i].is_draft = status_draft;
        }
    }
}
