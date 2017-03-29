
(function () {
    'use strict'
    var mutualApp = angular.module('mutualApp', ['LocalStorageModule', 'ngCookies', 'ngTouch']);

    //mutualApp.config(['$httpProvider', function ($httpProvider) {
    //    $httpProvider.interceptors.push('authInterceptorService');
    //}]);

    mutualApp.run(['$http', 'localStorageService', function ($http, localStorageService) {
        var token = localStorageService.get('user_access_token');
        if (token) {
            $http.defaults.headers.common['Authorization'] = 'Basic ' + token;
            $http.defaults.headers.common['Accept'] = 'application/json;odata=verbose';
        }
    }]);
})();

    // Dev
    var serviceBase = "http://localhost:52216/";

    // Prod
    // var serviceBase = "http://mutual.lk/";