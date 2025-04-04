using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Carrot.Carrot carrot;
    public IronSourceAds ads;
    [Header("Obj Main")]
    public GameObject panel_start;
    public GameObject panel_play_one;
    public GameObject panel_game_over;
    public GameObject panel_game_win;
    public GameObject panel_info_mode_draft;
    public Map_one_player map_one_player;
    public Game_two_Player game_two_player;
    public GameObject panel_tip_tomato;
    public Text txt_tip_tomato_on;
    public Text txt_tip_tomato_off;
    public Text txt_count_tomato_on;
    public Text txt_count_tomato_off;
    public GameObject obj_btn_next_level;
    public Play_Tip play_tip;
    public Image img_p1_icon_home;
    public Image img_p2_icon_home;

    [Header("Panel info")]
    public Text i_txt_level_info;
    public Text i_txt_game_info;
    public Image i_img_level_tomato;

    [Header("Obj Game")]
    public GameObject Tomato_row_prefab;
    public GameObject Tomato_col_prefab;
    public Transform area_tomato;
    public Game_heart heart;
    public Sprite sp_star_on;
    public Sprite sp_star_off;
    public Sprite sp_draft_on;
    public Sprite sp_draft_off;
    public Image[] img_star_win;

    public Image img_switch_tomato;
    public Image img_switch_tomato_red;
    public Image img_switch_tomato_orange;
    public Image img_tip_tomato_red;
    public Image img_tip_tomato_orange;
    public Image img_status_draft;
    public Sprite sp_tomato_on;
    public Sprite sp_tomato_off;
    public Color32 color_tomato_on;
    public Color32 color_tomato_off;
    public Color32 color_tomato_tip;

    [Header("Sound")]
    public AudioSource[] sound;

    private List<Tomato_col_item> list_col_tomato;
    private List<Tomato_row> list_row_tomato;
    private List<Tomato_item> list_tip_tomato;

    private bool sel_tomato_red = true;
    private int count_tomato_on;
    private int count_tomato_off;
    private bool is_model_draft = false;

    private Sprite sp_tomato_red;
    private Sprite sp_tomato_orange;

    void Start()
    {
        this.panel_start.SetActive(true);
        this.panel_play_one.SetActive(false);
        this.map_one_player.gameObject.SetActive(false);
        this.panel_game_win.SetActive(false);
        this.game_two_player.panel_game_two_player.SetActive(false);
        this.carrot.Load_Carrot(this.check_exit_app);
        this.ads.On_Load();

        this.ads.onRewardedSuccess=this.carrot.game.OnRewardedSuccess;
        this.carrot.act_buy_ads_success=this.ads.RemoveAds;
        this.carrot.game.act_click_watch_ads_in_music_bk=this.ads.ShowRewardedVideo;

        this.carrot.shop.onCarrotPaySuccess += this.carrot_by_success;
        this.play_tip.load_status();

        int index_skin = PlayerPrefs.GetInt("sel_skin", 0);
        this.change_skin_player(index_skin);

        if (this.carrot.get_status_sound())this.carrot.game.load_bk_music(this.sound[6]);
    }

    private void check_exit_app()
    {
        if (this.map_one_player.gameObject.activeInHierarchy)
        {
            this.btn_back_home();
            this.carrot.set_no_check_exit_app();
        }else if (this.panel_play_one.activeInHierarchy)
        {
            this.btn_back_map_select();
            this.carrot.set_no_check_exit_app();
        }else if (this.game_two_player.panel_game_two_player.activeInHierarchy)
        {
            this.btn_back_home();
            this.carrot.set_no_check_exit_app();
        }
    }

    private void create_table_tomato(int col_tomato,int row_tomato)
    {
        this.create_table_header(col_tomato);
        this.list_row_tomato = new List<Tomato_row>();
        for (int r = 0; r < row_tomato; r++)
        {
            GameObject obj_tomato_row = Instantiate(this.Tomato_row_prefab);
            obj_tomato_row.transform.SetParent(this.area_tomato);
            obj_tomato_row.transform.localScale = new Vector3(1f, 1f, 1f);
            obj_tomato_row.GetComponent<Tomato_row>().sp_tomato[0] = this.sp_tomato_red;
            obj_tomato_row.GetComponent<Tomato_row>().sp_tomato[1] = this.sp_tomato_orange;
            obj_tomato_row.GetComponent<Tomato_row>().create_tomato(col_tomato,r);
            obj_tomato_row.GetComponent<Tomato_row>().y = r;
            this.count_tomato_on = this.count_tomato_on+obj_tomato_row.GetComponent<Tomato_row>().get_count_tomato_on();
            this.count_tomato_off = this.count_tomato_off+obj_tomato_row.GetComponent<Tomato_row>().get_count_tomato_off();
            this.list_row_tomato.Add(obj_tomato_row.GetComponent<Tomato_row>());
        }
        this.check_number_header();
    }

    private void create_table_header(int col_tomato)
    {
        this.list_col_tomato = new List<Tomato_col_item>();

        GameObject obj_tomato_head = Instantiate(this.Tomato_row_prefab);
        obj_tomato_head.transform.SetParent(this.area_tomato);
        obj_tomato_head.transform.localScale = new Vector3(1f, 1f, 1f);
        this.carrot.clear_contain(obj_tomato_head.GetComponent<Tomato_row>().area_all_tomato);

        GameObject obj_tomato_row_none = Instantiate(this.Tomato_col_prefab);
        obj_tomato_row_none.transform.SetParent(obj_tomato_head.GetComponent<Tomato_row>().area_all_tomato);
        obj_tomato_row_none.transform.localScale = new Vector3(1f, 1f, 1f);
        obj_tomato_row_none.GetComponent<Tomato_col_item>().txt_number.gameObject.SetActive(false);
        obj_tomato_row_none.GetComponent<Tomato_col_item>().img_bk_number.enabled = false;
        obj_tomato_row_none.GetComponent<Image>().enabled = false;

        for (int c = 0; c < col_tomato; c++)
        {
            GameObject obj_tomato_row = Instantiate(this.Tomato_col_prefab);
            obj_tomato_row.transform.SetParent(obj_tomato_head.GetComponent<Tomato_row>().area_all_tomato);
            obj_tomato_row.transform.localScale = new Vector3(1f, 1f, 1f);
            obj_tomato_row.GetComponent<Tomato_col_item>().x_tomato = c;
            this.list_col_tomato.Add(obj_tomato_row.GetComponent<Tomato_col_item>());
        }
    }

    private void check_number_header()
    {
        for(int i = 0; i < this.list_col_tomato.Count; i++)
        {
            int count_num_tomato_orange = 0;
            for(int y = 0; y < this.map_one_player.get_cur_level().row_tomato; y++)
            {
                if (this.list_row_tomato[y].get_list_tomato()[i].get_type() == false) count_num_tomato_orange++;
            }
            this.list_col_tomato[i].txt_number.text = count_num_tomato_orange.ToString();
        }
    }

    public void btn_switch_tomato()
    {
        if (this.sel_tomato_red)
            this.sel_tomato_red = false;
        else
            this.sel_tomato_red = true;

        this.check_statu_tomato();
        this.play_sound(2);
    }

    public void select_tomato_red()
    {
        this.sel_tomato_red = true;
        this.check_statu_tomato();
        this.play_sound(2);
    }

    public void select_tomato_orange()
    {
        this.sel_tomato_red = false;
        this.check_statu_tomato();
        this.play_sound(2);
    }

    public void select_colum_show_tip(int x_tomato)
    {
        this.reset_tip_tomato();
        this.list_tip_tomato = new List<Tomato_item>();

        for(int i = 0; i < this.list_row_tomato.Count; i++)
        {
            Tomato_item t_col = this.list_row_tomato[i].get_list_tomato()[x_tomato];
            this.list_tip_tomato.Add(t_col);
        }

        this.check_tip_tomato();
    }

    public void select_row_show_tip(int y_tomato)
    {
        this.reset_tip_tomato();
        this.list_tip_tomato = new List<Tomato_item>();
        this.list_tip_tomato = this.list_row_tomato[y_tomato].get_list_tomato();
        this.check_tip_tomato();
    }

    private void check_statu_tomato()
    {
        if (this.sel_tomato_red)
        {
            this.i_img_level_tomato.sprite = this.sp_tomato_red;
            this.img_switch_tomato.sprite = this.sp_tomato_on;
            this.area_tomato.GetComponent<Image>().color = this.color_tomato_on;
            for (int i = 0; i < this.list_col_tomato.Count; i++)
            {
                this.list_col_tomato[i].img_bk_number.enabled = false;
                this.list_col_tomato[i].txt_number.color = Color.black;
            }

            for (int i = 0; i < this.list_row_tomato.Count; i++)
            {
                this.list_row_tomato[i].bk_number.enabled = true;
                this.list_row_tomato[i].txt_number_tomato.color = Color.white;
            }
        }
        else
        {
            this.i_img_level_tomato.sprite = this.sp_tomato_orange;
            this.img_switch_tomato.sprite = this.sp_tomato_off;
            this.area_tomato.GetComponent<Image>().color = this.color_tomato_off;
            for (int i = 0; i < this.list_col_tomato.Count; i++)
            {
                this.list_col_tomato[i].img_bk_number.enabled = true;
                this.list_col_tomato[i].txt_number.color = Color.white;
            }

            for (int i = 0; i < this.list_row_tomato.Count; i++)
            {
                this.list_row_tomato[i].bk_number.enabled = false;
                this.list_row_tomato[i].txt_number_tomato.color = Color.black;
            }
        }
    }

    public void check_tomato_item(Tomato_item t)
    {
        this.panel_tip_tomato.SetActive(false);
        this.reset_tip_tomato();

        if (this.is_model_draft)
        {
            t.play_draft();
            this.play_sound(1);
        }
        else
        {
            if (t.get_type() == this.sel_tomato_red)
            {
                t.play_true();
                this.play_sound(1);
            }
            else
            {
                t.play_false();
                this.heart.remove_heart();
                this.play_sound(3);
                this.carrot.play_vibrate();
            }

            if (t.get_type())
                this.count_tomato_on--;
            else
                this.count_tomato_off--;

            this.check_count_tomato();
        }

    }

    public void btn_play_one()
    {
        this.carrot.clear_contain(this.map_one_player.area_all_item_level);
        this.map_one_player.create_map();
        this.panel_start.SetActive(false);
        this.play_sound();
    }

    public void btn_play_two()
    {
        this.game_two_player.play_game();
        this.panel_start.SetActive(false);
        this.play_sound();
    }

    public void btn_back_home()
    {
        this.panel_start.SetActive(true);
        this.panel_play_one.SetActive(false);
        this.map_one_player.gameObject.SetActive(false);
        this.game_two_player.panel_game_two_player.SetActive(false);
        this.game_two_player.stop_game();
        this.play_sound();
        this.ads.show_ads_Interstitial();
    }

    public void btn_back_map_select()
    {
        this.ads.ShowBannerAd();
        this.map_one_player.gameObject.SetActive(true);
        this.panel_play_one.SetActive(false);
        this.panel_game_over.SetActive(false);
        this.panel_game_win.SetActive(false);
        this.play_sound();
        this.map_one_player.check_info_all_level();
        this.ads.show_ads_Interstitial();
    }

    public void select_map_level_player_one(Level_item lv)
    {
        this.ads.HideBannerAd();
        this.i_txt_level_info.text = lv.txt_level.text;
        this.i_txt_game_info.text = lv.row_tomato + " x " + lv.col_tomato;
        this.count_tomato_on = 0;
        this.count_tomato_off = 0;
        this.list_tip_tomato = null;
        this.map_one_player.set_cur_level(lv);
        this.carrot.clear_contain(this.heart.area_all_heart);
        this.heart.create_heart();
        this.carrot.clear_contain(this.area_tomato);
        this.panel_play_one.SetActive(true);
        this.panel_tip_tomato.SetActive(false);
        this.map_one_player.gameObject.SetActive(false);
        this.panel_game_win.SetActive(false);
        this.create_table_tomato(lv.col_tomato, lv.row_tomato);
        this.play_sound();
        this.check_count_tomato();
        this.check_statu_tomato();
        this.panel_info_mode_draft.SetActive(false);
        this.is_model_draft = false;
        this.img_status_draft.sprite = this.sp_draft_on;
    }

    public void play_sound(int index_sound=0)
    {
        if(this.carrot.get_status_sound()) this.sound[index_sound].Play();
    }

    public void show_game_over()
    {
        this.carrot.delay_function(1f, act_game_over);
    }

    private void act_game_over()
    {
        this.panel_game_over.SetActive(true);
    }

    public void btn_game_replay_level()
    {
        this.panel_game_over.SetActive(false);
        this.panel_game_win.SetActive(false);
        this.select_map_level_player_one(this.map_one_player.get_cur_level());
    }

    public void show_tip_tomato(Tomato_item t)
    {
        this.reset_tip_tomato();
        this.list_tip_tomato = new List<Tomato_item>();

        t.GetComponent<Image>().color = this.color_tomato_tip;
        this.list_tip_tomato.Add(t);

        if (t.y > 0&&t.x>0)
        {
            Tomato_item t_top_left = this.list_row_tomato[t.y - 1].get_list_tomato()[t.x-1];
            this.list_tip_tomato.Add(t_top_left);
        }

        if (t.y > 0) { 
            Tomato_item t_top = this.list_row_tomato[t.y-1].get_list_tomato()[t.x];
            this.list_tip_tomato.Add(t_top);
        }

        if (t.y > 0 && t.x < this.map_one_player.get_cur_level().col_tomato - 1)
        {
            Tomato_item t_top_right = this.list_row_tomato[t.y - 1].get_list_tomato()[t.x + 1];
            this.list_tip_tomato.Add(t_top_right);
        }

        if (t.x>0) { 
            Tomato_item t_left = this.list_row_tomato[t.y].get_list_tomato()[t.x-1];
            this.list_tip_tomato.Add(t_left);
        }

        if (t.x<this.map_one_player.get_cur_level().col_tomato-1)
        {
            Tomato_item t_right = this.list_row_tomato[t.y].get_list_tomato()[t.x+1];
            this.list_tip_tomato.Add(t_right);
        }

        if (t.y< this.map_one_player.get_cur_level().row_tomato-1)
        {
            Tomato_item t_bottom = this.list_row_tomato[t.y+1].get_list_tomato()[t.x];
            this.list_tip_tomato.Add(t_bottom);
        }

        if (t.y < this.map_one_player.get_cur_level().row_tomato - 1&&t.x>0)
        {
            Tomato_item t_bottom_left = this.list_row_tomato[t.y + 1].get_list_tomato()[t.x-1];
            this.list_tip_tomato.Add(t_bottom_left);
        }

        if (t.y < this.map_one_player.get_cur_level().row_tomato - 1 && t.x < this.map_one_player.get_cur_level().col_tomato - 1)
        {
            Tomato_item t_bottom_right = this.list_row_tomato[t.y + 1].get_list_tomato()[t.x + 1];
            this.list_tip_tomato.Add(t_bottom_right);
        }
        this.check_tip_tomato();
        this.play_sound(4);
    }

    private void check_tip_tomato()
    {
        this.panel_tip_tomato.SetActive(true);
        int count_tomato_on=0;
        int count_tomato_off=0;
        for (int i = 0; i < this.list_tip_tomato.Count; i++) { 
            this.list_tip_tomato[i].GetComponent<Image>().color = this.color_tomato_tip;
            if (this.list_tip_tomato[i].get_type())
                count_tomato_on++;
            else
                count_tomato_off++;
        }

        this.txt_tip_tomato_on.text = count_tomato_on.ToString();
        this.txt_tip_tomato_off.text = count_tomato_off.ToString();
    }

    private void reset_tip_tomato()
    {
        if (this.list_tip_tomato != null) for(int i = 0; i < this.list_tip_tomato.Count; i++) this.list_tip_tomato[i].GetComponent<Image>().color = Color.white;
    }

    public void close_tip_tomato()
    {
        this.panel_tip_tomato.SetActive(false);
        this.reset_tip_tomato();
    }

    public void check_count_tomato()
    {
        this.txt_count_tomato_on.text = this.count_tomato_on.ToString();
        this.txt_count_tomato_off.text = this.count_tomato_off.ToString();
        if (this.count_tomato_on == 0 && this.count_tomato_off == 0 && this.heart.count_heart()>1) this.carrot.delay_function(1f,show_game_win);
    }

    private void show_game_win()
    {
        this.map_one_player.get_cur_level().set_data_level(this.heart.get_num_heart());
        if (this.map_one_player.get_cur_level().index_level < this.map_one_player.num_level-1) { 
            this.map_one_player.get_next_level().set_open_level();
            this.obj_btn_next_level.SetActive(true);
        }
        else
        {
            this.obj_btn_next_level.SetActive(false);
        }

        for(int i = 0; i < this.img_star_win.Length; i++)
        {
            if (i < this.heart.get_num_heart())
                this.img_star_win[i].sprite = this.sp_star_on;
            else
                this.img_star_win[i].sprite = this.sp_star_off;
        }

        this.panel_game_win.SetActive(true);
        this.play_sound(5);
    }

    public void btn_next_level()
    {
        this.select_map_level_player_one(this.map_one_player.get_next_level());
    }

    public void btn_show_setting()
    {
        Carrot.Carrot_Box box_setting=this.carrot.Create_Setting();
        box_setting.set_act_before_closing(act_before_closing);
    }

    private void act_before_closing(List<string> list_change)
    {
        foreach(string s in list_change)
        {
            Debug.Log("change item:" + s);
            if (s == "list_bk_music") this.carrot.game.load_bk_music(this.sound[6]);
        }
        if (this.carrot.get_status_sound())
            this.sound[6].Play();
        else
            this.sound[6].Stop();

    }

    public void btn_show_rate()
    {
        this.play_sound();
        this.carrot.show_rate();
    }

    public void btn_show_share()
    {
        this.play_sound();
        this.carrot.show_share();
    }

    public void btn_show_list_other_app()
    {
        this.play_sound();
        this.carrot.show_list_carrot_app();
    }

    public void btn_remove_ads()
    {
        this.play_sound();
        this.carrot.buy_inapp_removeads();
    }

    public void btn_in_app_restore()
    {
        this.play_sound();
        this.carrot.restore_product();
    }

    public void btn_delete_all_data()
    {
        this.carrot.Delete_all_data();
        this.carrot.delay_function(1f,this.Start);
        this.play_sound();
    }

    public void carrot_by_success(string s_id_product)
    {
        if (s_id_product == this.carrot.shop.get_id_by_index(1))
        {
            this.carrot.Show_msg("Shop", "Successful item purchase! you can use!", Carrot.Msg_Icon.Success);
            this.GetComponent<Game_shop>().buy_success_skin();
        }
    }

    public void select_skin_player(int index_skin)
    {
        PlayerPrefs.SetInt("sel_skin",index_skin);
        this.change_skin_player(index_skin);
        this.play_sound();
    }

    public void buy_skin_player(int index_skin)
    {
        this.GetComponent<Game_shop>().set_index_buy(index_skin);
        this.carrot.buy_product(1);
        this.play_sound();
    }

    private void change_skin_player(int index_skin)
    {
        this.sp_tomato_red = this.GetComponent<Game_shop>().s_icon[index_skin];
        this.sp_tomato_orange = this.GetComponent<Game_shop>().s_icon2[index_skin];
        this.img_p1_icon_home.sprite = this.sp_tomato_red;
        this.img_p2_icon_home.sprite = this.sp_tomato_orange;
        this.game_two_player.img_icon_p1.sprite = this.sp_tomato_red;
        this.game_two_player.img_icon_p2.sprite = this.sp_tomato_orange;
        this.game_two_player.img_icon_p1_reslut.sprite = this.sp_tomato_red;
        this.game_two_player.img_icon_p2_reslut.sprite = this.sp_tomato_orange;
        this.img_switch_tomato_red.sprite = this.sp_tomato_red;
        this.img_switch_tomato_orange.sprite = this.sp_tomato_orange;
        this.img_tip_tomato_red.sprite = this.sp_tomato_red;
        this.img_tip_tomato_orange.sprite = this.sp_tomato_orange;
        this.game_two_player.img_icon_tip_p1_red.sprite = this.sp_tomato_red;
        this.game_two_player.img_icon_tip_p1_orange.sprite = this.sp_tomato_orange;
        this.game_two_player.img_icon_tip_p2_red.sprite = this.sp_tomato_red;
        this.game_two_player.img_icon_tip_p2_orange.sprite = this.sp_tomato_orange;
    }


    public void btn_active_model_draft()
    {
        if (this.is_model_draft)
        {
            this.btn_close_model_draft();
            this.set_model_draft_all_row_tomato();
            this.img_status_draft.sprite = this.sp_draft_on;
        }
        else
        {
            this.img_status_draft.sprite = this.sp_draft_off;
            this.is_model_draft = true;
            this.panel_info_mode_draft.SetActive(true);
            this.play_sound();
            this.set_model_draft_all_row_tomato();
        }
    }

    public void btn_close_model_draft()
    {
        this.img_status_draft.sprite = this.sp_draft_on;
        this.is_model_draft = false;
        this.panel_info_mode_draft.SetActive(false);
        this.play_sound();
    }

    private void set_model_draft_all_row_tomato()
    {
        for (int i = 0; i < this.list_row_tomato.Count; i++)
        {
            this.list_row_tomato[i].set_model_draft(this.is_model_draft);
        }
    }

    public void btn_show_list_game_music_background()
    {
        this.sound[6].Pause();
        this.play_sound();
        this.carrot.game.show_list_music_game();
    }

    public void btn_show_login_user()
    {
        this.carrot.user.show_login();
    }

    public void btn_show_ranks()
    {
        this.carrot.game.Show_List_Top_player();
    }
}
