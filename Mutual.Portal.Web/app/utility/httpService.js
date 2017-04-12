
(function () {
    'use strict';

    function httpService($http, localStorageService) {

        // Private methords

        function getHeader() {

            var token = localStorageService.get('user_access_token');

            if (token) {
              var config = {
                headers: {
                    'Authorization': 'Bearer ' + token
                }
            };

            return config;  
            }

            return {};
        };

        function get(url) {
            var h = getHeader();

            return $http.get(serviceBase + url, h)
                .then(function (results) {
                    return results;
                });
        };

        function post(url, obj) {
            var h = getHeader();

            return $http.post(serviceBase + url, obj, h)
                .then(function (results) {
                return results;
            });
        };

        function put(url, obj) {
            var h = getHeader();
            debugger;
            return $http.put(serviceBase + url, obj, h)
               .then(function (results) {
                   return results;
               });
        };

        var service = {
            get: get,
            post: post,
            put: put
        };

        return service;
    }

    httpService.$inject = ['$http', 'localStorageService'];

    angular.module('mutualApp').factory('httpService', httpService);
})();