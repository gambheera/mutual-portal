
(function () {
    'use strict';

    function homeCtrl(toastrService, swalService, userService) {
        var vm = this;
        vm.title = 'Homepage';

        vm.getUserInfo = function () {
            userService.getUserInfo().then(function (successRespond) {
                console.log(successRespond);
            }, function (errorRespond) {
                console.log(errorRespond);
            });
        };

        vm.testToastr = function() {
            swalService.confirm('Hello world!', 'The topic').then(function(res) {
                alert(res);
            });
        };
    }

    homeCtrl.$inject = ['toastrService', 'swalService', 'userService'];

    angular.module('mutualApp').controller('homeCtrl', homeCtrl);

})();