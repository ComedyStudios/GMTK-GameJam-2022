using System.Text;
using UnityEngine;

namespace scrips.GameMechanics
{
    public class DiceNumberManager : MonoBehaviour 
    {
        private int _number;
        
        public int GetNumber()
        {
            Debug.Log($"{transform.up} {transform.right} {transform.forward} || {transform.eulerAngles}");
            if (Mathf.Approximately(1.0f, Vector3.Dot(transform.up, Vector3.up)))
            {
                _number = 1;
            }
            else if(Mathf.Approximately(1.0f, Vector3.Dot(transform.up, Vector3.down)))
            {
                _number =  6;
            }
            else if (Mathf.Approximately(1.0f, Vector3.Dot(transform.right, Vector3.up)))
            {
                _number =  3;
            }
            else if(Mathf.Approximately(1.0f, Vector3.Dot(transform.right, Vector3.down)))
            {
                _number =  4;
            }
            else if (Mathf.Approximately(1.0f, Vector3.Dot(transform.forward, Vector3.up)))
            {
                _number =  2;
            }
            else if(Mathf.Approximately(1.0f, Vector3.Dot(transform.forward, Vector3.down)))
            {
                _number =  5;
            }
            return _number;
        }
    }
}