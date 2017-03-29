

(function () {
    'use strict';

    angular.module('mutualApp').factory('AuthInterceptorService', AuthInterceptorService);

    AuthInterceptorService.$inject = ['$http'];

    function AuthInterceptorService($http) {

        var authInterceptorServiceFactory = {
            request : _request,
            responseError : _responseError
        };

        return authInterceptorServiceFactory;

        var _request = function (config) {

            config.headers = config.headers || {};

            var authData = localStorageService.get('user_access_token');
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData;
            }

            return config;
        }

        var _responseError = function (rejection) {
            //if (rejection.status === 401) {
            //    var authService = $injector.get('authService');
            //    var authData = localStorageService.get('authorizationData');

            //    if (authData) {
            //        if (authData.useRefreshTokens) {
            //            $location.path('/refresh');
            //            return $q.reject(rejection);
            //        }
            //    }
            //    authService.logOut();
            //    $location.path('/login');
            //}
            //return $q.reject(rejection);
        }  
    }

    
})();