using UnityEngine;
using UnityEngine.AI;

namespace Unity.AI.Navigation.Samples
{
    /// <summary>
    /// Use physics raycast hit from mouse click to set agent destination 
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class ClickToMove : MonoBehaviour
    {
        NavMeshAgent m_Agent;
        RaycastHit m_HitInfo = new RaycastHit();

        public Transform player;
        
        void Start()
        {
            //playerPos = player.position;
            m_Agent = GetComponent<NavMeshAgent>();
            
        }
    
        void Update()
        {
            if (transform.position != player.position)
            {
                m_Agent.destination = player.position;
            }
            //if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
            //{
            //    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
            //        m_Agent.destination = m_HitInfo.point;
            //}
        }
    }
}