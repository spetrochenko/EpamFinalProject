﻿namespace EC.Entities.Entities
{
    public class SickLeave
    {
        public int Id { get; set; }

        public bool IsGive{ get; set; }

        public int Number{ get; set; }

        public int PeriodAction { get; set; }

        public int DiagnosisId { get; set; }

        public Diagnosis Diagnosis { get; set; }
    }
}
