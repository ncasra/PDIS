var pdisApp = angular.module("pdisApp", []);
pdisApp.factory('orderService', function() {
    var orderService;
    orderService.getRouteForDelivery = function(from, to, weight, type, longestDimension) {

    };
    return orderService;
});
pdisApp.controller('MainController', ['$scope', 'orderService', function ($scope, orderService) {
    $scope.rabat = 0;
    $scope.getRouteForDelivery = function(from, to, weight, type, longestDimension) {
        orderService.getRouteForDelivery(from, to, weight, type, longestDimension);
    };
}]);
