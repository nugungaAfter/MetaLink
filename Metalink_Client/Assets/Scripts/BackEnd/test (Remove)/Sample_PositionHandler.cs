using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// ���� - Position ����ȭ
public class Sample_PositionHandler : NetworkHandler
{
    [SerializeField] private Vector3 PrevPos = Vector3.zero;

    protected override void ParseData(string Context)
    {
        // ���� ���ڿ��� �Ľ��ϰ� ������ ���·� ����
        StringBuilder builder = new StringBuilder();
        builder.Append(Context);
        builder.Replace("(", "");
        builder.Replace(")", "");
        builder.Replace(" ", "");
        
        var values = builder.ToString().Split(",");
        
        // �Ľ�
        float x = float.Parse(values[0]);
        float y = float.Parse(values[1]);
        float z = float.Parse(values[2]);
        Vector3 vec = new Vector3(x, y, z);

        // �׽�Ʈ��
        Debug.Log(vec);
        
        // ����
        //transform.position = vec;
    }

    protected override string TargetObjectData()
    {
        PrevPos = transform.position;
        // ���� ��ġ�� ���ڿ��� ����� ����
        return transform.position.ToString();
    }

    protected override bool TrackingExpression()
    {
        return m_isSender == NetworkType.Send && PrevPos != transform.position;
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * 10 * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            transform.position -= Vector3.forward * 10 * Time.deltaTime;
        }
    }
}
