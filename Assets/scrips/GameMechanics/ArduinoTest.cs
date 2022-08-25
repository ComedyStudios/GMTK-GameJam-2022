using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;

using UnityEngine;
public class ArduinoTest : MonoBehaviour
{
    public ControllerInfo inputs = new ControllerInfo();
    private List<int> _dataList = new List<int>();
    private SerialPort _port;

    void Start()
    {
        OpenPort();
    }

    private void OpenPort()
    {
        var ports = SerialPort.GetPortNames();
        _port = new SerialPort(ports[^1], 9600);
        _port.Open();
    }

    // Update is called once per frame
    void Update()
    {
        string value = _port.ReadLine();
        parseString(value);
        var dataString = "";
        foreach (var data in _dataList)
        {
            dataString += data + ";";
        }
        TranslateToUnityInputs();
    }

    private void parseString(string inputs)
    {
        _dataList = new List<int>();
        var data = inputs;
        data = data.Replace(" ", "");
        data = data.Replace("\n", "");
        data = data.Replace("\r", "");
        if (data.IndexOf("#")> data.IndexOf("@"))
        {
            data = data.Substring(data.IndexOf("@") + 1, data.IndexOf("#")-1);
            var subs = data.Split(';');
            foreach (var sub in subs)
            {
                if (sub != "" && sub !=" ")
                {
                    var temp = int.Parse(sub);
                    _dataList.Add(temp);
                }
            }
        }
    }

    private void TranslateToUnityInputs()
    {
        inputs.stickL = new Vector2((float)ControllerToUnityValues(0), (float)ControllerToUnityValues(1));
        inputs.stickR = new Vector2((float)ControllerToUnityValues(2), (float)ControllerToUnityValues(3));
        inputs.b1 = _dataList[4] == 0;
        inputs.b2 = _dataList[5] == 0;
        inputs.l = _dataList[6] == 0;
        inputs.r = _dataList[7] == 0;
    }

    private double ControllerToUnityValues(int index)
    {
        var temp= (_dataList[index] - 512f)/(512f);
        if (Mathf.Abs((float)temp) > 0.1f)
        {
            return temp;
        }
        else
        {
            return 0;
        }
    }

    public void ClosePort()
    {
        _port.Close();
    }
    
}

public class ControllerInfo
{
    public Vector2 stickL;
    public Vector2 stickR;
    public bool b1;
    public bool b2;
    public bool l;
    public bool r;
}
