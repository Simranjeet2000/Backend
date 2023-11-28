using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Repository.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public class Eventrepo<T> : IEventrepo<T> where T : Event
    {
        private readonly appDbContext _context;
        private DbSet<T> entities;

        public Eventrepo(appDbContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }
        public IEnumerable<T> AddEvents(IEnumerable<T> newEvents)
        {
            foreach (var newEvent in newEvents)
            {
                var existingEvent = entities.FirstOrDefault(e => e.GoogleCalendarEventId == newEvent.GoogleCalendarEventId);

                if (existingEvent == null)
                {
                    entities.Add(newEvent);
                }
            }

            _context.SaveChanges();
            return entities.ToList();
        }


        public void DeleteEvent(T eventToDelete)
        {
            var matchingEvent = entities.FirstOrDefault(e => e.GoogleCalendarEventId == eventToDelete.GoogleCalendarEventId);

            if (matchingEvent != null)
            {
                entities.Remove(matchingEvent);
                _context.SaveChanges();
            }
        }
        public void UpdateEvent(T updatedEvent)
        {
            var existingEvent = entities.FirstOrDefault(e => e.GoogleCalendarEventId == updatedEvent.GoogleCalendarEventId);

            if (existingEvent != null)
            {
                existingEvent.title = updatedEvent.title;
                existingEvent.start = updatedEvent.start;
                existingEvent.end = updatedEvent.end;

                // Update other properties as needed

                _context.SaveChanges();
            }
        }
    }
}
