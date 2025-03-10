using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Application.AppService
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        // Get all events
        public async Task<IEnumerable<SchoolEventDto>> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllEventsAsync();
            return _mapper.Map<IEnumerable<SchoolEventDto>>(events);
        }

        // Get events by school name
        public async Task<IEnumerable<SchoolEventDto>> GetEventsBySchool(string schoolName)
        {
            var events = await _eventRepository.GetEventsBySchool(schoolName);
            return _mapper.Map<IEnumerable<SchoolEventDto>>(events);
        }

        // Add a new event
        public async Task<EventServiceResponse<SchoolEventDto>> AddSchoolEventsAsync(SchoolEventDto eventDetails)
        {
            var response = new EventServiceResponse<SchoolEventDto>();

            try
            {
                // Check if the school exists
                bool schoolExists = await _eventRepository.SchoolExistsAsync(eventDetails.SchoolId);
                if (!schoolExists)
                {
                    response.Success = false;
                    response.Message = "School does not exist.";
                    return response;
                }

                // Optional: Prevent duplicate event titles for the same school
                var existingEvent = await _eventRepository.GetEventByTitleAsync(eventDetails.Title, eventDetails.SchoolId);
                if (existingEvent != null)
                {
                    response.Success = false;
                    response.Message = "An event with this title already exists for the school.";
                    return response;
                }

                // Convert DTO to entity and save
                var eventEntity = _mapper.Map<SchoolEvent>(eventDetails);
                await _eventRepository.AddSchoolEventsAsync(eventEntity);

                response.Data = _mapper.Map<SchoolEventDto>(eventEntity);
                response.Message = "Event added successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error adding event: {ex.Message}";
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
                existingEvent.MediaUrl = schoolEventDto.MediaUrl;

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
    }

}
