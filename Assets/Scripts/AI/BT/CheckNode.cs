using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNode : Node
{
    /* Method signature for the check*/
    public delegate NodeStates CheckNodeDelegate();

    /* The delegate that is called to evaluate this node*/
    private CheckNodeDelegate m_check;

    /* Gets the check method and returns a
     * type NodeState enum. */
    public CheckNode(CheckNodeDelegate check)
    {
        m_check = check;
    }

    public override NodeStates Evaluate()
    {
        switch (m_check())
        {
            case NodeStates.SUCCESS:
                m_nodeState = NodeStates.SUCCESS;
                return m_nodeState;
            case NodeStates.FAILURE:
                m_nodeState = NodeStates.FAILURE;
                return m_nodeState;
            case NodeStates.RUNNING:
                m_nodeState = NodeStates.RUNNING;
                return m_nodeState;
            default:
                m_nodeState = NodeStates.FAILURE;
                return m_nodeState;
        }
    }
}
