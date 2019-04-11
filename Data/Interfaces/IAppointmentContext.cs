using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fontys.PTS2.Prototype.Classes;
using Fontys.PTS2.Prototype.Model;

namespace Fontys.PTS2.Prototype.Data.Contexts
{
    public interface IAppointmentContext
    {
        void CreateAppointment(Appointment appointment);
    }
}
