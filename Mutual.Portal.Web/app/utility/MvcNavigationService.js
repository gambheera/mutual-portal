
(function () {
    'use strict';

    angular.module('mutualApp').factory('MvcNavigationService', MvcNavigationService);

    MvcNavigationService.$inject = ['$http'];

    function MvcNavigationService($http) {
        var service = {
            navigate: _navigate
        };

        return service;

        function _navigate(url) {
            window.location = url;
            //$http({
            //    method: 'POST',
            //    url: url,
            //    data: { blah: JSON.stringify(data) },
            //})
            //.then(function (successResult) {
            //    window.location = url;
            //}, function (errorResult) {
            //    // Error
            //});
        };
    }
})();