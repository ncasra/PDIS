var pdisApp = angular.module("pdisApp", []);
pdisApp.factory('orderService', ['$q', '$http', function($q,$http) {
    var orderService = {};
    orderService.getRoutesForDelivery = function(from, to, weight, type, longestDimension) {
        return $q(function (resolve, reject) {
            setTimeout(function() {
                resolve([{ price: 3000, route: ["Dakar", "Wadai", "Caballero", "Sierra Leone"], time: 24 }, { price: 500, route: ["Dakar", "Sierra Leone"], time: 72 }]);
            },1000);
        });
    };
    orderService.getCheapestRouteForDelivery = function (from, to, weight, type, longestDimension) {
        return $q(function (resolve, reject) {
            resolve();
        });
    };
    return orderService;
}]);
pdisApp.controller('MainController', ['$scope', 'orderService', '$http', function ($scope, orderService, $http) {
    $scope.rabat = 0;

    $scope.initData = function (cargoTypes) {
        console.log(cargoTypes);
        $scope.cargoTypes = cargoTypes;
    };
    $scope.getRoutesForDelivery = function(from, to, weight, type, longestDimension) {
        orderService.getRoutesForDelivery(from, to, weight, type, longestDimension).then(function(routeDetails) {
            $scope.fastestRoute = routeDetails[0];
            $scope.cheapestRoute = routeDetails[1];
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
