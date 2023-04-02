using Model;
using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IShowTicket
    {
        List<TicketTable> GetAllTicket();

        TicketTable GetTicketById(int id);

       List<TicketTable> GetTicketByUserId(int userId);

        int userCreateTicket(TicketTable newTicketCreate);

        int CreateTicket(TicketTable product);

        int UpdateTicket(TicketTable product);

        int DeleteTicket(int id);

       int DeleteTicket(TicketTable product);

    }
}
