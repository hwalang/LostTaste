using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;

public abstract class BaseController : MonoBehaviour
{
	[SerializeField]
	protected Define.UnitType _unitType;	// Setting the Unity Editor

	protected Animator _animator;
	protected Rigidbody _rigidbody;
	protected NavMeshAgent _agent;
	// �������� ������ ���� �ð��� �����ϴ� ����
	protected Dictionary<int, float> lastAttackTimes = new Dictionary<int, float>();
	protected float damageCooldown = 0.3f; // ���ظ� �ٽ� �ޱ������ ��� �ð�(��)
	
	
	// ���� ��Ʈ��ũ
	[SerializeField]
	protected bool isConnected = false;
	public PhotonView photonView;


	protected StateMachine _statemachine;

    public State CurState
    {
        get { return _statemachine.CurState; }
		set { CurState = value; }
    }
	public NavMeshAgent Agent { get { return _agent; } }
	public StateMachine StateMachine { get { return _statemachine; } }
	public Define.UnitType UnitType { get { return _unitType; } }	

    private static long entity_ID = 0;
	private long id;
	[SerializeField]
	protected string entityName;
	private string personalColor;

	public long ID
	{
		set
		{
			id = value;
            entity_ID++;
		}
		get
		{
			return id;
		}
	}

	private void Awake()
	{
		Debug.Log("AWAKE!!!");
		_statemachine = new StateMachine();
		_animator = GetComponent<Animator>();
		_rigidbody = GetComponent<Rigidbody>();
		_agent = GetComponent<NavMeshAgent>();
		photonView = GetComponent<PhotonView>();
		Init();
	}

    private void Start()
    {
		isConnected = PhotonNetwork.IsConnected;


	}
    void Update()
	{
        //if (isConnected != PhotonNetwork.IsConnected)
        //{
        //    isConnected = PhotonNetwork.IsConnected;
        //}
        _statemachine.Execute();
	}

    //public abstract void Updated();
    public virtual void Setup(string name)
	{
		// id, �̸�, ���� ����
		ID = entity_ID;
        entityName = name;
		int color = Random.Range(0, 1000000);
		personalColor = $"#{color.ToString("X6")}";
	}
	public void PrintText(string text)
	{
		Debug.Log($"<color={personalColor}><b>{entityName}</b></color> : {text}");
	}


	public abstract void Init();

	public virtual void TakeDamage(int skillObjectId, int damage) 
	{
		Debug.Log($"{gameObject.name} is damaged {damage} by {skillObjectId}");
	}

	// IDLE
	public virtual void EnterIdle() { }
	public virtual void ExcuteIdle() { }
	public virtual void ExitIdle() { }

	// DIE
	public virtual void EnterDie() { }
	public virtual void ExcuteDie() { }
	public virtual void ExitDie() { }

	// SKILL
	public virtual void EnterSkill() { }
	public virtual void ExcuteSkill() { }
	public virtual void ExitSkill() { }

	// MOVE
	public virtual void EnterMove() { }
	public virtual void ExcuteMove() { }
	public virtual void ExitMove() { }

	// DASH
	public virtual void EnterDash() { }
	public virtual void ExcuteDash() { }
	public virtual void ExitDash() { }

    // DrillDuckSlideBeforeState - not loop
    public virtual void EnterDrillDuckSlideBeforeState() { }
	public virtual void ExcuteDrillDuckSlideBeforeState() { }
	public virtual void ExitDrillDuckSlideBeforeState() { }

    // DrillDuckSlideState - not loop
    public virtual void EnterDrillDuckSlideState() { }
    public virtual void ExcuteDrillDuckSlideState() { }
    public virtual void ExitDrillDuckSlideState() { }

    void OnHitEvent()
	{
		_statemachine.ChangeState(new IdleState(this));
	}

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    // 캐릭터에게 물리력을 받아도 밀려나는 가속도로 인해 이동에 방해받지 않는다.
    protected void FreezeVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}
