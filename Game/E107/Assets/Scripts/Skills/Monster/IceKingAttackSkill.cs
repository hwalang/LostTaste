using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceKingAttackSkill : Skill
{
    private IceKingController _controller;


    protected override void Init()
    {
        SkillCoolDownTime = 3.0f;
        _controller = GetComponent<IceKingController>();
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        Debug.Log("IceKing Attack");

        Root = transform.root;
        Vector3 dir = Root.forward;
        Root.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0);
        dir = new Vector3(dir.x, 0, dir.z);

        yield return new WaitForSeconds(0.5f);

        // SkillObject���� ����
        ParticleSystem ps = Managers.Effect.Play(Define.Effect.IceKingOrbEffect, Root);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        //skillObj.transform.parent = _controller.transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _attackDamage, _seq);

        ps.transform.position = new Vector3(ps.transform.position.x, ps.transform.position.y + 0.5f, ps.transform.position.z);

        // Managers.Sound.Play("swing1");

        skillObj.localScale = new Vector3(2.0f, 2.0f, 2.0f);    // 1.1f
        skillObj.position = Root.transform.position;
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation.SetLookRotation(dir);

        float moveDuration = 1.1f; // ����ü�� ���ư��� �ð��� �����մϴ�.
        float timer = 0; // Ÿ�̸� �ʱ�ȭ
        float speed = 20.0f; // ����ü�� �ӵ��� �����մϴ�.

        while (timer < moveDuration)
        {
            // ����ü�� ��ƼŬ �ý����� ������ �����Դϴ�.
            Vector3 moveStep = dir * speed * Time.deltaTime;
            skillObj.position += moveStep;
            ps.transform.position += moveStep;

            timer += Time.deltaTime; // Ÿ�̸Ӹ� ������Ʈ�մϴ�.
            yield return null; // ���� �����ӱ��� ����մϴ�.
        }

        yield return new WaitForSeconds(0.2f);
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
    }
}