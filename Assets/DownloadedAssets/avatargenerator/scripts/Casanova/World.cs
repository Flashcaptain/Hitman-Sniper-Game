#pragma warning disable 162,108,618
using Casanova.Prelude;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Games {public class World : MonoBehaviour{
public static int frame;
void Update () { Update(Time.deltaTime, this); 
 frame++; }
public bool JustEntered = true;


public void Start()
	{
		AvatarGenerator ___avaGen00;
		___avaGen00 = AvatarGenerator.Find();
		derp = true;
		Persons = (

Enumerable.Empty<Person>()).ToList<Person>();
		AvatarGen = ___avaGen00;
		
}
		public AvatarGenerator AvatarGen;
	public List<Person> __Persons;
	public List<Person> Persons{  get { return  __Persons; }
  set{ __Persons = value;
 foreach(var e in value){if(e.JustEntered){ e.JustEntered = false;
}
} }
 }
	public System.Boolean derp;
	public System.Int32 ___x00;
	public System.Int32 counter20;
	public Casanova.Prelude.Tuple<System.String,System.Collections.Generic.List<Casanova.Prelude.Tuple<System.Int32,System.Int32>>> ___parsedperson10;
	public System.Int32 counter21;
	public System.Collections.Generic.List<Casanova.Prelude.Tuple<System.Int32,System.Int32>> ___personalities10;

System.DateTime init_time = System.DateTime.Now;
	public void Update(float dt, World world) {
var t = System.DateTime.Now;

		for(int x0 = 0; x0 < Persons.Count; x0++) { 
			Persons[x0].Update(dt, world);
		}
		this.Rule0(dt, world);
		this.Rule1(dt, world);
		this.Rule2(dt, world);
	}





	int s0=-1;
	public void Rule0(float dt, World world){ switch (s0)
	{

	case -1:
	if(!(!(AvatarGen.AutoGenerateCharacters)))
	{

	s0 = -1;
return;	}else
	{

	goto case 1;	}
	case 1:
	
	counter20 = -1;
	if((((Enumerable.Range(0,((1) + (((((AvatarGen.Amount) - (1))) - (0))))).ToList<System.Int32>()).Count) == (0)))
	{

	goto case 0;	}else
	{

	___x00 = (Enumerable.Range(0,((1) + (((((AvatarGen.Amount) - (1))) - (0))))).ToList<System.Int32>())[0];
	goto case 2;	}
	case 2:
	counter20 = ((counter20) + (1));
	if((((((Enumerable.Range(0,((1) + (((((AvatarGen.Amount) - (1))) - (0))))).ToList<System.Int32>()).Count) == (counter20))) || (((counter20) > ((Enumerable.Range(0,((1) + (((((AvatarGen.Amount) - (1))) - (0))))).ToList<System.Int32>()).Count)))))
	{

	goto case 0;	}else
	{

	___x00 = (Enumerable.Range(0,((1) + (((((AvatarGen.Amount) - (1))) - (0))))).ToList<System.Int32>())[counter20];
	goto case 3;	}
	case 3:
	Persons = new Cons<Person>(new Person((

(AvatarGen.SettingsList).Select(__ContextSymbol1 => new { ___characteristic00 = __ContextSymbol1 })
.Select(__ContextSymbol2 => new Casanova.Prelude.Tuple<System.Int32, System.Int32>(__ContextSymbol2.___characteristic00.Item1,UnityEngine.Random.Range(__ContextSymbol2.___characteristic00.Item2.Item1,__ContextSymbol2.___characteristic00.Item2.Item2)))
.ToList<Casanova.Prelude.Tuple<System.Int32, System.Int32>>()).ToList<Casanova.Prelude.Tuple<System.Int32, System.Int32>>(),"random"), (Persons)).ToList<Person>();
	s0 = 2;
return;
	case 0:
	if(!(false))
	{

	s0 = 0;
return;	}else
	{

	s0 = -1;
return;	}	
	default: return;}}
	

	int s1=-1;
	public void Rule1(float dt, World world){ switch (s1)
	{

	case -1:
	if(!(AvatarGen.AutoGenerateCharacters))
	{

	s1 = -1;
return;	}else
	{

	goto case 1;	}
	case 1:
	
	counter21 = -1;
	if((((PersonParser.ParsedPersons).Count) == (0)))
	{

	goto case 0;	}else
	{

	___parsedperson10 = (PersonParser.ParsedPersons)[0];
	goto case 2;	}
	case 2:
	counter21 = ((counter21) + (1));
	if((((((PersonParser.ParsedPersons).Count) == (counter21))) || (((counter21) > ((PersonParser.ParsedPersons).Count)))))
	{

	goto case 0;	}else
	{

	___parsedperson10 = (PersonParser.ParsedPersons)[counter21];
	goto case 3;	}
	case 3:
	___personalities10 = ___parsedperson10.Item2;
	Persons = new Cons<Person>(new Person((

(___personalities10).Select(__ContextSymbol3 => new { ___personality10 = __ContextSymbol3 })
.Select(__ContextSymbol4 => new Casanova.Prelude.Tuple<System.Int32, System.Int32>(__ContextSymbol4.___personality10.Item1,__ContextSymbol4.___personality10.Item2))
.ToList<Casanova.Prelude.Tuple<System.Int32, System.Int32>>()).ToList<Casanova.Prelude.Tuple<System.Int32, System.Int32>>(),___parsedperson10.Item1), (Persons)).ToList<Person>();
	s1 = 2;
return;
	case 0:
	if(!(false))
	{

	s1 = 0;
return;	}else
	{

	s1 = -1;
return;	}	
	default: return;}}
	

	int s2=-1;
	public void Rule2(float dt, World world){ switch (s2)
	{

	case -1:
	HelperFunctions.Log(derp);
	derp = derp;
	s2 = -1;
return;	
	default: return;}}
	





}
public class Event{
public int frame;
public bool JustEntered = true;
private System.String Type;
	public int ID;
public Event(System.String Type)
	{JustEntered = false;
 frame = World.frame;
		UnityEvent ___unity_event00;
		___unity_event00 = UnityEvent.SpawnRandomEvent(Type);
		UnityEvent = ___unity_event00;
		
}
		public System.Int32 AmountOfEvents{  get { return UnityEvent.AmountOfEvents; }
  set{UnityEvent.AmountOfEvents = value; }
 }
	public System.Int32 AmountOfParticipants{  get { return UnityEvent.AmountOfParticipants; }
  set{UnityEvent.AmountOfParticipants = value; }
 }
	public System.Int32 AmountOfPlayerEvents{  get { return UnityEvent.AmountOfPlayerEvents; }
  set{UnityEvent.AmountOfPlayerEvents = value; }
 }
	public System.Int32 Completeness{  get { return UnityEvent.Completeness; }
  set{UnityEvent.Completeness = value; }
 }
	public UnityEngine.GameObject GameObject{  get { return UnityEvent.GameObject; }
 }
	public System.Int32 Id{  get { return UnityEvent.Id; }
  set{UnityEvent.Id = value; }
 }
	public System.Int32 InterestLevel{  get { return UnityEvent.InterestLevel; }
  set{UnityEvent.InterestLevel = value; }
 }
	public System.Boolean IsDestroyed{  get { return UnityEvent.IsDestroyed; }
 }
	public System.Boolean IsPlayerControlled{  get { return UnityEvent.IsPlayerControlled; }
  set{UnityEvent.IsPlayerControlled = value; }
 }
	public System.Int32 MaxAmountOfParticipants{  get { return UnityEvent.MaxAmountOfParticipants; }
  set{UnityEvent.MaxAmountOfParticipants = value; }
 }
	public System.Collections.Generic.List<Casanova.Prelude.Tuple<System.Int32,System.Int32>> PersonalityMinimums{  get { return UnityEvent.PersonalityMinimums; }
  set{UnityEvent.PersonalityMinimums = value; }
 }
	public UnityEngine.Vector3 Position{  get { return UnityEvent.Position; }
  set{UnityEvent.Position = value; }
 }
	public System.Single Radius{  get { return UnityEvent.Radius; }
  set{UnityEvent.Radius = value; }
 }
	public System.String TriggerKey{  get { return UnityEvent.TriggerKey; }
  set{UnityEvent.TriggerKey = value; }
 }
	public UnityEvent UnityEvent;
	public EventObject _eventObject{  get { return UnityEvent._eventObject; }
  set{UnityEvent._eventObject = value; }
 }
	public System.Single count_down1;
	public void Update(float dt, World world) {
frame = World.frame;

		this.Rule0(dt, world);

	}





	int s0=-1;
	public void Rule0(float dt, World world){ switch (s0)
	{

	case -1:
	count_down1 = 2.5f;
	goto case 12;
	case 12:
	if(((count_down1) > (0f)))
	{

	count_down1 = ((count_down1) - (dt));
	s0 = 12;
return;	}else
	{

	goto case 2;	}
	case 2:
	if(UnityEventController.IsEventReady(this.Id))
	{

	goto case 0;	}else
	{

	goto case 1;	}
	case 0:
	HelperFunctions.Log(("Completenesss: ") + (Completeness));
	Completeness = ((Completeness) + (10));
	s0 = 3;
return;
	case 3:
	if(((((Completeness) > (100))) || (((Completeness) == (100)))))
	{

	goto case 4;	}else
	{

	s0 = -1;
return;	}
	case 4:
	UnityEvent.Destroy();
	Completeness = Completeness;
	s0 = -1;
return;
	case 1:
	HelperFunctions.Log(("Completeness: ") + (Completeness));
	Completeness = ((Completeness) + (10));
	s0 = -1;
return;	
	default: return;}}
	






}
public class Person{
public int frame;
public bool JustEntered = true;
private List<Casanova.Prelude.Tuple<System.Int32, System.Int32>> Settings;
private System.String modelName;
	public int ID;
public Person(List<Casanova.Prelude.Tuple<System.Int32, System.Int32>> Settings, System.String modelName)
	{JustEntered = false;
 frame = World.frame;
		UnityNpc ___unity_npc00;
		___unity_npc00 = UnityNpc.Spawn(Settings,modelName);
		settings = Settings;
		actionIds = (

Enumerable.Empty<System.Int32>()).ToList<System.Int32>();
		UnityNpc = ___unity_npc00;
		PositionAvailable = false;
		
}
		public System.Collections.Generic.List<System.Int32> ActionsToPerform{  get { return UnityNpc.ActionsToPerform; }
 }
	public UnityEngine.Vector3 CurrentActionPosition{  get { return UnityNpc.CurrentActionPosition; }
 }
	public System.Int32 Id{  get { return UnityNpc.Id; }
 }
	public System.Boolean IsEventActor{  get { return UnityNpc.IsEventActor; }
  set{UnityNpc.IsEventActor = value; }
 }
	public System.Boolean IsInEvent{  get { return UnityNpc.IsInEvent; }
  set{UnityNpc.IsInEvent = value; }
 }
	public System.Boolean IsInEventRadius{  get { return UnityNpc.IsInEventRadius; }
  set{UnityNpc.IsInEventRadius = value; }
 }
	public UnityEngine.Vector3 Position{  get { return UnityNpc.Position; }
  set{UnityNpc.Position = value; }
 }
	public System.Boolean PositionAvailable;
	public UnityNpc UnityNpc;
	public List<System.Int32> actionIds;
	public System.Boolean enabled{  get { return UnityNpc.enabled; }
  set{UnityNpc.enabled = value; }
 }
	public UnityEngine.GameObject gameObject{  get { return UnityNpc.gameObject; }
 }
	public UnityEngine.HideFlags hideFlags{  get { return UnityNpc.hideFlags; }
  set{UnityNpc.hideFlags = value; }
 }
	public System.Boolean isActiveAndEnabled{  get { return UnityNpc.isActiveAndEnabled; }
 }
	public System.String name{  get { return UnityNpc.name; }
  set{UnityNpc.name = value; }
 }
	public List<Casanova.Prelude.Tuple<System.Int32, System.Int32>> settings;
	public System.String tag{  get { return UnityNpc.tag; }
  set{UnityNpc.tag = value; }
 }
	public UnityEngine.Transform transform{  get { return UnityNpc.transform; }
 }
	public System.Boolean useGUILayout{  get { return UnityNpc.useGUILayout; }
  set{UnityNpc.useGUILayout = value; }
 }
	public System.Single count_down2;
	public System.Single count_down3;
	public System.Int32 ___actionToExecute30;
	public System.Int32 ___actionToExecute41;
	public UnityEngine.Vector3 ___destination40;
	public System.Single ___distanceToDestination40;
	public System.Single count_down4;
	public System.Single count_down5;
	public void Update(float dt, World world) {
frame = World.frame;

		this.Rule0(dt, world);
		this.Rule1(dt, world);
		this.Rule2(dt, world);
		this.Rule3(dt, world);
		this.Rule4(dt, world);
		this.Rule5(dt, world);
		this.Rule6(dt, world);
		this.Rule7(dt, world);
	}





	int s0=-1;
	public void Rule0(float dt, World world){ switch (s0)
	{

	case -1:
	if(!(IsInEvent))
	{

	s0 = -1;
return;	}else
	{

	goto case 3;	}
	case 3:
	UnityNpc.UpdateCurrentNodesCollection();
	actionIds = UnityNpc.ActionsToPerform;
	PositionAvailable = false;
	s0 = 0;
return;
	case 0:
	count_down2 = 2f;
	goto case 1;
	case 1:
	if(((count_down2) > (0f)))
	{

	count_down2 = ((count_down2) - (dt));
	s0 = 1;
return;	}else
	{

	s0 = -1;
return;	}	
	default: return;}}
	

	int s1=-1;
	public void Rule1(float dt, World world){ switch (s1)
	{

	case -1:
	if(!(IsInEvent))
	{

	s1 = -1;
return;	}else
	{

	goto case 3;	}
	case 3:
	if(!(!(IsInEvent)))
	{

	s1 = 3;
return;	}else
	{

	goto case 2;	}
	case 2:
	UnityNpc.FreeEventActors();
	UnityNpc.UpdateCurrentNodesCollection();
	actionIds = UnityNpc.ActionsToPerform;
	PositionAvailable = false;
	s1 = -1;
return;	
	default: return;}}
	

	int s2=-1;
	public void Rule2(float dt, World world){ switch (s2)
	{

	case -1:
	actionIds = actionIds;
	PositionAvailable = false;
	s2 = 4;
return;
	case 4:
	UnityNpc.UpdateCurrentNodesCollection();
	actionIds = UnityNpc.ActionsToPerform;
	PositionAvailable = false;
	s2 = 1;
return;
	case 1:
	count_down3 = 25f;
	goto case 2;
	case 2:
	if(((count_down3) > (0f)))
	{

	count_down3 = ((count_down3) - (dt));
	s2 = 2;
return;	}else
	{

	goto case 0;	}
	case 0:
	if(!(!(IsInEvent)))
	{

	s2 = 0;
return;	}else
	{

	s2 = -1;
return;	}	
	default: return;}}
	

	int s3=-1;
	public void Rule3(float dt, World world){ switch (s3)
	{

	case -1:
	if(!(!(PositionAvailable)))
	{

	s3 = -1;
return;	}else
	{

	goto case 10;	}
	case 10:
	if(!(((actionIds.Count) > (0))))
	{

	s3 = 10;
return;	}else
	{

	goto case 9;	}
	case 9:
	HelperFunctions.Log("actionIds.Count > 0");
	___actionToExecute30 = (actionIds)[0];
	if(UnityNpc.IsPositionAvailable(___actionToExecute30))
	{

	goto case 0;	}else
	{

	goto case 1;	}
	case 0:
	HelperFunctions.Log("UnityNpc.IsPositionAvailable");
	UnityNpc.ClaimPosition(___actionToExecute30);
	actionIds = actionIds;
	PositionAvailable = true;
	s3 = -1;
return;
	case 1:
	HelperFunctions.Log("else");
	actionIds = (

(actionIds).Select(__ContextSymbol7 => new { ___id30 = __ContextSymbol7 })
.Where(__ContextSymbol8 => !(((__ContextSymbol8.___id30) == (___actionToExecute30))))
.Select(__ContextSymbol9 => __ContextSymbol9.___id30)
.ToList<System.Int32>()).ToList<System.Int32>();
	PositionAvailable = false;
	s3 = -1;
return;	
	default: return;}}
	

	int s4=-1;
	public void Rule4(float dt, World world){ switch (s4)
	{

	case -1:
	if(!(PositionAvailable))
	{

	s4 = -1;
return;	}else
	{

	goto case 12;	}
	case 12:
	if(!(((actionIds.Count) > (0))))
	{

	s4 = 12;
return;	}else
	{

	goto case 11;	}
	case 11:
	___actionToExecute41 = (actionIds)[0];
	___destination40 = UnityNpc.GetClaimedPosition(___actionToExecute41);
	UnityNpc.MoveTo(___actionToExecute41);
	___distanceToDestination40 = UnityEngine.Vector3.Distance(Position,___destination40);
	if(((2.2f) > (___distanceToDestination40)))
	{

	goto case 1;	}else
	{

	s4 = -1;
return;	}
	case 1:
	UnityNpc.UpdateAccumulatedValues(___actionToExecute41);
	count_down4 = UnityNpc.PlayAnimation(___actionToExecute41);
	goto case 6;
	case 6:
	if(((count_down4) > (0f)))
	{

	count_down4 = ((count_down4) - (dt));
	s4 = 6;
return;	}else
	{

	goto case 4;	}
	case 4:
	UnityNpc.RemoveClaimToPosition(___actionToExecute41);
	actionIds = (

(actionIds).Select(__ContextSymbol10 => new { ___id41 = __ContextSymbol10 })
.Where(__ContextSymbol11 => !(((__ContextSymbol11.___id41) == (___actionToExecute41))))
.Select(__ContextSymbol12 => __ContextSymbol12.___id41)
.ToList<System.Int32>()).ToList<System.Int32>();
	PositionAvailable = false;
	s4 = 2;
return;
	case 2:
	if(!(!(IsEventActor)))
	{

	s4 = 2;
return;	}else
	{

	s4 = -1;
return;	}	
	default: return;}}
	

	int s5=-1;
	public void Rule5(float dt, World world){ switch (s5)
	{

	case -1:
	count_down5 = 1f;
	goto case 2;
	case 2:
	if(((count_down5) > (0f)))
	{

	count_down5 = ((count_down5) - (dt));
	s5 = 2;
return;	}else
	{

	goto case 0;	}
	case 0:
	IsInEvent = UnityNpc.IsInterestedInEvent();
	s5 = -1;
return;	
	default: return;}}
	

	int s6=-1;
	public void Rule6(float dt, World world){ switch (s6)
	{

	case -1:
	if(!(IsEventActor))
	{

	s6 = -1;
return;	}else
	{

	goto case 2;	}
	case 2:
	if(!(!(IsEventActor)))
	{

	s6 = 2;
return;	}else
	{

	goto case 1;	}
	case 1:
	UnityNpc.UpdateCurrentNodesCollection();
	actionIds = UnityNpc.ActionsToPerform;
	PositionAvailable = false;
	s6 = -1;
return;	
	default: return;}}
	

	int s7=-1;
	public void Rule7(float dt, World world){ switch (s7)
	{

	case -1:
	if(!(IsEventActor))
	{

	s7 = -1;
return;	}else
	{

	goto case 1;	}
	case 1:
	UnityNpc.UpdateCurrentNodesCollection();
	actionIds = UnityNpc.ActionsToPerform;
	PositionAvailable = false;
	s7 = -1;
return;	
	default: return;}}
	





}
}                         