using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tomato_item : MonoBehaviour
{
    public Image img_tomato;
    public Image img_tomato_draft;
    private bool is_red_tomato = false;
    public GameObject obj_true;
    public GameObject obj_false;
    public Animator anim;
    private bool is_open;
    private bool is_one_player=true;
    public bool is_draft = false;
    public int x;
    public int y;

    public void click()
    {
        if (this.is_one_player) { 
            if (this.is_open==false) {
                if(this.is_draft==false)this.is_open = true;
                this.anim.enabled = true;
                this.img_tomato.gameObject.SetActive(true);
                GameObject.Find("Game").GetComponent<Game>().check_tomato_item(this);
            }
            else
            {
                GameObject.Find("Game").GetComponent<Game>().show_tip_tomato(this);
            }
        }
        else
        {
            if (this.is_open == false)
            {
                this.is_open = true;
                this.anim.enabled = true;
                this.img_tomato.gameObject.SetActive(true);
                GameObject.Find("Game").GetComponent<Game>().game_two_player.check_item_tomato(this);
            }
            else
                GameObject.Find("Game").GetComponent<Game>().game_two_player.show_n_tomato(this);
        }
    }

    public void set_type(bool is_red)
    {
        this.is_open = false;
        this.anim.enabled = false;
        this.img_tomato.gameObject.SetActive(false);
        this.obj_false.SetActive(false);
        this.obj_true.SetActive(false);
        this.img_tomato.gameObject.SetActive(false);
        this.is_red_tomato = is_red;
    }

    public bool get_type()
    {
        return this.is_red_tomato;
    }

    public void play_true()
    {
        this.anim.Play("Tomato_item_true");
    }

    public void play_draft()
    {
        this.img_tomato_draft.sprite = GameObject.Find("Game").GetComponent<Game>().i_img_level_tomato.sprite;
        this.anim.Play("Tomato_item_draft");
    }

    public void play_false()
    {
        this.anim.Play("Tomato_item_false");
    }

    public void stop_anim()
    {
        this.anim.Play("nomal");
        this.anim.enabled = false;
    }

    public void set_one_player(bool is_one)
    {
        this.is_one_player = is_one;
    }
}
