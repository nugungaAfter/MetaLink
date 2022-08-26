using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// 샘플 - Position 동기화
public class Sample_PositionHandler : NetworkHandler
{
    [SerializeField] private Vector3 PrevPos = Vector3.zero;

    protected override void ParseData(string Context)
    {
        // 받은 문자열을 파싱하게 적합한 형태로 수정
        StringBuilder builder = new StringBuilder();
        builder.Append(Context);
        builder.Replace("(", "");
        builder.Replace(")", "");
        builder.Replace(" ", "");
        
        var values = builder.ToString().Split(",");
        
        // 파싱
        float x = float.Parse(values[0]);
        float y = float.Parse(values[1]);
        float z = float.Parse(values[2]);
        Vector3 vec = new Vector3(x, y, z);

        // 테스트용
        Debug.Log(vec);
        
        // 대입
        //transform.position = vec;
    }

    protected override string TargetObjectData()
    {
        PrevPos = transform.position;
        // 현재 위치를 문자열로 만들어 보냄
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
