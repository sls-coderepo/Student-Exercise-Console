using studentExercises.Data;
using studentExercises.Model;
using System;

namespace studentExercises
{
    class Program
    {
        static void Main(string[] args)
        {
            //Exercise
            var exerciseRepo = new ExerciseRepository();
            var allExercises = exerciseRepo.GetAllExercises();
            Console.WriteLine("All Exercises--");
            foreach (var exercise in allExercises)
            {
                Console.WriteLine($"{exercise.Name} id-{exercise.Id}");
            }
            Console.WriteLine("------------------");

            //Filter Exercise
            Console.WriteLine("Find exercises by language?");
            var filterLanguage = Console.ReadLine();
            var filteredExercise = exerciseRepo.GetFilteredExercises(filterLanguage);
            Console.WriteLine("All Filtered Exercises--");
            foreach (var exercise in filteredExercise)
            {
                Console.WriteLine($"{exercise.Name} - {exercise.Language}");
            }
            Console.ReadLine();
            Console.WriteLine("------------------");
            //Add Exercise
            var newExercise = new Exercise();
            Console.WriteLine("What exercise do you want to add?");
            newExercise.Name = Console.ReadLine();
            Console.WriteLine("What language do you have to work?");
            newExercise.Language = Console.ReadLine();

            exerciseRepo.AddExercise(newExercise);
            Console.WriteLine("------------------");
            //Instructor with Cohort
            var instructorRepo = new InstructorRepository();
            var allInstructors = instructorRepo.GetAllIntructors();
            Console.WriteLine("All Intructors with cohort--");
            foreach (var instructor in allInstructors)
            {
                Console.WriteLine($"{instructor.FirstName} {instructor.LastName} is the instructor for {instructor.Cohort.Name}");
            }
            Console.WriteLine("------------------");

            //Add Instructor 
            var newInstructor = new Instructor();
            Console.WriteLine("What is your new instructor first name?");
            newInstructor.FirstName = Console.ReadLine();
            Console.WriteLine("What is your new instructor last name?");
            newInstructor.LastName = Console.ReadLine();
            Console.WriteLine("What is your new instructor slack handle?");
            newInstructor.SlackHandle = Console.ReadLine();
            Console.WriteLine("Where is your new instructor assigned?");

            newInstructor.Cohort.Id = Convert.ToInt32(Console.ReadLine());

            instructorRepo.AddInstructor(newInstructor);
            Console.WriteLine("------------------");
            //List Of Students
            var studentRepo = new StudentRepository();
            var allStudents = studentRepo.GetAllStudents();
            Console.WriteLine("List of Students------");
            foreach (var student in allStudents)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName} who's (id) is {student.Id} is in {student.Cohort.Name}");
            }
            Console.WriteLine("------------------");

            //Student by Id
            Console.WriteLine("Enter student id to choose student");
            var studentById = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter exercise id to choose student");
            var exerciseById = Convert.ToInt32(Console.ReadLine());

            studentRepo.AddExercise(studentById, exerciseById);
            Console.ReadLine();

            Console.WriteLine("------------------");
            // Get all exercise by StudentId
            Console.WriteLine("Choose Student By Id");
            var studentWithExercise = Convert.ToInt32(Console.ReadLine());
            var studentWithExercises = studentRepo.GetStudentById(studentWithExercise);
            
            

            foreach (var studentExercise in studentWithExercises.Exercise)
            {
                Console.WriteLine($"{studentWithExercises.FirstName} {studentWithExercises.LastName} in {studentWithExercises.Cohort.Name} is working on {studentExercise.Name}");
            }
            
            Console.ReadLine();


        }
    }
}
