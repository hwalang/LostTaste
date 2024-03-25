using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ���� ���¸� �����ϴ� Ŭ�����Դϴ�.
/// </summary>
public class BossStatusManager : MonoBehaviour
{
    // �帱�� ����
    [Header("[ �帱�� ���� ]")]
    public GameObject drillDuckStatus;
    public TextMeshProUGUI drillDuckNameText; // �̸� �ؽ�Ʈ
    public TextMeshProUGUI drillDuckHealthText; // ü�� �ؽ�Ʈ
    public Slider drillDuckHealthSlider; // ü�� �� �����̴�

    // ũ��Ŀ���� ����
    [Header("[ ũ��Ŀ���� ���� ]")]
    public GameObject crocodileStatus;
    public TextMeshProUGUI crocodileNameText; // �̸� �ؽ�Ʈ
    public TextMeshProUGUI crocodileHealthText; // ü�� �ؽ�Ʈ
    public Slider crocodileHealthSlider; // ü�� �� �����̴�

    // ���� ��Ʈ�ѷ�
    [Header("[ ���� ��Ʈ�ѷ� ]")]
    public DrillDuckController drillDuckController; // �帱��
    public CrocodileController crocodileController; // ũ��Ŀ����

    // �� �����Ӹ��� ȣ��Ǵ� Update �޼���
    void Update()
    {
        // �帱�� ���� ������Ʈ
        UpdateDrillDuckStatus();

        // ũ��Ŀ���� ���� ������Ʈ
        UpdateCrocodileStatus();

        // Stage 3 ���� ���� ������Ʈ

        // ���� ���� ���� ������Ʈ
    }

    // �帱�� ���¸� ������Ʈ �ϴ� �޼���
    void UpdateDrillDuckStatus()
    {
        // ���� GameObject�� ���� ��� �޼��带 ����
        if (GameObject.Find("DrillDuck(Clone)") == null) return;

        // ���� GameObject�� ã�Ƽ� BossController ������Ʈ�� bossController ������ �Ҵ�
        drillDuckController = GameObject.Find("DrillDuck(Clone)").GetComponent<DrillDuckController>();

        // ������ �̸� ������ TextMeshProUGUI�� ����
        drillDuckNameText.text = "Drill Duck";

        // ������ ���� ü���� ü�� �ٿ� �ݿ�
        int Hp = drillDuckController.Stat.Hp;
        int MaxHp = drillDuckController.Stat.MaxHp;
        drillDuckHealthSlider.value = (float)Hp / MaxHp;
        drillDuckHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // ������ ����� ��� ���� ���� â�� ��Ȱ��ȭ
        if (Hp <= 0)
        {
            drillDuckStatus.SetActive(false);
        }
    }

    // ũ��Ŀ���� ���¸� ������Ʈ �ϴ� �޼���
    void UpdateCrocodileStatus()
    {
        // ���� GameObject�� ���� ��� �޼��带 ����
        if (GameObject.Find("Crocodile(Clone)") == null) return;

        // ���� GameObject�� ã�Ƽ� BossController ������Ʈ�� bossController ������ �Ҵ�
        crocodileController = GameObject.Find("Crocodile(Clone)").GetComponent<CrocodileController>();

        // ������ �̸� ������ TextMeshProUGUI�� ����
        crocodileNameText.text = "Crocodile";

        // ������ ���� ü���� ü�� �ٿ� �ݿ�
        int Hp = crocodileController.Stat.Hp;
        int MaxHp = crocodileController.Stat.MaxHp;
        crocodileHealthSlider.value = (float)Hp / MaxHp;
        crocodileHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // ������ ����� ��� ���� ���� â�� ��Ȱ��ȭ
        if (Hp <= 0)
        {
            crocodileStatus.SetActive(false);
        }
    }
}
