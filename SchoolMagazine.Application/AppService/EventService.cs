using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Application.AppService
{
    public class EventService : IEventService
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper, ISchoolRepository schoolRepository)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _schoolRepository = schoolRepository;
        }

        


        public async Task<ServiceResponse<PagedResult<SchoolEventDto>>> GetAllEventsAsync(int pageNumber, int pageSize)
        {
            var pagedEvents = await _eventRepository.GetAllEventsAsync(pageNumber, pageSize);

            if (pagedEvents.Items.Count == 0)
                return new ServiceResponse<PagedResult<SchoolEventDto>>(null!, false, "No events found.");

            var eventDtos = _mapper.Map<List<SchoolEventDto>>(pagedEvents.Items);

            var result = new PagedResult<SchoolEventDto>
            {
                TotalCount = pagedEvents.TotalCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Items = eventDtos
            };

            return new ServiceResponse<PagedResult<SchoolEventDto>>(result, true, "Events retrieved successfully.");
        }


        // Get events by school name
        public async Task<IEnumerable<SchoolEventDto>> GetEventsBySchoolAsync(string schoolName)
        {
            var events = await _eventRepository.GetEventsBySchoolAsync(schoolName);
            return _mapper.Map<IEnumerable<SchoolEventDto>>(events);
        }

        public async Task<EventServiceResponse<IEnumerable<SchoolEventDto>>> GetEventsBySchool(Guid schoolId)
        {
            var response = new EventServiceResponse<IEnumerable<SchoolEventDto>>();
            try
            {
                var searchedEvents = await _eventRepository.GetEventsBySchoolId(schoolId); //  Get events by SchoolId

                if (searchedEvents == null || !searchedEvents.Any())
                {
                    response.Success = false;
                    response.Message = "No events found for this school.";
                    return response;
                }

                response.Data = _mapper.Map<IEnumerable<SchoolEventDto>>(searchedEvents);
                response.Success = true;
                response.Message = "Events retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error retrieving events: {ex.Message}";
            }

            return response;
        }

        // Add a new event
        public async Task<EventServiceResponse<CreateEventDto>> AddSchoolEventsAsync(CreateEventDto eventDto)
        {
            var response = new EventServiceResponse<CreateEventDto>();

            try
            {
                // 🔍 Step 1: Check if the School Exists
                var schoolExists = await _schoolRepository.GetSchoolByIdAsync(eventDto.SchoolId);
                if (schoolExists == null)
                {
                    response.Success = false;
                    response.Message = "School not found.";
                    return response;
                }

                // 🔍 Step 2: Check if Event Already Exists
                var existingEvent = await _eventRepository.GetEventByTitleAndDescription(eventDto.Title, eventDto.Description, eventDto.SchoolId);
                if (existingEvent != null)
                {
                    response.Success = false;
                    response.Message = "An event with the same title and description already exists for this school.";
                    return response;
                }

                // Step 3: Convert DTO to Entity
                var newEvent = _mapper.Map<SchoolEvent>(eventDto);
                newEvent.Id = Guid.NewGuid(); // Assign a new ID

                // Step 4: Save the Event
                await _eventRepository.AddSchoolEventsAsync(newEvent);

                response.Data = _mapper.Map<CreateEventDto>(newEvent);
                response.Message = "Event created successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error creating event: {ex.Message}";
            }

            return response;
        }



        // Update an existing event
        public async Task<EventServiceResponse<SchoolEventDto>> UpdateSchoolEventAsync(Guid id, SchoolEventDto schoolEventDto)
        {
            var response = new EventServiceResponse<SchoolEventDto>();

            try
            {
                var existingEvent = await _eventRepository.GetEventByIdAsync(id);
                if (existingEvent == null)
                {
                    response.Success = false;
                    response.Message = "Event not found.";
                    return response;
                }

                // Update event properties
                existingEvent.Title = schoolEventDto.Title;
                existingEvent.Description = schoolEventDto.Description;
                existingEvent.EventDate = schoolEventDto.EventDate;
                existingEvent.EventMediaItems = schoolEventDto.EventMediaItems;

                await _eventRepository.UpdateSchoolEventAsync(existingEvent);

                response.Data = _mapper.Map<SchoolEventDto>(existingEvent);
                response.Message = "Event updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error updating event: {ex.Message}";
            }

            return response;
        }

        // Delete an event
        public async Task DeleteSchoolEventAsync(Guid eventId)
        {
            await _eventRepository.DeleteSchoolEventAsync(eventId);
        }

        public async Task<ServiceResponse<PagedResult<SchoolEventDto>>> GetEventsAsync(
      string? title, string? description, Guid? schoolId, string? schoolName, int pageNumber, int pageSize)
        {
            var pagedEvents = await _eventRepository.GetEventsAsync(title, description, schoolId, schoolName, pageNumber, pageSize);

            if (pagedEvents.Items.Count == 0)
                return new ServiceResponse<PagedResult<SchoolEventDto>>(null!, false, "No events found matching criteria.");

            var eventDtos = _mapper.Map<List<SchoolEventDto>>(pagedEvents.Items);

            var result = new PagedResult<SchoolEventDto>
            {
                TotalCount = pagedEvents.TotalCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Items = eventDtos
            };

            return new ServiceResponse<PagedResult<SchoolEventDto>>(result, true, "Events retrieved successfully.");
        }




    }

}
