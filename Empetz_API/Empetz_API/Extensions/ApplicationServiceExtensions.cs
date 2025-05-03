using Domain.Models;
using Domain;
using Domain.Service.Category.Interfaces;
using Domain.Service.Category;
using Domain.Service.Login;
using Domain.Service.Login.Interfaces;
using Domain.Service.Register;
using Domain.Service.Register.Interfaces;
using Domain.Service.User;
using Microsoft.EntityFrameworkCore;
using Domain.Service.PetsBreed.Interfaces;
using Domain.Service.Beed;
using Domain.Service.MyPets.Interfaces;
using Domain.Service.MyPets;
using Domain.Service.User.Interfaces;
using DAL.Models;
using Domain.Service.WishLIst.Interfaces;
using Domain.Service.WishLIst;
using Domain.Service.Report.Interfaces;
using Domain.Service.Report;
using Domain.Service.ContactUs.Interfaces;
using Domain.Service.ContactUs;
using Domain.Service.PetsBreed;
using Domain.Service.Chat.Interfaces;
using Domain.Service.Chat;



namespace Empetz_API.Extensions
{
    public static class ApplicationServiceExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<EmpetzContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"))
            );
            services.AddScoped<ICategoryRequestService, CategoryRequestservice>();
            services.AddScoped<ICategoryRequestRepository, CategoryRepository>();
            services.AddScoped<IBreedRequestRepository, BreedRequestRepository>();
            services.AddScoped<IBreedRequestService, BreedRequestService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRequestRepository, CategoryRepository>();
            services.AddScoped<ILoginRequestService, LoginRequestService>();
            services.AddScoped<ILoginRequestRepository, LoginRequestRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPublicService, PublicService>();
            services.AddScoped<IPublicRepository, PublicRepository>();
            services.AddScoped<IPetservice, PetService>();
            services.AddScoped<IPetRepository, PetRepository>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IWishListRepository, WishListRepository>();
            services.AddScoped<IWishListService, WishListService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IContactUsRepository, ContactRepository>();
            services.AddScoped<IContactUsService, ContactUsService>();


            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IMessageGroupRepository, MessageGroupRepository>();

            return services;
        }
    }
}
