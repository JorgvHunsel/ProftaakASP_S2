﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Data.Interfaces
{
    public interface IAppointmentContext
    {
        void CreateAppointment(Appointment appointment);
        List<Appointment> GetAllAppointmentsFromUser(int userId);
        void DeleteAppointment(int appointmentId);
        void DeleteAppointmentByChat(int chatId);
    }
}
