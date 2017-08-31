var pdisApp = angular.module("pdisApp", []);
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


    orderService.placeOrder = function (from, to, weight, type, longestDimension, date, customerNo, discount) {
        //Opret ordre
        return $http({
            method: 'POST',
            url: '/someUrl'
        });
    }
    return orderService;
}]);
pdisApp.controller('MainController', ['$scope', 'orderService', function ($scope, orderService) {
    $scope.rabat = 0;
    $scope.cargoTypes = [
        { displayName: "Normal", enumName: "NORMAL" },
        { displayName: "Våben", enumName: "WEAPONS" },
        { displayName: "Levende dyr", enumName: "LIVEANIMALS" },
        { displayName: "Frostvarer", enumName: "REFRIGERATEDGOODS" }
    ];
    $scope.initData = function (locations) {
        $scope.selectedCargoType = $scope.cargoTypes[0];
        $scope.locations = locations;
    };
    $scope.getRoutesForDelivery = function(from, to, weight, cargoType, longestDimension, date) {
        orderService.getRoutesForDelivery(from, to, weight, cargoType.enumName, longestDimension, date).then(function (response) {
            console.log(response);
            $scope.fastestRoute = response.data.routeDetails[0];
            $scope.cheapestRoute = response.data.routeDetails[1];
        });
    };
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
