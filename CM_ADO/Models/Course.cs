using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CM_ADO.Models
{
  class Course
  {
    public int CourseId { get; set; }
    public string Name { get; set; }
    public string Teacher { get; set; }
    public string CourseType { get; set; }
    public DateTime? StartDate { get; set; }
    public bool LaptopRequired { get; set; }
    public int MaximumParticipants { get; set; }
    public double? Price { get; set; }

    public override string ToString()
    {
      return String.Format("*) Course {0} ({1}) is an {2} course, "
      + "will be given by {3}, "
      + "starts on {4}, costs {5:0.0} and "
      + "will have maximum {6} participants"
      , Name, CourseId, CourseType, Teacher
      , StartDate.HasValue ?
      StartDate.Value.ToString("dd/MM/yyyy")
      : "?"
      , Price ?? 0
      , MaximumParticipants);
    }

  }
}
