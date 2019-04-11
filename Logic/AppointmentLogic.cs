using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contexts;
using Models;

namespace Logic
{
    public class AppointmentLogic
    {
        private AppointmentContextSQL appointmentRepo = new AppointmentContextSQL();

        public void CreateAppointment(Appointment appointment) =>
            appointmentRepo.CreateAppointment(appointment);
    }
}
