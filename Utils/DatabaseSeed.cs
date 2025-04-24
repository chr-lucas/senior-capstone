using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NebulaTextbook.Data;
using NebulaTextbook.Models;

namespace NebulaTextbook.Models

{
    public class DatabaseSeed()
    {

        public void InitializeDB(IServiceProvider serviceProvider)
        {
            using (var _context = new NebulaTextbookContext(serviceProvider.GetRequiredService<DbContextOptions<NebulaTextbookContext>>()))
            {
                // Ensure database has neccessary tables
                if (_context == null ||
                    _context.Chapter == null ||
                    _context.Course == null ||
                    _context.Enrollment == null ||
                    _context.Organization == null ||
                    _context.Question == null ||
                    _context.Quiz == null ||
                    _context.Role == null ||
                    _context.StudentQuizScore == null ||
                    _context.Textbook == null ||
                    _context.User == null ||
                    _context.UserRole == null ||
                    _context.Video == null)
                {
                    throw new ArgumentNullException("Null NebulaTextbookContext"); // DB does not have required tables
                }

                // Look for any courses, users.
                // Seed utility populates CourseID and User test data,
                // so will only continue on an empty database
                if (_context.Course.Any() || _context.User.Any())
                {
                    return;   // DB already has data
                }
            }

            // Seed the database with test data
            SeedTables(serviceProvider);
        }

