using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class StudentProfile
    {
        private string StudentId, Name, Dept, Uni;
        private int Semester;
        private float CGPA;

        public void CreateProfile(string StudentId, string Name, string Dept, string Uni, int Sem, float cgpa, string path)
        {
            this.StudentId = StudentId;
            this.Name = Name;
            this.Dept = Dept;
            this.Uni = Uni;
            this.Semester = Sem;
            this.CGPA = cgpa;
            StreamWriter obj = File.AppendText(path);
            obj.AutoFlush = true;
            obj.WriteLine("Student ID: " + StudentId);
            obj.WriteLine("Student Name: " + Name);
            obj.WriteLine("Department: " + this.Dept);
            obj.WriteLine("University: " + Uni);
            obj.WriteLine("CGPA: " + CGPA);
            obj.WriteLine("Semester: " + Semester);
            obj.WriteLine("x");
            
            obj.Close();
        }

        public string SearchStudent(int choice, string Input, string path)
        {
            StreamReader reader = new StreamReader(path);
            string s_record = reader.ReadToEnd(),Student_info="",search_str="";
            reader.Close();
            string[] Student_Array = s_record.Split('x').ToArray();
            if (choice == 1)
                search_str = "Student ID: ";
            else if (choice == 2)
                search_str = "Student Name: ";
            else if (choice == 3)
                search_str = "Semester: ";
            if (s_record.Contains(search_str+Input))
            {
                for (int i = 0; i < Student_Array.Length; i++)
                {
                    if (Student_Array[i].Contains(Input))
                    {                        
                        Student_info+=Student_Array[i];
                    }
                }
                
                return Student_info;
            }
            
            return "No Record Found!";
        }


       public string DeleteRecord(string StudentID, string path)
        {
            StreamReader reader = new StreamReader(path);
            string s_record = reader.ReadToEnd(), first_str="",second_str="",final_file ="";
            reader.Close();
            if (s_record.Contains(StudentID))
              {
                    int first = s_record.IndexOf("Student ID: " + StudentID);
                    int last = s_record.IndexOf("Student ID: ", first + 1);
                    if (last <= 0)                //if the record being searched is at the last, then the next Student ID value would be -1
                        first_str = s_record.Substring(0, first);
                    else if (first == 0)         //if the string being searched is at the start
                        second_str = s_record.Substring(last);
                    else                         //if record being searched is in the middle somewhere
                    {
                        first_str = s_record.Substring(0, first);
                        second_str = s_record.Substring(last);
                    }
               }
            else
                {
                    return "No Record Found!";
                }
             
            final_file = string.Concat(first_str, second_str);
            reader.Close();
            return final_file;
        }

        public void Top3Students(int Semester, string path)
       {
            StreamReader reader = new StreamReader(path);
            string substr_cgpa = "", temp_str,s_record=reader.ReadToEnd();
            string[] record = s_record.Split('x').ToArray();
            float[] CGPAs=new float[record.Length];
            int index = 0, index2 = 0,sem_counter=0;
            float temp = 0;
            

            for (int i = 0; i < record.Length; i++ )   
            {
                if(record[i].Contains("Semester: "+Semester))
                {
                    index = record[i].IndexOf("CGPA: ");
                    index2 = record[i].IndexOf('\n',index+1);
                    substr_cgpa = record[i].Substring(index + 6, 4);
                    CGPAs[sem_counter]=float.Parse(substr_cgpa);
                    sem_counter++;
                }
            }

            for (int i = 0; i < CGPAs.Length; i++)
            {
                for (int j = 0; j < CGPAs.Length - i - 1; j++)
                {
                    if (CGPAs[j] < CGPAs[j + 1])
                    {
                        temp = CGPAs[j];
                        CGPAs[j] = CGPAs[j + 1];
                        CGPAs[j + 1] = temp;
                        temp_str = record[j];
                        record[j] = record[j + 1];
                        record[j + 1] = temp_str;
                    }
                }
            }
            
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("\t\t\t-----------------------------------------------------");
                Console.WriteLine("\t\t\t\t\t\t"+(i+1));
                Console.WriteLine("\t\t\t-----------------------------------------------------");
                Console.WriteLine(record[i]);
            }
            reader.Close();
       }

        public void MarkAttendance(int semester, string pathFrom, string pathTo)
        {
            StreamWriter writer = File.AppendText(pathTo);
            writer.AutoFlush = true;
            StreamReader reader = new StreamReader(pathFrom);
            string s_record = reader.ReadToEnd();
            reader.Close();

            string[] Student_Array = s_record.Split('x').ToArray();
            string Student_Info;
            char status;
            int ID_Index, Name_Index, ending_index, std_count = 0;

            for (int i = 0; i < Student_Array.Length; i++)
            {
                if (Student_Array[i].Contains("Semester: " + semester))
                {
                    ID_Index = Student_Array[i].IndexOf("Student ID: ");
                    Name_Index = Student_Array[i].IndexOf("Student Name: ");
                    ending_index = Student_Array[i].IndexOf('\n', Name_Index);
                    Student_Info = Student_Array[i].Substring(ID_Index, ending_index - 1);
                    std_count++;
                    Console.Write("\n" + std_count + ". " + Student_Info + "Status: ");
                    status = Convert.ToChar(Console.ReadLine());
                    Console.WriteLine();
                    writer.WriteLine("Semester: " + semester);
                    writer.Write(Student_Info);
                    writer.WriteLine("Attendance: " + status);
                    writer.WriteLine("Date: " + DateTime.Now);
                    writer.WriteLine("x");

                }
            }
            writer.Close();
        }

        public string ViewAttendance(string sem, string date,string path)
        {
            StreamReader reader = new StreamReader(path);
            string s_record = reader.ReadToEnd(),showAttendance="";
            reader.Close();
            string[] attendace = s_record.Split('x').ToArray();
            for (int j = 0; j < attendace.Length - 1; j++)
            {
                if (sem != "" && date != "")
                {
                    if (attendace[j].Contains("Semester: " + sem) && attendace.Contains("Date: " + date))
                    {
                        showAttendance += attendace[j];
                    }
                    //else
                    //    showAttendance += "No Record Found!";
                }
                else if (sem == "" )
                {
                    if ( attendace[j].Contains("Date: " + date))
                    {
                        showAttendance += attendace[j];
                    }
                    //else
                    //    showAttendance += "No Record Found!";
                }
                if ( date == "")
                {
                    if (attendace[j].Contains("Semester: " + sem) )
                    {
                        showAttendance += attendace[j];
                    }
                    //else
                    //    showAttendance += "No Record Found!";
                }
            }
            return showAttendance;
        }

        public string CheckUniqueID(string StudentId, string path)
        {
            StreamReader reader = new StreamReader(path);
            string s_record = reader.ReadToEnd();
            reader.Close();
            bool correct=false;

            while (!correct)
            {

                if (StudentId[2] == '-' && StudentId[9] == '-' && StudentId.Length.Equals(13))
                {
                    correct = true;
                }
                else
                {
                    correct = false;
                    Console.WriteLine("Invalid ID, try again: ");
                    StudentId = Console.ReadLine();
                }


                if (s_record.Contains(StudentId))
                {
                    while (s_record.Contains(StudentId))
                    {
                        Console.WriteLine("This Student ID has already been saved, try again: ");
                        StudentId = Console.ReadLine();
                    }
                }
            }
            return StudentId;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            
            string StudentId, Name, Dept, Uni,str,first1="",second2="";
            int Semester;
            char choice,search;
            float CGPA = 0f;
            StudentProfile obj = new StudentProfile();


            
            do
            {
                Console.Clear();
                Console.WriteLine("\n\t\t\t----------------------------------------------------------");
                Console.WriteLine("\t\t\t\t\t\t Student Profile");
                Console.WriteLine("\t\t\t----------------------------------------------------------");
                Console.WriteLine("\n\n\t\t\t\t\t 1. Create Profile");
                Console.WriteLine("\t\t\t\t\t 2. Search Student");
                Console.WriteLine("\t\t\t\t\t 3. Delete Student Record");
                Console.WriteLine("\t\t\t\t\t 4. View Top 3 Students Of Class");
                Console.WriteLine("\t\t\t\t\t 5. Mark Attendance");
                Console.WriteLine("\t\t\t\t\t 6. View Attendance");
                Console.Write("\n\t\t\t\t\t Enter number of your choice: ");
                choice = Convert.ToChar(Console.ReadLine());

                if (choice == '1')
                {
                    Console.Clear();
                    Console.WriteLine("\n\t\t\t--------------------------------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t Create Student Profile");
                    Console.WriteLine("\t\t\t----------------------------------------------------------");
                    Console.Write("Enter University: ");
                    Uni = Console.ReadLine();
                    Console.Write("Enter Department: ");
                    Dept = Console.ReadLine();
                    Console.Write("Enter ID: ");
                    StudentId = Console.ReadLine();
                    StudentId = obj.CheckUniqueID(StudentId, args[0]);
                    Console.Write("Enter Name: ");
                    Name = Console.ReadLine();
                    Console.Write("Enter Semester: ");
                    Semester = Convert.ToInt16(Console.ReadLine());
                    Console.Write("Enter CGPA: ");
                    CGPA = float.Parse(Console.ReadLine());
                    obj.CreateProfile(StudentId, Name, Dept, Uni, Semester, CGPA, args[0]);
                    Console.WriteLine("\n\n\n\t\t\t\tYour Profile Has Been Successfully Created!");

                }
                else if (choice == '2')
                {
                    Console.Clear();
                    Console.WriteLine("\n\t\t\t----------------------------------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t Search Student ");
                    Console.WriteLine("\t\t\t----------------------------------------------------------");
                    Console.WriteLine("\n\n\t\t\t\t\t 1. Search By ID");
                    Console.WriteLine("\t\t\t\t\t 2. Search By Name");
                    Console.WriteLine("\t\t\t\t\t 3. Search By Semester");
                    Console.Write("\n\t\t\t\t\t Enter number of your choice: ");
                    search = Convert.ToChar(Console.ReadLine());
                    if (search == '1')
                    {
                        Console.Write("\t\t\t\t\t Enter ID: ");
                        StudentId = Console.ReadLine();
                        Console.WriteLine(obj.SearchStudent(1, StudentId, args[0]));
                    }
                    else if (search == '2')
                    {
                        Console.Write("\t\t\t\t\t Enter Name: ");
                        Name = Console.ReadLine();
                        Console.WriteLine(obj.SearchStudent(2, Name, args[0]));
                    }
                    else if (search == '3')
                    {
                        Console.Write("\t\t\t\t\t Enter Semester: ");
                        Semester = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine(obj.SearchStudent(3, Semester.ToString(), args[0]));
                    }
                    else
                        Console.Write("\t\t\t\t\t Invalid Input");
                }
                else if (choice == '3')
                {
                    Console.Clear();
                    Console.WriteLine("\n\t\t\t----------------------------------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t Delete Student Profile");
                    Console.WriteLine("\t\t\t----------------------------------------------------------");
                    Console.Write("\t\t\t\t\t Enter ID: ");
                    StudentId = Console.ReadLine();
                    string result = obj.DeleteRecord(StudentId, args[0]);
                    StreamWriter writer = new StreamWriter(args[0]);
                    writer.AutoFlush = true;
                    if (result.Equals("No Record Found!"))
                        Console.WriteLine(result);
                    else
                    {
                        writer.WriteLine(result);
                        Console.WriteLine("\t\t\t\t\t The Record Has Been Successfully Deleted!");
                    }
                    writer.Close();
                }
                else if (choice == '4')
                {
                    Console.Clear();
                    Console.WriteLine("\n\t\t\t----------------------------------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t View Top 3 Students of Class");
                    Console.WriteLine("\t\t\t----------------------------------------------------------");
                    Console.Write("Enter Semester: ");
                    Semester = Convert.ToInt16(Console.ReadLine());
                    obj.Top3Students(Semester, args[0]);
                }
                else if (choice == '5')
                {
                    Console.Clear();
                    Console.WriteLine("\n\t\t\t----------------------------------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t Mark Attendance");
                    Console.WriteLine("\t\t\t----------------------------------------------------------");
                    Console.Write("Enter Semester: ");
                    Semester = Convert.ToInt16(Console.ReadLine());
                    obj.MarkAttendance(Semester, args[0], args[1]);
                }
                else if (choice == '6')
                {
                    Console.Clear();
                    Console.WriteLine("\n\t\t\t----------------------------------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t View Attendance");
                    Console.WriteLine("\t\t\t----------------------------------------------------------");
                    Console.Write("\t\t\t\t\t Enter Semester: ");
                    string Sem = Console.ReadLine();
                    Console.Write("\t\t\t\t\t EnterDate: ");
                    string date = Console.ReadLine();
                    Console.WriteLine(obj.ViewAttendance(Sem, date, args[1]));
                }
                else
                    Console.WriteLine("\t\t\t\t\t Invalid Input");




                Console.WriteLine("\t\t\t Press 0: Exit          Press Any Key To Continue ");
                choice = Convert.ToChar(Console.ReadLine());
            } while (choice != '0');

                Console.ReadKey();
        }
    }
}
