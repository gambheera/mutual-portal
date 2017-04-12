(function () {
    'use strict';

    function swalService($q) {

        function success(text, title) {
            swal({
                title: title,
                text: text,
                type: 'success',
                confirmButtonText: 'OK'
            });

        };

        function error(text, title) {
            swal({
                title: title,
                text: text,
                type: 'error',
                confirmButtonText: 'OK'
            });
        };

        function info(text, title) {
            swal({
                title: title,
                text: text,
                type: 'info',
                confirmButtonText: 'OK'
            });
        };

        function warning(text, title) {
            swal({
                title: title,
                text: text,
                type: 'warning',
                confirmButtonText: 'OK'
            });
        };

        function confirm(title, message) {
            var deferred = $q.defer();
            swal({
                title: title,
                text: message,
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes'
            }).then(function() {
                deferred.resolve(true);
            }, function() {
                deferred.resolve(false);
            });

            return deferred.promise;
        };


        var service = {
            success: success,
            error: error,
            info: info,
            warning: warning,
            confirm: confirm
        };

        return service;
    }

    swalService.$inject = ['$q'];

    angular.module('mutualApp').factory('swalService', swalService);
})();