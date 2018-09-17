/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # 498 capsrone
*-----------------------------------------------------------------------*/
/*Creator: Yan Zhang
 * ConditionReaction inherit to the Reaction
 * condition: contain mutiple condition and 
 * a boolean value satisifed
 */
public class ConditionReaction : Reaction
{
    public Condition condition;
    public bool satisfied;

    /*
     * if target interacte with the GameObject
     * the Condition instantly been statisfied
     * Yan
     */
    protected override void ImmediateReaction ()
    {
        condition.satisfied = satisfied;
    }
}
