(function () {
    'use strict';

    function userService($http, $q, localStorageService, httpService) {

        function logout() {
            localStorageService.remove('user_access_token');
            localStorageService.remove('user_info');
            return true;
        };

        function authenticateCurrentPosition() {
            var accessToken = localStorageService.get('user_access_token');
            if (!accessToken) {
                window.location = '/';
                return;
            }

            var userInfo = localStorageService.get('user_info');
            if (userInfo) {
                return userInfo.name;
            }

            return "";
        };

        function getUserInfo() {
            return httpService.get('api/user/get-user-info')
                .then(function(results) {
                    return results;
                });
        };

        function getUserEmployeeType() {
            return httpService.get('api/user/get-user-info')
                .then(function(response) {
                    return response;
                });
        };

        function register(obj) {
            var url = 'api/user/confirm-registration';
            return httpService.put(url, obj)
                .then(function (response) {
                    return response;
                });
        };

        function getUserSimpleInfo() {
            return httpService.get('api/user/get-user-simple-info')
                .then(function (response) {
                    return response;
                });
        };

        var service = {
            logout: logout,
            authenticateCurrentPosition: authenticateCurrentPosition,
            getUserInfo: getUserInfo,
            getUserEmployeeType: getUserEmployeeType,
            register: register,
            getUserSimpleInfo: getUserSimpleInfo
        };

        return service;
    }

    userService.$inject = ['$http', '$q', 'localStorageService', 'httpService'];

    angular.module('mutualApp').factory('userService', userService);
})();