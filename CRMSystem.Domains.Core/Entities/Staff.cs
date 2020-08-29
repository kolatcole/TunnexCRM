using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRMSystem.Domains
{
    public class Staff:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string HEL { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        public DateTime DateofBirth { get; set; }
        public string MiddleName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateEmployed { get; set; }

        public string NextofKin { get; set; }

        public string NextofKinAddress { get; set; }

        public string NextofKinPhone { get; set; }

        public string MaidenName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string StaffID { get; set; }

        public string MaritalStatus { get; set; }

        public string YearofMarriage { get; set; }

        public string Designation { get; set; }

        public string RelationshipToNextofKin { get; set; }

        public List<Qualification> Qualifications { get; set; }


    }
}
