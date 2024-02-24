using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

class Program
{
    static void Main()
    {
        // Прочитайте данные из файла JSON
        string jsonFilePath = "D:\\korporaTIV\\Новая папка\\ConsoleApp1\\ConsoleApp1\\data.json";
        string jsonData = File.ReadAllText(jsonFilePath);

        // Десериализуем JSON в объекты C#, проверяя на null
        var data = JsonSerializer.Deserialize<Data>(jsonData);
        if (data == null)
        {
            Console.WriteLine("Ошибка: Не удалось прочитать данные из файла JSON или данные отсутствуют.");
            return;
        }

        // Проверяем, что список дисциплин с оценками не равен null
        if (data.StudyPlan == null)
        {
            Console.WriteLine("Ошибка: Список дисциплин с оценками отсутствует.");
            return;
        }

        // Код студента, для которого нужно добавить оценку
        string studentCode = "S001";

        // Код предмета и оценка, которую необходимо добавить
        string subjectCode = "SUB006";
        int grade = 5;

        // Проверяем, есть ли уже оценка для указанного студента и предмета
        var existingGrade = data.StudyPlan.FirstOrDefault(entry => entry?.StudentCode == studentCode && entry?.SubjectCode == subjectCode);

        if (existingGrade != null)
        {
            Console.WriteLine($"Оценка для студента с кодом {studentCode} по предмету с кодом {subjectCode} уже существует.");
            return;
        }

        // Добавляем новую оценку в список дисциплин с оценкой
        if (data.StudyPlan == null)
        {
            data.StudyPlan = new List<StudyPlanEntry>(); // Убедимся, что список инициализирован перед добавлением элемента
        }
        data.StudyPlan.Add(new StudyPlanEntry { StudentCode = studentCode, SubjectCode = subjectCode, Grade = grade });

        // Сериализуем обновленные данные обратно в JSON
        string updatedJsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

        // Записываем сериализованные данные обратно в файл
        File.WriteAllText(jsonFilePath, updatedJsonData);

        Console.WriteLine($"Новая оценка {grade} добавлена для студента с кодом {studentCode} по предмету с кодом {subjectCode}.");
    }
}

class Data
{
    [JsonPropertyName("students")]
    public List<Student> Students { get; set; }
    
    [JsonPropertyName("subjects")]
    public List<Subject> Subjects { get; set; }
    
    [JsonPropertyName("study_plan")]
    public List<StudyPlanEntry> StudyPlan { get; set; }
}

class Student
{
    [JsonPropertyName("student_code")]
    public string StudentCode { get; set; }
    
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    
    [JsonPropertyName("middle_name")]
    public string MiddleName { get; set; }
}

class Subject
{
    [JsonPropertyName("subject_code")]
    public string SubjectCode { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("lecture_hours")]
    public int LectureHours { get; set; }
    
    [JsonPropertyName("practice_hours")]
    public int PracticeHours { get; set; }
}

class StudyPlanEntry
{
    [JsonPropertyName("student_code")]
    public string StudentCode { get; set; }
    
    [JsonPropertyName("subject_code")]
    public string SubjectCode { get; set; }
    
    [JsonPropertyName("grade")]
    public int Grade { get; set; }
}
