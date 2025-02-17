﻿BookMyTableApp.controller('BookMyTableCtrl', function ($scope, $http, $location, $timeout, $routeParams, toaster) {

    $scope.records = []; 
    $scope.HotelDetails = [];
    $scope.MenuList = [];
    $scope.HotelTablesDetails = [];
    $scope.EmailID = "";
    $scope.SearchOrder = [];

  
    $scope.SucessOrderID = $routeParams.successOrderID;
    $http({
        url: "http://108.168.203.227:9100/api/Hotel/GetRestaurantDetailsList",
        dataType: 'json',
        method: 'Get',
        headers: {
            "Content-Type": "application/json"
        }
    }).then(function (response) {
        $scope.records = response.data;
        $scope.records.HotelName = $scope.HotelName;
    });

    var OrderDetailsEntities = new Object();

    var totalPrice = 0;
    var Quantity = 0;
    var DisName = "";

    $scope.Quantity = 0;
    $scope.PlaceorderData = []
    $scope.TotalPrice = 0;
    var ToatalAmount = 0;
    $scope.HotelDetails = [];

    $scope.increaseQuantity = function (item) {

        // for display Quantity in ui only
        Quantity++;
        $scope.Quantity = Quantity;
        // for display totalPrice in ui only
        totalPrice = totalPrice + item.Price;
        $scope.TotalPrice = totalPrice;

        item.quantity++;
        item.ItemTotalAmount = item.Price * item.quantity;
    }

    $scope.decreaseQuantity = function (item) {

        if (item.quantity <= 0) {
            item.quantity = 0;
        } else {
            item.quantity--;
            // for display Quantity in ui only
            Quantity--;
            $scope.Quantity = Quantity;

            // for display totalPrice in ui only
            totalPrice = totalPrice - item.Price;
            $scope.TotalPrice = totalPrice;
            item.ItemTotalAmount = item.ItemTotalAmount - item.Price;
        }
    }

    $http({
        url: "http://108.168.203.227:9100/api/Hotel/GetRestaurantTablesDetailsList?hotelId=1",
        dataType: 'json',
        method: 'Get',
        headers: {
            "Content-Type": "application/json"
        }
    }).then(function (response) {
        $scope.HotelTablesDetails  = response.data;
    });

    $http({
        url: "http://108.168.203.227:9100/api/Hotel/GetRestaurantMenuCardDetailsList?hotelId=1",
        dataType: 'json',
        method: 'Get',
        headers: {
            "Content-Type": "application/json"
        }
    }).then(function (response) {

        $scope.HotelDetails = response.data;

        $scope.HotelDetails = $scope.HotelDetails.map(function (hotel) {
            hotel.quantity = 0;
            hotel.ItemTotalAmount = 0;
            hotel.IsPayed = 0;
            return hotel;
        });
    });

    $scope.BookOrderTable = function (obj) {
        $scope.DineID = obj;
        $location.path('/menuList/' + $scope.DineID)
    };

    $scope.BookMytableAndDish = function (PlaceOrderform) {
        var DineID = $routeParams.DineID;
        for (var i = 0; i < $scope.HotelDetails.length; i++) {
            if ($scope.HotelDetails[i].quantity > 0) {
                $scope.MenuList.push($scope.HotelDetails[i])
            }
        }
            OrderDetailsEntities.HotelId = 1;//$scope.HotelId;
            OrderDetailsEntities.TableID = DineID;
            OrderDetailsEntities.CustomerName = $scope.PlaceorderData.Name;
            OrderDetailsEntities.TotalAmount = $scope.TotalPrice;
            OrderDetailsEntities.EmailID = $scope.PlaceorderData.EmailID;
            OrderDetailsEntities.MenuList = $scope.MenuList;

                $http({
                    url: "http://108.168.203.227:9100/api/Hotel/AddTablesAndOrderBooking",
                    dataType: 'json',
                    method: 'POST',
                    data: OrderDetailsEntities,
                    headers: {
                        "Content-Type": "application/json"
                    }
                }).then(function (response) {
                    OrderDetailsEntities = [];

                    if (response.data[0].OrderID > 0)
                    {
                        toaster.pop('success', "Order No: " + response.data[0].OrderID, "Booked Successfully");
                        //$location.path('/Success/' + response.data[0].OrderID);
                        $timeout(function () {
                            $location.path('/');
                        }, 2000);
                    }
                    else
                        toaster.pop('alert', "Failed", "Booking Order");
                });
    };


    $scope.SearchMyOrder = function (SearchOrderform) {
        var searchorder = new Object();
        searchorder.EmailID = $scope.SearchOrder.OrderID;//$scope.HotelId;
        searchorder.OrderID = $scope.SearchOrder.EmailID;

        $http.get("http://108.168.203.227:9100/api/Hotel/GetOrderDetails",
            {
                params: {
                    "orderId": $scope.SearchOrder.OrderID,
                    "EmailID": $scope.SearchOrder.EmailID
                }
            })
            .then(function (response) {
                if (response.data.OrderID == 0 && response.data.OrderItemDetails == null) {
                    toaster.pop('note', "Order Not Available", "");
                }
                else
                    $location.path('/EditOrder/' + $scope.SearchOrder.OrderID + '/' + response.data.CustomerID);
            });
    };
});