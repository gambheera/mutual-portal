(function () {
    'use strict';

    angular.module('mutualApp').factory('Userservice', Userservice);

    Userservice.$inject = ['$http'];

    function Userservice($http) {
        var service = {
            getUserInfo: _getUserInfo
        };

        return service;

        function _getUserInfo() {
            return $http.get(serviceBase + 'api/user/get-user-info')
                .then(function (results) {
                    return results;
                });
        };
    }
})();