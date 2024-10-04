using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sequence= DG.Tweening.Sequence;
public class UI : MonoBehaviour
{
    public Image Healthbar;
    public Image dash;
    public Image stigma;
    public Image faint;
    public Image bowRate;
    public Image[] invenbigpicture;
    public Image[] invensmallpicture;
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
    public GameObject bullet4;
    public GameObject bullet5;
    public GameObject bullet6;
    public Text stigmarate;
    public Text potionText;
    public Text overtext;
    public Text todeserttext;
    public Text mainmenutext;
    public Text reloading;
    public Text damagerate;
    public Text inveninfo;
    private Player info;
    public WeaponManager infoweapon;
    public GunSystem guninfo;
    public BowSystem bowinfo;
    public MeleeSystem meleeinfo;
    public GameObject pauseui;
    public GameObject gameoverui;
    public GameObject invenui;
    public GameObject howtoui;
    public scenefade fader;
    public scenefade overfade;
    public Image fadepicture;
    private float currentHP;
    private float hp;
    public float dashtime;
    int mark;
    int potionCnt;
    int currentweapon;
    bool reload;
    int bullet;
    int damage;
    public bool isPause;
    public bool isOver;
    public bool isinven;
    public bool ishow;
    Sequence texting;
    Sequence reloadtext;
    public GameObject gunimage;
    public GameObject bowimage;
    public GameObject swordimage;
    public bool isconversation;
    public Slider bgmSlider;
    public Slider sfxSlider;
    
