using AutoMapper;
using AppointmentServiceAPI.Entities;
using AppointmentServiceAPI.DTOs;

namespace AppointmentServiceAPI.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<CreateAppointmentDto, Appointment>();
            CreateMap<Appointment, ReadAppointmentDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<UpdateAppointmentDto, Appointment>();
        }
    }
}
