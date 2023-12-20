﻿namespace Hospital.Entities
{
    public class Admin 
    {

        public string Name { get; set; }
        public string Surname { get; set; }

        public List<Doctor> Doctors { get; set; }
        public List<Patient> Patients { get; set; }
        public List<Appointment> Appointments { get; set; }

       // public  string  Role { get; set; } = "admin";

    }

}
