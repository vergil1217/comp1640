using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWSD.Domain
{
    public class Statistics
    {
        public int statisticId { get; set; }
        public string academicSession { get; set; }
        
        public string courseworkCode { get; set; }
        public double mean { get; set; }
        public double median { get; set; }
        public double standardDeviation { get; set; }
        public double gdGroup1 { get; set; }
        public double gdGroup2 { get; set; }
        public double gdGroup3 { get; set; }
        public double gdGroup4 { get; set; }
        public double gdGroup5 { get; set; }
        public double gdGroup6 { get; set; }
        public double gdGroup7 { get; set; }
        public double gdGroup8 { get; set; }
        public double gdGroup9 { get; set; }
        public double gdGroup10 { get; set; }
        public Statistics()
        {

        }

        public Statistics(int statisticId, string academicSession, string courseworkCode, double mean, double median, double standardDeviation, double gdGroup1, double gdGroup2, double gdGroup3, double gdGroup4, double gdGroup5, double gdGroup6, double gdGroup7, double gdGroup8, double gdGroup9, double gdGroup10)
        {
            this.statisticId = statisticId;
            this.academicSession = academicSession;
            this.courseworkCode = courseworkCode;
            this.mean = mean;
            this.median = median;
            this.standardDeviation = standardDeviation;
            this.gdGroup1 = gdGroup1;
            this.gdGroup2 = gdGroup2;
            this.gdGroup3 = gdGroup3;
            this.gdGroup4 = gdGroup4;
            this.gdGroup5 = gdGroup5;
            this.gdGroup6 = gdGroup6;
            this.gdGroup7 = gdGroup7;
            this.gdGroup8 = gdGroup8;
            this.gdGroup9 = gdGroup9;
            this.gdGroup10 = gdGroup10;
        }
    }
}