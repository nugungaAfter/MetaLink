using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStruct;

namespace MLSC
{
    // 코드 저장, 심볼 테이블 저장등
    public class MLSC_Storage
    {
        private Dictionary<string, MLSC_StorageTable> m_Storages;
        private Dictionary<string, bool> m_Flags;
        public static Deque<string> m_Code = new Deque<string>();

        public MLSC_Storage()
        {
            m_Storages = new Dictionary<string, MLSC_StorageTable>();
            m_Flags = new Dictionary<string, bool>();
        }
        public MLSC_StorageTable this[string p_TableName]
        {
            get => m_Storages[p_TableName];
        }
        public MLSC_StorageItem this[string p_TableName, string p_Name]
        {
            get => m_Storages[p_TableName][p_Name];
        }
        public bool FlagCheck(string name) => m_Flags[name];
        public void FlagSet(string name, bool value)
        {
            if(m_Flags.ContainsKey(name))
                m_Flags[name] = value;
            else
                m_Flags.Add(name, value);
        }
    }

    public class MLSC_StorageTable
    {
        public string m_TableName;
        Dictionary<string, MLSC_StorageItem> m_Items;

        public MLSC_StorageItem this[string p_Name]
        {
            get => m_Items[p_Name];
        }

        public MLSC_StorageTable(string p_tableName)
        {
            m_TableName = p_tableName;
            m_Items =  new Dictionary<string, MLSC_StorageItem>();
        }
    }

    public abstract class MLSC_StorageItem
    {
        public readonly string m_ItemName;
        public MLSC_StorageTable m_Table;

        public MLSC_StorageItem(string p_ItemName, MLSC_StorageTable p_Table)
        {
            m_Table = p_Table;
        }
    }
}