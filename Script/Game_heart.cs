using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_heart : MonoBehaviour
{
    public GameObject obj_heart_item;
    public Transform area_all_heart;
    public Sprite sp_heart_on;
    public Sprite sp_heart_off;

    private List<Image> list_heart;
    private int num_heart = 3;
    private bool is_one_player=true;

    public void add_heart()
    {
        GameObject heart_obj = Instantiate(this.obj_heart_item);
        heart_obj.transform.SetParent(this.area_all_heart);
        heart_obj.transform.localScale = new Vector3(1f, 1f, 1f);
        this.list_heart.Add(heart_obj.GetComponent<Image>());
    }

    public void create_heart(int num_heart = 3,bool is_one_p=true)
    {
        this.list_heart = new List<Image>();
        this.num_heart = 3;
        this.is_one_player = is_one_p;
        for (int i = 0; i < num_heart; i++) this.add_heart();
    }

    public int count_heart()
    {
        return this.list_heart.Count;
    }

    public void remove_heart()
    {
        this.num_heart--;
        if (this.num_heart<=0){
            if(this.is_one_player)
                GameObject.Find("Game").GetComponent<Game>().show_game_over();
            else
                GameObject.Find("Game").GetComponent<Game>().game_two_player.show_reslut();
        }

        for (int i=0;i<this.list_heart.Count; i++){
            if(i<this.num_heart)
                this.list_heart[i].sprite = this.sp_heart_on;
            else
                this.list_heart[i].sprite = this.sp_heart_off;
        }
    }

    public int get_num_heart()
    {
        return this.num_heart;
    }
}
