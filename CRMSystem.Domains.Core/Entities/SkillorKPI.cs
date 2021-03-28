using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRMSystem.Domains
{
    public class SkillorKPI:BaseEntity
    {
       // public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int StaffNumberWithSkillorKPI { get; set; }

        public string Type { get; set; }

    }

    


    public class StaffSkillorKPI
    {
        public int ID { get; set; }

        [ForeignKey("Staff")]
        public int StaffID { get; set; }
        [ForeignKey("SkillorKPI")]
        public int SkillorKPIID { get; set; }

        public List<Assessment> Assessments { get; set; }
        public int SupervisorID { get; set; }

      
        public decimal CompetencyValue { get; set; }

       

    }
    public class Assessment
    {
        public int ID { get; set; }

       // [ForeignKey("StaffSkillorKPI")]
        public int StaffSkillorKPIID { get; set; }
        public int SAS { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class StaffSkillorKPICompetency
    {
        public int StaffId { get; set; }

        public List<StaffSkillorKPI> AllSkillsOrKpis { get; set; }

        public decimal OverallCompetence { get; set; }
    }   
}
