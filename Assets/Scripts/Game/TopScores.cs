using System;
using UnityEngine;

namespace Game
{
    public static class TopScores
    {
        public static int PlayedGames
        {
            get => PlayerPrefs.GetInt("playedGames", 0);
            set => PlayerPrefs.SetInt("playedGames", value);
        }
        public static int RecordsBeated
        {
            get => PlayerPrefs.GetInt("recordsBeated", 0);
            set => PlayerPrefs.SetInt("recordsBeated", value);
        }
        public static double[] TimeTrialTopScore 
        {
            get 
            {
                string[] values = PlayerPrefs.GetString("timeTrialTopScore", "-1 -1").Trim().Split(" ");
                double[] valuesInt = new double[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    valuesInt[i] = Double.Parse(values[i]);
                }
                return valuesInt;
            }
            set
            {
                string values = "";
                for (int i = 0; i < value.Length; i++)
                {
                    values += value[i] + " ";
                }
                PlayerPrefs.SetString("timeTrialTopScore", values);
            }
        }
        public static double[] FreePlayTopScore 
        {
            get 
            {
                string[] values = PlayerPrefs.GetString("freePlayTopScore", "-1 -1").Trim().Split(" ");
                double[] valuesInt = new double[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    valuesInt[i] = Double.Parse(values[i]);
                }
                return valuesInt;
            }
            set
            {
                string values = "";
                for (int i = 0; i < value.Length; i++)
                {
                    values += value[i] + " ";
                }
                PlayerPrefs.SetString("freePlayTopScore", values);
            }
        }
        public static double[] TestCPSTopScore
        {
            get 
            {
                string[] values = PlayerPrefs.GetString("testCPSTopScore", "-1 -1").Trim().Split(" ");
                double[] valuesInt = new double[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    valuesInt[i] = Double.Parse(values[i]);
                }
                return valuesInt;
            }
            set
            {
                string values = "";
                for (int i = 0; i < value.Length; i++)
                {
                    values += value[i] + " ";
                }
                PlayerPrefs.SetString("testCPSTopScore", values);
            }
        }
        public static void Clear()
        {
            PlayerPrefs.DeleteAll();
        }

        public static double[] GetTopScore(int type)
        {
            switch (type)
            {
                case 0:
                    return TimeTrialTopScore;
                case 1:
                    return TestCPSTopScore;
                case 2:
                    return FreePlayTopScore;
                default:
                    return FreePlayTopScore;
            }
        }

        public static void SetTopScore(int type, double[] values)
        {
            switch (type)
            {
                case 0:
                    TimeTrialTopScore = values;
                    break;
                case 1:
                    TestCPSTopScore = values;
                    break;
                case 2:
                    FreePlayTopScore = values;
                    break;
                default:
                    FreePlayTopScore = values;
                    break;
            }
        }
    }
}