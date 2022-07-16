using System.Text;
using UnityEngine;

namespace scrips.GameMechanics
{
    public class DiceNumberManager : MonoBehaviour 
    {
        public int? GetNumber()
        {
            if (transform.up == Vector3.up)
            {
                return 1;
            }
            else if(transform.up == Vector3.down)
            {
                return 6;
            }
            if (transform.right == Vector3.up)
            {
                return 3;
            }
            else if(transform.right == Vector3.down)
            {
                return 4;
            }
            if (transform.forward == Vector3.up)
            {
                return 2;
            }
            else if(transform.forward == Vector3.down)
            {
                return 5;
            }
            else
            {
                return null;
            }
        }
    }
}