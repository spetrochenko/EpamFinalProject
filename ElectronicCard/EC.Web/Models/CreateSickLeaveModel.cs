﻿namespace EC.Web.Models
{
    public class CreateSickLeaveModel
    {
        public bool IsGive { get; set; }

        public int Number { get; set; }

        public int PeriodAction { get; set; }

        public int DiagnosisId { get; set; }
    }
}