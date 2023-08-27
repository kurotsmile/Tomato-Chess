using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_two_Player : MonoBehaviour
{
    public GameObject panel_game_two_player;
    public GameObject panel_game_reslut;
    public Transform all_item_tomato;
    public GameObject tomato_num_item_prefab;

    public Text txt_count_red;
    public Text txt_count_orange; 
    
    public Text txt_timer_red;
    public Text txt_timer_orange;

    public Text txt_n1_count_red;
    public Text txt_n1_count_orange;
    
    public Text txt_n2_count_red;
    public Text txt_n2_count_orange;
    
    public Text txt_n1_result;
    public Text txt_n2_result;

    public GameObject panel_n_player1;
    public GameObject panel_n_player2;

    public GameObject bk_red;
    public GameObject bk_orange;

    public GameObject obj_clock_player1;
    public GameObject obj_clock_player2;

    public Game_heart heart_red;
    public Game_heart heart_orange;

    public Button[] button_player_ready_replay;
    public Color32 color_ready_start;

    public Image img_icon_p1;
    public Image img_icon_p2;
    
    public Image img_icon_p1_reslut;
    public Image img_icon_p2_reslut;

    public Image img_icon_tip_p1_red;
    public Image img_icon_tip_p1_orange;

    public Image img_icon_tip_p2_red;
    public Image img_icon_tip_p2_orange;

    public Animator anim;

    private int row_tomato = 4;
    private int col_tomato = 10;

    private List<Tomato_row> list_row_tomato;

    private int count_red;
    private int count_orange;
    private bool is_red_player = true;
    private bool is_play = false;
    private float speed_timer_count_down = 3f;
    private int timer_count_tomato = 10;
    private List<Tomato_item> list_tip_tomato;
    private List<Tomato_col_item> list_head_red_col;
    private List<Tomato_col_item> list_head_red_row;
    private List<Tomato_col_item> list_head_orange_col;
    private List<Tomato_col_item> list_head_orange_row;
    private bool[] p_ready=new bool[2];

    public void play_game()
    {
        if (Random.Range(0, 4) % 2 == 0) this.is_red_player = true; else this.is_red_player = false;

        this.is_play = true;
        this.count_red = 0;
        this.count_orange = 0;

        this.panel_n_player1.SetActive(false);
        this.panel_n_player2.SetActive(false);

        this.GetComponent<Game>().carrot.clear_contain(this.heart_orange.area_all_heart);
        this.GetComponent<Game>().carrot.clear_contain(this.heart_red.area_all_heart);

        this.heart_red.create_heart(3,false);
        this.heart_orange.create_heart(3,false);
        
        this.list_row_tomato = new List<Tomato_row>();
        this.panel_game_two_player.SetActive(true);
        this.panel_game_reslut.SetActive(false);

        this.GetComponent<Game>().carrot.clear_contain(this.all_item_tomato);

        this.list_head_red_col = new List<Tomato_col_item>();
        this.list_head_red_row = new List<Tomato_col_item>();
        this.list_head_orange_col = new List<Tomato_col_item>();
        this.list_head_orange_row = new List<Tomato_col_item>();

        this.list_tip_tomato = null;

        GameObject obj_tomato_head_row = Instantiate(this.GetComponent<Game>().Tomato_row_prefab);
        obj_tomato_head_row.transform.SetParent(this.all_item_tomato);
        obj_tomato_head_row.transform.localScale = new Vector3(1f, 1f, 1f);
        obj_tomato_head_row.GetComponent<Tomato_row>().bk_number.transform.parent.GetComponent<Image>().enabled = false;
        obj_tomato_head_row.GetComponent<Tomato_row>().bk_number.enabled = false;
        obj_tomato_head_row.GetComponent<Tomato_row>().txt_number_tomato.enabled = false;

        for (int c = 0; c < col_tomato; c++)
        {
            GameObject obj_tomato_head = Instantiate(this.GetComponent<Game>().Tomato_col_prefab);
            obj_tomato_head.transform.SetParent(obj_tomato_head_row.GetComponent<Tomato_row>().area_all_tomato);
            obj_tomato_head.transform.localScale = new Vector3(-1f, -1f, 1f);
            obj_tomato_head.GetComponent<Tomato_col_item>().set_type_player(true);
            this.list_head_orange_col.Add(obj_tomato_head.GetComponent<Tomato_col_item>());
        }

        for (int i = 1; i <=this.row_tomato; i++)
        {
            GameObject obj_row = Instantiate(this.GetComponent<Game>().Tomato_row_prefab);
            obj_row.GetComponent<Tomato_row>().sp_tomato[0] = this.img_icon_p1.sprite;
            obj_row.GetComponent<Tomato_row>().sp_tomato[1] = this.img_icon_p2.sprite;
            obj_row.transform.SetParent(this.all_item_tomato);
            obj_row.transform.localScale = new Vector3(1f, 1f, 1f);
            obj_row.GetComponent<Tomato_row>().create_tomato(this.col_tomato, i,false);
            this.list_row_tomato.Add(obj_row.GetComponent<Tomato_row>());
            obj_row.GetComponent<Tomato_row>().col_head.set_type_player(true);
            this.list_head_red_row.Add(obj_row.GetComponent<Tomato_row>().col_head);
        }

        for(int i = 0; i < this.list_row_tomato.Count; i++)
        {
            int count_orange_in_row = 0;
            for(int y = 0; y < this.list_row_tomato[i].get_list_tomato().Count; y++)
            {
                Tomato_item t = this.list_row_tomato[i].get_list_tomato()[y];
                if (t.get_type())
                    this.count_red++;
                else
                {
                    this.count_orange++;
                    count_orange_in_row++;
                    t.transform.localScale = new Vector3(-1f, -1f, 1f);
                }  
            }

            GameObject obj_num_item = Instantiate(this.tomato_num_item_prefab);
            obj_num_item.transform.SetParent(this.list_row_tomato[i].area_all_tomato);
            obj_num_item.transform.localScale = new Vector3(-1f, -1f, 1f);
            obj_num_item.GetComponent<Tomato_col_item>().txt_number.text =count_orange_in_row.ToString();
            obj_num_item.GetComponent<Tomato_col_item>().set_type_player(true);
            this.list_head_orange_row.Add(obj_num_item.GetComponent<Tomato_col_item>());
        }

        GameObject obj_tomato_head_orange = Instantiate(this.GetComponent<Game>().Tomato_row_prefab);
        obj_tomato_head_orange.transform.SetParent(this.all_item_tomato);
        obj_tomato_head_orange.transform.localScale = new Vector3(1f, 1f, 1f);
        obj_tomato_head_orange.GetComponent<Tomato_row>().bk_number.transform.parent.GetComponent<Image>().enabled = false;
        obj_tomato_head_orange.GetComponent<Tomato_row>().bk_number.enabled = false;
        obj_tomato_head_orange.GetComponent<Tomato_row>().txt_number_tomato.enabled = false;

        for (int c = 0; c < col_tomato; c++)
        {
            GameObject obj_tomato_head = Instantiate(this.GetComponent<Game>().Tomato_col_prefab);
            obj_tomato_head.transform.SetParent(obj_tomato_head_orange.GetComponent<Tomato_row>().area_all_tomato);
            obj_tomato_head.transform.localScale = new Vector3(1f, 1f, 1f);
            obj_tomato_head.GetComponent<Image>().color = obj_tomato_head_orange.GetComponent<Tomato_row>().bk_number.transform.parent.GetComponent<Image>().color;
            obj_tomato_head.GetComponent<Tomato_col_item>().img_bk_number.color = obj_tomato_head_orange.GetComponent<Tomato_row>().bk_number.color;
            obj_tomato_head.GetComponent<Tomato_col_item>().set_type_player(true);
            this.list_head_red_col.Add(obj_tomato_head.GetComponent<Tomato_col_item>());
        }

        for(int i = 0; i < this.list_head_red_col.Count; i++)
        {
            int count_r = 0;
            int count_o = 0;
            for(int y = 0; y < this.list_row_tomato.Count; y++)
            {
                Tomato_item t = this.list_row_tomato[y].get_list_tomato()[i];
                if (t.get_type())
                    count_r++;
                else
                    count_o++;
            }
            this.list_head_red_col[i].txt_number.text = count_r.ToString();
            this.list_head_orange_col[i].txt_number.text = count_o.ToString();
        }

        this.check_player();
        this.reset_timer();
    }

    private void Update()
    {
        if (this.is_play)
        {
            this.speed_timer_count_down -= 2.5f*Time.deltaTime;
            if (this.speed_timer_count_down < 0f)
            {
                this.speed_timer_count_down = 3f;
                this.timer_count_tomato -= 1;
                if(this.is_red_player)
                    this.txt_timer_red.text = this.timer_count_tomato.ToString();
                else
                    this.txt_timer_orange.text = this.timer_count_tomato.ToString();

                if (this.timer_count_tomato<=0) this.switch_player();
            }
        }
    }

    private void reset_timer()
    {
        this.speed_timer_count_down = 3f;
        this.timer_count_tomato = 10;
        this.txt_timer_red.text = this.timer_count_tomato.ToString();
    }

    private void check_player()
    {
        if (this.is_red_player)
        {
            this.bk_red.SetActive(true);
            this.bk_orange.SetActive(false);
            this.obj_clock_player1.SetActive(true);
            this.obj_clock_player2.SetActive(false);
            this.anim.Play("p_red");
            for (int i = 0; i < this.list_head_orange_col.Count; i++) {this.list_head_orange_col[i].img_bk_number.enabled = false; this.list_head_orange_col[i].txt_number.color = Color.black; }
            for (int i = 0; i < this.list_head_orange_row.Count; i++) {this.list_head_orange_row[i].img_bk_number.enabled = false; this.list_head_orange_row[i].txt_number.color = Color.black; }
            for (int i = 0; i < this.list_head_red_col.Count; i++) {this.list_head_red_col[i].img_bk_number.enabled = true; this.list_head_red_col[i].txt_number.color = Color.white; }
            for (int i = 0; i < this.list_head_red_row.Count; i++) {this.list_head_red_row[i].img_bk_number.enabled = true; this.list_head_red_row[i].txt_number.color = Color.white; }
        }
        else
        {
            this.bk_red.SetActive(false);
            this.bk_orange.SetActive(true);
            this.obj_clock_player1.SetActive(false);
            this.obj_clock_player2.SetActive(true);
            this.anim.Play("p_orange");
            for (int i = 0; i < this.list_head_orange_col.Count; i++) {this.list_head_orange_col[i].img_bk_number.enabled = true; this.list_head_orange_col[i].txt_number.color = Color.white; }
            for (int i = 0; i < this.list_head_orange_row.Count; i++) {this.list_head_orange_row[i].img_bk_number.enabled = true; this.list_head_orange_row[i].txt_number.color = Color.white; }
            for (int i = 0; i < this.list_head_red_col.Count; i++) {this.list_head_red_col[i].img_bk_number.enabled = false; this.list_head_red_col[i].txt_number.color = Color.black; }
            for (int i = 0; i < this.list_head_red_row.Count; i++) {this.list_head_red_row[i].img_bk_number.enabled = false; this.list_head_red_row[i].txt_number.color = Color.black; }
        }
    }

    public void check_item_tomato(Tomato_item t)
    {
        this.panel_n_player1.SetActive(false);
        this.panel_n_player2.SetActive(false);
        this.reset_tip_tomato();

        if (t.get_type())
            this.count_red--;
        else
            this.count_orange--;

        if (t.get_type() != this.is_red_player) { 
            this.switch_player();
            this.GetComponent<Game>().play_sound(3);
            this.GetComponent<Game>().carrot.play_vibrate();
            if (!t.get_type())
                this.heart_red.remove_heart();
            else
                this.heart_orange.remove_heart();
            t.play_false();
        }
        else {
            this.reset_timer();
            this.GetComponent<Game>().play_sound(1);
            t.play_true();
        }

        this.txt_count_red.text = this.count_red.ToString();
        this.txt_count_orange.text = this.count_orange.ToString();

        if (this.count_red <= 0) this.show_reslut(1);
        if (this.count_orange <= 0) this.show_reslut(2);
    }

    private void switch_player()
    {
        this.reset_timer();
        this.reset_tip_tomato();

        this.panel_n_player1.gameObject.SetActive(false);
        this.panel_n_player2.gameObject.SetActive(false);

        if (this.is_red_player)
            this.is_red_player = false;
        else
            this.is_red_player = true;
        this.check_player();
        this.GetComponent<Game>().play_sound(2);
    }

    public void show_n_tomato(Tomato_item t)
    {
        if (this.is_red_player)
            this.panel_n_player1.SetActive(true);
        else
            this.panel_n_player2.SetActive(true);

        this.reset_tip_tomato();
        this.list_tip_tomato = new List<Tomato_item>();
        t.GetComponent<Image>().color = this.GetComponent<Game>().color_tomato_tip;
        this.list_tip_tomato.Add(t);

        if (t.y > 1) {
            if (t.x > 0) { 
                Tomato_item t_top_left = this.list_row_tomato[t.y-2].get_list_tomato()[t.x - 1];
                t_top_left.GetComponent<Image>().color = this.GetComponent<Game>().color_tomato_tip;
                this.list_tip_tomato.Add(t_top_left);
            }

            Tomato_item t_top_center = this.list_row_tomato[t.y - 2].get_list_tomato()[t.x];
            t_top_center.GetComponent<Image>().color = this.GetComponent<Game>().color_tomato_tip;
            this.list_tip_tomato.Add(t_top_center);

            if (t.x < this.col_tomato - 1) { 
                Tomato_item t_top_right = this.list_row_tomato[t.y - 2].get_list_tomato()[t.x+1];
                t_top_right.GetComponent<Image>().color = this.GetComponent<Game>().color_tomato_tip;
                this.list_tip_tomato.Add(t_top_right);
            }
        }

        if (t.x > 0)
        {
            Tomato_item t_left = this.list_row_tomato[t.y - 1].get_list_tomato()[t.x - 1];
            t_left.GetComponent<Image>().color = this.GetComponent<Game>().color_tomato_tip;
            this.list_tip_tomato.Add(t_left);
        }

        if (t.x < this.col_tomato - 1)
        {
            Tomato_item t_right = this.list_row_tomato[t.y - 1].get_list_tomato()[t.x + 1];
            t_right.GetComponent<Image>().color = this.GetComponent<Game>().color_tomato_tip;
            this.list_tip_tomato.Add(t_right);
        }

        if (t.y < this.row_tomato) {
            if (t.x > 0) { 
                Tomato_item t_bottom_left = this.list_row_tomato[t.y].get_list_tomato()[t.x - 1];
                t_bottom_left.GetComponent<Image>().color = this.GetComponent<Game>().color_tomato_tip;
                this.list_tip_tomato.Add(t_bottom_left);
            }

            Tomato_item t_bottom_center = this.list_row_tomato[t.y].get_list_tomato()[t.x];
            t_bottom_center.GetComponent<Image>().color = this.GetComponent<Game>().color_tomato_tip;
            this.list_tip_tomato.Add(t_bottom_center);

            if (t.x < this.col_tomato - 1)
            {
                Tomato_item t_bottom_right = this.list_row_tomato[t.y].get_list_tomato()[t.x+1];
                t_bottom_right.GetComponent<Image>().color = this.GetComponent<Game>().color_tomato_tip;
                this.list_tip_tomato.Add(t_bottom_right);
            }
        }

        int n_count_red = 0;
        int n_count_orange = 0;
        for (int i = 0; i < this.list_tip_tomato.Count; i++)
        {
            if (this.list_tip_tomato[i].get_type())
                n_count_red++;
            else
                n_count_orange++;
        }

        this.txt_n1_count_red.text = "x"+n_count_red.ToString();
        this.txt_n2_count_red.text = "x" + n_count_red.ToString();
        this.txt_n1_count_orange.text = "x" + n_count_orange.ToString();
        this.txt_n2_count_orange.text = "x" + n_count_orange.ToString();

        this.GetComponent<Game>().play_sound(4);

    }

    private void reset_tip_tomato()
    {
        if (this.list_tip_tomato != null) for (int i = 0; i < this.list_tip_tomato.Count; i++) this.list_tip_tomato[i].GetComponent<Image>().color = Color.white;
    }

    public void show_reslut(int index_win=0)
    {
        this.is_play = false;
        this.panel_game_reslut.SetActive(true);
        if (index_win == 0) { 
            if (this.heart_orange.get_num_heart() <= 0 || this.heart_red.get_num_heart() <= 0)
            {
                if(this.heart_orange.get_num_heart() <= 0)
                {
                    this.txt_n1_result.text = "You win!";
                    this.txt_n2_result.text = "You lose!";
                    this.anim.Play("player_win_orange");
                }
                else
                {
                    this.txt_n1_result.text = "You lose!";
                    this.txt_n2_result.text = "You win!";
                    this.anim.Play("player_win_red");
                }

            }
        }
        else
        {
            if (index_win == 1)
            {
                this.txt_n1_result.text = "You win!";
                this.txt_n2_result.text = "You lose!";
                this.anim.Play("player_win_orange");
            }

            if (index_win == 2)
            {
                this.txt_n1_result.text = "You lose!";
                this.txt_n2_result.text = "You win!";
                this.anim.Play("player_win_red");
            }

        }

        this.p_ready[0] = false;
        this.p_ready[1] = false;
        this.button_player_ready_replay[0].image.color = Color.black;
        this.button_player_ready_replay[1].image.color = Color.black;
        this.GetComponent<Game>().play_sound(5);
    }

    public void stop_game()
    {
        this.is_play = false;
    }

    public void btn_ready_game(int index_player)
    {
        this.button_player_ready_replay[index_player].image.color = this.color_ready_start;
        this.p_ready[index_player] = true;
        this.GetComponent<Game>().play_sound();
        if (this.p_ready[0] == true && this.p_ready[1] == true){
            this.play_game();
            this.GetComponent<Game>().play_sound();
        }
    }
}
