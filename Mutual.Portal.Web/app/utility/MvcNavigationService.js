
(function () {
    'use strict';

    function mvcNavigationService($http) {

        function navigate(url) {
            window.location = url;
        }

        var service = {
            navigate: navigate
        };

        return service;
    }

    mvcNavigationService.$inject = ['$http'];

    angular.module('mutualApp').factory('mvcNavigationService', mvcNavigationService);
})();