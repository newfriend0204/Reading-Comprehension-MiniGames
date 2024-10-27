using System.IO;
using UnityEngine;

public class FileManager {
    private string filePath;

    public FileManager() {
        filePath = Path.Combine(Application.persistentDataPath, "data.txt");

        if (!File.Exists(filePath)) {
            string[] initialLines = new string[10];
            for (int i = 0; i < initialLines.Length; i++) {
                initialLines[i] = "0";
            }
            File.WriteAllLines(filePath, initialLines);
            Debug.Log("data.txt 파일이 초기화되었습니다. 총 줄 수: " + initialLines.Length);
        } else {
            var lines = File.ReadAllLines(filePath);
            Debug.Log("data.txt 파일이 존재합니다. 총 줄 수: " + lines.Length);
        }
    }

    public void SaveData(int value, int lineNumber) {
        var lines = File.ReadAllLines(filePath);

        if (lineNumber >= 0 && lineNumber < lines.Length) {
            lines[lineNumber] = value.ToString();
            File.WriteAllLines(filePath, lines);
        } else {
            Debug.LogError($"SaveData: 유효하지 않은 줄 번호입니다. (줄 번호: {lineNumber})");
        }
    }

    public void AddData(int value, int lineNumber) {
        var lines = File.ReadAllLines(filePath);

        if (lineNumber >= 0 && lineNumber < lines.Length) {
            int currentValue = int.Parse(lines[lineNumber]);
            currentValue += value;
            lines[lineNumber] = currentValue.ToString();
            File.WriteAllLines(filePath, lines);
        } else {
            Debug.LogError($"AddData: 유효하지 않은 줄 번호입니다. (줄 번호: {lineNumber})");
        }
    }

    public int LoadData(int lineNumber) {
        var lines = File.ReadAllLines(filePath);

        if (lineNumber >= 0 && lineNumber < lines.Length) {
            return int.Parse(lines[lineNumber]);
        } else {
            Debug.LogError($"LoadData: 유효하지 않은 줄 번호입니다. (줄 번호: {lineNumber})");
            return 0;
        }
    }
}
