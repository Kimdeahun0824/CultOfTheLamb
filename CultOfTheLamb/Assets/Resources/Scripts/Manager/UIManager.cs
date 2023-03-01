using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IObserver
{
    #region Inspector
    [Header("HP_UI")]
    public Transform ui_Hp_Parent;
    public GameObject ui_Hp;
    public List<Sprite> ui_hp_sprites;
    private List<GameObject> list_Ui_Hp_Obj;

    public Player player;
    #endregion
    private void Awake()
    {
        HpUiInit();
    }

    public void UpdateDate(GameObject data)
    {
        SetHpUi(data);
    }

    public void HpUiInit()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        player.RegisterObserver(this);

        list_Ui_Hp_Obj = new List<GameObject>();

        float hpNumber = player.MaxHp * 0.5f;

        for (int i = 0; i < hpNumber; i++)
        {
            GameObject tempObj = Instantiate(ui_Hp, Vector3.zero, Quaternion.identity, ui_Hp_Parent);
            list_Ui_Hp_Obj.Add(tempObj);
        }
    }

    private void SetHpUi(GameObject data)
    {
        float playerCurrentHp = data.GetComponent<Player>().CurrentHp * 0.5f;

        for (int i = 0; i < list_Ui_Hp_Obj.Count; i++)
        {
            list_Ui_Hp_Obj[i].transform.GetChild(0).gameObject.SetImageSprite(ui_hp_sprites[0]);
        }

        for (int i = 0; i < playerCurrentHp; i++)
        {
            list_Ui_Hp_Obj[i].transform.GetChild(0).gameObject.SetImageSprite(ui_hp_sprites[2]);
            if (playerCurrentHp % 1 != 0)
            {
                if (playerCurrentHp - 1 <= i)
                {
                    list_Ui_Hp_Obj[i].transform.GetChild(0).gameObject.SetImageSprite(ui_hp_sprites[1]);
                }
            }
        }
    }
}
