using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviourPunCallbacks
{

    Dictionary<string, GameObject> _clothes = new Dictionary<string, GameObject>();
    Skill _classSkill;
    PlayerStat playerStat;
    PhotonView photonView;
    PlayerController _playerController;
    Define.ClassType _currentClass;
    
    
    public Skill ClassSkill
    {
        get { return _classSkill; }
        set { _classSkill = value; }
    }
    
    void Start()
    {
        
        Init();
        
    }


    void Init()
    {

        photonView = gameObject.GetComponent<PhotonView>();
        playerStat = gameObject.GetComponent<PlayerController>().Stat;
        _playerController = gameObject.GetComponent<PlayerController>();



        string[] names = System.Enum.GetNames(typeof(Define.Clothes));

        foreach(string name in names)
        {
            Debug.Log(name);
            GameObject go = Util.FindChild(gameObject, name, true);
            _clothes.Add(name, go);
            go.SetActive(false);
        }

        _clothes["NoneBody"].SetActive(true);

        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom && !photonView.IsMine)
        { // 내가 방에 들어 왔는데 다른 사람들 캐릭터가 불러와지는 경우
            Player p = photonView.Owner;
            if (p.CustomProperties.TryGetValue("Class", out object classType))
            {
                Define.ClassType type = (Define.ClassType)classType;
                ChangeClass(type);

            }
        }
        else
        {
            ChangeClass(Define.ClassType.None);
        }

        

    }

    void UndresseAll()
    {
        foreach(var go in _clothes.Values)
        {
            go.SetActive(false);
        }
        
    }

    public void ChangeClass(Define.ClassType type)
    {
        UndresseAll();
        _currentClass = type;
        if (photonView.IsMine && PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            Managers.Player.UpdateLocalPlayerInfo(type);
        }
        switch (type)
        {
            case Define.ClassType.Warrior:
                DressUpWarriorSet();
                break;
            case Define.ClassType.Priest:
                DressUpPriestSet();
                break;
            case Define.ClassType.Mage:
                DressUpMageSet();
                break;
            case Define.ClassType.Ninja:
                DressUpNinjaSet();
                break;
            default:
                DressUpNoneSet();
                break;
        }

    }
    void DressUpNoneSet()
    {
        // stat

        playerStat.MaxHp = 100;
        playerStat.Hp = 100;
        playerStat.MaxMp = 100;
        playerStat.Mp = 100;
        playerStat.MoveSpeed = 5.0f;


        _clothes["NoneBody"].SetActive(true);


        _playerController.ObtainWeapon("0028_BubbleWand");

        _classSkill = gameObject.GetOrAddComponent<EmptySkill>();
    }


    void DressUpWarriorSet()
    {
        playerStat.MaxHp = 300;
        playerStat.Hp = 300;
        playerStat.MaxMp = 100;
        playerStat.Mp = 100;
        playerStat.MoveSpeed = 5.0f;

        _clothes["WarriorBody"].SetActive(true);
        _clothes["WarriorHat"].SetActive(true);
        _clothes["WarriorCloak"].SetActive(true);
        _clothes["WarriorShield"].SetActive(true);

        _playerController.ObtainWeapon("0012_HeroSword");

        _classSkill = gameObject.GetOrAddComponent<WarriorClassSkill>();
    }
    void DressUpPriestSet()
    {
        playerStat.MaxHp = 150;
        playerStat.Hp = 150;
        playerStat.MaxMp = 200;
        playerStat.Mp = 200;
        playerStat.MoveSpeed = 5.0f;

        _clothes["PriestBody"].SetActive(true);
        _clothes["PriestHat"].SetActive(true);

        _playerController.ObtainWeapon("0028_BubbleWand");

        _classSkill = gameObject.GetOrAddComponent<PriestClassSkill>();
    }
    void DressUpMageSet()
    {

        playerStat.MaxHp = 100;
        playerStat.Hp = 100;
        playerStat.MaxMp = 300;
        playerStat.Mp = 300;
        playerStat.MoveSpeed = 5.0f;

        _clothes["MageBody"].SetActive(true);
        _clothes["MageHat"].SetActive(true);
        _clothes["MageBackPack"].SetActive(true);


        _playerController.ObtainWeapon("0028_BubbleWand");

        _classSkill = gameObject.GetOrAddComponent<MageClassSkill>();
    }
    void DressUpNinjaSet()
    {
        playerStat.MaxHp = 100;
        playerStat.Hp = 100;
        playerStat.MaxMp = 150;
        playerStat.Mp = 150;
        playerStat.MoveSpeed = 6.5f;



        _clothes["NinjaBody"].SetActive(true);
        _clothes["NinjaHair"].SetActive(true);
        _clothes["NinjaMask"].SetActive(true);


        _playerController.ObtainWeapon("0012_HeroSword");

        _classSkill = gameObject.GetOrAddComponent<NinjaClassSkill>();
    }


}