using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fontys.PTS2.Prototype.Data;
using Fontys.PTS2.Prototype.Model;

namespace Fontys.PTS2.Prototype.Logic
{
    public class AppointmentLogic
    {
        private AppointmentRepository appointmentRepo = new AppointmentRepository();

        public void CreateAppointment(Appointment appointment) =>
            appointmentRepo.CreateAppointment(appointment);
    }
}
