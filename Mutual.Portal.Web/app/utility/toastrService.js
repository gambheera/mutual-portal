(function () {
    'use strict';

    function toastrService(toastr) {

        function success(message) {
            toastr.success(message, "Success", {
                closeButton: true,
                timeOut: 5000,
                progressBar: true,
                positionClass: "toast-top-full-width"
            });
        };

        function error(message) {
            toastr.error(message, "Failed", {
                closeButton: true,
                timeOut: 5000,
                progressBar: true,
                positionClass: "toast-top-full-width"
            });
        };

        function info(message) {
            toastr.info(message, "Info", {
                closeButton: true,
                timeOut: 5000,
                progressBar: true,
                positionClass: "toast-top-full-width"
            });
        };

        function warning(message) {
            toastr.warning(message, "Warning", {
                closeButton: true,
                timeOut: 5000,
                progressBar: true,
                positionClass: "toast-top-full-width"
            });
        };


        var service = {
            success: success,
            error: error,
            info: info,
            warning: warning
        };

        return service;
    }

    toastrService.$inject = ['toastr'];

    angular.module('mutualApp').factory('toastrService', toastrService);
})();