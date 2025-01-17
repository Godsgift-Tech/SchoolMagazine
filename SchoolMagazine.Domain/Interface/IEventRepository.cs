﻿using SchoolMagazine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Interface
{
    public interface IEventRepository
    {
Task<IEnumerable<SchoolEvent>> GetAllEventsAsync();
Task<IEnumerable<SchoolEvent>> GetEventsByIdAsync(Guid id);
Task<IEnumerable<SchoolEvent>> AddEventsAsync(SchoolEvent schoolEvent);
        
        
    }
}
