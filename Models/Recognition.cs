using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team6New_MIS4200.Models
{
    public class Recognition
    {
        [Key]
        public int surveyID { get; set; }
        [Display(Name = "Core value recognized")]

        public CoreValue award { get; set; }

        [Display(Name = "ID of Person giving the recognition")]
        public Guid recognizor { get; set; }

        [Display(Name = "ID of Person receiving the recognition")]


        public Guid Nominee { get; set; }
        [Display(Name = "Date recognition given")]

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime recognizationDate { get; set; }


        public enum CoreValue
        {
            Excellence = 1,
            Integrity = 2,
            Stewardship = 3,
            Culture = 4,
            Passion = 5,
            Innovate = 6,
            Balanced = 7
        }

        [ForeignKey("Recognizor")]

        public virtual Employees Nominator {​​ get; set; }​​


        [ForeignKey("Nominee")]

        public virtual Employees recognized {​​ get; set; }​​]

    }
}