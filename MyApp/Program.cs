using System.Text.Json;
using Domain;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Repositories;

StudentRepository studentrepo = new StudentRepository(){};
CourseRepository courserepo = new CourseRepository(){};
MentorRepository mentorrepo = new MentorRepository(){};
GroupRepository grouprepo = new GroupRepository(){};
StudentGroupRepository stdgrouprepo = new StudentGroupRepository(){};

while (true)
{
    System.Console.WriteLine($"""
    Please enter the number what do you want to control:


    ========================= Students =========================

            1.  Get List of students.
            2.  Get student by id.
            3.  Delete student.
            4.  Update student.


    ========================= Courses =========================

            5.  Get List of courses.
            6.  Get course by id.
            7.  Delete course.
            8.  Update course.


    ========================= Mentors =========================

            9.  Get List of mentors.
            10.  Get mentor by id.
            11.  Delete mentor.
            12.  Update mentor.




    ========================= Groups =========================

            13.  Get List of groups.
            14.  Get groups by id.
            15.  Delete groups.
            16.  Update groups.


    ========================= Student Groups =========================

            17.  Get List of student groups.
            18.  Get student group by id.
            19.  Delete student group.
            20.  Update student group.


    0.  Exit





    """);
    var a = Convert.ToInt32(Console.ReadLine());

    switch (a)
    {
        case 0:
            return;
        case >= 1 and <= 4:
            Console.WriteLine("=============== Students ===============");
            break;
        case >= 5 and <= 8:
            Console.WriteLine("=============== Courses ===============");
            break;
        case >= 9 and <= 12:
            Console.WriteLine("=============== Mentors ===============");
            break;
        case >= 13 and <= 16:
            Console.WriteLine("=============== Groups ===============");
            break;
        case >= 17 and <= 20:
            Console.WriteLine("=============== StudentGroups ===============");
            break;
        default:
            Console.WriteLine("There is no this command!");
            break;
    }


    switch (a)
    {
        case 1:
            PrintResult(await studentrepo.GetListOfStudents());
            break;
        case 2:
            PrintResult(await studentrepo.GetStudentById(ReadInt("Enter student id: ")));
            break;
        case 3:
            PrintResult(await studentrepo.DeleteStudent(ReadInt("Enter student id: ")));
            break;
        case 4:
            PrintResult(await studentrepo.UpdateStudent(new Student
            {
                StudentId = ReadInt("Enter student id: "),
                FullName = ReadText("Enter full name: "),
                Email = ReadText("Enter email: "),
                Phone = ReadText("Enter phone: "),
                EnrollmentDate = ReadDate("Enter enrollment date (yyyy-MM-dd): ")
            }));
            break;
        case 5:
            PrintResult(await courserepo.GetListOfCourses());
            break;
        case 6:
            PrintResult(await courserepo.GetCourseById(ReadInt("Enter course id: ")));
            break;
        case 7:
            PrintResult(await courserepo.DeleteCourse(ReadInt("Enter course id: ")));
            break;
        case 8:
            PrintResult(await courserepo.UpdateCourse(new Course
            {
                CourseId = ReadInt("Enter course id: "),
                Title = ReadText("Enter title: "),
                Description = ReadText("Enter description: "),
                DurationWeek = ReadInt("Enter duration in weeks: ")
            }));
            break;
        case 9:
            PrintResult(await mentorrepo.GetListMentors());
            break;
        case 10:
            PrintResult(await mentorrepo.GetMentorById(ReadInt("Enter mentor id: ")));
            break;
        case 11:
            PrintResult(await mentorrepo.DeleteMentor(ReadInt("Enter mentor id: ")));
            break;
        case 12:
            PrintResult(await mentorrepo.UpdateMentor(new Mentor
            {
                MentorId = ReadInt("Enter mentor id: "),
                FullName = ReadText("Enter full name: "),
                Email = ReadText("Enter email: "),
                Phone = ReadText("Enter phone: "),
                Specialization = ReadText("Enter specialization: ")
            }));
            break;
        case 13:
            PrintResult(await grouprepo.GetListOfGroups());
            break;
        case 14:
            PrintResult(await grouprepo.GetGroupById(ReadInt("Enter group id: ")));
            break;
        case 15:
            PrintResult(await grouprepo.DeleteGroup(ReadInt("Enter group id: ")));
            break;
        case 16:
            PrintResult(await grouprepo.UpdateGroup(new Groups
            {
                GroupId = ReadInt("Enter group id: "),
                GroupName = ReadText("Enter group name: "),
                CourseId = ReadInt("Enter course id: "),
                MentorId = ReadInt("Enter mentor id: "),
                StartDate = ReadDate("Enter start date (yyyy-MM-dd): "),
                EndDate = ReadDate("Enter end date (yyyy-MM-dd): ")
            }));
            break;
        case 17:
            PrintResult(await stdgrouprepo.GetListOfStudentGroups());
            break;
        case 18:
            PrintResult(await stdgrouprepo.GetStudentGroupById(ReadInt("Enter student group id: ")));
            break;
        case 19:
            PrintResult(await stdgrouprepo.DeleteStudentGroup(ReadInt("Enter student group id: ")));
            break;
        case 20:
            PrintResult(await stdgrouprepo.UpdateStudentGroup(new StudentGroup
            {
                StudentGroupId = ReadInt("Enter student group id: "),
                StudentId = ReadInt("Enter student id: "),
                GroupId = ReadInt("Enter group id: "),
                Status = ReadStatus()
            }));
            break;
        default:
            break;
    }
}

static void PrintResult<T>(T result)
{
    Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));
}

static string ReadText(string prompt)
{
    Console.Write(prompt);
    return Console.ReadLine() ?? string.Empty;
}

static int ReadInt(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out var value))
            return value;

        Console.WriteLine("Please enter a valid number.");
    }
}

static DateOnly ReadDate(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (DateOnly.TryParse(Console.ReadLine(), out var value))
            return value;

        Console.WriteLine("Please enter a valid date.");
    }
}

static StatusEnum ReadStatus()
{
    while (true)
    {
        Console.Write("Enter status (Active, Completed, Expelled): ");
        if (Enum.TryParse<StatusEnum>(Console.ReadLine(), true, out var status) &&
            Enum.IsDefined(status))
            return status;

        Console.WriteLine("Please enter a valid status.");
    }
}