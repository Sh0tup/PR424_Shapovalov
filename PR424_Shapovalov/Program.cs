using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public int Code { get; set; }

    private string Name;
    private string LastName;
    private int YearEnrollment;

    public string name
    {
        get { return Name; }
        set { if (value.Length > 0) Name = value; }
    }

    public string lastname
    {
        get { return LastName; }
        set { if (value.Length > 0) LastName = value; }
    }

    public int yearenrollment
    {
        get { return YearEnrollment; }
        set { if (2027 - value <= 6 && value <= 2027) YearEnrollment = value; }
    }

    public List<Course> Courses = new List<Course>();

    public Student(int code, string name, string lastname, int yearenrollment)
    {
        Code = code;
        Name = name;
        LastName = lastname;
        YearEnrollment = yearenrollment;
    }

    public void Enroll(Course course)
    {
        if (!Courses.Contains(course))
        {
            Courses.Add(course);
            course.Students.Add(this);
        }
    }

    public void PrintInfo()
    {
        Console.WriteLine("Код: " + Code);
        Console.WriteLine("Имя: " + Name);
        Console.WriteLine("Фамилия: " + LastName);
        Console.WriteLine("Год зачисления: " + YearEnrollment);
        Console.WriteLine("Записан на курсов: " + Courses.Count);
    }
}

class Teacher
{
    public int Id { get; set; }

    private string Name;
    private string LastName;
    private string Department;

    public string name
    {
        get { return Name; }
        set { if (value.Length > 0) Name = value; }
    }

    public string lastname
    {
        get { return LastName; }
        set { if (value.Length > 0) LastName = value; }
    }

    public string department
    {
        get { return Department; }
        set { if (value.Length > 0) Department = value; }
    }

    public List<Course> Courses = new List<Course>();

    public Teacher(int id, string name, string lastname, string department)
    {
        Id = id;
        Name = name;
        LastName = lastname;
        Department = department;
    }

    public void PrintInfo()
    {
        Console.WriteLine("Id: " + Id);
        Console.WriteLine("Имя: " + Name);
        Console.WriteLine("Фамилия: " + LastName);
        Console.WriteLine("Кафедра: " + Department);
        Console.WriteLine("Ведёт курсов: " + Courses.Count);
    }
}

class Course
{
    private static int nextId = 1;

    public int Id { get; set; }

    private string Title;
    public Teacher Teacher;

    public string title
    {
        get { return Title; }
        set { if (value.Length > 0) Title = value; }
    }

    public List<Student> Students = new List<Student>();

    public Course(string title)
    {
        Id = nextId++;
        Title = title;
    }
    public void AssignTeacher(Teacher teacher)
    {
        Teacher = teacher;
        if (!teacher.Courses.Contains(this))
            teacher.Courses.Add(this);
    }

    public void PrintInfo()
    {
        Console.WriteLine("Id курса: " + Id);
        Console.WriteLine("Название: " + Title);
        Console.WriteLine("Преподаватель: " + (Teacher != null ? Teacher.name + " " + Teacher.lastname : "не назначен"));
        Console.WriteLine("Записано студентов: " + Students.Count);
    }
}

class University
{
    public List<Student> Students = new List<Student>();
    public List<Teacher> Teachers = new List<Teacher>();
    public List<Course> Courses = new List<Course>();

    public Student FindStudent(int code) => Students.FirstOrDefault(s => s.Code == code);
    public Teacher FindTeacher(int id) => Teachers.FirstOrDefault(t => t.Id == id);
    public Course FindCourse(int id) => Courses.FirstOrDefault(c => c.Id == id);

    public void PrintAllStudents()
    {
        if (Students.Count == 0) 
        {
            Console.WriteLine("Студентов нет.");
            return;
        }
        foreach (var s in Students) 
        { 
            s.PrintInfo(); Console.WriteLine(); 
        }
    }

    public void PrintAllTeachers()
    {
        if (Teachers.Count == 0) 
        {
            Console.WriteLine("Преподавателей нет.");
            return;
        }
        foreach (var t in Teachers) 
        {
            t.PrintInfo(); Console.WriteLine(); 
        }
    }

    public void PrintAllCourses()
    {
        if (Courses.Count == 0) 
        {
            Console.WriteLine("Курсов нет.");
            return;
        }
        foreach (var c in Courses) 
        {
            c.PrintInfo(); Console.WriteLine();
        }
    }

    public void PrintCoursesOfStudent(int code)
    {
        var s = FindStudent(code);
        if (s == null) { Console.WriteLine("Студент не найден."); return; }
        if (s.Courses.Count == 0) 
        { 
            Console.WriteLine("Студент не записан ни на один курс.");
            return;
        }
        foreach (var c in s.Courses)
            Console.WriteLine("Курс: " + c.title + " (Id: " + c.Id + ")");
    }

