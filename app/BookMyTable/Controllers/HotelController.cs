
using BookMyTableDataLayer.Repository;
using BookMyTableEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Cors;
using System.Web.Http;
using FoodOrderingWebService.Repository;
using BookMyTableEntities.ViewModels;

namespace BookMyTable.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Hotel")]
    public class HotelController : ApiController
    {

        // private readonly IFoodOrderService IFoodOrderService;
        private readonly IFoodOrderService _repository;
        private readonly IUserManangement _repositoryUser;

        public HotelController(IFoodOrderService IFoodOrderService, IUserManangement IUserManangement)
        {
            _repository = IFoodOrderService;
            _repositoryUser = IUserManangement;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>

        [Route("GetUserDetailsWithSuccesLogin")]
        [AcceptVerbs("GET", "POST")]
        public UserDetails GetUserDetailsWithSuccesLogin(string userName, string Password)
        {
            UserDetails userSucees = new UserDetails();
            try
            {
                userSucees = _repositoryUser.GetUserDetails(userName, Password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userSucees;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("GetRestaurantDetailsList")]
        [HttpGet]
        public List<RestaurantEntities> GetRestaurantDetailsList()
        {
            List<RestaurantEntities> restaurantEntities = new List<RestaurantEntities>();
            try
            {
                restaurantEntities = _repository.GetllRestaurantDetails().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return restaurantEntities;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        /// 

        [Route("GetRestaurantTablesDetailsList")]
        [HttpGet]
        public List<HotelTableEntities> GetRestaurantTablesDetailsList(int hotelId)
        {
            List<HotelTableEntities> hotelTableEntities = new List<HotelTableEntities>();
            try
            {
                hotelTableEntities = _repository.GetHotelstables(hotelId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return hotelTableEntities;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// 

        [Route("GetOrderDetailsForApproval")]
        [HttpGet]
        public List<OrderDetails> GetOrderDetailsForApproval(int employeeId)
        {
            List<OrderDetails> orderDetails = new List<OrderDetails>();
            try
            {
                orderDetails = _repository.GetOrderDetailsForApproval(employeeId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return orderDetails;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        /// 
        [Route("GetRestaurantMenuCardDetailsList")]
        [HttpGet]
        public List<MenuCardEntities> GetRestaurantMenuCardDetailsList(int hotelId)
        {
            List<MenuCardEntities> menuCardEntities = new List<MenuCardEntities>();
            try
            {
                menuCardEntities = _repository.GetHotelMenuDetails(hotelId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return menuCardEntities;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>

        [Route("GetOrderDetails")]
        [HttpGet]
        public OrderDetails GetOrderDetails(int orderId,string EmailID)
        {
            OrderDetails orderDetails = new OrderDetails();
            try
            {
                orderDetails = _repository.GetOrderDetails(orderId, EmailID);
            }
            catch (Exception ex)
            {
                throw;
            }
            return orderDetails;
        }

        [Route("GetEditOrderDetails")]
        [HttpGet]
        public OrderDetails GetEditOrderDetails(int orderId, int customerId)
        {
            OrderDetails orderItemDetail = new OrderDetails();
            try
            {
                orderItemDetail = _repository.GetEditOrderDetails(orderId, customerId);
            }
            catch (Exception ex)
            {
                throw;
            }
            return orderItemDetail;
        }

        [Route("UpdateExistingOrder")]
        [HttpPost]
        public string UpdateExistingOrder(OrderDetails orderDetail)
        {
            string response = string.Empty;
            try
            {
                response = _repository.UpdateExistingOrder(orderDetail);
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        [Route("CancelOrder")]
        [HttpPost]
        public string CancelOrder(CancelOrderVw data)//int orderId, string UserType)
        {
            string response = string.Empty;
            try
            {
                response = _repository.CancelOrder(data.OrderId, data.UserType);
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        [Route("AddTablesAndOrderBooking")]
        [AcceptVerbs("GET", "POST")]
        public List<OrderDetails> AddTablesAndOrderBooking(OrderDetailsEntities orderDetails)
        {
            List<OrderDetails> orderDetailsResponse = new List<OrderDetails>();
            try
            {
                orderDetailsResponse = _repository.AddTablesAndOrderBooking(orderDetails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return orderDetailsResponse;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("SendMailToCustomer")]
        [HttpGet]
        public int SendMailToCustomer()
        {
            int ISuceess = 1;
            try
            {
                // _repository.SendMailToCustomer();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ISuceess;
        }




    }
}
