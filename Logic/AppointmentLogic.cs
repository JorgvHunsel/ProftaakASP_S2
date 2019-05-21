using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contexts;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class AppointmentLogic
    {
        private readonly IAppointmentContext _appointment;

        public AppointmentLogic(IAppointmentContext Appointment)
        {
            _appointment = Appointment;
        }

        public void CreateAppointment(Appointment appointment) =>
            _appointment.CreateAppointment(appointment);

        public List<Appointment> GetAllAppointmentsFromUser(int userId)
        {
            return _appointment.GetAllAppointmentsFromUser(userId);
        }
    }
}
