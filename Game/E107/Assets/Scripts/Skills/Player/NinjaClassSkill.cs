using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaClassSkill : Skill
{

    [field: SerializeField]
    public int Damage { get; set; }
    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        Root = transform.root;
        Vector3 dir = Root.forward;

        PlayerController _playerController = gameObject.GetComponent<PlayerController>();

        //Root.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.NinjaClassSkillStartEffect, Root);
        ps.transform.parent = _playerController._righthand.transform;
        ps.transform.localPosition = new Vector3();
        _playerController.StateMachine.ChangeState(new IdleState(_playerController));


        yield return new WaitForSeconds(0.8f);
        Managers.Effect.Stop(ps);

        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, Damage, _seq);

        skillObj.localScale = new Vector3(0.7f, 0.7f, 0.7f);

        skillObj.position = Root.transform.position;
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation.SetLookRotation(dir);


        ParticleSystem finishEffect1 = Managers.Effect.Play(Define.Effect.NinjaClassSkillFinishEffect, Root);
        ps.transform.position = _playerController._righthand.transform.position;
        

        ParticleSystem finishEffect2 = Managers.Effect.Play(Define.Effect.GalaxyZzzSkillEffect, Root);
        ps.transform.position = _playerController._righthand.transform.position;


        yield return new WaitForSeconds(0.2f);
        Managers.Resource.Destroy(skillObj.gameObject);

        yield return new WaitForSeconds(0.5f);
        Managers.Effect.Stop(finishEffect1);
        Managers.Effect.Stop(finishEffect2);




    }
}