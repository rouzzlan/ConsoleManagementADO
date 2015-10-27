using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
namespace CM_ADO
{
  class Program
  {
    private static SqlConnection GetConnection()
    {
      string connString = ConfigurationManager.ConnectionStrings["myCM"].ConnectionString;
      return new SqlConnection(connString);
    }
    static void Main(string[] args)
    {
      Stap9();
    }
    private static void Stap6a()
    {
      Console.WriteLine("Gegevens ophalen...");
      string query = "SELECT Number, Name, Teacher, CourseType, StartDate, "
      + "LaptopRequired, MaximumParticipants, Price FROM Courses";
      List<Models.Course> courses = new List<Models.Course>();
      using (SqlConnection conn = GetConnection())
      {
        //Toewijzen van zowel query alsook de connectie aan het commando object
        SqlCommand cmd = new SqlCommand(query, conn);
        conn.Open();
        //Commando uitvoeren op de DB en de resultatentabel uitlezen via onze
        //DataReader cursor
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
          Models.Course loadingCourse = new Models.Course();
          loadingCourse.CourseId = reader.GetInt32(0);
          loadingCourse.Name = reader.GetString(1);
          loadingCourse.Teacher = reader.IsDBNull(2) ? null : reader.GetString(2);
          loadingCourse.CourseType = reader.GetString(3);
          if (!reader.IsDBNull(4))
            loadingCourse.StartDate = reader.GetDateTime(4);
          loadingCourse.LaptopRequired = reader.GetBoolean(5);
          loadingCourse.MaximumParticipants = reader.GetInt32(6);
          if (!reader.IsDBNull(7))
            loadingCourse.Price = reader.GetDouble(7);
          courses.Add(loadingCourse);
        }
        //Cursor ook afsluiten
        reader.Close();
        conn.Close();
      }
      foreach (var item in courses)
        Console.WriteLine(item);
      Console.WriteLine("Gegevens ophalen gelukt");
      Console.ReadLine();
    }
    private static void Stap6b()
    {
      Console.WriteLine("Gegevens ophalen...");
      string query = "SELECT Number, Name, Teacher, CourseType, StartDate, "
      + "LaptopRequired, MaximumParticipants, Price FROM Courses "
      + "WHERE LaptopRequired = @p_required";
      List<Models.Course> courses = new List<Models.Course>();
      using (SqlConnection conn = GetConnection())
      {
        //Toewijzen van zowel query alsook de connectie aan het commando object
        //Ook een parameter gaan we toevoegen aan het commando. De naam is dezelfde
        //als deze in de query, de waarde geven we als tweede parameter mee
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@p_required", false);
        conn.Open();

        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
          Models.Course loadingCourse = new Models.Course();
          loadingCourse.CourseId = reader.GetInt32(0);
          loadingCourse.Name = reader.GetString(1);
          loadingCourse.Teacher = reader.IsDBNull(2) ? null : reader.GetString(2);
          loadingCourse.CourseType = reader.GetString(3);
          if (!reader.IsDBNull(4))
            loadingCourse.StartDate = reader.GetDateTime(4);

          loadingCourse.LaptopRequired = reader.GetBoolean(5);
          loadingCourse.MaximumParticipants = reader.GetInt32(6);
          if (!reader.IsDBNull(7))
            loadingCourse.Price = reader.GetDouble(7);
          courses.Add(loadingCourse);
        }
        //Cursor ook afsluiten
        reader.Close();
        conn.Close();
      }
      foreach (var item in courses)
        Console.WriteLine(item);
      Console.WriteLine("Gegevens ophalen gelukt");
      Console.ReadLine();
    }

    private static void Stap6c()
    {
      Console.WriteLine("Gegevens ophalen...");
      string query = "SELECT Number, Name, Teacher, CourseType, StartDate, "
      + "LaptopRequired, MaximumParticipants, Price FROM Courses "
      + "WHERE LaptopRequired = @p_required";
      List<Models.Course> courses = new List<Models.Course>();
      using (SqlConnection conn = GetConnection())
      {
        //Toewijzen van zowel query alsook de connectie aan het commando object
        //Ook een parameter gaan we toevoegen aan het commando. De naam is dezelfde
        //als deze in de query, de waarde geven we als tweede parameter mee
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@p_required", false);
        conn.Open();

        SqlDataReader reader = cmd.ExecuteReader();
        int ixNumber = reader.GetOrdinal("Number");
        int ixName = reader.GetOrdinal("Name");
        int ixTeacher = reader.GetOrdinal("Teacher");
        int ixCourseType = reader.GetOrdinal("CourseType");
        int ixStartDate = reader.GetOrdinal("StartDate");
        int ixLaptopReq = reader.GetOrdinal("LaptopRequired");
        int ixMax = reader.GetOrdinal("MaximumParticipants");
        int ixPrice = reader.GetOrdinal("Price");
        while (reader.Read())
        {
          Models.Course loadingCourse = new Models.Course();
          loadingCourse.CourseId = reader.GetInt32(ixNumber);

          loadingCourse.Name = reader.GetString(ixName);
          loadingCourse.Teacher
          = reader.IsDBNull(ixTeacher) ? null : reader.GetString(ixTeacher);
          loadingCourse.CourseType = reader.GetString(ixCourseType);
          if (!reader.IsDBNull(ixStartDate))
            loadingCourse.StartDate = reader.GetDateTime(ixStartDate);
          loadingCourse.LaptopRequired = reader.GetBoolean(ixLaptopReq);
          loadingCourse.MaximumParticipants = reader.GetInt32(ixMax);
          if (!reader.IsDBNull(ixPrice))
            loadingCourse.Price = reader.GetDouble(ixPrice);
          courses.Add(loadingCourse);
        }
        reader.Close();
        conn.Close();
      }
      foreach (var item in courses)
        Console.WriteLine(item);
      Console.WriteLine("Gegevens ophalen gelukt"); Console.ReadLine();
    }
    private static void Stap6d()
    {
      int? courseCount = null;
      using (SqlConnection conn = GetConnection())
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Courses", conn);
        object result = cmd.ExecuteScalar();
        if (result != DBNull.Value)
          courseCount = Convert.ToInt32(result);
        conn.Close();
      }
      Console.WriteLine(String.Format("Aantal cursussen: {0}"
      , courseCount == null ? "geen" : courseCount.ToString()));
      Console.ReadLine();
    }
    private static void Stap7()
    {
      string query = "UPDATE Courses SET Teacher = @p_Teacher "
      + "WHERE MaximumParticipants > @p_MaxP";
      using (SqlConnection conn = GetConnection())
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@p_Teacher", "Gates B.");
        cmd.Parameters.AddWithValue("@p_MaxP", 40);
        int affectedRows = cmd.ExecuteNonQuery();
        conn.Close();
        Console.WriteLine(String.Format("Gewijzigde records: {0}", affectedRows));
      }
      Console.ReadLine();
    }
    private static void Stap8()
    {
      Console.WriteLine("Transactie test");
      string query1 = "UPDATE Courses SET Price = Price * 1.1";
      string query2 = "UPDATE Courses SET MaximumParticipants = 10 "
      + "WHERE Price IS NULL";
      using (SqlConnection conn = GetConnection())
      {
        conn.Open();
        SqlCommand cmd1 = new SqlCommand(query1, conn);
        SqlCommand cmd2 = new SqlCommand(query2, conn);

        using (SqlTransaction trans = conn.BeginTransaction())
        {
          cmd1.Transaction = trans;
          cmd2.Transaction = trans;
          cmd1.ExecuteNonQuery();
          cmd2.ExecuteNonQuery();
          //Als we onderstaande commit niet doen,
          // wordt de transactie gerollbacked!
          //Ook bij een exception (als ons transactieobject out-of-scope komt)
          // wordt de transactie gerollbacked automatisch
          trans.Commit();
        }
        conn.Close();
        Console.WriteLine("Transacties gelukt");
      }
      Console.ReadLine();
    }

    private static void Stap9()
    {
      double totalPrice = 0;
      using (SqlConnection conn = GetConnection())
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand("sp_CalculateTotalPrice", conn);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@p_FromDate", new DateTime(2015, 11, 1));
        object result = cmd.ExecuteScalar();
        if (result != DBNull.Value)
          totalPrice = Convert.ToDouble(result);
        conn.Close();
      }
      Console.WriteLine(String.Format("Totale prijs: {0:0.00}", totalPrice));
      Console.ReadLine();
    }
  }
}