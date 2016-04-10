using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWSD.Domain
{
    public class Report
    {
        public int reportId { get; set; }
        public ArrayList statistics { get; set; }
        public int studentCount { get; set; }
        public string comments { get; set; }
        public string actionTaken { get; set; }
        public DateTime reportDate { get; set; }
        public Staff approvedBy { get; set; }
        public string dltComment { get; set; }
        public DateTime dltCommentDate { get; set; }
        public Staff dlt { get; set; }
        public Report()
        {

        }

        public Report(int reportId, ArrayList statistics, int studentCount, string comments, string actionTaken, DateTime reportDate, Staff approvedBy, string dltComment, DateTime dltCommentDate, Staff dlt)
        {
            this.reportId = reportId;
            this.statistics = statistics;
            this.studentCount = studentCount;
            this.comments = comments;
            this.actionTaken = actionTaken;
            this.reportDate = reportDate;
            this.approvedBy = approvedBy;
            this.dltComment = dltComment;
            this.dltCommentDate = dltCommentDate;
            this.dlt = dlt;
        }
    }
}