    public void PrintStudentsOfCourse(int id)
    {
        var c = FindCourse(id);
        if (c == null) 
        {
            Console.WriteLine("Курс не найден.");
            return;
        }
        if (c.Students.Count == 0) 
        {
            Console.WriteLine("На курс никто не записан.");
            return;
        }
        foreach (var s in c.Students)
            Console.WriteLine("Студент: " + s.name + " " + s.lastname + " (код " + s.Code + ")");
    }
}

class Program
{
    static University kip = new University();

    static int ReadInt(string prompt)
    {
        Console.Write(prompt);
        return int.Parse(Console.ReadLine());
    }

    static void AddStudent()
    {
        int code = ReadInt("Код студента: ");
        Console.Write("Имя: ");
        string name = Console.ReadLine();
        Console.Write("Фамилия: ");
        string lastname = Console.ReadLine();
        int year = ReadInt("Год зачисления: ");
        kip.Students.Add(new Student(code, name, lastname, year));
        Console.WriteLine("Студент добавлен.");
    }

    static void AddTeacher()
    {
        int id = ReadInt("Id преподавателя: ");
        Console.Write("Имя: ");
        string name = Console.ReadLine();
        Console.Write("Фамилия: ");
        string lastname = Console.ReadLine();
        Console.Write("Кафедра: ");
        string dep = Console.ReadLine();
        kip.Teachers.Add(new Teacher(id, name, lastname, dep));
        Console.WriteLine("Преподаватель добавлен.");
    }

    static void AddCourse()
    {
        Console.Write("Название курса: ");
        kip.Courses.Add(new Course(Console.ReadLine()));
        Console.WriteLine("Курс создан.");
    }

    static void PrintStudentInfo()
    {
        var s = kip.FindStudent(ReadInt("Код студента: "));
        if (s == null) 
        {
            Console.WriteLine("Студент не найден.");
            return;
        }
        s.PrintInfo();
    }

    static void PrintTeacherInfo()
    {
        var t = kip.FindTeacher(ReadInt("Id преподавателя: "));
        if (t == null) 
        {
            Console.WriteLine("Преподаватель не найден.");
            return;
        }
        t.PrintInfo();
    }

    static void PrintCourseInfo()
    {
        var c = kip.FindCourse(ReadInt("Id курса: "));
        if (c == null) 
        {
            Console.WriteLine("Курс не найден.");
            return;
        }
        c.PrintInfo();
    }

    static void EnrollStudent()
    {
        var s = kip.FindStudent(ReadInt("Код студента: "));
        if (s == null) 
        {
            Console.WriteLine("Студент не найден.");
            return;
        }
        var c = kip.FindCourse(ReadInt("Id курса: "));
        if (c == null) 
        { 
            Console.WriteLine("Курс не найден.");
            return;
        }
        s.Enroll(c);
        Console.WriteLine("Студент записан на курс.");
    }

    static void AssignTeacher()
    {
        var t = kip.FindTeacher(ReadInt("Id преподавателя: "));
        if (t == null) 
        {
            Console.WriteLine("Преподаватель не найден.");
            return;
        }
        var c = kip.FindCourse(ReadInt("Id курса: "));
        if (c == null) 
        {
            Console.WriteLine("Курс не найден.");
            return;
        }
        c.AssignTeacher(t);
        Console.WriteLine("Преподаватель назначен на курс.");
    }

    static void Main()
    {
        while (true)
        { 
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Просмотреть информацию о студенте");
            Console.WriteLine("3. Список всех студентов");
            Console.WriteLine("4. Добавить преподавателя");
            Console.WriteLine("5. Просмотреть информацию о преподавателе");
            Console.WriteLine("6. Список всех преподавателей");
            Console.WriteLine("7. Создать курс");
            Console.WriteLine("8. Просмотреть информацию о курсе");
            Console.WriteLine("9. Список всех курсов");
            Console.WriteLine("10. Записать студента на курс");
            Console.WriteLine("11. Курсы студента");
            Console.WriteLine("12. Студенты на курсе");
            Console.WriteLine("13. Назначить преподавателя на курс");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите пункт: ");

            switch (Console.ReadLine())
            {
                case "1": AddStudent(); 
                    break;
                case "2": PrintStudentInfo();
                    break;
                case "3": kip.PrintAllStudents(); 
                    break;
                case "4": AddTeacher(); 
                    break;
                case "5": PrintTeacherInfo(); 
                    break;
                case "6": kip.PrintAllTeachers(); 
                    break;
                case "7": AddCourse(); 
                    break;
                case "8": PrintCourseInfo();
                    break;
                case "9": kip.PrintAllCourses();
                    break;
                case "10": EnrollStudent();
                    break;
                case "11": kip.PrintCoursesOfStudent(ReadInt("Код студента: "));
                    break;
                case "12": kip.PrintStudentsOfCourse(ReadInt("Id курса: "));
                    break;
                case "13": AssignTeacher();
                    break;
                case "0": return;

                default: Console.WriteLine("Неверный пункт меню."); break;
            }
            Console.WriteLine();
        }
    }
}