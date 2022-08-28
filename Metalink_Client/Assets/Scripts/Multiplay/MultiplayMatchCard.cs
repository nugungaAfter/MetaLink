using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using LitJson;

namespace Multiplay
{
    public class MultiplayMatchCard : MonoBehaviour
    {
        public string inDate;
        public string matchTitle;
        public bool enable_sandbox;
        public string matchType;
        public string matchModeType;
        public int matchHeadCount;
        public bool enable_battle_royale;
        public int match_timeout_m;
        public int transit_to_sandbox_timeout_ms;
        public int match_start_waiting_time_s;
        public int match_increment_time_s;
        public int maxMatchRange;
        public int increaseAndDecrease;
        public string initializeCycle;
        public int defaultPoint;
        public int version;
        public string result_processing_type;
        public Dictionary<string, int> savingPoint = new Dictionary<string, int>(); // 팀전/개인전에 따라 키값이 달라질 수 있음.
        public override string ToString()
        {
            string savingPointString = "savingPont : \n";
            foreach (var dic in savingPoint) {
                savingPointString += $"{dic.Key} : {dic.Value}\n";
            }
            savingPointString += "\n";
            return $"inDate : {inDate}\n" +
            $"matchTitle : {matchTitle}\n" +
            $"enable_sandbox : {enable_sandbox}\n" +
            $"matchType : {matchType}\n" +
            $"matchModeType : {matchModeType}\n" +
            $"matchHeadCount : {matchHeadCount}\n" +
            $"enable_battle_royale : {enable_battle_royale}\n" +
            $"match_timeout_m : {match_timeout_m}\n" +
            $"transit_to_sandbox_timeout_ms : {transit_to_sandbox_timeout_ms}\n" +
            $"match_start_waiting_time_s : {match_start_waiting_time_s}\n" +
            $"match_increment_time_s : {match_increment_time_s}\n" +
            $"maxMatchRange : {maxMatchRange}\n" +
            $"increaseAndDecrease : {increaseAndDecrease}\n" +
            $"initializeCycle : {initializeCycle}\n" +
            $"defaultPoint : {defaultPoint}\n" +
            $"version : {version}\n" +
            $"result_processing_type : {result_processing_type}\n" +
            savingPointString;
        }

        public MultiplayMatchCard(JsonData jsonData)
        {
            inDate = jsonData["inDate"].ToString();
            result_processing_type = jsonData["result_processing_type"].ToString();
            version = Int32.Parse(jsonData["version"].ToString());
            matchTitle = jsonData["matchTitle"].ToString();
            enable_sandbox = jsonData["enable_sandbox"].ToString() == "true" ? true : false;
            matchType = jsonData["matchType"].ToString();
            matchModeType = jsonData["matchModeType"].ToString();
            matchHeadCount = Int32.Parse(jsonData["matchHeadCount"].ToString());
            enable_battle_royale = jsonData["enable_battle_royale"].ToString() == "true" ? true : false;
            match_timeout_m = Int32.Parse(jsonData["match_timeout_m"].ToString());
            transit_to_sandbox_timeout_ms = Int32.Parse(jsonData["transit_to_sandbox_timeout_ms"].ToString());
            match_start_waiting_time_s = Int32.Parse(jsonData["match_start_waiting_time_s"].ToString());

            if (jsonData.ContainsKey("match_increment_time_s")) {
                match_increment_time_s = Int32.Parse(jsonData["match_increment_time_s"].ToString());
            }
            if (jsonData.ContainsKey("maxMatchRange")) {
                maxMatchRange = Int32.Parse(jsonData["maxMatchRange"].ToString());
            }
            if (jsonData.ContainsKey("increaseAndDecrease")) {
                increaseAndDecrease = Int32.Parse(jsonData["increaseAndDecrease"].ToString());
            }
            if (jsonData.ContainsKey("initializeCycle")) {
                initializeCycle = jsonData["initializeCycle"].ToString();
            }
            if (jsonData.ContainsKey("defaultPoint")) {
                defaultPoint = Int32.Parse(jsonData["defaultPoint"].ToString());
            }

            if (jsonData.ContainsKey("savingPoint")) {
                if (jsonData["savingPoint"].IsArray) {
                    for (int listNum = 0; listNum < jsonData["savingPoint"].Count; listNum++) {
                        var keyList = jsonData["savingPoint"][listNum].Keys;
                        foreach (var key in keyList) {
                            savingPoint.Add(key, Int32.Parse(jsonData["savingPoint"][listNum][key].ToString()));
                        }
                    }
                }
                else {
                    foreach (var key in jsonData["savingPoint"].Keys) {
                        savingPoint.Add(key, Int32.Parse(jsonData["savingPoint"][key].ToString()));
                    }
                }
            }
        }
    }
}
