using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void ProtocolCallback(params object[] args);
	
public class Protocol : MonoBehaviour 
{	
	// singleton
	public static Protocol _this;
	public int _id = 0;
	
	// holds the types, and how many bytes each takes to send over the wire
	Dictionary<string, int> types;
	public static Dictionary<string, int> Types {
		get { return _this.types; }
	}
	
	public class FunctionRef {
		public int id;
		public string name;
		public string[] paramTypes;
		public ProtocolCallback callback;
	}	
	
	List<FunctionRef> functions;
	
	public static void testfunc(params object[] args) {
		Debug.Log (Random.value + ": testfunc called: " + args[0] + " | " + args[1]);
	}
	
	// initialization
	void Awake() {
		_this = this;
		
		types = new Dictionary<string, int>();
		types["short"]  = 1;
		types["ushort"] = 1;
		types["int"]    = 2;
		types["uint"]   = 2;
		types["long"]   = 4;
		types["ulong"]  = 4;
		types["float"]  = 4;
		types["double"] = 8;		
		
		functions = new List<FunctionRef>();
		
		Define ("test", new[] { "uint", "uint" }, testfunc);
		//Call("test", 5, 6);
	}
	
	public static void Define( string name, string[] paramTypes, ProtocolCallback callback ) {
		FunctionRef fr = new FunctionRef();
		fr.id = _this._id++;
		fr.name = name;
		fr.paramTypes = paramTypes;
		fr.callback = callback;	
		
		_this.functions.Add(fr);
	}
	
	public static void Call( string name, params object[] args ) {
		FunctionRef fr = _this.functions.Find( x => x.name == name);
		fr.callback(args);
	}
}
