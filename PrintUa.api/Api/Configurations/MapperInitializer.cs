using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Models.OrderController;
using Api.Models.OrderController.DetailOrderInfoModels;
using Api.Models.OrderController.OrderInfoModels;
using AutoMapper;
using Business.Contract.Model;
using Business.Interface.Models;
using Business.Model;
using Domain.Entity;
using UserIdentity.Data;

namespace Api.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<SystemUser, RegisterUserModel>().ReverseMap();
            CreateMap<Product, CreateProductModel>().ReverseMap();
            CreateMap<Order, CreateOrderModel>().ReverseMap();
            CreateMap<CreateServiceViewModel, ProductService>().ReverseMap();
            CreateMap<UpdateServiceViewModel, ProductService>().ReverseMap();
            CreateMap<ProductService, ServiceViewModel>().ReverseMap();
            CreateMap<Product, ProductImageViewModel>();
            CreateMap<OrderComment, CommentViewModel>().ReverseMap();
            CreateMap<OrderComment, CreateCommentViewModel>().ReverseMap();

            CreateMap<Order, CustomerRoleOrderInfoModel>()
                .ForMember("State", opt => opt.MapFrom(order => order.State.ToString()))
                .ForMember("Price", opt => opt.MapFrom(order => CulculatePrice(order.MyProducts)))
                .AfterMap((src, dest, context) =>
                {
                    dest.ImageArray = context.Mapper.Map<ICollection<Product>, ICollection<ProductImageViewModel>>(src.MyProducts);
                    dest.Comments = context.Mapper.Map<ICollection<OrderComment>, ICollection<CommentViewModel>>(src.MyComments);
                });

            CreateMap<Order, EmployeeRoleOrderInfoModel>()
                .ForMember("State", opt => opt.MapFrom(order => order.State.ToString()));

            CreateMap<Order, AdminRoleOrderInfoModel>()
                .ForMember("State", opt => opt.MapFrom(order => order.State.ToString()))
                .ForMember("EmployeeName", opt => opt.MapFrom(order => $"{order.Employee.Name} {order.Employee.Surname}"))
                .ForMember("CustomerName", opt => opt.MapFrom(order => $"{order.Customer.Name} {order.Customer.Surname}"));

            CreateMap<User, UserInfoModel>();

            CreateMap<Customer, UserInfoViewModel>()
                .ForMember("Role", opt => opt.MapFrom(c => "Customer"));
            CreateMap<Admin, UserInfoViewModel>()
                .ForMember("Role", opt => opt.MapFrom(a => "Admin"));
            CreateMap<Employee, UserInfoViewModel>()
                .ForMember("Role", opt => opt.MapFrom(e => "Employee"));

            CreateMap<Product, ProductInfoModel>().AfterMap((src, dest, context) =>
            {
                dest.Service = context.Mapper.Map<ProductService, ServiceInfoModel>(src.ServiceInProduct);
            });

            CreateMap<ProductService, ServiceViewModel>().ReverseMap();
            CreateMap<ProductService, CreateServiceViewModel>().ReverseMap();

            CreateMap<ProductService, ServiceInfoModel>().AfterMap((src, dest, context) =>
            {
                dest.Material = context.Mapper.Map<ServiceMaterial, ServiceMaterialInfoModel>(src.Material);
                dest.Format = context.Mapper.Map<ServiceFormat, ServiceFormatInfoModel>(src.Format);
            });

            CreateMap<ServiceMaterial, ServiceMaterialInfoModel>()
                .ForMember("Name", opt => opt.MapFrom(m => m.MaterialName));

            CreateMap<ServiceFormat, ServiceFormatInfoModel>()
                .ForMember("Name", opt => opt.MapFrom(f => f.FormatName));

            CreateMap<Order, CustomerRoleDetailOrderInfoModel>().AfterMap((src, dest, context) =>
            {
                dest.State = src.State.ToString();
                dest.Employee = context.Mapper.Map<User, UserInfoModel>(src.Employee);
                dest.Products = context.Mapper.Map<ICollection<Product>, ICollection<ProductInfoModel>>(src.MyProducts);
            });

            CreateMap<Order, EmployeeRoleDetailOrderInfoModel>().AfterMap((src, dest, context) =>
            {
                dest.State = src.State.ToString();
                dest.Customer = context.Mapper.Map<User, UserInfoModel>(src.Customer);
                dest.Products = context.Mapper.Map<ICollection<Product>, ICollection<ProductInfoModel>>(src.MyProducts);
            });

            CreateMap<Order, AdminRoleDetailOrderInfoModel>().AfterMap((src, dest, context) =>
            {
                dest.State = src.State.ToString();
                dest.Customer = context.Mapper.Map<User, UserInfoModel>(src.Customer);
                dest.Employee = context.Mapper.Map<User, UserInfoModel>(src.Employee);
                dest.Products = context.Mapper.Map<ICollection<Product>, ICollection<ProductInfoModel>>(src.MyProducts);
            });

            CreateMap<Customer, ProfileInfoModel>();

            CreateMap<Employee, ProfileInfoModel>();

            CreateMap<Admin, ProfileInfoModel>();
        }

        private static decimal CulculatePrice(ICollection<Product> myProducts)
        {
            decimal sum = 0;
            
            foreach (var item in myProducts)
            {
                sum += item.Amount * item.ServiceInProduct.Cost;
            }

            return sum;
        }
    }
}
