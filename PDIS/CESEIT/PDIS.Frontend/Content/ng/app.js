var pdisApp = angular.module("pdisApp", ['ui.bootstrap']);
pdisApp.factory('orderService', ['$q', '$http', function($q,$http) {
    var orderService = {};
    var encodeUrlParameters = function (paramArray) {
        var uri = "";
        for (var i = 0; i < paramArray.length; i++) {
            uri += "/" + paramArray[i];
        }
        return uri;
    };
    orderService.getRoutesForDelivery = function (from, to, weight, cargoType, longestDimension, date) {
        //Få ruter
        return $http({
            method: 'GET',
            url: '/api/Route/GetRouteInfo'+encodeUrlParameters([from,to,cargoType,weight,longestDimension,date.toDateString()])
        });
    };


    orderService.placeOrder = function (routeId, type, weight, discount) {
        //Opret ordre
        return $http({
            method: 'POST',
            url: '/api/Route/BuyRoute'+encodeUrlParameters([routeId,type,weight,discount])
        });
    }
    return orderService;
}]);
pdisApp.controller('MainController', ['$scope','$window', 'orderService', function ($scope, $window, orderService) {
    $scope.popup1 = {
        opened: false
    }
    $scope.dateOptions = {
        minDate: new Date()
    }
    $scope.orderDetails = {
        customerNo: "",
        discount: 0
    };
    $scope.closeKvittering = function() {
        //RESET ALL THE THINGS
        $window.location.reload();
        /*orderDetails.customerNo = "";
        orderDetails.discount = 0;
        $scope.selectedCargoType = $scope.cargoTypes[0];
        $scope.selectedFromLocation = undefined;
        $scope.selectedToLocation = undefined;*/

    }
    $scope.cargoTypes = [
        { displayName: "Standard", enumName: "NORMAL" },
        { displayName: "Våben", enumName: "WEAPONS" },
        { displayName: "Levende dyr", enumName: "LIVEANIMALS" },
        { displayName: "Frostvarer", enumName: "REFRIGERATEDGOODS" }
    ];

    $scope.initData = function (locations) {
        $scope.selectedCargoType = $scope.cargoTypes[0];
        $scope.locations = locations;
    };
    $scope.getRoutesForDelivery = function (from, to, weight, cargoType, longestDimension, date) {
        $scope.queriedDate = date;
        $scope.loadingDeliveryOptions = true;
        orderService.getRoutesForDelivery(from, to, weight, cargoType.enumName, longestDimension, date).then(function (response) {
            $scope.loadingDeliveryOptions = false;
            var jsonResponse = angular.fromJson(response.data);
            $scope.cheapestRoute = jsonResponse[0];
            $scope.fastestRoute = jsonResponse[1];
        });
    };
    $scope.getMinDateString = function () {
        var date = new Date();
        var month = date.getMonth() + 1;
        var day = date.getDate();
        return date.getFullYear() + "-" + (month<10?"0"+month:month) + "-" + (day<10?"0"+day:day);
    }

    $scope.getDeliveryDate = function (queriedDate, totalTime) {
        var queriedDateTime = new Date(queriedDate).getTime();
        var date = new Date(queriedDateTime + (totalTime * 60 * 60 * 1000));
        var month = date.getMonth() + 1;
        var day = date.getDate();
        return (day<10?"0"+day:day) + "-" + (month<10?"0"+month:month)+ "-" +date.getFullYear();
    };

    $scope.selectRoute = function(selectedRoute) {
        $scope.selectedRoute = selectedRoute;
    }

    $scope.createOrder = function () {
        orderService.placeOrder($scope.selectedRoute.Item2.RouteId, $scope.selectedCargoType.enumName, $scope.cargoWeight, $scope.orderDetails.discount);
        $scope.showKvittering = true;
    }
    $scope.generateRouteList = function (route) {
        if (!route) {
            return;
        }
        var routeString = "";
        for (var i = 0; i < route.length; i++) {
            routeString += route[i];
            if (i < route.length - 1) {
                routeString += " - ";
            }
        }
        return routeString;
    };
}]);
