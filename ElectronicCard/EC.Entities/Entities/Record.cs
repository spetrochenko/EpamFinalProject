﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EC.Entities.Entities
{
    public class Record
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateRecord { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }

        public int DiagnosisId { get; set; }

        public Diagnosis Diagnosis { get; set; }

        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public int SickLeaveId { get; set; }

        public SickLeave SickLeave { get; set; }

        public virtual IReadOnlyCollection<Procedure> Procedures { get; set; }

        public virtual IReadOnlyCollection<Preparation> Preparations { get; set; }
    }
}
