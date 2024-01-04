using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class DataCollectionManager
{
    private static List<GameSessionData> sessions = new List<GameSessionData>();

    public static void AddSessionData(string gameName, string duration, int speed, string hand, int limbMovementAmplitude, int successRate)
    {
        sessions.Add(new GameSessionData
        {
            GameName = gameName,
            Duration = duration,
            Speed = speed,
            Hand = hand,
            LimbMovementAmplitude = limbMovementAmplitude,
            SuccessRate = successRate
        });
    }

    public static void SaveToCsv(string filePath)
    {
        var csv = new StringBuilder();
        csv.AppendLine("Game Name,Duration (min),Speed (level),Hand (Right/Left),Limb Movement Amplitude (degrees),Success Rate (%)");

        foreach (var session in sessions)
        {
            var newLine = string.Join(",", session.GameName, session.Duration, session.Speed, session.Hand, session.LimbMovementAmplitude, session.SuccessRate);
            csv.AppendLine(newLine);
        }

        File.WriteAllText(filePath, csv.ToString());
    }

    private class GameSessionData
    {
        public string GameName { get; set; }
        public string Duration { get; set; }
        public int Speed { get; set; }
        public string Hand { get; set; }
        public int LimbMovementAmplitude { get; set; }
        public int SuccessRate { get; set; }
    }
}