    // Start is called before the first frame update
    private void Awake()
    {
        info = GetComponent<Player>();
        infoweapon = GetComponentInChildren<WeaponManager>();
        guninfo= GetComponentInChildren<GunSystem>(true);
        bowinfo= GetComponentInChildren<BowSystem>(true);
        meleeinfo=GetComponentInChildren<MeleeSystem>(true);
        dashtime = 1.7f;
        isPause = false;
        isOver = false;
        isinven = false;
        ishow = false;
        isconversation = false;
        fadepicture.enabled = false;
        pauseui.SetActive(false);
        gameoverui.SetActive(false);
        invenui.SetActive(false);
        howtoui.SetActive(false);
        gunimage.SetActive(false);
        bowimage.SetActive(false);
        swordimage.SetActive(false);
        bullet1.SetActive(false);
        bullet2.SetActive(false);
        bullet3.SetActive(false);
        bullet4.SetActive(false);
        bullet5.SetActive(false);
        bullet6.SetActive(false);
        texting = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(overtext.DOText("Game over", 2f))
            .Append(todeserttext.DOText("사막으로", 2f))
            .Join(mainmenutext.DOText("메인메뉴로", 2f)).OnComplete(() => isOver = true);
        reloadtext = DOTween.Sequence().SetAutoKill(false).Pause()
             .Append(reloading.DOText("Reloading...", 1f));
        for(int i = 0; i < 12; i++)
        {
            invenbigpicture[i].enabled = false;
        }
        for (int i = 1; i < 12; i++)
        {
            invensmallpicture[i].enabled = false;
        }


    }
    private void Start()
    {
        currentHP = info.playerHP;
        hp = info.playerMaxHP;
        faint.enabled = false;
        mark = info.markCnt;
        potionCnt = info.potionCnt;
        weaponchange();
        gunreload();
        bowrate();

    }
    private void Update()
    {
        currentHP = info.playerHP;
        mark = info.markCnt;
        potionCnt = info.potionCnt;
        hpbarchange();
        dashbarchange();
        faintchange();
        Markcntchange();
        PotionChange();
        gunreload();
        bowrate();
        if (Input.GetMouseButtonDown(1)&& dashtime>=1.7f&& isPause==false )
        {
            dashtime = 0;

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            weaponchange();
        }
        
           
        
        if (dashtime <1.7f)
        {
            dashtime += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Escape)&& isconversation==false)
        {
            if (isPause == false&& currentHP>0 && isinven==false && ishow==false)
            {
                info.isPlaying = false;
                Time.timeScale = 0;
                pauseui.SetActive(true);
                isPause = true;
                SoundManager.instance.Filtering(true);
            }
            else if (isPause == true)
            {
                Cursor.visible = false;
                info.isPlaying = true;
                Time.timeScale = 1;
                pauseui.SetActive(false);
                isPause = false;
                SoundManager.instance.Filtering(false);
            }
            else if (isinven == true)
            {
                isinven = false;
                isPause = true;
                invenui.SetActive(false);
                pauseui.SetActive(true);
            }
            else if (ishow == true)
            {
                ishow = false;
                isPause = true;
                howtoui.SetActive(false);
                pauseui.SetActive(true);
            }
            
         
        }
        if (isPause == true)
        {
            Cursor.visible = true;
            Vector3 point = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x,
                   Input.mousePosition.y, -Camera.main.transform.position.z));
            
                if (point.x >= 0.4369231 && point.x <= 0.5838462 && point.y >= 0.525625 && point.y <= 0.5838462)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    invencheck();
                    isinven = true;
                    isPause = false;
                    pauseui.SetActive(false);
                    invenui.SetActive(true);
                }
            }
                if (point.x >= 0.40 && point.x <= 0.59 && point.y >= 0.39 && point.y <= 0.45)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    fadepicture.enabled = true;
                    menuani.mainmenustate = 5;
                    Time.timeScale = 1;
                    SoundManager.instance.Filtering(false);
                    overfade.FadeTo("gamemenu");
                }
            }
            if (point.x >= 0.4809437 && point.x <= 0.5526316 && point.y >= 0.2950318 && point.y <= 0.3215744)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ishow = true;
                    isPause = false;
                    pauseui.SetActive(false);
                    howtoui.SetActive(true);
                }
            }
        }
        if (isinven == true)
        {
            Vector3 point = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x,
                  Input.mousePosition.y, -Camera.main.transform.position.z));
            if (Input.GetMouseButtonDown(0))
            {
                if (point.x >= 0.07230769 && point.x <= 0.1515385 && point.y >= 0.704375 && point.y <= 0.875625)
                {
                   
                    for (int i = 0; i < 12; i++)
                    {
                        invenbigpicture[i].enabled = false;
                    }
                    invenbigpicture[0].enabled = true;
                    inveninfo.text = "보아뱀의 독\n\n병에 뱀이 감겨져있는 모형이다.\n이 별에 떨어졌을 때부터 있었던 듯 싶다.\n병에는 '전 소유자:TLP-612'라고 적혀있다.";
                }
                if (point.x >= 0.1784615 && point.x <= 0.2269231 && point.y >= 0.711875 && point.y <= 0.794375)
                {
                    if (Datamanager.instance.nowPlayer.record1 == true)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            invenbigpicture[i].enabled = false;
                        }
                        invenbigpicture[1].enabled = true;
                        inveninfo.text = "파란색 레코드\n\n파란색 레코드이다.\n'#정원사태에 대하여1'이라고 적혀있다.\n사막의 레코드플레이어에서 확인 해볼 수 있을 것 같다.";
                    }
                }
                if (point.x >= 0.2592308 && point.x <= 0.3053846 && point.y >= 0.709375 && point.y <= 0.793125)
                {
                    if (Datamanager.instance.nowPlayer.record2 == true)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            invenbigpicture[i].enabled = false;
                        }
                        invenbigpicture[2].enabled = true;
                        inveninfo.text = "녹색 레코드\n\n녹색 레코드이다.\n'#정원사태에 대하여2'라고 적혀있다.\n사막의 레코드플레이어에서 확인 해볼 수 있을 것 같다.";
                    }
                }
                if (point.x >= 0.3353846 && point.x <= 0.3853846 && point.y >= 0.710625 && point.y <= 0.791875)
                {
                    if (Datamanager.instance.nowPlayer.record3 == true)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            invenbigpicture[i].enabled = false;
                        }
                        invenbigpicture[3].enabled = true;
                        inveninfo.text = "연두색 레코드\n\n연두색 레코드이다.\n'#정원사태에 대하여3'이라고 적혀있다.\n사막의 레코드플레이어에서 확인 해볼 수 있을 것 같다.";
                    }
                }
                if (point.x >= 0.4161538 && point.x <= 0.4653846 && point.y >= 0.714375 && point.y <= 0.794375)
                {
                    if (Datamanager.instance.nowPlayer.record4 == true)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            invenbigpicture[i].enabled = false;
                        }
                        invenbigpicture[4].enabled = true;
                        inveninfo.text = "주황색 레코드\n\n주황색 레코드이다.\n'#정원사태에 대하여4'라고 적혀있다.\n사막의 레코드플레이어에서 확인 해볼 수 있을 것 같다.";
                    }
                }
                if (point.x >= 0.4992308 && point.x <= 0.5469231 && point.y >= 0.714375 && point.y <= 0.791875)
                {
                    if (Datamanager.instance.nowPlayer.record5 == true)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            invenbigpicture[i].enabled = false;
                        }
                        invenbigpicture[5].enabled = true;
                        inveninfo.text = "핑크색 레코드\n\n핑크색 레코드이다.\n'#후일담'라고 적혀있다.\n사막의 레코드플레이어에서 확인 해볼 수 있을 것 같다.";
                    }
                }
                if (point.x >= 0.5815384 && point.x <= 0.6253846 && point.y >= 0.716875 && point.y <= 0.791875)
                {
                    if (Datamanager.instance.nowPlayer.record6 == true)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            invenbigpicture[i].enabled = false;
                        }
                        invenbigpicture[6].enabled = true;
                        inveninfo.text = "보라색 레코드\n\n보라색 레코드이다.\n'#보아뱀의 독에 관해 알아낸 사실'이라고 적혀있다.\n사막의 레코드플레이어에서 확인 해볼 수 있을 것 같다.";
                    }
                }
                if (point.x >= 0.6607692 && point.x <= 0.7092308 && point.y >= 0.713125 && point.y <= 0.790625)
                {
                    if (Datamanager.instance.nowPlayer.record7 == true)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            invenbigpicture[i].enabled = false;
                        }
                        invenbigpicture[7].enabled = true;
                        inveninfo.text = "빨간색 레코드\n\n빨간색 레코드이다.\n'#세계선의 법칙'이라고 적혀있다.\n사막의 레코드플레이어에서 확인 해볼 수 있을 것 같다.";
                    }
                }
                if (point.x >= 0.7407692 && point.x <= 0.7861539 && point.y >= 0.714375 && point.y <= 0.786875)
                {
                    if (Datamanager.instance.nowPlayer.record8 == true)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            invenbigpicture[i].enabled = false;
                        }
                        invenbigpicture[8].enabled = true;
                        inveninfo.text = "자홍색 레코드\n\n자홍색 레코드이다.\n'#별의 주인에 대해서'라고 적혀있다.\n사막의 레코드플레이어에서 확인 해볼 수 있을 것 같다.";
                    }

                }
                if (point.x >= 0.8246154 && point.x <= 0.8630769 && point.y >= 0.718125 && point.y <= 0.794375)
                {
                    if (Datamanager.instance.nowPlayer.record9 == true)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            invenbigpicture[i].enabled = false;
                        }
                        invenbigpicture[9].enabled = true;
                        inveninfo.text = "노란색 레코드\n\n노란색 레코드이다.\n'#결계에 대하여'라고 적혀있다.\n사막의 레코드플레이어에서 확인 해볼 수 있을 것 같다.";
                    }
                }
                if (point.x >= 0.7946154 && point.x <= 0.8161538 && point.y >= 0.860625 && point.y <= 0.956875)
                {
                    if (Datamanager.instance.nowPlayer.progress == 2 || Datamanager.instance.nowPlayer.progress == 4)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            invenbigpicture[i].enabled = false;
                        }
                        invenbigpicture[10].enabled = true;
                        inveninfo.text = "TLP의 조각(좌)\n\n'고독한 왕'과 흡수되었던 TLP의 조각 중 하나이다.\n두 개를 모으면 제단에 바쳐 결계를 없앨 수 있을것이다.";
                    }
                }
                if (point.x >= 0.8469231 && point.x <= 0.8684615 && point.y >= 0.856875 && point.y <= 0.963125)
                {
                    if (Datamanager.instance.nowPlayer.progress == 3 || Datamanager.instance.nowPlayer.progress == 4)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            invenbigpicture[i].enabled = false;
                        }
                        invenbigpicture[11].enabled = true;
                        inveninfo.text = "TLP의 조각(우)\n\n'점등인'과 흡수되었던 TLP의 조각 중 하나이다.\n두 개를 모으면 제단에 바쳐 결계를 없앨 수 있을것이다.";
                    }
                }

              
            }
        }
        if (currentHP <= 0)
        {
            gameoverui.SetActive(true);
            texting.Play();
            
            
        }
        if (isOver == true)
        {
            fadepicture.enabled = true;
            Vector3 point = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x,
                   Input.mousePosition.y, -Camera.main.transform.position.z));

            if (point.x >= 0.41 && point.x <= 0.57 && point.y >= 0.59 && point.y <= 0.65)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    currentHP = 10f;
                    Datamanager.instance.nowPlayer.hp = currentHP;
                    Datamanager.instance.SaveData();
                    gameoverui.SetActive(false);
                    isOver = false;
                    
                    overfade.FadeTo("desertmap");
                    
                }
            }
            if (point.x >= 0.40 && point.x <= 0.59 && point.y >= 0.39 && point.y <= 0.45)
            {
                if (Input.GetMouseButtonDown(0))
                {

                    currentHP = 10f;
                    Datamanager.instance.nowPlayer.hp = currentHP;
                    Datamanager.instance.SaveData();
                    gameoverui.SetActive(false);
                    isOver = false;
                    menuani.mainmenustate = 5;
                    overfade.FadeTo("gamemenu");
                }
            }
           
        }
        
      
    }
    void hpbarchange()
    {
        Healthbar.fillAmount = Mathf.Lerp(Healthbar.fillAmount, currentHP/hp, (float)0.2f);
        
    }
    void dashbarchange()
    {
        if (info.canDash == false)
            dash.fillAmount = Mathf.Lerp(dash.fillAmount, dashtime / 1.7f, 1);
        else dash.fillAmount = 1;

    }
    void faintchange()
    {
        if (info._stateType == PlayerStateType.Stunned)
        {
            faint.enabled = true;
        }
        else faint.enabled = false;
       
    }
    void Markcntchange()
    {
        if (mark > 0)
        {
            stigma.enabled = true;
            stigmarate.text = mark.ToString();

        }
        else
        {
            stigma.enabled = false;
            stigmarate.text = "";
        }
    }

    void PotionChange()
    {
        potionText.text = "x " + potionCnt.ToString();
    }
    void weaponchange()
    {
        currentweapon = infoweapon.currWeaponIdx;
        //currentweapon = Datamanager.instance.nowPlayer.weapon;
        if (currentweapon == 0)
        {
            gunimage.SetActive(true);
            bowimage.SetActive(false);
            swordimage.SetActive(false);
            damage = (int)guninfo.data.damage;
            damagerate.text = "Damage :" +" "+ damage;
        }
        else if(currentweapon == 1)
        {
            gunimage.SetActive(false);
            bowimage.SetActive(true);
            swordimage.SetActive(false);
        }
        else if (currentweapon == 2)
        {
            gunimage.SetActive(false);
            bowimage.SetActive(false);
            swordimage.SetActive(true);
            damage = (int)meleeinfo.data.damage;
            damagerate.text = "Damage :" + " " + damage;
        }
    }
    void gunreload()
    {
        if (currentweapon == 0)
        {
            reload = guninfo.isReloading;
            if (reload == true)
            {
                reloadtext.Play();
            }
            else
            {
                reloadtext.Rewind();
                bullet = guninfo.data.currAmmo;
                if (bullet == 6)
                {
                    bullet1.SetActive(true);
                    bullet2.SetActive(true);
                    bullet3.SetActive(true);
                    bullet4.SetActive(true);
                    bullet5.SetActive(true);
                    bullet6.SetActive(true);
                }
                else if (bullet == 5)
                {
                    bullet1.SetActive(false);
                }
                else if (bullet == 4)
                {
                    bullet2.SetActive(false);
                }
                else if (bullet == 3)
                {
                    bullet3.SetActive(false);
                }
                else if (bullet == 2)
                {
                    bullet4.SetActive(false);
                }
                else if (bullet == 1)
                {
                    bullet5.SetActive(false);
                }
                else { bullet6.SetActive(false); }
            }
        }
    }
    void bowrate()
    {
        if (currentweapon == 1)
        {
            damage = (int)bowinfo.arrowDamage;
            damagerate.text = "Damage :" + " " + damage.ToString();
            // bowRate.fillAmount= Mathf.Lerp(bowRate.fillAmount, (bowinfo.arrowDamage - 3) / 7, 0.2f);
            bowRate.fillAmount= Mathf.Lerp(bowRate.fillAmount, ((bowinfo.arrowDamage - bowinfo.data.defaultDamage) / (bowinfo.data.maxDamage - bowinfo.data.defaultDamage)), 0.2f);
            Color ratecolor = Color.Lerp(Color.blue, Color.red, ((bowinfo.arrowDamage - bowinfo.data.defaultDamage) / (bowinfo.data.maxDamage - bowinfo.data.defaultDamage)));
            // Color ratecolor = Color.Lerp(Color.blue, Color.red, (bowinfo.arrowDamage - 3) / 7);
            bowRate.color = ratecolor;
        }
    }
    void invencheck()
    {
        if (Datamanager.instance.nowPlayer.record1 == true)
            invensmallpicture[1].enabled = true;
        if (Datamanager.instance.nowPlayer.record2 == true)
            invensmallpicture[2].enabled = true;
        if (Datamanager.instance.nowPlayer.record3 == true)
            invensmallpicture[3].enabled = true;
        if (Datamanager.instance.nowPlayer.record4 == true)
           invensmallpicture[4].enabled = true;
        if (Datamanager.instance.nowPlayer.record5 ==true)
            invensmallpicture[5].enabled = true;
        if (Datamanager.instance.nowPlayer.record6 ==true)
           invensmallpicture[6].enabled = true;
        if (Datamanager.instance.nowPlayer.record7 == true)
            invensmallpicture[7].enabled = true;
        if (Datamanager.instance.nowPlayer.record8 == true)
            invensmallpicture[8].enabled = true;
        if (Datamanager.instance.nowPlayer.record9 == true)
            invensmallpicture[9].enabled = true;
        if (Datamanager.instance.nowPlayer.progress == 2)
            invensmallpicture[10].enabled = true;
        else if (Datamanager.instance.nowPlayer.progress == 3)
            invensmallpicture[11].enabled = true;
        else if (Datamanager.instance.nowPlayer.progress == 4)
        {
            invensmallpicture[10].enabled = true;
            invensmallpicture[11].enabled = true;
        }
    }

    public void SetBGM()
    {
        SoundManager.instance.BGMSoundVolume(bgmSlider.value);
    }

    public void SetSFX()
    {
        SoundManager.instance.SFXSoundVolume(sfxSlider.value);
    }
}