        public void SeedTables(IServiceProvider serviceProvider)
        {
            using (var _context = new NebulaTextbookContext(serviceProvider.GetRequiredService<DbContextOptions<NebulaTextbookContext>>()))
            {
                // Chapter Table
                // Seeds 1 Chapter, belonging to TextbookID 1.
                // Used in the Chapter Demo view from the landing page.
                using (var transaction = _context.Database.BeginTransaction())
                {

                    _context.Chapter.AddRange(
                        new Chapter
                        {
                            ChapterID = 1,
                            TextbookID = 1,
                            ChapterName = "Hashing, Part 2"
                        });
                    // Temporarily override DB controlled primary key
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Chapter ON;");
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Chapter OFF");
                    //
                    transaction.Commit();
                }

                // Course Table
                // Seeds 1 Course.
                // Currently not used in the application. Used as target associations for database test records.
                using (var transaction = _context.Database.BeginTransaction())
                {

                    _context.Course.AddRange(
                        new Course
                        {
                            CourseID = 1,
                            InstructorID = 1,
                            AccessCode = "abc123",
                            TextbookID = 1,
                            StartDate = new DateTime(2000, 1, 1),
                            EndDate = new DateTime(2100, 12, 31),
                            OrganizationID = 1
                        });
                    // Temporarily override DB controlled primary key
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Course ON;");
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Course OFF");
                    //
                    transaction.Commit();
                }

                // Enrollment Table
                // Seeds 1 Enrollment, assigning Student with UserID 2 to CourseID 1.
                // Currently not used in the application. Used as target associations for database test records.
                using (var transaction = _context.Database.BeginTransaction())
                {

                    _context.Enrollment.AddRange(
                        new Enrollment
                        {
                            EnrollmentID = 1,
                            StudentID = 2,
                            CourseID = 1
                        });
                    // Temporarily override DB controlled primary key
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Enrollment ON;");
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Enrollment OFF");
                    //
                    transaction.Commit();
                }

                // Question Table
                // Seeds 1 Questions, assigning to Quiz 1.
                // Used for the quiz in the Chapter Demo view from landing page.
                using (var transaction = _context.Database.BeginTransaction())
                {

                    _context.Question.AddRange(
                        new Question
                        {
                            QuestionID = 1,
                            QuizID = 1,
                            QuestionText = "Which of the following is NOT a disadvantage to the usage of array?",
                            Answer1 = "Fixed Size",
                            Answer2 = "There are chances of memory wastage of memory space",
                            Answer3 = "Insertion based on position",
                            Answer4 = "Accessing elements at specified positions",
                            CorrectAnswer = "Accessing elements at specified positions"
                        },
                        new Question
                        {
                            QuestionID = 2,
                            QuizID = 1,
                            QuestionText = "Whath is the time complexity of inserting at the end in dynamic arrays?",
                            Answer1 = "O(1)",
                            Answer2 = "Either O(1) or O(n)",
                            Answer3 = "O(logn)",
                            Answer4 = "O(n)",
                            CorrectAnswer = "Either O(1) or O(n)"
                        },
                        new Question
                        {
                            QuestionID = 3,
                            QuizID = 1,
                            QuestionText = "What is the time complexity to count the number of elements in the linked list?",
                            Answer1 = "O(1)",
                            Answer2 = "O(n)",
                            Answer3 = "O(logn)",
                            Answer4 = "O(n<sup>2</sup>)",
                            CorrectAnswer = "O(n)"
                        },
                        new Question
                        {
                            QuestionID = 4,
                            QuizID = 1,
                            QuestionText = "In a Singly Linked List, what does the last node’s next pointer point to?",
                            Answer1 = "The first node",
                            Answer2 = "The second to last node",
                            Answer3 = "Null",
                            Answer4 = "Itself",
                            CorrectAnswer = "Null"
                        },
                        new Question
                        {
                            QuestionID = 5,
                            QuizID = 2,
                            QuestionText = "Which is a disadvantage of Merge Sort?",
                            Answer1 = "Stability",
                            Answer2 = "Easy to Implement",
                            Answer3 = "Space Complexity",
                            Answer4 = "Parallelizable",
                            CorrectAnswer = "Space Complexity"
                        },
                        new Question
                        {
                            QuestionID = 6,
                            QuizID = 2,
                            QuestionText = "Which of the following has a Best-Case time complexity of O(n<sup>2</sup>)?",
                            Answer1 = "Selection Sort",
                            Answer2 = "Merge Sort",
                            Answer3 = "Insertion Sort",
                            Answer4 = "Quick Sort",
                            CorrectAnswer = "Selection Sort"
                        },
                        new Question
                        {
                            QuestionID = 7,
                            QuizID = 2,
                            QuestionText = "Which of the following does not have a worst-case time complexity of O(n<sup>2</sup>)?",
                            Answer1 = "Merge Sort",
                            Answer2 = "Quick Sort",
                            Answer3 = "Selection Sort",
                            Answer4 = "Bubble Sort",
                            CorrectAnswer = "Merge Sort"
                        },
                        new Question
                        {
                            QuestionID = 8,
                            QuizID = 2,
                            QuestionText = "Which of the sorting algorithms is the most efficient for large data sets?",
                            Answer1 = "Quick Sort",
                            Answer2 = "Insertion Sort",
                            Answer3 = "Selection Sort",
                            Answer4 = "Bubble Sort",
                            CorrectAnswer = "Quick Sort"
                        },
                        new Question
                        {
                            QuestionID = 9,
                            QuizID = 2,
                            QuestionText = "Which is an advantage of Quick Sort?",
                            Answer1 = "Not a good choice for small data sets",
                            Answer2 = "It has a worse case time complexity of O(n<sup>2</sup>)",
                            Answer3 = "Efficient for large data sets",
                            Answer4 = "It is not stable",
                            CorrectAnswer = "Efficient for large data sets"
                        },
                        new Question
                        {
                            QuestionID = 10,
                            QuizID = 3,
                            QuestionText = "What is the primary use of a hash table?",
                            Answer1 = "Search for a key",
                            Answer2 = "Calculate mathematical hashes",
                            Answer3 = "Store data in a tree format",
                            Answer4 = "All of the above",
                            CorrectAnswer = "Search for a key"
                        },
                        new Question
                        {
                            QuestionID = 11,
                            QuizID = 3,
                            QuestionText = "Which of the following best describes a collision in a hash table?",
                            Answer1 = "Two keys having the same value",
                            Answer2 = "Two keys hashing to the same index",
                            Answer3 = "Forgetting to store a key",
                            Answer4 = "All keys having unique values",
                            CorrectAnswer = "Two keys hashing to the same index"
                        },
                        new Question
                        {
                            QuestionID = 13,
                            QuizID = 3,
                            QuestionText = "Which method resolves collisions by inserting the collided item into the next available space?",
                            Answer1 = "Separate chaining",
                            Answer2 = "Linear probing",
                            Answer3 = "Quadratic probing",
                            Answer4 = "Double hashing",
                            CorrectAnswer = "Linear probing"
                        },
                        new Question
                        {
                            QuestionID = 14,
                            QuizID = 3,
                            QuestionText = "In the context of a hash table, what does the load factor represent?",
                            Answer1 = "The number of keys",
                            Answer2 = "The total size of the array",
                            Answer3 = "The ratio of the number of keys to the array size",
                            Answer4 = "The number of collisions",
                            CorrectAnswer = "The ratio of the number of keys to the array size"
                        },
                        new Question
                        {
                            QuestionID = 15,
                            QuizID = 3,
                            QuestionText = "Which of the following data structures is commonly used for implementing collision resolution in hash tables?",
                            Answer1 = "Arrays",
                            Answer2 = "Linked Lists",
                            Answer3 = "Trees",
                            Answer4 = "Graphs",
                            CorrectAnswer = "Linked Lists"
                        },
                        new Question
                        {
                            QuestionID = 16,
                            QuizID = 3,
                            QuestionText = "Which of the following best describes open addressing?",
                            Answer1 = "Only one key-value pair can exist per index",
                            Answer2 = "Multiple key-value pairs can exist per index using linked lists",
                            Answer3 = "The hash table uses trees for storage",
                            Answer4 = "Each key is associated with a unique address",
                            CorrectAnswer = "Only one key-value pairs can exist per index"
                        });
                    // Temporarily override DB controlled primary key
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Question ON;");
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Question OFF");
                    //
                    transaction.Commit();
                }

                // Quiz Table
                // Seeds 2 Quizzes
                // Quiz 1 is used in the Chapter Demo view from landing page.
                // Quiz 2 is used in the Quiz view from the landing page.
                using (var transaction = _context.Database.BeginTransaction())
                {

                    _context.Quiz.AddRange(
                        new Quiz
                        {
                            QuizID = 1,
                            Score = 100,
                            Title = "Linked List Quiz",
                            Description = "Please Answer the following Questions about Linked Lists."
                        },
                        new Quiz
                        {
                            QuizID = 2,
                            Score = 100,
                            Title = "Sorting Quiz",
                            Description = "Please Answer these qusestions about sorting algorithms."
                        },
                        new Quiz
                        {
                            QuizID = 3,
                            Score = 120,
                            Title = "Hash Collision Quiz",
                            Description = "Covers open addressing and collision resolution."
                        });
                    // Temporarily override DB controlled primary key
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Quiz ON;");
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Quiz OFF");
                    //
                    transaction.Commit();
                }

                // StudentQuizScore Table
                // Seeds 0 QuizScores.
                // Commented out to prevent score prepopulation on test student for test chapter.
                // Currently not used in the application. Used as target associations for database test records.
                /*
                using (var transaction = _context.Database.BeginTransaction())
                {
                    _context.StudentQuizScore.AddRange(
                        new StudentQuizScore
                        {
                            ScoreID = 1,
                            StudentID = 2,
                            QuizID = 1,
                            QuizScore = 90
                        });
                    // Temporarily override DB controlled primary key
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.StudentQuizScore ON;");
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.StudentQuizScore OFF");
                    //
                    transaction.Commit();
                }
                */

                // Role Table
                // Seeds 2 Roles.
                // Currently not used in the application. Used as target associations for database test records.
                // All required Roles could be added here unless design requires dynamic role creation.
                using (var transaction = _context.Database.BeginTransaction())
                {

                    _context.Role.AddRange(
                        new Role
                        {
                            RoleID = 1,
                            RoleName = "Instructor"
                        },
                        new Role
                        {
                            RoleID = 2,
                            RoleName = "Student"
                        });
                    // Temporarily override DB controlled primary key
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Role ON;");
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Role OFF");
                    //
                    transaction.Commit();
                }

                // Textbook Table
                // Seeds 1 Textbook.
                // Currently not used in the application.
                using (var transaction = _context.Database.BeginTransaction())
                {

                    _context.Textbook.AddRange(
                        new Textbook
                        {
                            TextbookID = 1,
                            Title = "Test Textbook",
                            Subject = "Data Structures and Algorithms Test",
                            Author = "Spring 2025 Nebula Dev Team",
                            PublishedDate = new DateTime(2025, 1, 6)
                        });
                    // Temporarily override DB controlled primary key
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Textbook ON;");
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Textbook OFF");
                    //
                    transaction.Commit();
                }

                // User Table
                // Seeds 2 Users. 1 Instructor and 1 Student.
                // Currently not used in the application. Used as target associations for database test records.
                // PASSWORD CURRENTLY STORED IN PLAINTEXT
                using (var transaction = _context.Database.BeginTransaction())
                {

                    _context.User.AddRange(
                        new User
                        {
                            UserId = 1,
                            Username = "InstructorTestUser",
                            Password = "Pass1234",
                            Email = "instructor@test.edu",
                            FirstName = "Test",
                            LastName = "Instructor"
                        },
                        new User
                        {
                            UserId = 2,
                            Username = "StudentTestUser",
                            Password = "Pass1234",
                            Email = "student@test.edu",
                            FirstName = "Test",
                            LastName = "Student"
                        });
                    // Temporarily override DB controlled primary key
                    // Requires additional escape of MSSQL User keyword.
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT [dbo].[User] ON;");
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT [dbo].[User] OFF");
                    //
                    transaction.Commit();
                }

                // UserRole Table
                // Seeds 2 UserRoles. Assigns User 1 as an Instructor and User 2 as a Student.
                // Currently not used in the application. Used as target associations for database test records.
                using (var transaction = _context.Database.BeginTransaction())
                {

                    _context.UserRole.AddRange(
                        new UserRole
                        {
                            UserRoleID = 1,
                            UserID = 1,
                            RoleID = 1
                        },
                        new UserRole
                        {
                            UserRoleID = 2,
                            UserID = 2,
                            RoleID = 2
                        });
                    // Temporarily override DB controlled primary key
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.UserRole ON;");
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.UserRole OFF");
                    //
                    transaction.Commit();
                }

                // Video Table
                // Seeds 1 Video.
                // Used for example video in Embed view from landing page.
                // ~/Assets has been configured as the static file directory for the project.
                using (var transaction = _context.Database.BeginTransaction())
                {

                    _context.Video.AddRange(
                        new Video
                        {
                           VideoID = 1,
                           Duration = 212,
                           Title = "Test Video",
                           FilePath = "~/Assets/Bunny.mp4",
                           UploadDate = new DateTime(2025, 1, 6),
                           UploadedBy = 1,
                           ThumbnailPath = "~/Assets/Bunny.mp4"
                        });
                    // Temporarily override DB controlled primary key
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Video ON;");
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Video OFF");
                    //
                    transaction.Commit();
                }
            }
        }
    }
}
