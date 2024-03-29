using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// HUD(Head-Up Display)를 관리하는 클래스입니다.
/// </summary>
public class HUDManager : MonoBehaviour
{
    // 모험 상태
    [Header("[ 모험 상태 ]")]
    public TextMeshProUGUI nicknameText; // 닉네임 텍스트

    // 플레이어 상태
    [Header("[ 플레이어 상태 ]")]
    public TextMeshProUGUI playerHealthText; // 플레이어 체력 텍스트
    public Slider playerHealthSlider; // 플레이어 체력 바 슬라이더
    public TextMeshProUGUI playerManaText; // 플레이어 마나 텍스트
    public Slider playerManaSlider; // 플레이어 마나 바 슬라이더
    public PlayerController playerController; // 플레이어 컨트롤러

    // 알림
    [Header("[ 알림 ]")]
    public GameObject announcementPanel; // 알림 패널
    public TextMeshProUGUI announcementText; // 알림 텍스트

    // 팝업 창
    [Header("[ 팝업 창 ]")]
    public GameObject AdventureResultWindow; // 모험 결과 창

    private bool gameOverDisplayed = false; // 게임 오버 창이 표시되었는지 여부

    // 시작 시 호출되는 Start 메서드
    void Start()
    {
        // 사용자 정보 업데이트
        UpdateUserInfoDisplay();

        // 알림 UI 비활성화 상태로 초기화
        announcementPanel.SetActive(false);

        // 플레이어 GameObject를 찾아서 PlayerController 컴포넌트를 playerController 변수에 할당
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // 매 프레임마다 호출되는 Update 메서드
    void Update()
    {
        // 플레이어 상태 업데이트
        UpdatePlayerStatus();

        // @@@@@@@@@@@@@@@@@@@@@@@@@@ 파티원이 들어왔을 때 알림을 켭니다 (코드 추가 필요) @@@@@@@@@@@@@@@@@@@@@@@@@@
        //
        //
        //
        // 코르틴이 실행되는 조건이 필요해요 (파티원이 들어왔을 때!)

        StartCoroutine(ShowAnnouncementPanel());

        //
        //
        //
        //
        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    }

    // 사용자 정보를 UI에 업데이트하는 메서드
    void UpdateUserInfoDisplay()
    {
        // UserInfo 인스턴스 가져오기
        UserInfo userInfo = UserInfo.GetInstance();

        // 닉네임 정보 가져오기
        string nickname = userInfo.getNickName();

        // 가져온 정보를 TextMeshProUGUI에 적용
        nicknameText.text = nickname;
    }

    // 플레이어 상태를 업데이트 하는 메서드
    void UpdatePlayerStatus()
    {
        // 플레이어의 현재 체력을 체력 바에 반영
        int Hp = playerController.Stat.Hp;
        int MaxHp = playerController.Stat.MaxHp;
        playerHealthSlider.value = (float)Hp / MaxHp;
        playerHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // 플레이어의 현재 마나를 마나 바에 반영
        int Mp = playerController.Stat.Mp;
        int MaxMp = playerController.Stat.MaxMp;
        playerManaSlider.value = (float)Mp / MaxMp;
        playerManaText.text = string.Format("{0:0} / {1:0}", Mp, MaxMp);

        // 플레이어가 사망할 경우 게임 오버 창을 활성화
        if (Hp <= 0 && !gameOverDisplayed)
        {
            AdventureResultWindow.SetActive(true);
            gameOverDisplayed = true; // 게임 오버 창이 표시되었음을 표시
        }
    }

    // 1.5초간 플레이어 입장 알림을 활성화하고, 다시 비활성화 하는 코루틴
    IEnumerator ShowAnnouncementPanel()
    {
        // @@@@@@@@@@@@@@@@@@@@@@@@@@ 파티원이 들어왔을 때 알림을 켭니다 (코드 추가 필요) @@@@@@@@@@@@@@@@@@@@@@@@@@
        //
        //
        // 알림 텍스트에 들어온 사람의 닉네임을 넣어야함
        // 인스펙터 창에서 연결은 다 해두었습니다

        // 알림 텍스트 
        announcementText.text = $"{111}님이 파티에 참가하였습니다.";
        announcementPanel.SetActive(true);

        //
        //
        //
        //
        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        // 1.5초 기다림
        yield return new WaitForSeconds(1.5f);

        // 알림 텍스트 초기화 및 패널 비활성화
        announcementText.text = "";
        announcementPanel.SetActive(false);
    }
}

