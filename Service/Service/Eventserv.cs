using DomainLayer.Models;
using Repository.IRepo;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class Eventserv : IEventserv<Event>
    {
        private readonly IEventrepo<Event> _eventRepository;

        public Eventserv(IEventrepo<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public IEnumerable<Event> AddEvents(IEnumerable<Event> newEvents)
        {
            return _eventRepository.AddEvents(newEvents);
        }
       
        public void DeleteEvent(Event eventToDelete)
        {
            _eventRepository.DeleteEvent(eventToDelete);
        }
        public void UpdateEvent(Event updatedEvent)
        {
            _eventRepository.UpdateEvent(updatedEvent);
        }
    }
}
