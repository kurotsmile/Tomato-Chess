using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Play_Tip : MonoBehaviour
{
    public GameObject panel_play_tip;
    public GameObject btn_play_tip;
    public Text txt_show_tip;
    public Text txt_home_tip;
    private bool is_tip = false;
    public string[] s_tip;

    private int index_tip=0;
    private float speed_tip = 0f;

    public void load_status()
    {
        if (PlayerPrefs.GetInt("is_tip", 0) == 0)
            this.is_tip = false;
        else
            this.is_tip = true;

        this.check_status_tip();
        this.show_tip();
    }

    public void btn_show_hide_play_tip()
    {
        if (this.is_tip)
        {
            this.is_tip = false;
            PlayerPrefs.SetInt("is_tip", 0);
        }
        else
        {
            this.is_tip = true;
            PlayerPrefs.SetInt("is_tip", 1);
        }
        this.check_status_tip();
        this.GetComponent<Game>().play_sound();
    }

    private void Update()
    {

            this.speed_tip+= 2f * Time.deltaTime;
            if (this.speed_tip >6f)
            {
                this.index_tip++;
                if (this.index_tip >= this.s_tip.Length) this.index_tip = 0;
                this.show_tip();
                this.speed_tip = 0f;
            }

    }

    private void check_status_tip()
    {
        if (this.is_tip)
        {
            this.panel_play_tip.SetActive(true);
            this.btn_play_tip.SetActive(false);
        }
        else
        {
            this.panel_play_tip.SetActive(false);
            this.btn_play_tip.SetActive(true);
        }
    }

    private void show_tip()
    {
        this.txt_show_tip.text = this.s_tip[this.index_tip];
        this.txt_home_tip.text = this.s_tip[this.index_tip];
    }
}
