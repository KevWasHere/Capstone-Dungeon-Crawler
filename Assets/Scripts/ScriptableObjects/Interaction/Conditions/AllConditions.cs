using UnityEngine;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # 498 capstone
  #
*-----------------------------------------------------------------------*/
/* Creator: Yan Zhang, Shane Weerasuriya, Myles Hagen
 * AllCondition is a scriptable object that hold all the condition needed
 * conditions: the conditions array of all conditions
 * instance: the instance
 * loadPath: the loading path of the all conditions
 */
public class AllConditions : ResettableScriptableObject
{
    public Condition[] conditions;
    private static AllConditions instance;
    private const string loadPath = "AllConditions";
    
    /*
     * set the instance for the allcondition
     * so other script could access to it
     */
    public static AllConditions Instance
    {
        get
        {
            if (!instance)
                instance = FindObjectOfType<AllConditions> ();
            if (!instance)
                instance = Resources.Load<AllConditions> (loadPath);
            if (!instance)
                Debug.LogError ("AllConditions has not been created yet.  Go to Assets > Create > AllConditions.");
            return instance;
        }
        set { instance = value; }
    }
		
    /*
     * reset all condition in the array of condition
     * Creator: Yan, Shane Weerasuriya, Myles Hagen
     */
    public override void Reset ()
    {
		//Debug.Log ("in reset!!!!!!!!!!!!!!");
        if (conditions == null)
            return;

        for (int i = 0; i < conditions.Length; i++)
        {
            conditions[i].satisfied = false;
        }
    }

    /*
     * Check the condition throught the array of  all conditions
     */
    public static bool CheckCondition (Condition requiredCondition)
    {
        Condition[] allConditions = Instance.conditions;
        Condition globalCondition = null;
        
        if (allConditions != null && allConditions[0] != null)
        {
            for (int i = 0; i < allConditions.Length; i++)
            {
                if (allConditions[i].hash == requiredCondition.hash)
                    globalCondition = allConditions[i];
            }
        }

        if (!globalCondition)
            return false;

        return globalCondition.satisfied == requiredCondition.satisfied;
    }

	//change the conditon by look at the name of the conditions
	//Creator: Yan, Shane Weerasuriya, Myles Hagen
	public static void ChangeCondition(string s){
		foreach (var condition in AllConditions.Instance.conditions) {
			if (condition.description == s) {
				condition.satisfied = true;
			}
		}
	}

	//Return a condition's status by looking the condition's description
	//Creator: Yan, Shane Weerasuriya, Myles Hagen
	public static bool returnCondition(string s){
		//Gothrough the conditions and find the condition need to be set
		foreach (var condition in AllConditions.Instance.conditions) {
			if (condition.description == s) {
				Debug.Log ("the check true" + condition.description + " " + condition.satisfied);
				return condition.satisfied;
			}
		}
		return false;
	}
}
