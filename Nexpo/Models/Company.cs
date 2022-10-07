﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nexpo.Models
{
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public string Website { get; set; }
        public string HostName { get; set; }
        public string HostEmail { get; set; }
        public string HostPhone { get; set; }

        public IEnumerable<Degree> desiredDegrees { get; set;}

        public IEnumerable<Guild> desiredGuilds { get; set;}

        public IEnumerable<Position> positions { get; set;}

        public IEnumerable<Industry> Industries { get; set;}
        public IEnumerable<User> Representatives { get; set; }
        public IEnumerable<StudentSessionTimeslot> StudentSessionTimeslots { get; set; }
    }

    public enum Degree {
        Bachelor, Master, PhD
    }

    public enum Position {
        Thesis, TraineeEmployment, Internship, SummerJob, ForeignOppurtunity, PartTime 
    }

    public enum Industry {
        
        ElectricityEnergyPower, Environment, BankingFinance, Union, Investment, Insurance, Recruitment, Construction, Architecture, 
        GraphicDesign, DataIT, FinanceConsultancy, Telecommunication, Consulting, Management, Media, Industry, NuclearPower, LifeScience, 
        MedicalTechniques, PropertyInfrastructure, Research, Coaching
    }
}

