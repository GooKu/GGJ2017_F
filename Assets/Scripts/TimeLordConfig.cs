using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLordConfig {

	static TimeSpan debug;

	public static TimeSpan Debug {
		get{
			return debug;
		}
		set{
			if (debug != value) {
				debug = value;
				if (Changed != null) {
					Changed (null, System.EventArgs.Empty);
				}
			}
		}
	}

	public static TimeSpan Current {
		get {
			if (Debug != TimeSpan.None) {
				return Debug;
			}

			var h = System.DateTime.Now.Hour;
			if (h > 8 && h < 16){
				return TimeSpan.Morning;
			}else if (h >= 16 && h < 24){
				return TimeSpan.Noon;
			}else{
				return TimeSpan.Night;
			}
				
		}
	}

	public static event System.EventHandler Changed;

	public enum TimeSpan{
		None,
		Morning,
		Noon,
		Night,
	}
}
