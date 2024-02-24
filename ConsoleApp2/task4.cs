using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;

class task4
{
    static void Main()
    {
        // Загрузка данных из JSON-файла
        var jsonData = File.ReadAllText("D:\\korporaTIV\\Новая папка\\ConsoleApp1\\ConsoleApp2\\data.json");

        // Десериализация JSON в объекты C#
        var data = JsonSerializer.Deserialize<JsonNode>(jsonData);

        // Код студента, для которого нужно сформировать список дисциплин с оценкой
        string studentCode = "S001";

        // Получение списка дисциплин с оценкой для указанного студента
        var studyPlan = (JsonArray)data["study_plan"];
        var subjects = (JsonArray)data["subjects"];

        var result = new List<(string, int)>();

        foreach (var entry in studyPlan)
        {
            var studentCodeEntry = (string)((JsonObject)entry)["student_code"];
            var subjectCode = (string)((JsonObject)entry)["subject_code"];
            var grade = (int)((JsonObject)entry)["grade"];

            if (studentCodeEntry == studentCode)
            {
                var subjectName = ((JsonObject)subjects.First(s => (string)((JsonObject)s)["subject_code"] == subjectCode))["name"];
                result.Add(((string)subjectName, grade));
            }
        }

        // Вывод результата
        Console.WriteLine($"Список дисциплин с оценкой для студента с кодом {studentCode}:");
        foreach (var item in result)
        {
            Console.WriteLine($"Дисциплина: {item.Item1}, Оценка: {item.Item2}");
        }

        // Подсчет процента оценок
        var excellentCount = result.Count(grade => grade.Item2 == 5);
        var goodCount = result.Count(grade => grade.Item2 == 4);
        var satisfactoryCount = result.Count(grade => grade.Item2 == 3);
        var totalCount = result.Count;

        // Вывод процента оценок
        Console.WriteLine($"Процент оценок:");
        Console.WriteLine($"Отлично: {(double)excellentCount / totalCount * 100}%");
        Console.WriteLine($"Хорошо: {(double)goodCount / totalCount * 100}%");
        Console.WriteLine($"Удовлетворительно: {(double)satisfactoryCount / totalCount * 100}%");
    }
}