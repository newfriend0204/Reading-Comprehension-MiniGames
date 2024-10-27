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
            Debug.Log("data.txt ������ �ʱ�ȭ�Ǿ����ϴ�. �� �� ��: " + initialLines.Length);
        } else {
            var lines = File.ReadAllLines(filePath);
            Debug.Log("data.txt ������ �����մϴ�. �� �� ��: " + lines.Length);
        }
    }

    public void SaveData(int value, int lineNumber) {
        var lines = File.ReadAllLines(filePath);

        if (lineNumber >= 0 && lineNumber < lines.Length) {
            lines[lineNumber] = value.ToString();
            File.WriteAllLines(filePath, lines);
        } else {
            Debug.LogError($"SaveData: ��ȿ���� ���� �� ��ȣ�Դϴ�. (�� ��ȣ: {lineNumber})");
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
            Debug.LogError($"AddData: ��ȿ���� ���� �� ��ȣ�Դϴ�. (�� ��ȣ: {lineNumber})");
        }
    }

    public int LoadData(int lineNumber) {
        var lines = File.ReadAllLines(filePath);

        if (lineNumber >= 0 && lineNumber < lines.Length) {
            return int.Parse(lines[lineNumber]);
        } else {
            Debug.LogError($"LoadData: ��ȿ���� ���� �� ��ȣ�Դϴ�. (�� ��ȣ: {lineNumber})");
            return 0;
        }
    }
}
